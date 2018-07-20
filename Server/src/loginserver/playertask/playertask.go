package playertask

import (
	"base/glog"
	"base/gonet"
	"common"
	"net"
	"usercmd"
)

type PlayerTask struct {
	gonet.TcpTask
}

func NewPlayerTask(conn net.Conn) *PlayerTask {
	s := &PlayerTask{
		TcpTask: *gonet.NewTcpTask(conn),
	}
	s.Derived = s
	return s
}

func (this *PlayerTask) ParseMsg(data []byte) bool {

	cmd := usercmd.DemoTypeCmd(common.GetCmd(data))
	glog.Info("cmd = ", cmd)
	switch cmd {
	case usercmd.DemoTypeCmd_LoginReq:
		glog.Info("receive login req")
		recvCmd, ok := common.DecodeCmd(data, &usercmd.LoginC2SMsg{}).(*usercmd.LoginC2SMsg)
		if ok {
			glog.Info("recv = ", recvCmd.GetName())
		}
	default:
		glog.Error("[cmd] no mathcing funciton %u", cmd)
	}
	return true
}

func (this *PlayerTask) OnClose() {

}
