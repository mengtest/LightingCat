using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightingCat;
using usercmd;
using System.Threading;

public class Test1 : MonoBehaviour
{
    TcpConnector connector;

    FramePacker packer = new FramePacker(16 * 1024);
    FrameUnPacker unpacker = new FrameUnPacker(16 * 1024);

    void Start()
    {

        byte[] recv = new byte[16 * 1024];

        connector = new TcpConnector("192.168.31.236", 8888);
        connector.On(SocketEvents.Message, (d) =>
        {

            byte[] reData = d as byte[];
            Debug.LogError(reData.Length);
            unpacker.Input(reData);
            int length;
            ushort msgId;
            while (unpacker.MoveNext(recv, out length, out msgId))
            {
                LoginS2C lmsg = ProtobufTool.BytesToProtoBuf<LoginS2C>(recv, 0, length);
                Debug.LogError("recv msgID: " + msgId + " data:" + lmsg.playerId);
            }
        });
        var wait = connector.Connect();

        if (!Util.Wait(wait, 1000))
        {
            connector.Dispose();
        }




    }

    void Update()
    {
        if (connector != null && connector.Connected)
        {
            LoginC2S msg = new LoginC2S();
            msg.name = "huashao";
            byte[] dd = ProtobufTool.ProtoBufToBytes<LoginC2S>(msg);
            for (int i = 0; i < 10; i++)
            {
                packer.Input((ushort)DemoTypeCmd.LoginReq, dd);
                Debug.LogError("sendSize:" + packer.CurSize);
                var await = connector.Send(packer.SendBuf, 0, packer.CurSize);
                packer.Clear();
            }
        }
    }
}
