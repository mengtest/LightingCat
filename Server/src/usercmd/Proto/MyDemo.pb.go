// Code generated by protoc-gen-gogo. DO NOT EDIT.
// source: Proto/MyDemo.proto

/*
Package usercmd is a generated protocol buffer package.

It is generated from these files:
	Proto/MyDemo.proto

It has these top-level messages:
	LoginC2SMsg
	LoginS2CMsg
*/
package usercmd

import proto "github.com/gogo/protobuf/proto"
import fmt "fmt"
import math "math"

import io "io"

// Reference imports to suppress errors if they are not otherwise used.
var _ = proto.Marshal
var _ = fmt.Errorf
var _ = math.Inf

// This is a compile-time assertion to ensure that this generated file
// is compatible with the proto package it is being compiled against.
// A compilation error at this line likely means your copy of the
// proto package needs to be updated.
const _ = proto.GoGoProtoPackageIsVersion2 // please upgrade the proto package

type DemoTypeCmd int32

const (
	DemoTypeCmd_LoginReq DemoTypeCmd = 1
	DemoTypeCmd_LoginRes DemoTypeCmd = 2
)

var DemoTypeCmd_name = map[int32]string{
	1: "LoginReq",
	2: "LoginRes",
}
var DemoTypeCmd_value = map[string]int32{
	"LoginReq": 1,
	"LoginRes": 2,
}

func (x DemoTypeCmd) Enum() *DemoTypeCmd {
	p := new(DemoTypeCmd)
	*p = x
	return p
}
func (x DemoTypeCmd) String() string {
	return proto.EnumName(DemoTypeCmd_name, int32(x))
}
func (x *DemoTypeCmd) UnmarshalJSON(data []byte) error {
	value, err := proto.UnmarshalJSONEnum(DemoTypeCmd_value, data, "DemoTypeCmd")
	if err != nil {
		return err
	}
	*x = DemoTypeCmd(value)
	return nil
}
func (DemoTypeCmd) EnumDescriptor() ([]byte, []int) { return fileDescriptorMyDemo, []int{0} }

type LoginC2SMsg struct {
	Name string `protobuf:"bytes,1,opt,name=name" json:"name"`
}

func (m *LoginC2SMsg) Reset()                    { *m = LoginC2SMsg{} }
func (m *LoginC2SMsg) String() string            { return proto.CompactTextString(m) }
func (*LoginC2SMsg) ProtoMessage()               {}
func (*LoginC2SMsg) Descriptor() ([]byte, []int) { return fileDescriptorMyDemo, []int{0} }

func (m *LoginC2SMsg) GetName() string {
	if m != nil {
		return m.Name
	}
	return ""
}

type LoginS2CMsg struct {
	PlayerId uint32 `protobuf:"varint,1,opt,name=playerId" json:"playerId"`
}

func (m *LoginS2CMsg) Reset()                    { *m = LoginS2CMsg{} }
func (m *LoginS2CMsg) String() string            { return proto.CompactTextString(m) }
func (*LoginS2CMsg) ProtoMessage()               {}
func (*LoginS2CMsg) Descriptor() ([]byte, []int) { return fileDescriptorMyDemo, []int{1} }

func (m *LoginS2CMsg) GetPlayerId() uint32 {
	if m != nil {
		return m.PlayerId
	}
	return 0
}

func init() {
	proto.RegisterType((*LoginC2SMsg)(nil), "usercmd.LoginC2SMsg")
	proto.RegisterType((*LoginS2CMsg)(nil), "usercmd.LoginS2CMsg")
	proto.RegisterEnum("usercmd.DemoTypeCmd", DemoTypeCmd_name, DemoTypeCmd_value)
}
func (m *LoginC2SMsg) Marshal() (dAtA []byte, err error) {
	size := m.Size()
	dAtA = make([]byte, size)
	n, err := m.MarshalTo(dAtA)
	if err != nil {
		return nil, err
	}
	return dAtA[:n], nil
}

