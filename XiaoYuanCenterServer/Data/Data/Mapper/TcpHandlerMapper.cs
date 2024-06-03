using YSF;

namespace CenterServer
{
    public class TcpHandlerMapper : BaseMap<short, ITCPRequestHandle>
    {
        protected override void Config()
        {
        }
        public void Init()
        {
            AddTcpHandler(new TcpMainServerHandler(XiaoYuanCenterMain.Instance.centerServer.tcpServer));
        }
        public void AddTcpHandler(ITCPRequestHandle handler)
        {
            Add(handler.requestCode,handler);
        }
    }
}
