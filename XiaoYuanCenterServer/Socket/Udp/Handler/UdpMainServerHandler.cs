using System.Net;
using YSF;

namespace CenterServer
{
    public class UdpMainServerHandler : BaseUdpRequestHandle
    {
        public override short requestCode => (short)UdpRequestCode.MainServer;
        public UdpMainServerHandler(IUdpServer server) : base(server)
        {

        }
        protected override void ComfigActionCode()
        {
            Add((short)JuHeYaUdpCode.MainServerHeartBeat, MainServerHeartBeat);
            Add((short)JuHeYaUdpCode.GetBaseServerPoint, GetBaseServerPoint);
        }

        /// <summary>
        /// 获取登录分布式服务器Point
        /// </summary>
        /// <param name="data"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private byte[] GetBaseServerPoint(byte[] data, EndPoint point)
        {
            if (data.IsNullOrEmpty()) return null;
            SubServerType subServerType = (SubServerType)data.ToByte();
            EndPoint targetPoint = SubServerModule.Instance.FindPoint(subServerType);
            if (targetPoint == null)
            {
                return null;
            }
            return targetPoint.ToBytes();
        }
        /// <summary>
        /// 心跳包
        /// </summary>
        /// <param name="data"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private byte[] MainServerHeartBeat(byte[] data, EndPoint point)
        {
            return BytesConst.TRUE_BYTES;
        }
    }
}
