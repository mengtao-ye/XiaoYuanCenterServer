using System;
using System.Collections.Generic;
using System.Net;
using YSF;

namespace CenterServer
{
    /// <summary>
    /// 分布式服务器收集器
    /// </summary>
    public class SubServerCollection
    {
        public SubServerType subServerType { get; private set; }
        private List<SubServerData> mSubServerList;
        private Random mRandom;
        public SubServerCollection(SubServerType subServerType)
        {
            this.subServerType = subServerType;
            mRandom = new Random();
            mSubServerList = new List<SubServerData>();
        }
        /// <summary>
        /// 检查是否有掉线的
        /// </summary>
        public void Check()
        {
            for (int i = 0; i < mSubServerList.Count; i++)
            {
                if (mSubServerList[i].Check())
                {
                    Check();
                }
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="id"></param>
        /// <param name="endPoint"></param>
        public void Add(int id, EndPoint endPoint)
        {
            if (Contains(id))
            {
                Debug.LogError("SubServerDict contains id:" + id);
                return;
            }
            if (endPoint == null)
            {
                Debug.LogError("endPoint is null");
                return;
            }
            mSubServerList.Add(new SubServerData(this,endPoint, id));
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="endPoint"></param>
        public void Update(int id, EndPoint endPoint)
        {
            SubServerData data = Find(id);
            if (data == null) return;
            if (endPoint == null) return;
            data.Update(endPoint);
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private SubServerData Find(int id)
        {
            for (int i = 0; i < mSubServerList.Count; i++)
            {
                if (mSubServerList[i].ID == id)
                {
                    return mSubServerList[i];
                }
            }
            return null;
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool Contains(int id)
        {
            return Find(id) != null;
        }

        public EndPoint GetPoint(long identify)
        {
            if (identify == 0) return GetRandomSubServer().Point;
            for (int i = 0; i < mSubServerList.Count; i++)
            {
                if (mSubServerList[i].ContainsIdentify(identify))
                {
                    return mSubServerList[i].Point;
                }
            }
            SubServerData subServerData = GetRandomSubServer();
            if (subServerData == null) return null;
            subServerData.AddIdentify(identify);
            return subServerData.Point;
        }

        /// <summary>
        /// 随机获取一个分布式服务求对象
        /// </summary>
        /// <returns></returns>
        private SubServerData GetRandomSubServer()
        {
            if (mSubServerList.Count == 0)
            {
                return null;
            }
            int index = mRandom.Next(0, mSubServerList.Count);
            return mSubServerList[index];
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="subServerData"></param>
        public void Remove(SubServerData subServerData)
        {
            if (mSubServerList.Contains(subServerData))
            {
                mSubServerList.Remove(subServerData);
            }
        }
    }
}
