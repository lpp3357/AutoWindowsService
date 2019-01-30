using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Ailand.WebService.wsclass
{
    /// <summary>
    /// 执行信息类
    /// </summary>
    public class PerformClass
    {

        public DataTable CreateResultTable()
        {
            DataTable data = new DataTable();
            //模块名称
            data.Columns.Add("ModuleName", typeof(string));
            //执行结果
            data.Columns.Add("Result", typeof(bool));
            //异常信息
            data.Columns.Add("TryMessage", typeof(string));
            return data;
        }
    }
}