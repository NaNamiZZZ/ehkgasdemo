using EhkBackend.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EhkBackend.Model.param;

namespace EhkBackend.IBLL
{
   public interface IacemailService:IBaseService<acemail>
    {
        IQueryable<acemail> LoadPageEntities(Model.param.AcemailParam acemailParam);
    }
}
