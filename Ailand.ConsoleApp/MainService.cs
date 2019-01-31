
using System;
using System.CodeDom.Compiler;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace Ailand.ConsoleApp
{
    partial class MainService : ServiceBase
    {

        public MainService()
        {
            InitializeComponent();
        }



        protected override void OnStart(string[] args)
        {
            //初始化计时器    //60秒 执行一次
            System.IO.File.AppendAllText("D:\\log.txt", "服务已启动……----------" + DateTime.Now.ToString());
            Timer timer = new Timer(60 * 1000);
            //绑定需定时触发的事件
            timer.Elapsed += new ElapsedEventHandler((sender, e) =>
            {
                System.IO.File.AppendAllText("D:\\log.txt", "服务已开始……----------" + DateTime.Now.ToString());
            });
            //重复执行
            timer.AutoReset = true;
            //执行事件
            timer.Enabled = true;
            //开始执行
            timer.Start();



            //创建C#编译器实例   
            //CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");
            ////编译器的传入参数   
            //CompilerParameters cp = new CompilerParameters();
            //StringBuilder sbCode = new StringBuilder();
            //cp.ReferencedAssemblies.Add("System.dll"); //添加程序集 system.dll 的引用
            //cp.ReferencedAssemblies.Add("System.data.dll"); //添加程序集 system.data.dll 的引用
            //cp.ReferencedAssemblies.Add("System.xml.dll"); //添加程序集 system.xml.dll 的引用
            //cp.ReferencedAssemblies.Add("System.core.dll"); //添加程序集 system.core.dll 的引用
            //cp.GenerateExecutable = false;//不生成可执行文件   
            //cp.GenerateInMemory = true;                             //在内存中运行   
            ////需创建的代码
            ////添加using指令
            //sbCode.Append("using System;");
            //sbCode.Append("using System.ServiceProcess;");
            //sbCode.Append("using System.Timers;");
            //sbCode.Append(" namespace A.TimerT {");//构建命名空间
            //sbCode.Append(" public class  _Evaluator { ");//构建类
            //sbCode.Append(" public  _Evaluator() { ");//构建构造方法
            //sbCode.Append("Timer timer = new Timer(5* 1000);");
            //sbCode.Append("timer.Elapsed += new ElapsedEventHandler(StartEvent);");
            //sbCode.Append("timer.AutoReset = true;");
            //sbCode.Append("timer.Enabled = true;");
            //sbCode.Append("timer.Start();");
            //sbCode.Append("}");//构造方法结束
            ////定义执行事件
            //sbCode.Append("public void StartEvent(object sender, ElapsedEventArgs e){");
            //sbCode.Append("System.IO.File.AppendAllText(\"D:\\log.txt\", \"服务已开始……----------\" + DateTime.Now.ToString());");
            //sbCode.Append("}");//执行事件结束
            //sbCode.Append("}}");
            //CompilerResults cr = provider.CompileAssemblyFromSource(cp, sbCode.ToString()); //得到编译器实例的返回结果   
            //if (cr.Errors.HasErrors)
            //{
            //    foreach (CompilerError err in cr.Errors)
            //    {
            //        System.IO.File.AppendAllText("D:\\log.txt", err.ErrorText);
            //    }

            //}
        }


        public void StartEvent(object sender, ElapsedEventArgs e)
        {
            System.IO.File.AppendAllText("D:\\log.txt", "服务已开始……----------" + DateTime.Now.ToString());
            //try
            //{
            //    DataTable data = winService.StartServiceEvent();
            //    if (data == null) return;
            //    if (data.Rows.Count == 0) return;
            //    List<string> list = new List<string>();
            //    //循环异常信息
            //    foreach (DataRow row in data.Rows)
            //    {
            //        if (row["Result"].ToString() == "false")
            //        {
            //            list.Add(row["ModuleName"].ToString() + "&" + row["TryMessage"].ToString());
            //        }
            //    }
            //    if (list.Count > 0)
            //    {
            //        //服务失败时调用
            //        winService.TryServiceEvent(list.ToArray());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //服务异常时调用
            //    winService.TryServiceEvent(new string[] { $"服务异常&时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}信息：{ex.Message}" });
            //}

        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            //System.IO.File.AppendAllText("D:\\log.txt", "服务已停止……" + DateTime.Now.ToString());
            //winService.EndServiceEvent(string.Empty);
        }
    }
}