func (m *LoginC2SMsg) MarshalTo(dAtA []byte) (int, error) {
	var i int
	_ = i
	var l int
	_ = l
	dAtA[i] = 0xa
	i++
	i = encodeVarintMyDemo(dAtA, i, uint64(len(m.Name)))
	i += copy(dAtA[i:], m.Name)
	return i, nil
}

func (m *LoginS2CMsg) Marshal() (dAtA []byte, err error) {
	size := m.Size()
	dAtA = make([]byte, size)
	n, err := m.MarshalTo(dAtA)
	if err != nil {
		return nil, err
	}
	return dAtA[:n], nil
}

func (m *LoginS2CMsg) MarshalTo(dAtA []byte) (int, error) {
	var i int
	_ = i
	var l int
	_ = l
	dAtA[i] = 0x8
	i++
	i = encodeVarintMyDemo(dAtA, i, uint64(m.PlayerId))
	return i, nil
}

func encodeFixed64MyDemo(dAtA []byte, offset int, v uint64) int {
	dAtA[offset] = uint8(v)
	dAtA[offset+1] = uint8(v >> 8)
	dAtA[offset+2] = uint8(v >> 16)
	dAtA[offset+3] = uint8(v >> 24)
	dAtA[offset+4] = uint8(v >> 32)
	dAtA[offset+5] = uint8(v >> 40)
	dAtA[offset+6] = uint8(v >> 48)
	dAtA[offset+7] = uint8(v >> 56)
	return offset + 8
}
func encodeFixed32MyDemo(dAtA []byte, offset int, v uint32) int {
	dAtA[offset] = uint8(v)
	dAtA[offset+1] = uint8(v >> 8)
	dAtA[offset+2] = uint8(v >> 16)
	dAtA[offset+3] = uint8(v >> 24)
	return offset + 4
}
func encodeVarintMyDemo(dAtA []byte, offset int, v uint64) int {
	for v >= 1<<7 {
		dAtA[offset] = uint8(v&0x7f | 0x80)
		v >>= 7
		offset++
	}
	dAtA[offset] = uint8(v)
	return offset + 1
}
func (m *LoginC2SMsg) Size() (n int) {
	var l int
	_ = l
	l = len(m.Name)
	n += 1 + l + sovMyDemo(uint64(l))
	return n
}

func (m *LoginS2CMsg) Size() (n int) {
	var l int
	_ = l
	n += 1 + sovMyDemo(uint64(m.PlayerId))
	return n
}

