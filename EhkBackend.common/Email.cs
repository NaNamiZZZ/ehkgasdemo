using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EhkBackend.common
{
   public class Email
    {
        private MailMessage mMailMessag;//邮件内容
        private SmtpClient mSmtpClient;//主要处理用smtp方式发送此邮件的配置信息（如：邮件服务器、发送端口号、验证方式等等）
        private int mSenderPort;//发送邮件所用端口号；
        private string mSenderServerHost;//发件箱的服务器地址;
        private string mSenderPassword;//发件箱密码；
        private string mSendUsername;//发件箱用户名；
        private bool mEnableSsl;//是否对邮件内容进行socketc层加密传输
        private bool mEnablePwdAuthentication;//是否对



        public Email(string server, string toMail, string fromMail, string subject, string emailBoby, string username, string password, string port, bool sslEnable, bool pwdCheckEnable)
        {


            try
            {

                mMailMessag = new MailMessage();
                mMailMessag.To.Add(toMail);
                mMailMessag.From = new MailAddress(fromMail);
                mMailMessag.Subject = subject;
                mMailMessag.Body = emailBoby;
                mMailMessag.IsBodyHtml = true;
                mMailMessag.BodyEncoding = System.Text.Encoding.UTF8;
                mMailMessag.Priority = MailPriority.Normal;
                this.mSenderServerHost = server;
                this.mSendUsername = username;
                this.mSenderPassword = password;
                this.mSenderPort = Convert.ToInt32(port);
                this.mEnableSsl = sslEnable;
                this.mEnablePwdAuthentication = pwdCheckEnable;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        //添加附件

        public void AddAttachments(string attachmentsPath)
        {
            try
            {
                string[] path = attachmentsPath.Split(';'); //以什么符号分隔可以自定义
                Attachment data;
                ContentDisposition disposition;
                for (int i = 0; i < path.Length; i++)
                {
                    data = new Attachment(path[i], MediaTypeNames.Application.Octet);
                    disposition = data.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(path[i]);
                    disposition.ModificationDate = File.GetLastWriteTime(path[i]);
                    disposition.ReadDate = File.GetLastWriteTime(path[i]);
                    mMailMessag.Attachments.Add(data);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //邮件发送

        public bool send()
        {
            try
            {
                if (mMailMessag != null)
                {
                    mMailMessag.IsBodyHtml = true;
                    mSmtpClient = new SmtpClient();
                    mSmtpClient.Host = this.mSenderServerHost;
                    mSmtpClient.Port = this.mSenderPort;
                    mSmtpClient.UseDefaultCredentials = false;
                    mSmtpClient.EnableSsl = this.mEnableSsl;
                    if (this.mEnablePwdAuthentication)
                    {

                        System.Net.NetworkCredential nc = new System.Net.NetworkCredential(this.mSendUsername, this.mSenderPassword);
                        mSmtpClient.Credentials = nc.GetCredential(mSmtpClient.Host, mSmtpClient.Port, "NTLM");
                    }
                    else
                    {

                        mSmtpClient.Credentials = new System.Net.NetworkCredential(this.mSendUsername, this.mSenderPassword);
                    }
                    mSmtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    ServicePointManager.ServerCertificateValidationCallback = delegate (Object obj, System.Security.Cryptography.X509Certificates.X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };// 解決根据验证过程,远程证书无效 。
                    mSmtpClient.Send(mMailMessag);
                }

            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
