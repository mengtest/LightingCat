using UnityEngine;
using System.Collections;
using System;

//固定摇杆,适合做技能摇杆
public class JoystackControl : MonoBehaviour 
{

    //摇杆的事件
    public event Action<JoystickData> OnTouchDown;
    public event Action<JoystickData> OnTouchMove;
    //加上相对开始触摸点的距离
    public event Action<JoystickData,float> OnTouchUp;

    //移动半径（UGUI像素）
    public float StickRadius = 200.0f;
    public float ActiveMoveDistance = 1;       //激活移动的最低距离

    //按下的原点
    private Vector3 touchOrigin;
    //自己Transform
    private Transform self;
    private Vector3 selfDefaultPosition;

    private Vector3 dragPos;

    //摇杆事件的参数
    public JoystickData data = new JoystickData();

    //是否正在拖拽
    private bool isDragged = false;

    private bool isStarted = false;


    void Awake()
    {
        //初始化
        self = transform;
        //初始化赋值
        selfDefaultPosition = self.localPosition;

        //绑定操作事件
        EventTriggerListener.Get(this.gameObject).onDrag = OnJoystickDragAction;
        EventTriggerListener.Get(this.gameObject).onUp = OnJoystickUpAction;
        EventTriggerListener.Get(this.gameObject).onDown = OnJoystickDownAction;
    }

    //Update is Called once per frame
    void Update()
    {
        if (isDragged && OnTouchMove != null)
        {
            //派发事件
            OnTouchMove(data);
        }
    }

    void OnDisable()
    {
        Reset();
    }

    private void TouchDown(Vector3 inputPosition)
    {
        Vector3 touchPosition = inputPosition;
        touchOrigin = touchPosition;
        if (isStarted)
        {
            return;
        }
        isStarted = true;
        dragPos = touchPosition;

        if (OnTouchDown != null)
            OnTouchDown(data);
    }

    private void TouchMove(Vector3 inputPosition)
    {
        Vector3 touch = touchOrigin;
        Vector3 now = inputPosition;
        float distance = Vector3.Distance(now, touch);
        if (distance < ActiveMoveDistance)
            return;

        isDragged = true;

        Vector3 direction = now - touch;
        float radians = Mathf.Atan2(direction.y, direction.x);

        //移动摇杆

        if (distance > StickRadius)
            distance = StickRadius;

        float mx = Mathf.Cos(radians) * distance;
        float my = Mathf.Sin(radians) * distance;
        Vector3 uiPos = selfDefaultPosition;
        uiPos.x += mx;
        uiPos.y += my;
        self.localPosition = uiPos;

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

        ReplaceImmediate();



        if (OnTouchUp != null)
            OnTouchUp(data, Vector3.Distance(touchOrigin, dragPos));
    }

    private void OnJoystickDragAction(GameObject go, Vector2 delta)
    {
        dragPos += new Vector3(delta.x, delta.y, 0);
        TouchMove(dragPos);
    }


    private void OnJoystickUpAction(GameObject go)
    {
        TouchUp();
    }

    private void OnJoystickDownAction(GameObject go,Vector2 pos)
    {
        TouchDown(new Vector3(pos.x,pos.y,0f));
    }

    //立即还原
    public void ReplaceImmediate()
    {
        self.localPosition = selfDefaultPosition;
    }

    public void Reset()
    {
        //isOnArea = false;
        isDragged = false;
        isStarted = false;
        ReplaceImmediate();
    }
}