func sovMyDemo(x uint64) (n int) {
	for {
		n++
		x >>= 7
		if x == 0 {
			break
		}
	}
	return n
}
func sozMyDemo(x uint64) (n int) {
	return sovMyDemo(uint64((x << 1) ^ uint64((int64(x) >> 63))))
}
func (m *LoginC2SMsg) Unmarshal(dAtA []byte) error {
	l := len(dAtA)
	iNdEx := 0
	for iNdEx < l {
		preIndex := iNdEx
		var wire uint64
		for shift := uint(0); ; shift += 7 {
			if shift >= 64 {
				return ErrIntOverflowMyDemo
			}
			if iNdEx >= l {
				return io.ErrUnexpectedEOF
			}
			b := dAtA[iNdEx]
			iNdEx++
			wire |= (uint64(b) & 0x7F) << shift
			if b < 0x80 {
				break
			}
		}
		fieldNum := int32(wire >> 3)
		wireType := int(wire & 0x7)
		if wireType == 4 {
			return fmt.Errorf("proto: LoginC2SMsg: wiretype end group for non-group")
		}
		if fieldNum <= 0 {
			return fmt.Errorf("proto: LoginC2SMsg: illegal tag %d (wire type %d)", fieldNum, wire)
		}
		switch fieldNum {
		case 1:
			if wireType != 2 {
				return fmt.Errorf("proto: wrong wireType = %d for field Name", wireType)
			}
			var stringLen uint64
			for shift := uint(0); ; shift += 7 {
				if shift >= 64 {
					return ErrIntOverflowMyDemo
				}
				if iNdEx >= l {
					return io.ErrUnexpectedEOF
				}
				b := dAtA[iNdEx]
				iNdEx++
				stringLen |= (uint64(b) & 0x7F) << shift
				if b < 0x80 {
					break
				}
			}
			intStringLen := int(stringLen)
			if intStringLen < 0 {
				return ErrInvalidLengthMyDemo
			}
			postIndex := iNdEx + intStringLen
			if postIndex > l {
				return io.ErrUnexpectedEOF
			}
			m.Name = string(dAtA[iNdEx:postIndex])
			iNdEx = postIndex
		default:
			iNdEx = preIndex
			skippy, err := skipMyDemo(dAtA[iNdEx:])
			if err != nil {
				return err
			}
			if skippy < 0 {
				return ErrInvalidLengthMyDemo
			}
			if (iNdEx + skippy) > l {
				return io.ErrUnexpectedEOF
			}
			iNdEx += skippy
		}
	}

	if iNdEx > l {
		return io.ErrUnexpectedEOF
	}
	return nil
}
func (m *LoginS2CMsg) Unmarshal(dAtA []byte) error {
	l := len(dAtA)
	iNdEx := 0
	for iNdEx < l {
		preIndex := iNdEx
		var wire uint64
		for shift := uint(0); ; shift += 7 {
			if shift >= 64 {
				return ErrIntOverflowMyDemo
			}
			if iNdEx >= l {
				return io.ErrUnexpectedEOF
			}
			b := dAtA[iNdEx]
			iNdEx++
			wire |= (uint64(b) & 0x7F) << shift
			if b < 0x80 {
				break
			}
		}
		fieldNum := int32(wire >> 3)
		wireType := int(wire & 0x7)
		if wireType == 4 {
			return fmt.Errorf("proto: LoginS2CMsg: wiretype end group for non-group")
		}
		if fieldNum <= 0 {
			return fmt.Errorf("proto: LoginS2CMsg: illegal tag %d (wire type %d)", fieldNum, wire)
		}
		switch fieldNum {
		case 1:
			if wireType != 0 {
				return fmt.Errorf("proto: wrong wireType = %d for field PlayerId", wireType)
			}
			m.PlayerId = 0
			for shift := uint(0); ; shift += 7 {
				if shift >= 64 {
					return ErrIntOverflowMyDemo
				}
				if iNdEx >= l {
					return io.ErrUnexpectedEOF
				}
				b := dAtA[iNdEx]
				iNdEx++
				m.PlayerId |= (uint32(b) & 0x7F) << shift
				if b < 0x80 {
					break
				}
			}
		default:
			iNdEx = preIndex
			skippy, err := skipMyDemo(dAtA[iNdEx:])
			if err != nil {
				return err
			}
			if skippy < 0 {
				return ErrInvalidLengthMyDemo
			}
			if (iNdEx + skippy) > l {
				return io.ErrUnexpectedEOF
			}
			iNdEx += skippy
		}
	}

	if iNdEx > l {
		return io.ErrUnexpectedEOF
	}
	return nil
}
func skipMyDemo(dAtA []byte) (n int, err error) {
	l := len(dAtA)
	iNdEx := 0
	for iNdEx < l {
		var wire uint64
		for shift := uint(0); ; shift += 7 {
			if shift >= 64 {
				return 0, ErrIntOverflowMyDemo
			}
			if iNdEx >= l {
				return 0, io.ErrUnexpectedEOF
			}
			b := dAtA[iNdEx]
			iNdEx++
			wire |= (uint64(b) & 0x7F) << shift
			if b < 0x80 {
				break
			}
		}
		wireType := int(wire & 0x7)
		switch wireType {
		case 0:
			for shift := uint(0); ; shift += 7 {
				if shift >= 64 {
					return 0, ErrIntOverflowMyDemo
				}
				if iNdEx >= l {
					return 0, io.ErrUnexpectedEOF
				}
				iNdEx++
				if dAtA[iNdEx-1] < 0x80 {
					break
				}
			}
			return iNdEx, nil
		case 1:
			iNdEx += 8
			return iNdEx, nil
		case 2:
			var length int
			for shift := uint(0); ; shift += 7 {
				if shift >= 64 {
					return 0, ErrIntOverflowMyDemo
				}
				if iNdEx >= l {
					return 0, io.ErrUnexpectedEOF
				}
				b := dAtA[iNdEx]
				iNdEx++
				length |= (int(b) & 0x7F) << shift
				if b < 0x80 {
					break
				}
			}
			iNdEx += length
			if length < 0 {
				return 0, ErrInvalidLengthMyDemo
			}
			return iNdEx, nil
		case 3:
			for {
				var innerWire uint64
				var start int = iNdEx
				for shift := uint(0); ; shift += 7 {
					if shift >= 64 {
						return 0, ErrIntOverflowMyDemo
					}
					if iNdEx >= l {
						return 0, io.ErrUnexpectedEOF
					}
					b := dAtA[iNdEx]
					iNdEx++
					innerWire |= (uint64(b) & 0x7F) << shift
					if b < 0x80 {
						break
					}
				}
				innerWireType := int(innerWire & 0x7)
				if innerWireType == 4 {
					break
				}
				next, err := skipMyDemo(dAtA[start:])
				if err != nil {
					return 0, err
				}
				iNdEx = start + next
			}
			return iNdEx, nil
		case 4:
			return iNdEx, nil
		case 5:
			iNdEx += 4
			return iNdEx, nil
		default:
			return 0, fmt.Errorf("proto: illegal wireType %d", wireType)
		}
	}
	panic("unreachable")
}

