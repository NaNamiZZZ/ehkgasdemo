using EhkBackend.IBLL;
using EhkBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EhkBackend.Model.param;

namespace EhkBackend.BLL
{
    public class acemailService : BaseService<acemail>, IacemailService
    {
        public  IQueryable<acemail> LoadPageEntities(AcemailParam acemailParam)
        {
            var temp = dbsession.acemailDal.GetEntities(a => a.delflag==true );
            if (!string.IsNullOrEmpty(acemailParam.Account))
            {
                temp = temp.Where(a => a.account.Contains(acemailParam.Account)).AsQueryable();
            }
            if (!string.IsNullOrEmpty(acemailParam.Email))
            {
                temp = temp.Where(a => a.email.Contains(acemailParam.Email)).AsQueryable();
            }
            acemailParam.Total = temp.Count();

            return temp
                 .OrderByDescending(a => a.account)
                 .Skip(acemailParam.PageSize * (acemailParam.PageIndex - 1))
                 .Take(acemailParam.PageSize).AsQueryable();

            //throw new NotImplementedException();
        }

        public override void SetCurrentDal()
        {
            CurrentDal = this.dbsession.acemailDal;
          //  throw new NotImplementedException();
        }
    }
}
