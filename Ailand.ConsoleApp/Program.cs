using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Ailand.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "s")
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new MainService(),
                };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                CreateService(true);
            }
        }
        static void CreateService(bool first)
        {
            if (first) Console.WriteLine("Ailand Window Service 程序");
            Console.WriteLine("请选择：[1]安装服务 [2]卸载服务");
            string readStr = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(readStr)) return;
            var rs = int.Parse(readStr);
            switch (rs)
            {
                case 1:
                    //取当前可执行文件路径，加上"s"参数，证明是从windows服务启动该程序
                    var path = Process.GetCurrentProcess().MainModule.FileName + " s";
                    //创建服务
                    Process.Start("sc", "create AilandWinService binpath= \"" + path + "\" displayName= AilandWinService start= auto");
                    //启动服务
                    Process.Start("sc", "start AilandWinService");
                    Console.WriteLine("安装成功，服务已启动");
                    CreateService(false);
                    Console.Read();
                    break;
                case 2:
                    //停止服务
                    Process.Start("sc", "stop AilandWinService");
                    //删除服务
                    Process.Start("sc", "delete AilandWinService");
                    Console.WriteLine("卸载成功，服务已停止");
                    CreateService(false);
                    Console.Read();
                    break;
                default:
                    break;
            }
        }
    }
}
