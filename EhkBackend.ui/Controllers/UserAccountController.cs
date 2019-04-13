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
    public class UserAccountController : Controller
    {
        // GET: UserAccount
        public ActionResult UserAccountIndex()
        {
            return View();
        }

        public ActionResult ExImport(HttpPostedFileBase file)
        {
            var fileName = file.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "FileExecel"));
            string path = Path.Combine(filePath, fileName);
            file.SaveAs(path);

            DataTable excelTable = new DataTable();

            excelTable = imExpro.GetExcelDataTable(path);

            DataTable dtable = new DataTable();
            dtable.Columns.Add("account");
            dtable.Columns.Add("idpassno");
            


            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                DataRow dr = excelTable.Rows[i];
                DataRow dr_ = dtable.NewRow();
                dr_["account"] = dr["users_id"];
                dr_["idpassno"] = dr["users_pwd"];
               
                dtable.Rows.Add(dr_);

            }

            string constr = System.Configuration.ConfigurationManager.AppSettings["EFeesEntities"];
            using (TransactionScope tran = new TransactionScope())
            {
                string sql = "delete  from m_bills1";//导入 新的mb1
                SqlHelp.ExecuteNonQuery(constr, sql);
                DataTableSql.SqlBulkCopyByDatatable(constr, "m_bills1", dtable);

                tran.Complete();
            }
            return Content(fileName.ToString());

        }
    }
}