using EhkBackend.BLL;
using EhkBackend.IBLL;
using EhkBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhkBackend.BLL
{
    public class AdminService : BaseService<Admins>,IAdminService
    {
        public Admins checkLogin(Admins admin_)
        {
            return dbsession.AdminDal.GetEntities(a => a.admin_id.Equals(admin_.admin_id) && a.admin_pwd.Equals(admin_.admin_pwd)).FirstOrDefault();
          //  throw new NotImplementedException();
        }

        public override void SetCurrentDal()
        {
            CurrentDal = this.dbsession.AdminDal;
            //throw new NotImplementedException();
        }
    }
}
