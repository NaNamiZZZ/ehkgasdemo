using EhkBackend.IBLL;
using EhkBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhkBackend.BLL
{
    public class Month1Service : BaseService<month1>,IMonth1Service
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.dbsession.Month1Dal;
            //throw new NotImplementedException();
        }
    }
}
