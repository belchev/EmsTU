using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;

namespace EmsTU.Common.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repo<TEntity>() where TEntity : class;
        TransactionScope CreateTransactionScope(System.Transactions.IsolationLevel level = System.Transactions.IsolationLevel.ReadCommitted);
        void Save();
    }
}
