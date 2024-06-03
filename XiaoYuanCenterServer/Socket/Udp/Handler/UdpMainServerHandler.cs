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
            Add((short)JuHeYaUdpCode.GetLoginServerPoint, GetLoginServerPoint);
            Add((short)JuHeYaUdpCode.GetMetaSchoolServerPoint, GetMetaSchoolServerPoint);
        }

        /// <summary>
        /// 获取校园分布式服务器Point
        /// </summary>
        /// <param name="data"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private byte[] GetMetaSchoolServerPoint(byte[] data, EndPoint point)
        {
            if (data.IsNullOrEmpty()) return null;
            SubServerType subServerType = (SubServerType)data.ToByte();
            int identify = data.ToInt(1);
            byte[] targetPoint = SubServerModule.Instance.GetPoint(subServerType, identify);
            if (targetPoint == null)
            {
                return null;
            }
            return targetPoint;
        }
        /// <summary>
        /// 获取登录分布式服务器Point
        /// </summary>
        /// <param name="data"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private byte[] GetLoginServerPoint(byte[] data, EndPoint point)
        {
            if (data.IsNullOrEmpty()) return null;
            SubServerType subServerType = (SubServerType)data.ToByte();
            byte[] targetPoint = SubServerModule.Instance.GetPoint(subServerType,0);
            if (targetPoint == null)
            {
                return null;
            }
            return targetPoint;
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
