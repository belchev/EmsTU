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
    public static class BuildingRepositoryExtensions
    {
        public static bool CheckForExistingImageName(this IRepository<Building> repository, string imageName)
        {
            return
                repository.Query()
                .Where(d => d.ImagePath == imageName)
                .Any();
        }
    }
}
