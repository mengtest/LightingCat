package main

import (
	"base/glog"
	"base/gonet"
	"common"
	"consts"
	"net"
	"os"
	"strconv"
	"usercmd"
)

type playertask struct {
	gonet.TcpTask
}

func (this *playertask) ParseMsg(data []byte) bool {
	cmd := usercmd.DemoTypeCmd(common.GetCmd(data))
	glog.Info("cmd = ", cmd)
	switch cmd {
	case usercmd.DemoTypeCmd_LoginRes:
		recvCmd, ok := common.DecodeCmd(data, &usercmd.LoginS2CMsg{}).(*usercmd.LoginS2CMsg)
		if !ok {
			glog.Error("login res error")
		}
		glog.Info("[login res] id = ", recvCmd.GetPlayerId())
		//this.id = recvCmd.GetPlayerId()
	}

	return true
}

func (this *playertask) OnClose() {
	glog.Info("断开连接")
}

func (this *playertask) loginReq() {
	glog.Info("login req")
	m := usercmd.LoginC2SMsg{
		Name: "玩家123",
	}
	d, _ := common.EncodeCmd(uint16(usercmd.DemoTypeCmd_LoginReq), &m)
	this.AsyncSend(d)
}

func main() {
	conn, _ := net.Dial("tcp", consts.IpAddress)
	s := &playertask{
		TcpTask: *gonet.NewTcpTask(conn),
	}
	s.Derived = s
	// 设置发送缓冲区限制
	s.SetSendBuffSizeLimt(64 * 1024)
	s.Start()
	for {
		input := make([]byte, 1)
		os.Stdin.Read(input)
		cmdStr := string(input[0:1])
		cmdInt, _ := strconv.Atoi(cmdStr)
		switch cmdInt {
		case 1:
			s.loginReq()
		}
	}
}
