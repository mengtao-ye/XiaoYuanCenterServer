using YSF;

namespace CenterServer
{
    public class TcpMainServerHandler : BaseTcpRequestHandle
    {
        protected override short mRequestCode =>(short) TcpRequestCode.MainServer;
        public TcpMainServerHandler(TCPServer server) : base(server)
        {
        }
        protected override void ComfigActionCode()
        {
            Add((short)TcpCode.TesCode, TestCode);
        }

        private byte[] TestCode(byte[] data)
        {
            Debug.Log("收到数据:"+data.ToStr());
            return data;
        }

    }
}
