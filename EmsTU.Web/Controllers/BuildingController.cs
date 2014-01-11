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
using System.Transactions;


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

        [HttpPost]
        public HttpResponseMessage PostBuilding(NewBuildingDO building)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    Building newBuilding = new Building();

                    newBuilding.Name = building.Name;

                    newBuilding.ImagePath = !String.IsNullOrEmpty(building.ImagePath) ? building.ImagePath : "app\\img\\nopic.jpg";

                    newBuilding.Slogan = building.Slogan;
                    newBuilding.WebSite = building.WebSite;
                    newBuilding.DistrictId = building.DistrictId;
                    newBuilding.MunicipalityId = building.MunicipalityId;
                    newBuilding.SettlementId = building.SettlementId;
                    newBuilding.Address = building.Address;
                    newBuilding.ContactName = building.ContactName;
                    newBuilding.ContactPhone = building.ContactPhone;
                    newBuilding.Info = building.Info;
                    newBuilding.WorkingTime = building.WorkingTime;
                    newBuilding.Price = building.Price;
                    newBuilding.SeatsInside = building.SeatsInside;
                    newBuilding.SeatsOutside = building.SeatsOutside;

                    newBuilding.IsActive = true;
                    newBuilding.ModifyDate = DateTime.Now;
                    newBuilding.ModifyUserId = this.userContext.UserId;

                    this.unitOfWork.Repo<Building>().Add(newBuilding);

                    this.unitOfWork.Save();
                    transactionScope.Complete();

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }

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
            predicate = predicate.And(d => d.IsActive);

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
               .Include(e => e.District)
               .Include(e => e.Municipality)
               .Include(e => e.Settlement)
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
