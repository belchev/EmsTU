using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EmsTU.Common.Data
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        TEntity Find(int id);

        TEntity Find(int id, params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> Query();

        void Add(TEntity entity);

        void Remove(TEntity entity);

        IList<TSpEntity> ExecProcedure<TSpEntity>(string procedureName, List<SqlParameter> parameters);

        IList<TSpEntity> SqlQuery<TSpEntity>(string sql, List<SqlParameter> parameters);
    }
}
