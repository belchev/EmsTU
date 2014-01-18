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
    public static class NomRepositoryExtensions
    {
        public static Nom GetByAlias(this IRepository<Nom> repository, string alias)
        {
            return repository.Query().FirstOrDefault(u => u.Alias.ToLower() == alias.ToLower());
        }

        public static IQueryable<Nom> FindByNomTypeIdNameAliasAndIsActive(this IRepository<Nom> repository, int nomTypeId, string name, string alias, bool? isActive)
        {
            var predicate = PredicateBuilder.True<Nom>();

            predicate = predicate.And(u => u.NomTypeId == nomTypeId);

            if (isActive.HasValue)
            {
                predicate = predicate.And(u => u.IsActive == isActive.Value);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                predicate = predicate.And(u => u.Name.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(alias))
            {
                predicate = predicate.And(u => u.Alias.Contains(alias));
            }

            return repository.FindByPredicate(predicate);
        }

        public static IQueryable<Nom> FindByPredicate(this IRepository<Nom> nomRepository, Expression<Func<Nom, bool>> predicate)
        {
            return
                nomRepository.Query()
                .Include(e => e.NomType)
                .Where(predicate)
                .OrderByDescending(e => e.NomId);
        }
    }
}
