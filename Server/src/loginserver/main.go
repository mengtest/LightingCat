package main

import (
	"base/glog"
	"base/gonet"
	"consts"
	"loginserver/playertask"
)

type LoginServer struct {
	gonet.Service
	tcpser *gonet.TcpServer
}

var m_server *LoginServer

func LoginServer_GetMe() *LoginServer {
	if m_server == nil {
		m_server = &LoginServer{
			tcpser: &gonet.TcpServer{},
		}
		m_server.Derived = m_server
	}
	return m_server
}

func (this *LoginServer) Init() bool {
	err := this.tcpser.Bind(consts.IpAddress)
	if err != nil {
		glog.Error("[server] Bind")
		return false
	}
	return true
}

func (this *LoginServer) MainLoop() {
	conn, err := this.tcpser.Accept()
	if err != nil {
		return
	}
	playertask.NewPlayerTask(conn).Start()
}

func (this *LoginServer) Final() bool {
	this.tcpser.Close()
	return true
}

func main() {
	glog.Info("[server] LoginServer start")
	LoginServer_GetMe().Main()
}
