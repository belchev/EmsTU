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
    public static class UserRepositoryExtensions
    {
        public static List<User> FindByUsernameAndFullname(this IRepository<User> repository, string username, string fullname, bool exact, bool? showActive)
        {
            var predicate = PredicateBuilder.True<User>();
            if (showActive != null)
            {
                predicate = predicate.And(u => u.IsActive == showActive);
            }

            if (!string.IsNullOrWhiteSpace(username))
            {
                if (exact)
                {
                    predicate = predicate.And(u => u.Username == username);
                }
                else
                {
                    predicate = predicate.And(u => u.Username.Contains(username));
                }
            }
            if (!string.IsNullOrWhiteSpace(fullname))
            {
                if (exact)
                {
                    predicate = predicate.And(u => u.Fullname == fullname);
                }
                else
                {
                    predicate = predicate.And(u => u.Fullname.Contains(fullname));
                }
            }

            return repository.FindByPredicate(predicate);
        }

        public static List<User> FindByPredicate(this IRepository<User> userRepository, Expression<Func<User, bool>> predicate)
        {
            return
                userRepository.Query()
                .Include(u => u.Role)
                .Where(predicate)
                .ToList();
        }

        public static User GetByUsername(this IRepository<User> repository, string username)
        {
            return repository.Query()
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Username == username);
        }
    }
}
