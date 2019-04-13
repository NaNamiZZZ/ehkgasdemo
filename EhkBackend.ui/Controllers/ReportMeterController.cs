using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EhkBackend.IBLL;
using EhkBackend.BLL;
using System.Data.Linq.SqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace EhkBackend.ui.Controllers
{
    public class ReportMeterController : Controller
    {
        IReportMeterService reportService = new ReportMeterService();
        // GET: ReportMeter
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReportIndex()
        {

            return View();
        }

        public ActionResult GetMonthReport()
        {

            var m1 = reportService.GetEntities(b => b.ReadDate.Month == DateTime.Now.Month).AsQueryable().ToList();
            HSSFWorkbook book = new HSSFWorkbook();

            ISheet sheet1 = book.CreateSheet("sheet1");
            IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("account");
            row1.CreateCell(2).SetCellValue("meter");
            row1.CreateCell(3).SetCellValue("ReadDate");
            row1.CreateCell(4).SetCellValue("contact");
           

           // row1.CreateCell(1).SetCellValue("paycode");
            for (int i = 0; i < m1.Count; i++)
            {

                IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(m1[i].account.ToString());
                rowtemp.CreateCell(1).SetCellValue(m1[i].meter.ToString());
                rowtemp.CreateCell(2).SetCellValue(m1[i].ReadDate.ToShortDateString());
                rowtemp.CreateCell(3).SetCellValue(m1[i].email.ToString());
                


            }
            MemoryStream ms = new MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "本月Report.xls");
        //    return View();
        }
    }
}