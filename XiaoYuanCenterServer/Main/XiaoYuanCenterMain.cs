using YSF;

namespace CenterServer
{
    public class XiaoYuanCenterMain
    {
        private ServerCenter mCenterServer;
        public ServerCenter centerServer { get { return mCenterServer; } }
        public static XiaoYuanCenterMain Instance { get; private set; }
        public XiaoYuanCenterMain()
        {
            Instance = this;
        }
        /// <summary>
        /// 启动模块
        /// </summary>
        public void Launcher() 
        {

            mCenterServer = new ServerCenter();
            //初始化助手
            mCenterServer.InitHelper(new XiaoYuanDataHelper());
            //初始化mySQL 
            //mCenterServer.LauncherMySQL(MySQLConfig.IP , MySQLConfig.DatabaseName, MySQLConfig.UserName, MySQLConfig.Password);
            //初始化udp服务
            UdpHandlerMapper udpHandlerMapper = new UdpHandlerMapper();
            mCenterServer.LauncherUDPServer(ServerData.IPAddress,ServerData.UdpServerPort, udpHandlerMapper);
            udpHandlerMapper.Init();
            //初始化tcp服务
            TcpHandlerMapper tcpHandlerMapper = new TcpHandlerMapper();
            mCenterServer.LauncherTCPServer(ServerData.IPAddress, ServerData.TcpServerPort, tcpHandlerMapper);
            tcpHandlerMapper.Init();
        }
    }
}
