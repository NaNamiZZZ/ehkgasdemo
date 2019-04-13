using EhkBackend.IDal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EhkBackend.staticDalFactory
{
    public class StaticDalFactory
    {
        public static string assemblyName = ConfigurationManager.AppSettings["DalAssemblyName"];
        public static IM_bills1Dal GetM_bills1Dal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".M_bills1Dal") as IM_bills1Dal;
        }
        public static IM_detail1Dal GetM_detail1Dal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".M_detail1Dal") as IM_detail1Dal;
        }
        public static IacemailDal GetAcemailDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".acemailDal") as IacemailDal;
        }
        public static IAdminDal GetAdminDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".AdminDal") as IAdminDal;
        }
        public static IMonth1Dal GetMonth1Dal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".Month1Dal") as IMonth1Dal;
        }
        public static IReportMeterDal GetReportMeter()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".ReportMeterDal") as IReportMeterDal;
        }
        //public static IUserAccountDal GetUserAccount()
        //{
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserAccountDal") as IUserAccountDal;

        //}
        //public static IBillDal GetBill()
        //{
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".BillDal") as IBillDal;

        //}
        //public static IM_BILLDal GetM_BILLDal()
        //{
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".M_BILLDal") as IM_BILLDal;
        //}
        //public static IAdminDal GetAdminDal()
        //{
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".AdminDal") as IAdminDal;
        //}
        //public static IacemailDal GetAcemail()
        //{
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".acemailDal") as IacemailDal;
        //}

        //public static Im_bills2Dal GetM_bills2Dal()
        //{
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".m_bills2Dal") as Im_bills2Dal;
        //}
        //public static Im_bills3Dal GetM_bills3Dal()
        //{
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".m_bills3Dal") as Im_bills3Dal;
        //}
        //public static Im_detail1Dal GetM_detail1Dal()
        //{
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".m_detail1Dal") as Im_detail1Dal;
        //}
        //public static Im_detail2Dal GetM_detail2Dal()
        //{
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".m_detail2Dal") as Im_detail2Dal;
        //}
        //public static Im_detail3Dal GetM_detail3Dal()
        //{
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".m_detail3Dal") as Im_detail3Dal;
        //}
        //public static Imonth1Dal GetMonth1Dal()
        //{
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".month1Dal") as Imonth1Dal;
        //}
        //public static Imonth2Dal GetMonth2Dal()
        //{
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".month2Dal") as Imonth2Dal;
        //}
        //public static Imonth3Dal GetMonth3Dal()
        //{
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".month3Dal") as Imonth3Dal;
        //}
        //public static IMRDal GetMRDal()
        //{
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".MRDal") as IMRDal;
        //}
        //public static IuseridDal GetUseridDal()
        //{
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".useridDal") as IuseridDal;
        //}

    }
}
