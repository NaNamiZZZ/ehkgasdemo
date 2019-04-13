using EhkBackend.Dal;
using EhkBackend.IDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhkBackend.staticDalFactory
{
    public class DbSession : IDbsession
    {
        public IacemailDal acemailDal
        {
            get
            {
                return staticDalFactory.StaticDalFactory.GetAcemailDal();
                //throw new NotImplementedException();
            }
        }

        public IM_bills1Dal M_bills1Dal
        {
            get { return staticDalFactory.StaticDalFactory.GetM_bills1Dal(); }
        }

        public IM_detail1Dal M_detail1Dal
        {
            get
            {
                return staticDalFactory.StaticDalFactory.GetM_detail1Dal();
            }
        }

        public IAdminDal AdminDal
        {
            get
            {
                return staticDalFactory.StaticDalFactory.GetAdminDal();
            }
        }
        public IMonth1Dal Month1Dal
        {
            get
            {
                return staticDalFactory.StaticDalFactory.GetMonth1Dal();
            }
        }
        public IReportMeterDal ReportMeterDal
        {
            get {
                return staticDalFactory.StaticDalFactory.GetReportMeter();
            }
        }



        //public IUserAccountDal UserAccountDal
        //{
        //    get { return staticDalFactory.StaticDalFactory.GetUserAccount(); }
        //}
        //public IBillDal BillDal
        //{
        //    get { return staticDalFactory.StaticDalFactory.GetBill(); }
        //}
        //public IM_BILLDal M_BILLDal
        //{
        //    get { return staticDalFactory.StaticDalFactory.GetM_BILLDal(); }
        //}
        //public IAdminDal AdminDal
        //{
        //    get { return staticDalFactory.StaticDalFactory.GetAdminDal(); }
        //}
        //public IacemailDal acemailDal
        //{
        //    get { return staticDalFactory.StaticDalFactory.GetAcemail(); }
        //}
        //public Im_bills1Dal m_bills1Dal
        //{
        //    get { return staticDalFactory.StaticDalFactory.GetM_bills1Dal(); }
        //}
        //public Im_bills2Dal m_bills2Dal
        //{
        //    get { return staticDalFactory.StaticDalFactory.GetM_bills2Dal(); }
        //}
        //public Im_bills3Dal m_bills3Dal
        //{
        //    get { return staticDalFactory.StaticDalFactory.GetM_bills3Dal(); }
        //}
        //public Im_detail1Dal m_detail1Dal
        //{
        //    get { return staticDalFactory.StaticDalFactory.GetM_detail1Dal(); }
        //}
        //public Im_detail2Dal m_detail2Dal
        //{
        //    get { return staticDalFactory.StaticDalFactory.GetM_detail2Dal(); }
        //}
        //public Im_detail3Dal m_detail3Dal
        //{
        //    get { return staticDalFactory.StaticDalFactory.GetM_detail3Dal(); }
        //}
        //public Imonth1Dal month1Dal
        //{
        //    get { return staticDalFactory.StaticDalFactory.GetMonth1Dal(); }
        //}
        //public Imonth2Dal month2Dal
        //{
        //    get { return staticDalFactory.StaticDalFactory.GetMonth2Dal(); }
        //}
        //public Imonth3Dal month3Dal
        //{
        //    get { return staticDalFactory.StaticDalFactory.GetMonth3Dal(); }
        //}
        //public IuseridDal useridDal
        //{
        //    get { return staticDalFactory.StaticDalFactory.GetUseridDal(); }
        //}
        //public IMRDal MRDal
        //{
        //    get { return staticDalFactory.StaticDalFactory.GetMRDal(); }
        //}
        public int SaveChanges()
        {
            try
            {
                return DbContextFactory.GetCurrentDbContext().SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

    }
}
