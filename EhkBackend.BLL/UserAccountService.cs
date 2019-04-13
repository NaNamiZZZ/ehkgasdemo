using EhkBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhkBackend.BLL
{
    public class UserAccountService : BaseService<UserAccount>
    {
        public override void SetCurrentDal()
        {
            throw new NotImplementedException();
        }
    }
}
