using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using EmsTU.Model.Infrastructure;
using EmsTU.Common.Data;
using EmsTU.Model.Models;
using EmsTU.Model.Data.RepositoryExtensions;
using EmsTU.Common.Utils;
using EmsTU.Model.DataObjects;
using EmsTU.Model.Utils;
using System.Text;


namespace EmsTU.Web.Controllers
{
    public class BuildingController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private UserContext userContext;

        public BuildingController(IUnitOfWork unitOfWork, IUserContextProvider userContextProvider)
        {
            this.unitOfWork = unitOfWork;
            this.userContext = userContextProvider.GetCurrentUserContext();
        }

        /// <summary>
        /// Генерира нова сграда
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetNewBuilding()
        {
            User user = unitOfWork.Repo<User>()
               .Query()
               .Include(e => e.Role)
               .SingleOrDefault(e => e.UserId == this.userContext.UserId);

            //todo has permissions to get new building
            
            var returnValue = new NewBuildingDO();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        [HttpGet]
        public HttpResponseMessage GetBuildings(
            string name,
            int? buildingTypeId,
            int? kitchenTypeId,
            int? musicTypeId,
            int? occasionTypeId,
            int? extraId,
            int limit,
            int offset)
        {
            DateTime currentDate = DateTime.Now;

            User user = unitOfWork.Repo<User>()
                .Query()
                .Include(e => e.Role)
                .SingleOrDefault(e => e.UserId == this.userContext.UserId);

            int totalCounts;
            var predicate = PredicateBuilder.True<Building>();

            if (!String.IsNullOrWhiteSpace(name))
            {
                predicate = predicate.And(d => d.Name.Contains(name.Trim()));
            }

            if (buildingTypeId.HasValue)
            {
                predicate = predicate.And(d => d.BuildingBuildingTypes.Any(e => e.BuildingTypeId == buildingTypeId.Value));
            }

            if (kitchenTypeId.HasValue)
            {
                predicate = predicate.And(d => d.BuildingKitchenTypes.Any(e => e.KitchenTypeId == kitchenTypeId.Value));
            }

            if (musicTypeId.HasValue)
            {
                predicate = predicate.And(d => d.BuildingMusicTypes.Any(e => e.MusicTypeId == musicTypeId.Value));
            }

            if (occasionTypeId.HasValue)
            {
                predicate = predicate.And(d => d.BuildingOccasionTypes.Any(e => e.OccasionTypeId == occasionTypeId.Value));
            }

            if (extraId.HasValue)
            {
                predicate = predicate.And(d => d.BuildingExtras.Any(e => e.ExtraId == extraId.Value));
            }

            //todo constraint with user permissions!

            var query = this.unitOfWork.Repo<Building>().Query();

            query = query
                .Where(predicate)
                .OrderByDescending(e => e.BuildingId)
                .Take(10000);

            totalCounts = query.Count();

            List<BuildingsListItemDO> returnValue = query
               .Skip(offset)
               .Take(limit)
               .Include(e => e.BuildingBuildingTypes)
               .Include(e => e.Users)
               .Include(e => e.BuildingBuildingTypes.Select(g => g.BuildingType))
               .ToList()
               .Select(e => new BuildingsListItemDO(e))
               .ToList();

            StringBuilder sb = new StringBuilder();

            if (totalCounts >= 10000)
            {
                sb.Append("Има повече от 10000 резултата, моля, въведете допълнителни филтри.");
            }

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new
            {
                buildings = returnValue,
                buildingsCount = totalCounts,
                msg = sb.ToString(),
            });
        }
    }
}
