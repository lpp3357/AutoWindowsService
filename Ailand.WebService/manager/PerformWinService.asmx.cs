using Ailand.WebService.wsclass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Ailand.WebService.manager
{
    /// <summary>
    /// PerformWinService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class PerformWinService : System.Web.Services.WebService
    {

        ///主服务1分钟执行一次


        /// <summary>
        /// 服务开始执行
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "服务开始执行")]
        public DataTable StartServiceEvent()
        {
            DataTable data = new PerformClass().CreateResultTable();
            //更新门店旧料库存
            DataRow dr = new StockOldDoorClass().StockOldDoor_UpDateStock(data);
            if (dr != null)
            {
                data.Rows.Add(dr.ItemArray);
            }
            return data;
        }


        /// <summary>
        /// 服务异常执行
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "服务异常执行")]
        public void TryServiceEvent(string[] resultArr)
        {

        }

        /// <summary>
        /// 服务终止执行
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "服务终止执行")]
        public void EndServiceEvent(string message = "")
        {

        }
    }
}
