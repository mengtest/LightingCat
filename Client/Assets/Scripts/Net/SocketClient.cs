using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net.Sockets;
using UnityEngine;
using System;

public class SocketClient
{ 

    private static TcpClient m_Socket = null;
    private static NetworkStream m_Stream = null;
    private bool m_IsConnected = false;

    private Thread m_Trecv;
    private bool m_IsThreadStart = false;

    public bool IsConnected
    {
        get { return this.m_IsConnected;  }
    }

    protected virtual void Init()
    {
        m_Socket = new TcpClient();
    }

    public bool Startup()
    {
        this.Init();
        if (this.Connect())
        {
            m_Trecv = new Thread(new ThreadStart(this.ThreadRecv));
   
            m_Trecv.Start();
            //m_Tsend.Start();
            m_IsThreadStart = true;
        }
        else
        {
            return false;
        }
        return true;

    }

    private bool Connect()
    {
        try
        {
            m_Socket.Connect(Config.SERVER_IP, Config.SERVER_PORT);
            m_Stream = m_Socket.GetStream();
            Debug.Log("连接成功");
            this.m_IsConnected = true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
        return true;
    }

    private void ThreadRecv()
    {
        int dataSize = 0;
        uint length = 0;
        bool isReadHead = false;
        ushort msgId = 0;
        byte[] buffer = new byte[Config.MAX_BUFFER_SIZE];
        byte[] tmpBuffer = new byte[Config.MAX_BUFFER_SIZE];
        while (true)
        {
            Thread.Sleep(1);
            int c = 0;
            try
            {
               
                c = m_Stream.Read(buffer, dataSize, Config.MAX_BUFFER_SIZE);
                int curSize = c + dataSize;
                if(!isReadHead)
                {
                    if(curSize >= 6)
                    {
                        length = ((uint)buffer[3] << 24) + ((uint)buffer[2] << 16) + ((uint)buffer[1] << 8) + buffer[0];
                        msgId = (ushort)((buffer[4] << 8) + buffer[5]);
                        isReadHead = true;
                    }
                }
                else
                {
                    if(curSize >= 2 + length)
                    {
                        isReadHead = false;

                        Array.Copy(buffer, tmpBuffer, length + 6);

                    }
                }
                
                

                if (c > 0)
                {
                    //string str = System.Text.Encoding.UTF8.GetString(buffer, 0, c);
                    //Debug.Log(str);
                    byte[] bytes = new byte[c];
                    Array.Copy(buffer, bytes, c);
                    try
                    {
                        //sea.NetProto netData = ProtobufTool.BytesToProtoBuf<sea.NetProto>(bytes);
                        ////数据包不全则忽略
                        //if (netData.dataCount > 0 && netData.dataCount == netData.data.Length)
                        //{
                        //    NetworkManager.AddMsg(netData);
                        //}
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                    }
                    //Array.Clear(buffer, 0, c);
                }
            }
            catch (Exception e)
            {
                Debug.Log("连接已经断开" + e.Message);
                
                Exit();
                return;
            };
        }
    }



    //退出
    public void Exit()
    {
        if (m_IsThreadStart)
        {
            m_Trecv.Abort();
            Debug.Log("[NetWork]:Socket Thread Close");
        }
        if (m_IsConnected)
        {
            this.m_IsConnected = false;
            m_Stream.Close();
            m_Socket.Close();
        }
    }

    public void SendMessage(byte[] buffer)
    {
       
        if (buffer.Length > Config.MAX_BUFFER_SIZE)
        {
            Debug.LogError("buffer size out of range");
        }
        m_Stream.Write(buffer, 0, buffer.Length);
    }

    public void SendMessage(string msg)
    {
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(msg);
        SendMessage(buffer);
    }

}
