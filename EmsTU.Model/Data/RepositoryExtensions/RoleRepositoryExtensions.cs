using EmsTU.Common.Data;
using EmsTU.Model.Models;
using EmsTU.Model.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EmsTU.Model.Data.RepositoryExtensions
{
    public static class RoleRepositoryExtensions
    {
        public static List<Role> FindByName(this IRepository<Role> repository, string name)
        {
            var predicate = PredicateBuilder.True<Role>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                predicate = predicate.And(u => u.Name == name);
            }

            return repository.FindByPredicate(predicate);
        }

        public static List<Role> FindByPredicate(this IRepository<Role> repository, Expression<Func<Role, bool>> predicate)
        {
            return
                repository.Query()
                .Where(predicate)
                .ToList();
        }
    }
}
