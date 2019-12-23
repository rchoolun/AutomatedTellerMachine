using NVisionIT.AutomatedTellerMachine.Service.Models;
using System.Data.Entity;

namespace NVisionIT.AutomatedTellerMachine.Service.Data
{
    public interface IDbContext
    {
        IDbSet<UserModel> Users { get; set; }

        IDbSet<AccountModel> Accounts { get; set; }

        int SaveChanges();
    }

    /// <summary>
    /// Is is a fake DB context. There is no connection to the DB
    /// </summary>
    public class DbContext : IDbContext
    {
        public DbContext()
        {
            Users = MockData.AllUsers();
            Accounts = MockData.GetAccounts();
        }

        public IDbSet<UserModel> Users { get; set; }

        public IDbSet<AccountModel> Accounts { get; set; }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
