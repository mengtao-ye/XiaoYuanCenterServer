using YSF;

namespace CenterServer
{
    public  class UdpHandlerMapper : BaseMap<short, IUdpRequestHandle>
    {
        protected override void Config()
        {
            
        }
        public void Init()
        {
            Add((short)UdpRequestCode.MainServer, new UdpMainServerHandler(XiaoYuanCenterMain.Instance.centerServer.udpServer));
            Add((short)UdpRequestCode.SubServer, new UdpSubServerHandler(XiaoYuanCenterMain.Instance.centerServer.udpServer));
        }
    }
}
