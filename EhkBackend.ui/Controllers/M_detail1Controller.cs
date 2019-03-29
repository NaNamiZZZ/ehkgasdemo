using EhkBackend.BLL;
using EhkBackend.IBLL;
using EhkBackend.ui.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace EhkBackend.ui.Controllers
{
    public class M_detail1Controller : Controller
    {

        IM_detail1Service mdService = new M_detail1Service();
        // GET: M_detail1
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult mdetail1Index()
        {
            return View();
        }
        public ActionResult mdetailList()
        {
            int pageIndex = Request["Page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int total = 0;

            var data = mdService.LoadPageEntities(pageIndex, pageSize, out total, md => true, true, md => md.account);
            var result = new { total = total, rows = data.ToList() };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult importExDetail(HttpPostedFileBase file)
        {
            var fileName = file.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "FileExecel"));
            string path = Path.Combine(filePath, fileName);
            file.SaveAs(path);

            DataTable excelTable = new DataTable();

            excelTable = imExpro.GetExcelDataTable(path);

            DataTable dtable = new DataTable();
            dtable.Columns.Add("account");
            dtable.Columns.Add("billno");
            dtable.Columns.Add("actcode1");
            dtable.Columns.Add("chargeamt1");
            dtable.Columns.Add("actcode2");
            dtable.Columns.Add("chargeamt2");
            dtable.Columns.Add("actcode3");
            dtable.Columns.Add("chargeamt3");
            dtable.Columns.Add("actcode4");
            dtable.Columns.Add("chargeamt4");
            dtable.Columns.Add("actcode5");
            dtable.Columns.Add("chargeamt5");
            dtable.Columns.Add("actcode6");
            dtable.Columns.Add("chargeamt6");
            dtable.Columns.Add("actcode7");
            dtable.Columns.Add("chargeamt7");
            dtable.Columns.Add("actcode8");
            dtable.Columns.Add("chargeamt8");
            dtable.Columns.Add("actcode9");
            dtable.Columns.Add("chargeamt9");


            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                DataRow dr = excelTable.Rows[i];
                DataRow dr_ = dtable.NewRow();
                dr_["account"] = dr["account"];
                dr_["billno"] = dr["billno"];
                dr_["actcode1"] = dr["actcode1"];
                dr_["chargeamt1"] = dr["chargeamt1"];
                dr_["actcode2"] = dr["actcode2"];
                dr_["chargeamt2"] = dr["chargeamt2"];
                dr_["actcode3"] = dr["actcode3"];
                dr_["chargeamt3"] = dr["chargeamt3"];
                dr_["actcode4"] = dr["actcode4"];
                dr_["chargeamt4"] = dr["chargeamt4"];
                dr_["actcode5"] = dr["actcode5"];
                dr_["chargeamt5"] = dr["chargeamt5"];
                dr_["actcode6"] = dr["actcode6"];
                dr_["chargeamt6"] = dr["chargeamt6"];
                dr_["actcode7"] = dr["actcode7"];
                dr_["chargeamt7"] = dr["chargeamt7"];
                dr_["actcode8"] = dr["actcode8"];
                dr_["chargeamt8"] = dr["chargeamt8"];
                dr_["actcode9"] = dr["actcode9"];
                dr_["chargeamt9"] = dr["chargeamt9"];
                dtable.Rows.Add(dr_);

            }

            string constr = System.Configuration.ConfigurationManager.AppSettings["EFeesEntities"];
            using (TransactionScope tran = new TransactionScope())
            {
                string sql = "delete  from m_detail1";//导入 新的mdetail1
                SqlHelp.ExecuteNonQuery(constr, sql);
                DataTableSql.SqlBulkCopyByDatatable(constr, "m_detail1", dtable);

                tran.Complete();
            }
            return Content(fileName.ToString());
        }
    }
}