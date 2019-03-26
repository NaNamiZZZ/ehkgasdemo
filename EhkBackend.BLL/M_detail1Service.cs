using EhkBackend.IBLL;
using EhkBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhkBackend.BLL
{
    public class M_detail1Service : BaseService<m_detail1>,IM_detail1Service
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.dbsession.M_detail1Dal;
          //  throw new NotImplementedException();
        }
    }
}
