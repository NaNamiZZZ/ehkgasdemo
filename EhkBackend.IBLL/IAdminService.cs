using EhkBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhkBackend.IBLL
{
   public interface IAdminService:IBaseService<Admins>
    {
        Admins checkLogin(Admins admin_);
    }
}
