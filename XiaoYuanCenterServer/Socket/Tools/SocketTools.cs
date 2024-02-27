using System.Net;
using YSF;

namespace CenterServer
{
    public static class SocketTools
    {
        /// <summary>
        /// 解析分布式服务器数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="point"></param>
        public static void ResponseSubMsg(byte[] data, EndPoint point,SubServerType subServerEnum,short actionCode,IUdpServer server)
        {
            if (data.IsNullOrEmpty()) return;
            EndPoint targetPoint = SubServerModule.Instance.FindPoint(subServerEnum);
            if (targetPoint == null)
            {
                Debug.Log(subServerEnum.ToString() +"未找到目标");
                return;
            }
            server.UdpSend(targetPoint, actionCode, ByteTools.Concat(point.ToBytes(), data));
        }
    }
}
