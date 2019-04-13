using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EhkBackend.Model;
using EhkBackend.IBLL;

namespace EhkBackend.BLL
{
    public class ReportMeterService : BaseService<ReportMeterReading>,IReportMeterService
    {
        public override void SetCurrentDal()
        {
            //throw new NotImplementedException();

            CurrentDal = this.dbsession.ReportMeterDal;
        }
    }
}
