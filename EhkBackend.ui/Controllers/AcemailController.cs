using EhkBackend.BLL;
using EhkBackend.common;
using EhkBackend.IBLL;
using EhkBackend.Model;
using EhkBackend.Model.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EhkBackend.ui.Controllers
{
    public class AcemailController : Controller
    {
        IacemailService emailService = new acemailService();
        IM_bills1Service m_bills1Service = new M_bills1Service();
        // GET: Acemail
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AcemailIndex()
        {
            return View();
        }
        public ActionResult GetAcemailLsit()
        {

            int pageIndex = Request["Page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int total = 0;
            string account_ = Request["account"];
            string email_ = Request["email"];
            var queryAcemail = new AcemailParam()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Total = total,
                Account = account_,
                Email = email_
            };

            var data = emailService.LoadPageEntities(queryAcemail);
            // var data = emailService.LoadPageEntities(pageIndex, pageSize, out total, md => true, true, md => md.account);
            var result = new { total = queryAcemail.Total, rows = data.ToList() };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddEmail()
        {
            string account_ = Request["Account"].ToUpper();//使输入Email为大写
            string email_ = Request["Email"];
            var youaccount = emailService.GetEntities(y => y.account.Contains(account_) && y.delflag == true).Count();
            var youemail = emailService.GetEntities(y => y.email.Contains(email_) && y.delflag == true).Count();
            var gai = emailService.GetEntities(y => y.account.Contains(account_)).FirstOrDefault();
            if (youaccount == 1 || youemail == 1)
            {
                gai.account = account_;
                gai.email = email_;
                emailService.update(gai);
                return Content("success_update");
            }

            acemail ac = new acemail();
            if (account_ != string.Empty)
            {
                if (email_ != string.Empty)
                {


                    ac.account = account_;
                    ac.email = email_;
                    ac.delflag = true;
                    emailService.Add(ac);
                    return Content("success");
                }
                else
                {
                    return Content("error_emailnull");
                }
            }
            else
            {
                return Content("error_accountnull");
            }


        }


        public ActionResult delAcemail(string Account_)
        {
            //acemail ac = new acemail();
            List<string> alist = new List<string>();
            if (Account_ != string.Empty)
            {
                alist.Add(Account_);
                emailService.DeleteListLogical(alist);
                return Content("success");
            }
            else {
                return Content("error");
            }
            //return View();
        }


        public ActionResult editEmail()
        {
            return View();
        }


        public JsonResult GetOneEmail(string Account_)
        {

            var ae = emailService.GetEntities(a => a.account == Account_).FirstOrDefault();

            var ac = new
            {
                succcess = true,
                Account = ae.account,
                Email = ae.email
            };

            return Json(ac);
            //  return View();
        }

        public ActionResult ALLSendBillEmail()
        {
            var emailList = emailService.GetEntities(a => a.delflag != false).AsQueryable().ToList();

            if (emailList.Count() != 0)
            {
                foreach (var item in emailList)
                {
                    try
                    {
                        var M_B = m_bills1Service.GetEntities(m => m.account.Equals(item.account)).FirstOrDefault();
                        if (M_B != null)
                        {
                            var acEmail = emailService.GetEntities(A => A.account.Equals(item.account)).FirstOrDefault();
                            //  M_B.DESC1 = "已發送";
                            //m_billService.update(M_B);
                            string BillNo = M_B.billno;
                            var D = M_B.date;
                            var u = M_B.bill;
                            var accountno = M_B.account;
                            //int mon = D.Month;
                            //int year = D.Year;
                            string mailaddress = acEmail.email;
                          //  sendBillEmail(BillNo, D, u, accountno, mailaddress);
                        }
                    }
                    catch (Exception ex)
                    {
                        string exnam = ex.Message;
                    }

                }
                return Content("發送成功");
            }
            else
            {
                return Content("error");
            }

            //return View();
        }

        public ActionResult sendBillEmail(string BillNo, string D, decimal? u, string accountno, string mailaddress)
        {
            string toMailAddress = mailaddress;
            //string toMailAddress = "1041173580@qq.com";
            //  string senderServerIp = "smtp.gmail.com";
            string senderServerIp = "mail.ehkgas.com";
            string fromMailAddress = "bills@ehkgas.com";
            // string fromMailAddress = "infoehkgas@gmail.com";
            string mailUsername = "bills@ehkgas.com";
            // string mailUsername = "infoehkgas@gmail.com";
            string subjectInfo = "Gas Invoice" + D;
            // string mailUsername = "bills@ehkgas.com";
            //string mailUsername = "info@universalship.com.hk";
            // string fromMailAddress = "info@universalship.com.hk";
            string mailPassword = "Weare1127$";
            // string mailPassword = "ehk123456";
            //string mailPassword = "un12ab";
            string mailPort = "366";
            //  string mailPort = "587";
            string bodyInfo = "";
            if (u < 100)
            {
                bodyInfo = "<p>" + "Dear Customer" + "</p>" +
                                    "<span>" + "Please find attached the San Hing (LPG) Co. Ltd. gas invoice for " + D + " usage. If you have any questions, please feel free to contact us by calling 29876738 or simply reply to this Email. " + "</span>"
                                    + "<p>" + "<span>" + "If you’re making online or ATM payment, please kindly transfer the amount to our HSBC account: 511-783888-838 and notify us by Email or whatsapp to us @ 9267 8757." + "</span>" + "</P>" + "</br>"
                                    + "<a href='http://47.75.253.158:8686/m_bills1/JDownloadPdf/" + BillNo + "'>" + "click here download current invoice" + "</a>" + "</br>"
                                    + "<a href='http://47.75.253.158:8686/m_bills1/DownBillBackPdf'>" + "click here for terms and our kindly reminder " + "</a>"
                     //string bodyInfo = "<p>" + "Dear Customer" + "</p>" + "," + "</br>"

                     ;
            }
            else
            {
                bodyInfo = "<p>" + "Dear Customer" + "</p>" +
                                    "<span>" + "Please find attached the San Hing (LPG) Co. Ltd. gas invoice for " + D + " usage. If you have any questions, please feel free to contact us by calling 29876738 or simply reply to this Email. " + "</span>"
                                    + "<p>" + "<span>" + "If you’re making online or ATM payment, please kindly transfer the amount to our HSBC account: 511-783888-838 and notify us by Email or whatsapp to us @ 9267 8757." + "</span>" + "</P>" + "</br>"
                                    + "<a href='http://47.75.253.158:8686/m_bills1/JDownloadPdf/" + BillNo + "'>" + "click here download current invoice" + "</a>" + "</br>"
                                    + "<a href='http://47.75.253.158:8686/m_bills1/DownBillBackPdf'>" + "click here for terms and our kindly reminder " + "</a>" + "</br>"
                                    + "<a href='http://47.75.253.158:8686/m_bills1/paymentQrcode/" + accountno + "'>" + "Link to QR Code for payment" + "</a>" + "</br>";
            }
            Email myEmail = new Email(senderServerIp, toMailAddress, fromMailAddress, subjectInfo, bodyInfo, mailUsername, mailPassword, mailPort, true, false);

            if (myEmail.send())
            {
                return Content("<script>alert('邮件已成功发送!')</script>");
            }
            else
            {
                return Content("<script>alert('邮件发送失败!')</script>");
            }
            // return View();
        }
    }
}