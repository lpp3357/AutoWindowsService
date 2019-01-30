using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Ailand.WebService.wsclass
{
    public class StockOldDoorClass
    {
        private DataClass dataClass = new DataClass();

        /// <summary>
        /// 计数器
        /// </summary>
        private static int Count = 0;

        /// <summary>
        /// 计数器最大值  10分钟执行一次
        /// </summary>
        private static int MaxCount = 10 * ServiceInfoClass.TimeInterval;

        /// <summary>
        /// 更新门店旧料库存
        /// </summary>
        /// <returns></returns>
        public DataRow StockOldDoor_UpDateStock(DataTable data)
        {

            Count += ServiceInfoClass.TimeInterval;
            //该逻辑10分钟执行一次
            if (Count != MaxCount) return null;
            //每天23时至次日7时不执行
            if (DateTime.Now.Hour >= 23 || DateTime.Now.Hour <= 7) return null;
            //计数器清零
            Count = 0;
            bool result = false;
            string TryMessage = "SUCCESS";
            //更新对应的出入库记录
            try
            {
                string sql = @"UPDATE TOP(10) menu_otherrecord SET classtype=(CASE WHEN a.salestyle=16 OR a.salestyle=151 THEN (SELECT TOP 1 id FROM dbo.frbillclass  WHERE parentId=1 AND  classname='以旧换新') WHEN a.salestyle=10 THEN (SELECT TOP 1 id FROM dbo.frbillclass  WHERE parentId=1 AND  classname='单独回购') WHEN a.salestyle=13 THEN (SELECT TOP 1 id FROM dbo.frbillclass WHERE parentId=6 AND classname='退旧料' AND statesid=1) END) FROM dbo.detailsales a,menu_otherrecord b WHERE a.salesno=b.otheNO AND a.barcode=b.barcode AND b.id IN( SELECT TOP 10 id FROM dbo.menu_otherrecord ORDER BY id DESC);";
                //根据旧料出入库记录更新旧料库存   isstock=1 代表已废弃的库存数据
                sql += @"UPDATE dbo.stockolddoor SET tgold=b.gold FROM dbo.stockolddoor a,(SELECT SUM(tab.gold)gold,oldpricetype,nowagenceid FROM (SELECT -SUM(gold)gold,oldpricetype,nowagenceid FROM dbo.menu_otherrecord WHERE oldpricetype IN(SELECT id FROM dbo.frprice  WHERE  productid IN(1,4,5,8,21) AND statesid=1) AND classtype IN(SELECT id FROM dbo.frbillclass WHERE classname IN('采购出库','旧转新','维修补金','退旧料'))  AND ISNULL(isstock,0)<>1 GROUP BY oldpricetype,nowagenceid
               UNION 
               SELECT SUM(gold)gold,oldpricetype,nowagenceid FROM dbo.menu_otherrecord WHERE oldpricetype IN(SELECT id FROM dbo.frprice  WHERE  productid IN(1,4,5,8,21) AND statesid=1) AND  classtype     NOT    IN      (SELECT id FROM dbo.frbillclass WHERE classname IN('采购出库','旧转新','维修补金','退旧料'))   AND ISNULL(isstock,0)<>1
               GROUP BY oldpricetype,nowagenceid)tab GROUP BY oldpricetype,nowagenceid)b WHERE a.priclassid=b.oldpricetype AND a.agenctstr=b.nowagenceid AND a.tgold<>b.gold;";
                //更新指定门店
                sql += @" UPDATE dbo.stockolddoor SET tgold=b.gold FROM dbo.stockolddoor a,(SELECT SUM(tab.gold)gold,oldpricetype,nowagenceid FROM (SELECT -SUM(gold)gold,oldpricetype,nowagenceid FROM dbo.menu_otherrecord WHERE oldpricetype IN(SELECT id FROM dbo.frprice  WHERE  productid IN(1,4,5,8,21) AND statesid=1) AND  nowagenceid IN(SELECT agenceid FROM dbo.fragence WHERE agecename IN('四川成都锦江春熙店','四川成都崇州1店','四川乐山夹江2店','陕西西安户县店')) AND CONVERT(DATE,maketime)>='2018-12-12' AND classtype IN(SELECT id FROM dbo.frbillclass WHERE classname IN('采购出库','旧转新','维修补金','退旧料'))  GROUP BY oldpricetype,nowagenceid
               UNION 
               SELECT SUM(gold)gold,oldpricetype,nowagenceid FROM dbo.menu_otherrecord WHERE oldpricetype IN(SELECT id FROM dbo.frprice  WHERE  productid IN(1,4,5,8,21) AND statesid=1) AND     nowagenceid  IN   (SELECT agenceid FROM dbo.fragence WHERE agecename IN('四川成都锦江春熙店','四川成都崇州1店','四川乐山夹江2店','陕西西安户县店')) AND CONVERT(DATE,maketime)   >='2018-12-12' AND    classtype NOT   IN (SELECT id FROM dbo.frbillclass WHERE classname IN('采购出库','旧转新','维修补金','退旧料'))   
               GROUP BY oldpricetype,nowagenceid)tab GROUP BY oldpricetype,nowagenceid)b WHERE a.priclassid=b.oldpricetype AND a.agenctstr=b.nowagenceid AND a.tgold<>b.gold;";
                result = dataClass.ExeNonQuerys(sql);
                TryMessage = result ? TryMessage : "FAIL";
            }
            catch (Exception ex)
            {
                result = false;
                TryMessage = ex.Message + "\r" + ex.StackTrace;
            }
            DataRow dr = data.NewRow();
            dr["ModuleName"] = "更新门店旧料库存";
            dr["Result"] = result;
            dr["TryMessage"] = TryMessage;
            return dr;
        }


    }
}