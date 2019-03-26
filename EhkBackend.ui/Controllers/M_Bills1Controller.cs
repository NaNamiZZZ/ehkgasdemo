using EhkBackend.BLL;
using EhkBackend.common;
using EhkBackend.IBLL;
using EhkBackend.ui.Common;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace EhkBackend.ui.Controllers
{
    public class M_Bills1Controller : Controller
    {

        IM_bills1Service mb1Service = new M_bills1Service();
        // GET: M_Bills1
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult mb1index()
        {
            return View();
        }

        public ActionResult mb1List()
        {
            int pageIndex = Request["Page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int total = 0;

            var data = mb1Service.LoadPageEntities(pageIndex, pageSize, out total, md => true, true, md => md.account);
            var result = new { total = total, rows = data.ToList() };
            return Json(result, JsonRequestBehavior.AllowGet);

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
            dtable.Columns.Add("billno");
            dtable.Columns.Add("prev");
            dtable.Columns.Add("pres");
            dtable.Columns.Add("unit");
            dtable.Columns.Add("type");
            dtable.Columns.Add("bf");
            dtable.Columns.Add("odd");
            dtable.Columns.Add("chg");
            dtable.Columns.Add("misc");
            dtable.Columns.Add("bill");
            dtable.Columns.Add("auto");
            dtable.Columns.Add("descs");
            dtable.Columns.Add("name");
            dtable.Columns.Add("address1");
            dtable.Columns.Add("address2");
            dtable.Columns.Add("address3");
            dtable.Columns.Add("date");


            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                DataRow dr = excelTable.Rows[i];
                DataRow dr_ = dtable.NewRow();
                dr_["account"] = dr["account"];
                dr_["billno"] = dr["billno"];
                dr_["prev"] = dr["prev"];
                dr_["pres"] = dr["pres"];
                dr_["unit"] = dr["unit"];
                dr_["type"] = dr["type"];
                dr_["bf"] = dr["bf"];
                dr_["odd"] = dr["odd"];
                dr_["chg"] = dr["chg"];
                dr_["misc"] = dr["misc"];
                dr_["bill"] = dr["bill"];
                dr_["auto"] = dr["auto"];
                dr_["descs"] = dr["descs"];
                dr_["name"] = dr["name"];
                dr_["address1"] = dr["address1"];
                dr_["address2"] = dr["address2"];
                dr_["address3"] = dr["address3"];
                dr_["date"] = dr["date"];
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



        public ActionResult exceportMbills()            
        {
            var mb1 = mb1Service.GetEntities(b => true).AsQueryable().ToList();

            List<string> checkesum1 = new List<string>();
            foreach (var item in mb1)
            {
                string b = item.account.ToString().Substring(item.account.ToString().Length - 3, 3);
                string straccount = b;
                StringBuilder sb = new StringBuilder();
                sb.Append(straccount);
                sb.Append(item.billno.ToString());
                // sb.ToString();
                string Ocode = CheckA.checksum(sb.ToString());
                checkesum1.Add("62210" + sb.ToString() + Ocode);
            }
            HSSFWorkbook book = new HSSFWorkbook();

            ISheet sheet1 = book.CreateSheet("sheet1");
            IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("account");
            row1.CreateCell(2).SetCellValue("billno");
            row1.CreateCell(3).SetCellValue("prev");
            row1.CreateCell(4).SetCellValue("pres");
            row1.CreateCell(5).SetCellValue("unit");
            row1.CreateCell(6).SetCellValue("type");
            row1.CreateCell(7).SetCellValue("bf");
            row1.CreateCell(8).SetCellValue("odd");
            row1.CreateCell(9).SetCellValue("chg");
            row1.CreateCell(10).SetCellValue("misc");
            row1.CreateCell(11).SetCellValue("bill");
            row1.CreateCell(12).SetCellValue("auto");
            row1.CreateCell(13).SetCellValue("descs");
            row1.CreateCell(14).SetCellValue("name");
            row1.CreateCell(15).SetCellValue("address1");
            row1.CreateCell(16).SetCellValue("address2");
            row1.CreateCell(17).SetCellValue("address3");
            row1.CreateCell(18).SetCellValue("date");
            
            row1.CreateCell(1).SetCellValue("paycode");
            for (int i = 0; i < mb1.Count; i++)
            {
                
                IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(mb1[i].account.ToString());
                rowtemp.CreateCell(1).SetCellValue(checkesum1[i].ToString());
                rowtemp.CreateCell(2).SetCellValue(mb1[i].billno.ToString());
                rowtemp.CreateCell(3).SetCellValue(mb1[i].prev.ToString());
                rowtemp.CreateCell(4).SetCellValue(mb1[i].pres.ToString());
                rowtemp.CreateCell(5).SetCellValue(mb1[i].unit.ToString());
                rowtemp.CreateCell(6).SetCellValue(mb1[i].type.ToString());
                rowtemp.CreateCell(7).SetCellValue(mb1[i].bf.ToString());
                rowtemp.CreateCell(8).SetCellValue(mb1[i].odd.ToString());
                rowtemp.CreateCell(9).SetCellValue(mb1[i].chg.ToString());
                rowtemp.CreateCell(10).SetCellValue(mb1[i].misc.ToString());
                rowtemp.CreateCell(11).SetCellValue(mb1[i].bill.ToString());
                rowtemp.CreateCell(12).SetCellValue(mb1[i].auto.ToString());
                rowtemp.CreateCell(13).SetCellValue(mb1[i].descs.ToString());
                rowtemp.CreateCell(14).SetCellValue(mb1[i].name.ToString());
                rowtemp.CreateCell(15).SetCellValue(mb1[i].address1.ToString());
                rowtemp.CreateCell(16).SetCellValue(mb1[i].address2.ToString());
                rowtemp.CreateCell(17).SetCellValue(mb1[i].address3.ToString());
                rowtemp.CreateCell(18).SetCellValue(mb1[i].date.ToString());


            }
            MemoryStream ms = new MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "code.xls");
        
        //  return View();
    }
        public ActionResult testcheck()
        {

            return View();
        }
        [HttpPost]
        public ActionResult testcheck1(string data)
        {

            StringBuilder sb = new StringBuilder(data);

            sb.Append(CheckA.checksum(data));

            ViewData["Qrcode"] = sb.ToString();

            return Content("62210" + sb.ToString());
        }
    }
}