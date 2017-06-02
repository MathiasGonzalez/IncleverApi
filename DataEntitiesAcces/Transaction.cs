using DataEntitiesAcces.CommonEntities;


using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntitiesAcces
{
    public class Transaction :IDisposable
    {

        private DbContext _db { get; set; }


        public DbContextTransaction NewTransaction
        {

            get
            {

                return _db.Database.BeginTransaction();

            }
        }

        void End() {
            _db.Dispose();
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
            }
        }

        Transaction()
        {
            _db = new db(false);

        }
        ~Transaction() {
            if (_db != null) {
                _db.Dispose();
            }
        }


    }
}