var (
	ErrInvalidLengthMyDemo = fmt.Errorf("proto: negative length found during unmarshaling")
	ErrIntOverflowMyDemo   = fmt.Errorf("proto: integer overflow")
)

func init() { proto.RegisterFile("Proto/MyDemo.proto", fileDescriptorMyDemo) }

var fileDescriptorMyDemo = []byte{
	// 164 bytes of a gzipped FileDescriptorProto
	0x1f, 0x8b, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0xff, 0xe2, 0x12, 0x0a, 0x28, 0xca, 0x2f,
	0xc9, 0xd7, 0xf7, 0xad, 0x74, 0x49, 0xcd, 0xcd, 0xd7, 0x2b, 0x00, 0x71, 0x84, 0xd8, 0x4b, 0x8b,
	0x53, 0x8b, 0x92, 0x73, 0x53, 0x94, 0xd4, 0xb9, 0xb8, 0x7d, 0xf2, 0xd3, 0x33, 0xf3, 0x9c, 0x8d,
	0x82, 0x7d, 0x8b, 0xd3, 0x85, 0x24, 0xb8, 0x58, 0xf2, 0x12, 0x73, 0x53, 0x25, 0x18, 0x15, 0x18,
	0x35, 0x38, 0x9d, 0x58, 0x4e, 0xdc, 0x93, 0x67, 0x08, 0x02, 0x8b, 0x28, 0xe9, 0x43, 0x15, 0x06,
	0x1b, 0x39, 0x83, 0x14, 0x2a, 0x70, 0x71, 0x14, 0xe4, 0x24, 0x56, 0xa6, 0x16, 0x79, 0xa6, 0x80,
	0x15, 0xf3, 0x42, 0x15, 0xc3, 0x45, 0xb5, 0x34, 0xb9, 0xb8, 0x41, 0x16, 0x86, 0x54, 0x16, 0xa4,
	0x3a, 0xe7, 0xa6, 0x08, 0xf1, 0x70, 0x71, 0x80, 0xf5, 0x07, 0xa5, 0x16, 0x0a, 0x30, 0x22, 0xf1,
	0x8a, 0x05, 0x98, 0x9c, 0x04, 0x4e, 0x3c, 0x92, 0x63, 0xbc, 0xf0, 0x48, 0x8e, 0xf1, 0xc1, 0x23,
	0x39, 0xc6, 0x09, 0x8f, 0xe5, 0x18, 0x00, 0x01, 0x00, 0x00, 0xff, 0xff, 0xd0, 0x7d, 0x4e, 0x03,
	0xb4, 0x00, 0x00, 0x00,
}
