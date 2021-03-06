﻿using EhkBackend.BLL;
using EhkBackend.IBLL;
using EhkBackend.Model;
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
    public class Month1Controller : Controller
    {

        IMonth1Service monthService = new Month1Service();
        // GET: Month1
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Month1Index()
        {

            return View();
        }
        public ActionResult GetMonth1List()
        {

            int pageIndex = Request["Page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int total = 0;

            var data = monthService.LoadPageEntities(pageIndex, pageSize, out total, md => true, true, md => md.bildate);
            var result = new { total = total, rows = data.ToList() };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExImport(HttpPostedFileBase file)
        {
            //month1 导入数据表
            var fileName = file.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "FileExecel"));
            string path = Path.Combine(filePath, fileName);
            file.SaveAs(path);

            DataTable excelTable = new DataTable();

            excelTable = imExpro.GetExcelDataTable(path);

            DataTable dtable = new DataTable();
            dtable.Columns.Add("billdate");
            dtable.Columns.Add("duedate");
            dtable.Columns.Add("rate");
            dtable.Columns.Add("remarks1");
            dtable.Columns.Add("remarks2");
           


            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                DataRow dr = excelTable.Rows[i];
                DataRow dr_ = dtable.NewRow();
                dr_["bildate"] = dr["bildate"];
                dr_["duedate"] = dr["Duedate"];
                dr_["rate"] = dr["Rate"];
                dr_["remarks1"] = dr["Remarks1"];
                dr_["remarks2"] = dr["Remarks2"];
                
                dtable.Rows.Add(dr_);

            }

            string constr = System.Configuration.ConfigurationManager.AppSettings["EFeesEntities"];
            using (TransactionScope tran = new TransactionScope())
            {
                string sql = "delete  from month1";//导入 新的mb1
                SqlHelp.ExecuteNonQuery(constr, sql);
                DataTableSql.SqlBulkCopyByDatatable(constr, "month1", dtable);

                tran.Complete();
            }
            return Content(fileName.ToString());

        }


        public ActionResult GetOneMonth(int mid) {
            var b = monthService.GetEntities(bi => bi.mid.Equals(mid)).FirstOrDefault();

            var bc = new
            {
                success = true,
                bildatae = b.bildate,
                Duedate = b.Duedate,
                Rate = b.Rate,
                Remarks1 = b.Remarks1,
                Remarks2 = b.Remarks2

            };
            return Json(bc);
        }


        public ActionResult AddMonth()
        {
            //int mid = Convert.ToInt32(Request["mid"]);
            var mon = monthService.GetEntities(b => true).FirstOrDefault();
            DateTime bildate = Convert.ToDateTime(Request["bildate"]);
            DateTime Duedate = Convert.ToDateTime(Request["Duedate"]);
             string Rate= Request["Rate"];
            string Remarks1= Request["Remarks1"];
            string Remarks2= Request["Remarks2"];


            if (mon != null)
            {
                month1 m = new month1();
                m.Rate = Rate;
                m.Remarks1 = Remarks1;
                m.Remarks2 = Remarks2;
                m.Duedate = Duedate;
                m.bildate = bildate;
                monthService.update(m);
                return Content("success");
            }
            else {

                return Content("monNo_exit");
            }
            return View();
        }
    }
}