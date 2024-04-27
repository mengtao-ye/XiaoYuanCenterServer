using System.Collections.Generic;
using System.Net;
using YSF;

namespace CenterServer
{
    public class SubServerData
    {
        public EndPoint Point { get; private set; }
        public int ID { get; private set; }
        private int mFrashCount;
        private SubServerCollection mCollection;
        private List<long> mIdentifyList;//标识
        public SubServerData(SubServerCollection collection, EndPoint point, int id)
        {
            mFrashCount = 0;
            ID = id;
            mCollection = collection;
            mIdentifyList = new List<long>();
            Update(point);
        }
        public void Update(EndPoint point)
        {
            mFrashCount = 0;
            Point = point;
        }
        public override string ToString()
        {
            return Point.ToString();
        }
        public bool Check()
        {
            mFrashCount++;
            if (mFrashCount > 5)
            {
                Debug.Log(mCollection.subServerType.ToString() + "_掉线了：" + Point?.ToString());
                mCollection.Remove(this);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 是否包含标识符
        /// </summary>
        /// <param name="identify"></param>
        /// <returns></returns>
        public bool ContainsIdentify(long identify) 
        {
            for (int i = 0; i < mIdentifyList.Count; i++)
            {
                if (mIdentifyList[i] == identify) return true;
            }
            return false;
        }
        /// <summary>
        /// 添加标识符
        /// </summary>
        /// <param name="identify"></param>
        public void AddIdentify(long identify)
        {
            if (!mIdentifyList.Contains(identify))
            {
                mIdentifyList.Add(identify);
            }
        }
    }
}
