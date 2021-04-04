using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Thitipong.Upload.Model;
using Thitipong.Upload.Service.Data;

namespace Thitipong.Upload.Service
{
    public sealed class App
    {
        internal readonly AppDb db;
        private Lazy<TransactionService> _Transaction;

        public App(AppDb db)
        {
            this.db = db;

            _Transaction = new Lazy<TransactionService>(() => new TransactionService(this));
        }

        public TransactionService Transactions => _Transaction.Value;

        public int SaveChanges() => db.SaveChanges();
        public Task<int> SaveChangesAsync() => db.SaveChangesAsync();

        public Func<DateTime> Now { get; private set; } = () => DateTime.Now;
        public void SetNow(DateTime now) => Now = () => now;
        public void ResetNow() => Now = () => DateTime.Now;
        public DateTime Today() => Now().Date;

    }
}
