using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//可动摇杆,适合移动摇杆
public class Joystick : MonoBehaviour
{
    //摇杆圆心（Stick）
    public Transform Stick;
    //Stick移动半径（UGUI像素）
    public float StickRadius = 200.0f;
    //摇杆移动半径（UGUI像素）
    public float MoveRadius = 300.0f;
    //摇杆还原时间
    public float replaceTime = 0.1f;

    //摇杆的事件
    public event Action OnTouchDown;
    public event Action<JoystickData> OnTouchMove;
    public event Action OnTouchUp;

    //按下的原点
    private Vector3 touchOrigin;
    //自己Transform
    private Transform self;
    //自己和Stick的默认位置
    private Vector3 selfDefaultPosition;
    private Vector3 ctrlDefaultLocalPos;
    //摇杆事件的参数
    public JoystickData data = new JoystickData();

    //Canvas 的转换因子
    //private float scaleFactor;

    //是否正在复位
    private bool isReplace = false;
    //复位参数
    private float replaceCount = 0f;
    private Vector3 selfReplaceSpd;
    private Vector3 ctrlReplaceSpd;

    //是否正在拖拽
    private bool isDragged = false;

    //是否点击在区域上
    //private bool isOnArea = false;


    private Vector3 dragPos;

    private bool isStarted = false;

    // Use this for initialization
    void Start () {
        //初始化
        self = transform;
        //初始化赋值
        selfDefaultPosition = self.position;
        ctrlDefaultLocalPos = Stick.localPosition;

        //获取转换系数
        //Canvas canvas = this.GetComponentInParent<Canvas>();
        //scaleFactor = canvas.scaleFactor;
        //绑定操作事件
        EventTriggerListener.Get(this.gameObject).onDrag = OnJoystickDragAction;
        EventTriggerListener.Get(this.gameObject).onUp = OnJoystickUpAction;
        EventTriggerListener.Get(this.gameObject).onDown = OnJoystickDownAction;

    }


    void OnDisable()
    {
        Reset();
    }

    // Update is called once per frame
    void Update () {
        if (isDragged && OnTouchMove != null)
        {
            //派发事件
            OnTouchMove(data);
        }
        if (isReplace)
        {
            replaceCount += Time.deltaTime;
            if (replaceCount < replaceTime)
            {
                self.position += selfReplaceSpd * Time.deltaTime;
                Stick.transform.localPosition += ctrlReplaceSpd * Time.deltaTime;
            }
            else
            {
                isReplace = false;
                self.position = selfDefaultPosition;
                Stick.transform.localPosition = ctrlDefaultLocalPos;
            }
        }
    }


    private void OnJoystickDragAction(GameObject go, Vector2 delta)
    {
        dragPos += new Vector3(delta.x, delta.y, 0);
        //Debug.Log(dragPos);
        TouchMove(dragPos);
        //Debug.Log("Drag");
    }


    private void OnJoystickUpAction(GameObject go)
    {
        TouchUp();
    }

    private void OnJoystickDownAction(GameObject go,Vector2 pos)
    { 
        TouchDown(new Vector3(pos.x,pos.y,0));
    }

    private void TouchDown(Vector3 inputPosition)
    {
        Vector3 touchPosition = inputPosition;
        Vector2 touchScreen = new Vector2(touchPosition.x / Screen.width, touchPosition.y / Screen.height);
        if(isStarted)
        {
            return;
        }
        isStarted = true;
        isReplace = false;
        dragPos = touchPosition;

        //移动摇杆
        Vector3 direction = touchPosition - selfDefaultPosition;
        float distance = Vector3.Distance(touchPosition, selfDefaultPosition);
        float radians = Mathf.Atan2(direction.y, direction.x);
        if (distance > MoveRadius)
        {
            distance = MoveRadius;
            float mx = Mathf.Cos(radians) * distance;
            float my = Mathf.Sin(radians) * distance;
            Vector3 uiPos = selfDefaultPosition;
            uiPos.x += mx;
            uiPos.y += my;
            touchOrigin = uiPos;
            TouchMove(touchPosition);
        }
        else
        {
            touchOrigin = touchPosition;
        }

        self.position = touchOrigin;

        if (OnTouchDown != null)
            OnTouchDown();
    }

    private void TouchMove(Vector3 inputPosition)
    {
        Vector3 touch = touchOrigin;
        Vector3 now = inputPosition;
        float distance = Vector3.Distance(now, touch);
        if (distance < 0.01f)
            return;

        isDragged = true;

        Vector3 direction = now - touch;
        float radians = Mathf.Atan2(direction.y, direction.x);

        //移动摇杆
        if (Stick != null)
        {
            if (distance > StickRadius)
                distance = StickRadius;

            float mx = Mathf.Cos(radians) * distance;
            float my = Mathf.Sin(radians) * distance;
            Vector3 uiPos = ctrlDefaultLocalPos;
            uiPos.x += mx;
            uiPos.y += my;
            Stick.localPosition = uiPos;
        }

        //得到新的摇杆参数
        data.power = distance / StickRadius;
        data.radians = radians;
        data.angle = radians * Mathf.Rad2Deg;
        data.angle360 = data.angle < 0 ? 360 + data.angle : data.angle;
    }
    private void TouchUp()
    {
        //isOnArea = false;
        isDragged = false;
        isStarted = false;
        //一种插值还原，一种瞬间还原
        if (replaceTime > 0f)
        {
            isReplace = true;
            replaceCount = 0f;
            selfReplaceSpd = (selfDefaultPosition - self.position) / replaceTime;
            ctrlReplaceSpd = (ctrlDefaultLocalPos - Stick.transform.localPosition) / replaceTime;
        }
        else
        {
            ReplaceImmediate();
        }


        if (OnTouchUp != null)
            OnTouchUp();
    }

    //立即还原
    public void ReplaceImmediate()
    {
        isReplace = false;
        self.position = selfDefaultPosition;
        Stick.localPosition = ctrlDefaultLocalPos;
    }

    public void Reset()
    {
        //isOnArea = false;
        isDragged = false;
        isReplace = false;
        isStarted = false;
        ReplaceImmediate();
    }
}