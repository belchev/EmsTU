using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Transactions;

namespace EmsTU.Common.Data
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext, new()
    {
        private bool disposed;

        public UnitOfWork()
        {
            this.disposed = false;

            this.DataContext = new TContext();
            this.DataContext.Configuration.LazyLoadingEnabled = false;
            this.DataContext.Configuration.ProxyCreationEnabled = false;
        }

        public TContext DataContext { get; private set; }

        public IRepository<TEntity> Repo<TEntity>() where TEntity : class
        {
            return new Repository<TContext, TEntity>(this);
        }

        public void Save()
        {
            this.DataContext.SaveChanges();
        }

        public TransactionScope CreateTransactionScope(System.Transactions.IsolationLevel level = System.Transactions.IsolationLevel.ReadCommitted)
        {
            var transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = level;
            transactionOptions.Timeout = TransactionManager.MaximumTimeout;
            return new TransactionScope(TransactionScopeOption.Required, transactionOptions);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing && this.DataContext != null)
                {
                    this.DataContext.Dispose();
                }

                this.DataContext = null;
                this.disposed = true;
            }
        }
    }
}
