using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using usercmd;
using System;

public class Test : MonoBehaviour {

    private static TcpClient m_Socket = null;
    private static NetworkStream m_Stream = null;
    // Use this for initialization
    void Start () {
        m_Socket = new TcpClient();
        m_Socket.Connect("192.168.31.236", 8888);
        m_Stream = m_Socket.GetStream();
        LoginC2SMsg msg = new LoginC2SMsg();
        msg.name = "huashao";
        byte[] dd = ProtobufTool.ProtoBufToBytes<LoginC2SMsg>(msg);
        int len = dd.Length+2;
        Debug.LogError(len);
        byte[] buf = new byte[len + 6];
        buf[0] = (byte)(len & 0xff);
        buf[1] = (byte)(len >> 8 & 0xff);
        buf[2] = (byte)(len >> 16 & 0xff);
        buf[3] = (byte)(len >> 24 & 0xff);
        buf[4] = 1;
        buf[5] = 0;
        Array.Copy(dd, 0, buf, 6, dd.Length);
        m_Stream.Write(buf,0,buf.Length);
        byte[] recv = new byte[1024];
        int c = m_Stream.Read(recv, 0, 1024);
        int rlen = (recv[0] | recv[1] << 8 | recv[2] << 16 | recv[3] << 24);
        int rId = (recv[4] | recv[5] << 8);
        Debug.LogError(string.Format("recv len:{0} id:{1}", rlen, rId));
        byte []rbuf = new byte[rlen-2];
        Array.Copy(recv, 6, rbuf, 0, rlen - 2);
        LoginS2CMsg rmsg = ProtobufTool.BytesToProtoBuf<LoginS2CMsg>(rbuf);
        Debug.LogError(rmsg.playerId);
        m_Stream.Close();
        m_Socket.Close();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
