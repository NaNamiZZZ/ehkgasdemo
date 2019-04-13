using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhkBackend.IDal
{
    public interface IDbsession
    {

        IM_bills1Dal M_bills1Dal { get; }

        IacemailDal acemailDal { get; }

        IM_detail1Dal M_detail1Dal { get; }

        IAdminDal AdminDal { get; }
        IMonth1Dal Month1Dal { get; }
        IReportMeterDal ReportMeterDal { get; }
        int SaveChanges();
    }
}
