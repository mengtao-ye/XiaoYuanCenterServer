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
        public SubServerData(SubServerCollection collection,EndPoint point,int id)
        {
            mFrashCount = 0;
            ID = id;
            mCollection = collection;
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
                Debug.Log(mCollection.subServerType.ToString() + "_掉线了：" +Point?.ToString());
                mCollection.Remove(this);
                return true;
            }
            return false;
        }
    }
}
