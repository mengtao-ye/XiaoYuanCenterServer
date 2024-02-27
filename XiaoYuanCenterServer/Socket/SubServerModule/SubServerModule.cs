using System.Collections.Generic;
using System.Net;
using System.Threading;
using YSF;

namespace CenterServer
{
    /// <summary>
    /// 分布式服务器模块
    /// </summary>
    public class SubServerModule : YSF.Singleton<SubServerModule>
    {
        private  int SubServerID;//分布式服务器的id
        private  List<SubServerCollection> mSubServerList;
        private Thread mUpdateThead;
        public SubServerModule()
        {
            mSubServerList = new List<SubServerCollection>();
            mUpdateThead = new Thread(Update);
            mUpdateThead.Start();
        }

        private void Update() 
        {
            while (true) 
            {
                Thread.Sleep(1000);
                if (mSubServerList.IsNullOrEmpty()) continue;
                for (int i = 0; i < mSubServerList.Count; i++)
                {
                    mSubServerList[i].Check();
                }
            }
        }

        /// <summary>
        /// 注册分布式服务器
        /// </summary>
        /// <param name="subServerEnum"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public int RegisterSubServer(SubServerType subServerEnum, EndPoint point)
        {
            int id = ++SubServerID;
            SubServerCollection collection = FindSubServerCollection(subServerEnum);
            if (collection == null)
            {
                collection = new SubServerCollection(subServerEnum);
                mSubServerList.Add(collection);
            }
            collection.Add(id, point);
            return id;
        }
        /// <summary>
        /// 刷新注册服务器
        /// </summary>
        /// <param name="subServerEnum"></param>
        /// <param name="id"></param>
        /// <param name="point"></param>
        public void UpdateSubServer(SubServerType subServerEnum, int id,EndPoint point) {
            SubServerCollection collection = FindSubServerCollection(subServerEnum);
            if (collection == null)
            {
                collection = new SubServerCollection(subServerEnum);
                mSubServerList.Add(collection);
            }
            collection.Update(id, point);
        }
        /// <summary>
        /// 获取Point
        /// </summary>
        /// <param name="subServerEnum"></param>
        /// <returns></returns>
        public EndPoint FindPoint(SubServerType subServerEnum)
        {
            return FindSubServerCollection(subServerEnum)?.GetRandomPoint();
        }

        /// <summary>
        /// 获取Point
        /// </summary>
        /// <param name="subServerEnum"></param>
        /// <returns></returns>
        public SubServerCollection FindSubServerCollection(SubServerType subServerEnum)
        {
            if (mSubServerList.IsNullOrEmpty()) return null;
            for (int i = 0; i < mSubServerList.Count; i++)
            {
                if (mSubServerList[i].subServerType == subServerEnum)
                    return mSubServerList[i];
            }
            return null;
        }
    }
}
