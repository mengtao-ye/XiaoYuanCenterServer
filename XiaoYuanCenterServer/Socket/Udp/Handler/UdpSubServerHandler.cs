using System.Net;
using YSF;

namespace CenterServer
{
    public class UdpSubServerHandler : BaseUdpRequestHandle
    {
        public override short requestCode => (short)UdpRequestCode.SubServer;
        public UdpSubServerHandler(UdpServer server) : base(server)
        {

        }
        protected override void ComfigActionCode()
        {
            Add((short)JuHeYaUdpCode.BaseSubServerRegister, BaseSubServerRegister);
            Add((short)JuHeYaUdpCode.BaseSubServerHeartBeat, BaseSubServerHeartBeat);
        }

        /// <summary>
        ///登基础布式服务器心跳包
        /// </summary>
        /// <param name="data"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private byte[] BaseSubServerHeartBeat(byte[] data, EndPoint point) 
        {
            if (data.IsNullOrEmpty() || data.Length != 4)
            {
                return null;
            }
            SubServerModule.Instance.UpdateSubServer( SubServerType.LoginServer,data.ToInt(), point);
            return BytesConst.TRUE_BYTES;
        }

        /// <summary>
        /// 注册基础服务器
        /// </summary>
        /// <param name="data"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private byte[] BaseSubServerRegister(byte[] data, EndPoint point)
        {
            SubServerType subServerType =(SubServerType) data.ToByte();
            int id = SubServerModule.Instance.RegisterSubServer(subServerType, point);
            Debug.Log("SubServerID:"+id);
            return id.ToBytes();
        }

    }
}
