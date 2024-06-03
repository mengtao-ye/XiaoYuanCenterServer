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
            //Add((short)JuHeYaUdpCode.LoginSubServerRegister, BaseSubServerRegister);
            //Add((short)JuHeYaUdpCode.LoginSubServerHeartBeat, BaseSubServerHeartBeat);
            Add((short)JuHeYaUdpCode.MetaSchoolSubServerRegister, MetaSchoolSubServerRegister);
            Add((short)JuHeYaUdpCode.MetaSchoolSubServerHeartBeat, MetaSchoolSubServerHeartBeat);
            Add((short)JuHeYaUdpCode.TcpLoginSubServerRegister, BaseSubServerRegister);
            Add((short)JuHeYaUdpCode.TcpLoginSubServerHeartBeat, BaseSubServerHeartBeat);
        }
        /// <summary>
        ///校园布式服务器心跳包
        /// </summary>
        /// <param name="data"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private byte[] MetaSchoolSubServerHeartBeat(byte[] data, EndPoint point)
        {
            if (data.IsNullOrEmpty() || data.Length != 4)
            {
                return null;
            }
            SubServerModule.Instance.UpdateSubServer(SubServerType.MetaSchoolServer, data.ToInt(), point);
            return BytesConst.TRUE_BYTES;
        }

        /// <summary>
        /// 注册校园服务器
        /// </summary>
        /// <param name="data"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private byte[] MetaSchoolSubServerRegister(byte[] data, EndPoint point)
        {
            SubServerType subServerType = (SubServerType)data.ToByte();
            byte[] pointBytes = ByteTools.SubBytes(data, 1);
            int id = SubServerModule.Instance.RegisterSubServer(subServerType, point, pointBytes);
            Debug.Log("MetaSchoolSubServerID:" + id);
            return id.ToBytes();
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
            byte[] pointBytes = ByteTools.SubBytes(data,1);
            int id = SubServerModule.Instance.RegisterSubServer(subServerType, point, pointBytes);
            Debug.Log("LoginSubServerID:"+id);
            return id.ToBytes();
        }

    }
}
