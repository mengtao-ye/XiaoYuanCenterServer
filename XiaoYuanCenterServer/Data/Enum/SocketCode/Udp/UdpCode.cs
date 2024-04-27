namespace CenterServer
{
    public enum JuHeYaUdpCode : short
    {
        //Common
        ServerBigDataResponse = UdpRequestCode.Common + 1,//服务器端大数据下标反馈
        ClientBigDataResponse = UdpRequestCode.Common + 2,//客户端大数据下标反馈

        //帧同步数据
        LockStep_ClientData = UdpRequestCode.LockStep + 1,//客户端帧同步数据
        LockStep_ServerData = UdpRequestCode.LockStep + 2,//服务器端帧同步数据

        //SubServer
        LoginSubServerRegister = UdpRequestCode.SubServer + 1,//基础分布式服务器注册
        LoginSubServerHeartBeat = UdpRequestCode.SubServer + 2,//基础分布式服务器心跳包
        MetaSchoolSubServerRegister = UdpRequestCode.SubServer + 3,//校园分布式服务器注册
        MetaSchoolSubServerHeartBeat = UdpRequestCode.SubServer + 4,//校园分布式服务器心跳包
        //MainServer
        MainServerHeartBeat = UdpRequestCode.MainServer + 1,//主服务器心跳包
        GetLoginServerPoint = UdpRequestCode.MainServer + 2,//获取登录服务器point
        GetMetaSchoolServerPoint = UdpRequestCode.MainServer + 3,//获取校园服务器point

    }
}
