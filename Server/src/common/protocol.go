package common

import (
	"base/glog"
	"encoding/binary"

	"github.com/gogo/protobuf/proto"
)

const CmdHeaderSize int = 2

type Message interface {
	Marshal() (data []byte, err error)
	MarshalTo(data []byte) (n int, err error)
	Size() (n int)
	Unmarshal(data []byte) error
}

// 获取指令号
func GetCmd(buf []byte) uint16 {
	if len(buf) < CmdHeaderSize {
		return 0
	}
	return uint16(buf[0]) | uint16(buf[1])<<8
}

// 生成二进制数据,返回数据和标识
func EncodeCmd(cmd uint16, msg proto.Message) ([]byte, error) {
	data, err := proto.Marshal(msg)
	if err != nil {
		glog.Error("[协议] protobuf生成二进制数据错误 ", err)
		return nil, err
	}
	var mbuff []byte = data

	p := make([]byte, len(mbuff)+CmdHeaderSize)
	binary.LittleEndian.PutUint16(p[0:], cmd)
	copy(p[2:], mbuff)
	return p, nil
}

// 解析protobuf数据
func DecodeCmd(buf []byte, pb proto.Message) proto.Message {
	if len(buf) < CmdHeaderSize {
		glog.Error("[协议] 解析protobuf数据错误 ", buf)
		return nil
	}
	var mbuff []byte
	mbuff = buf[CmdHeaderSize:]

	err := proto.Unmarshal(mbuff, pb)
	if err != nil {
		glog.Error("[协议] 生成protobuf数据错误 ", err)
		return nil
	}
	return pb
}
