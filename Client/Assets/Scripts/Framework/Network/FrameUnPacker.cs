
using System;
using System.IO;
namespace LightingCat
{
    public class FrameUnPacker
    {

        private int _recvMaxSize;

        private byte[] _recvBuf;

        public int CurSize
        {
            private set;
            get;
        }

        /// <summary>
        /// 包总长
        /// </summary>
        private int _allPacketLength;


        public FrameUnPacker(int maxSize)
        {
            _recvMaxSize = maxSize;
            _recvBuf = new byte[maxSize];
            _allPacketLength = 0;
        }

        public void Input(byte[] data)
        {
            if(data!=null && data.Length>0 && CurSize + data.Length <= _recvMaxSize)
            {
                Buffer.BlockCopy(data, 0, _recvBuf, CurSize, data.Length);
                CurSize += data.Length;
            }
        }

        public bool MoveNext(byte[] unpackData, out int dataLen, out ushort msgId)
        {
            if(_allPacketLength == 0)
            {
                if (CurSize >= NetDefine.PackHeadLen)
                {
                    _allPacketLength = 0;
                    for (int i = 0; i < NetDefine.PackHeadLen; i++)
                    {
                        _allPacketLength += _recvBuf[i] << (i * 8);
                    }
                    _allPacketLength += NetDefine.PackHeadLen;
                }
            }
            if(_allPacketLength > 0 && CurSize >= _allPacketLength)
            {
                ushort mId = 0;
                int len = _allPacketLength - NetDefine.MsgIDLen - NetDefine.PackHeadLen;
                for (int i = 0; i < NetDefine.MsgIDLen; i++)
                {
                    mId += (ushort)(_recvBuf[i + NetDefine.PackHeadLen] << (i * 8));
                }
                msgId = mId;
                dataLen = len;
                Buffer.BlockCopy(_recvBuf, NetDefine.PackHeadLen + NetDefine.MsgIDLen, unpackData, 0, len);
                if(CurSize == _allPacketLength)
                {
                    CurSize = 0;
                    _allPacketLength = 0;
                }
                else
                {
                    Buffer.BlockCopy(_recvBuf, _allPacketLength, _recvBuf, 0, CurSize - _allPacketLength);
                    CurSize -= _allPacketLength;
                    _allPacketLength = 0;
                }
                return true;
            }
            
            dataLen = 0;
            msgId = 0;
            return false;
        }

        public void Clear()
        {
            _allPacketLength = 0;
            CurSize = 0;
        }
    }

}

