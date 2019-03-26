using EhkBackend.IBLL;
using EhkBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhkBackend.BLL
{
    public class M_bills1Service : BaseService<m_bills1>,IM_bills1Service
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.dbsession.M_bills1Dal;
            //throw new NotImplementedException();
        }
    }
}
