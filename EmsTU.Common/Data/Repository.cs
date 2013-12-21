using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EmsTU.Common.Data
{
    public class Repository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext, new()
        where TEntity : class
    {
        private IUnitOfWork unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public virtual TEntity Find(int id)
        {
            return ((UnitOfWork<TContext>)this.unitOfWork).DataContext.Set<TEntity>().Find(id);
        }

        public TEntity Find(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            object[] keyValues = new object[] { id };

            return this.FindInStore(keyValues, includes);
        }

        public IQueryable<TEntity> Query()
        {
            return ((UnitOfWork<TContext>)this.unitOfWork).DataContext.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            ((UnitOfWork<TContext>)this.unitOfWork).DataContext.Set<TEntity>().Add(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            ((UnitOfWork<TContext>)this.unitOfWork).DataContext.Set<TEntity>().Remove(entity);
        }

        public virtual IList<TSpEntity> ExecProcedure<TSpEntity>(string storedProcedureName, List<SqlParameter> parameters)
        {
            string sql = storedProcedureName + " ";
            for (int i = 0; i < parameters.Count; i++)
            {
                sql += "@" + parameters[i].ParameterName;
                if (i != parameters.Count - 1)
                {
                    sql += ", ";
                }
            }

            return ((UnitOfWork<TContext>)this.unitOfWork).DataContext.Database.SqlQuery<TSpEntity>(sql, parameters.ToArray()).ToList();
        }

        public virtual IList<TSpEntity> SqlQuery<TSpEntity>(string sql, List<SqlParameter> parameters)
        {
            return ((UnitOfWork<TContext>)this.unitOfWork).DataContext.Database.SqlQuery<TSpEntity>(sql, parameters.ToArray()).ToList();
        }

        #region Private methods

        private TEntity FindInStore(object[] keyValues, params Expression<Func<TEntity, object>>[] includes)
        {
            ObjectContext objectContext = ((IObjectContextAdapter)((UnitOfWork<TContext>)this.unitOfWork).DataContext).ObjectContext;
            ObjectSet<TEntity> objectSet = objectContext.CreateObjectSet<TEntity>();

            string quotedEntitySetName = string.Format(
                CultureInfo.InvariantCulture,
                "{0}.{1}",
                this.QuoteIdentifier(objectSet.EntitySet.EntityContainer.Name),
                this.QuoteIdentifier(objectSet.EntitySet.Name));

            var queryBuilder = new StringBuilder();
            queryBuilder.AppendFormat("SELECT VALUE X FROM {0} AS X WHERE ", quotedEntitySetName);

            var entityKeyValues = this.CreateEntityKey(objectSet.EntitySet, keyValues).EntityKeyValues;
            var parameters = new ObjectParameter[entityKeyValues.Length];

            for (var i = 0; i < entityKeyValues.Length; i++)
            {
                if (i > 0)
                {
                    queryBuilder.Append(" AND ");
                }

                var name = string.Format(CultureInfo.InvariantCulture, "p{0}", i.ToString(CultureInfo.InvariantCulture));
                queryBuilder.AppendFormat("X.{0} = @{1}", this.QuoteIdentifier(entityKeyValues[i].Key), name);
                parameters[i] = new ObjectParameter(name, entityKeyValues[i].Value);
            }

            IQueryable<TEntity> query = objectContext.CreateQuery<TEntity>(queryBuilder.ToString(), parameters);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.SingleOrDefault();
        }

        private string QuoteIdentifier(string identifier)
        {
            return "[" + identifier.Replace("]", "]]") + "]";
        }

        private EntityKey CreateEntityKey(EntitySet entitySet, object[] keyValues)
        {
            if (keyValues == null || !keyValues.Any() || keyValues.Any(v => v == null))
            {
                throw new ArgumentException("Parameter keyValues cannot be empty or contain nulls.");
            }

            var keyNames = entitySet.ElementType.KeyMembers.Select(m => m.Name).ToList();
            if (keyNames.Count != keyValues.Length)
            {
                throw new ArgumentException("Invalid number of key values.");
            }

            return new EntityKey(entitySet.EntityContainer.Name + "." + entitySet.Name, keyNames.Zip(keyValues, (name, value) => new KeyValuePair<string, object>(name, value)));
        }

        #endregion
    }
}
