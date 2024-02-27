using System;

namespace CenterServer
{
    class Program
    {
        static void Main(string[] args)
        {
            XiaoYuanCenterMain juHeYaCenterMain = new XiaoYuanCenterMain();
            juHeYaCenterMain.Launcher();
            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
