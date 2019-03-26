using EhkBackend.IDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace EhkBackend.staticDalFactory
{
    public class DbSessionFactory
    {
        public static IDbsession GetCurrentDbSession()
        {
            IDbsession db = CallContext.GetData("DbSession") as DbSession;
            if (db == null)
            {
                db = new DbSession();
                CallContext.SetData("DbSession", db);
            }
            return db;
        }

    }
}
