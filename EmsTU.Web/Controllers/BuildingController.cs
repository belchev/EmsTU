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
        /// Редакция на заведение
        /// </summary>
        /// <param name="id">Идентификатор на заведение</param>
        /// <param name="building">Нови данни на заведение</param>
        /// <returns></returns>
        [HttpPut]
        public HttpResponseMessage PutBuilding(int id, BuildingDO building)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    User user = unitOfWork.Repo<User>()
                       .Query()
                       .Include(e => e.Role)
                       .Include(e => e.Buildings)
                       .SingleOrDefault(e => e.UserId == this.userContext.UserId);

                    var oldBuilding = this.unitOfWork.Repo<Building>().
                        Find(id,
                            u => u.BuildingTypes,
                            u => u.KitchenTypes,
                            u => u.MusicTypes,
                            u => u.PaymentTypes,
                            u => u.Extras,
                            u => u.OccasionTypes
                        );

                    if (oldBuilding == null)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "Заведението не може да бъде намерено." });
                    }

                    if (!oldBuilding.Version.SequenceEqual(building.Version))
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "Съществува нова версия на заведението." });
                    }

                    oldBuilding.Name = building.Name;
                    oldBuilding.ImagePath = !String.IsNullOrEmpty(building.ImagePath) ? building.ImagePath : "app\\img\\nopic.jpg";
                    oldBuilding.Slogan = building.Slogan;
                    oldBuilding.WebSite = building.WebSite;
                    oldBuilding.DistrictId = building.DistrictId;
                    oldBuilding.MunicipalityId = building.MunicipalityId;
                    oldBuilding.SettlementId = building.SettlementId;
                    oldBuilding.Address = building.Address;
                    oldBuilding.ContactName = building.ContactName;
                    oldBuilding.ContactPhone = building.ContactPhone;
                    oldBuilding.Info = building.Info;
                    oldBuilding.WorkingTime = building.WorkingTime;
                    oldBuilding.Price = building.Price;
                    oldBuilding.SeatsInside = building.SeatsInside;
                    oldBuilding.SeatsOutside = building.SeatsOutside;
                    oldBuilding.IsActive = building.IsActive;
                    oldBuilding.IsDeleted = building.IsDeleted;
                    oldBuilding.ModifyDate = DateTime.Now;
                    oldBuilding.ModifyUserId = this.userContext.UserId;

                    bool newType;
                    bool oldType;
                    foreach (var buildingType in this.unitOfWork.Repo<BuildingType>().Query())
                    {
                        newType = building.BuildingTypes.Any(e => e.NomId == buildingType.BuildingTypeId && !e.IsDeleted);
                        oldType = oldBuilding.BuildingTypes.Any(e => e.BuildingTypeId == buildingType.BuildingTypeId);

                        if (newType && !oldType)
                            oldBuilding.BuildingTypes.Add(this.unitOfWork.Repo<BuildingType>().Find(buildingType.BuildingTypeId));

                        if (!newType && oldType)
                            oldBuilding.BuildingTypes.Remove(this.unitOfWork.Repo<BuildingType>().Find(buildingType.BuildingTypeId));
                    }

                    foreach (var kitchenType in this.unitOfWork.Repo<KitchenType>().Query())
                    {
                        newType = building.KitchenTypes.Any(e => e.NomId == kitchenType.KitchenTypeId && !e.IsDeleted);
                        oldType = oldBuilding.KitchenTypes.Any(e => e.KitchenTypeId == kitchenType.KitchenTypeId);

                        if (newType && !oldType)
                            oldBuilding.KitchenTypes.Add(this.unitOfWork.Repo<KitchenType>().Find(kitchenType.KitchenTypeId));

                        if (!newType && oldType)
                            oldBuilding.KitchenTypes.Remove(this.unitOfWork.Repo<KitchenType>().Find(kitchenType.KitchenTypeId));
                    }

                    foreach (var musicType in this.unitOfWork.Repo<MusicType>().Query())
                    {
                        newType = building.MusicTypes.Any(e => e.NomId == musicType.MusicTypeId && !e.IsDeleted);
                        oldType = oldBuilding.MusicTypes.Any(e => e.MusicTypeId == musicType.MusicTypeId);

                        if (newType && !oldType)
                            oldBuilding.MusicTypes.Add(this.unitOfWork.Repo<MusicType>().Find(musicType.MusicTypeId));

                        if (!newType && oldType)
                            oldBuilding.MusicTypes.Remove(this.unitOfWork.Repo<MusicType>().Find(musicType.MusicTypeId));
                    }

                    foreach (var occasionType in this.unitOfWork.Repo<OccasionType>().Query())
                    {
                        newType = building.OccasionTypes.Any(e => e.NomId == occasionType.OccasionTypeId && !e.IsDeleted);
                        oldType = oldBuilding.OccasionTypes.Any(e => e.OccasionTypeId == occasionType.OccasionTypeId);

                        if (newType && !oldType)
                            oldBuilding.OccasionTypes.Add(this.unitOfWork.Repo<OccasionType>().Find(occasionType.OccasionTypeId));

                        if (!newType && oldType)
                            oldBuilding.OccasionTypes.Remove(this.unitOfWork.Repo<OccasionType>().Find(occasionType.OccasionTypeId));
                    }

                    foreach (var paymentType in this.unitOfWork.Repo<PaymentType>().Query())
                    {
                        newType = building.PaymentTypes.Any(e => e.NomId == paymentType.PaymentTypeId && !e.IsDeleted);
                        oldType = oldBuilding.PaymentTypes.Any(e => e.PaymentTypeId == paymentType.PaymentTypeId);

                        if (newType && !oldType)
                            oldBuilding.PaymentTypes.Add(this.unitOfWork.Repo<PaymentType>().Find(paymentType.PaymentTypeId));

                        if (!newType && oldType)
                            oldBuilding.PaymentTypes.Remove(this.unitOfWork.Repo<PaymentType>().Find(paymentType.PaymentTypeId));
                    }

                    foreach (var extra in this.unitOfWork.Repo<Extra>().Query())
                    {
                        newType = building.Extras.Any(e => e.NomId == extra.ExtraId && !e.IsDeleted);
                        oldType = oldBuilding.Extras.Any(e => e.ExtraId == extra.ExtraId);

                        if (newType && !oldType)
                            oldBuilding.Extras.Add(this.unitOfWork.Repo<Extra>().Find(extra.ExtraId));

                        if (!newType && oldType)
                            oldBuilding.Extras.Remove(this.unitOfWork.Repo<Extra>().Find(extra.ExtraId));
                    }



                    this.unitOfWork.Save();

                    transactionScope.Complete();

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "", buildingId = oldBuilding.BuildingId });
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        /// <summary>
        /// Метод, който връща данни за заведение
        /// </summary>
        /// <param name="id">Уникален идентификатор</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetBuilding(int id)
        {
            User user = unitOfWork.Repo<User>()
               .Query()
               .Include(e => e.Role)
               .Include(e => e.Buildings)
               .SingleOrDefault(e => e.UserId == this.userContext.UserId);

            bool isAdmin = this.userContext.Permissions.Contains("sys#admin");

            if (!user.Buildings.Any(e => e.BuildingId == id && !e.IsDeleted) && !isAdmin)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            var query = this.unitOfWork.Repo<Building>()
                .Find(id,
                    d => d.Settlement,
                    d => d.BuildingTypes,
                    d => d.KitchenTypes,
                    d => d.MusicTypes,
                    d => d.OccasionTypes,
                    d => d.PaymentTypes,
                    d => d.Extras
                );

            if (query == null)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NoContent);
            }

            var returnValue = new BuildingDO(query, isAdmin);

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
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

                    newBuilding.IsActive = false;
                    newBuilding.IsDeleted = false;
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

            if (!this.userContext.Permissions.Contains("sys#admin"))
            {
                predicate = predicate.And(d => d.Users.Any(e => e.UserId == this.userContext.UserId));
                predicate = predicate.And(d => !d.IsDeleted);
            }

            if (!String.IsNullOrWhiteSpace(name))
            {
                predicate = predicate.And(d => d.Name.Contains(name.Trim()));
            }

            if (buildingTypeId.HasValue)
            {
                predicate = predicate.And(d => d.BuildingTypes.Any(e => e.BuildingTypeId == buildingTypeId.Value));
            }

            if (kitchenTypeId.HasValue)
            {
                predicate = predicate.And(d => d.KitchenTypes.Any(e => e.KitchenTypeId == kitchenTypeId.Value));
            }

            if (musicTypeId.HasValue)
            {
                predicate = predicate.And(d => d.MusicTypes.Any(e => e.MusicTypeId == musicTypeId.Value));
            }

            if (occasionTypeId.HasValue)
            {
                predicate = predicate.And(d => d.OccasionTypes.Any(e => e.OccasionTypeId == occasionTypeId.Value));
            }

            if (extraId.HasValue)
            {
                predicate = predicate.And(d => d.Extras.Any(e => e.ExtraId == extraId.Value));
            }

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
               .Include(e => e.Users)
               .Include(e => e.BuildingTypes)
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
