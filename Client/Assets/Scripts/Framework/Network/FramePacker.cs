
using System;
namespace LightingCat
{
    /// <summary>
    /// 打包发送数据
    /// </summary>
    public class FramePacker
    {
        private int _sendMaxSize;
        private byte[] _sendBuf;
        public byte[] SendBuf
        {
            get
            {
                return _sendBuf;
            }
        }
        public  int CurSize
        {
            private set;
            get;
        }

        public FramePacker(int maxSize)
        {
            _sendMaxSize = maxSize;
            _sendBuf = new byte[maxSize];
        }

        public virtual bool Input(ushort msgId ,byte[] buf)
        {
            int len = buf.Length;
            int bodyLen = len + NetDefine.MsgIDLen;
            if(CurSize + bodyLen + NetDefine.PackHeadLen > _sendMaxSize)
            {
                return false;
            }
            Buffer.BlockCopy(buf, 0, _sendBuf, CurSize + NetDefine.PackHeadLen + NetDefine.MsgIDLen, len);
            for(int i = 0;i< NetDefine.PackHeadLen; i++)
            {
                _sendBuf[CurSize+i] = (byte)(bodyLen >> (i * 8) & 0xff);
            }
            for(int i=0;i<NetDefine.MsgIDLen;i++)
            {
                _sendBuf[CurSize + NetDefine.PackHeadLen  + i] = (byte)(msgId >> (i * 8) & 0xff);
            }
            CurSize += bodyLen + NetDefine.PackHeadLen;
            return true;
        }

        public virtual void Clear()
        {
            CurSize = 0;
        }
    }

}
