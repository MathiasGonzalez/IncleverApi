using DataEntitiesAcces.CommonEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntitiesAcces
{

    public static class DbContextUtils<TContext>
where TContext : DbContext
    {
        static object _InitializeLock = new object();
        static bool _InitializeLoaded = false;

        /// <summary>
        /// Method to allow running a DatabaseInitializer exactly once
        /// </summary>   
        /// <param name="initializer">A Database Initializer to run</param>
        public static void SetInitializer(IDatabaseInitializer<TContext> initializer = null)

        {
            if (_InitializeLoaded)
                return;

            // watch race condition
            lock (_InitializeLock)
            {
                // are we sure?
                if (_InitializeLoaded)
                    return;

                _InitializeLoaded = true;

                // force Initializer to load only once
                System.Data.Entity.Database.SetInitializer<TContext>(initializer);
            }
        }
    }
    public class db : DbContext
    {

        // public DbSet<pr> Clientes { get; set; }

        // public DbSet<pr> Telefonos { get; set; }

        //public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Usuarios { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }
        public DbSet<Snippet> Snippets { get; set; }


        private static string strConCnf
        {
            get
            {

#if (DEBUG)
                return @"Data Source=DESKTOP-8RQ2CBK\LOCALHOST; Database = inclever; User Id = sa;
                Password = tito90; ";
                //Data Source = DESKTOP - JGOVRF2\LOCAL; Integrated Security = True; Connect Timeout = 15; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False
#else
                return    @"Data Source=localhost;Initial Catalog=inclever;Integrated Security=false;User ID=inclever;Password=tito90";
                //Server=tcp:inclever.database.windows.net,1433;Initial Catalog=inclever;Persist Security Info=False;User ID=inclever;Password=0x024EF0;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;

#endif
            }
        }

        public db() : base(strConCnf)
        {
            DbContextUtils<db>.SetInitializer(new MigrateDatabaseToLatestVersion<db, DataEntitiesAcces.Migrations.Configuration>());

        }
        public db(string sc) : base(sc) {
            DbContextUtils<db>.SetInitializer(new MigrateDatabaseToLatestVersion<db, DataEntitiesAcces.Migrations.Configuration>());
        }
        public db(bool initialize) : base(strConCnf)
        {
            if (initialize)
            {
                DbContextUtils<db>.SetInitializer(new MigrateDatabaseToLatestVersion<db, DataEntitiesAcces.Migrations.Configuration>());
            }
        }
        public db(string sc,bool initialize) : base(sc)
        {
            if (initialize)
            {
                DbContextUtils<db>.SetInitializer(new MigrateDatabaseToLatestVersion<db, DataEntitiesAcces.Migrations.Configuration>());
            }
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //  Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogContext, Configuration>());

            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

    }

}
