﻿using System;
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
    public class BuildingController : BaseController
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="unitOfWork">Базов интерфейс за достъп до базата данни</param>
        /// <param name="userContextProvider">Интерфейс за достъп до потребителските данни</param>
        public BuildingController(IUnitOfWork unitOfWork, IUserContextProvider userContextProvider)
            : base(unitOfWork, userContextProvider)
        {
        }

        [HttpGet]
        public HttpResponseMessage GetBuildingRequests(
            string buildingName,
            string contactName,
            string userName,
            int limit,
            int offset
            )
        {
            if (!HasAdminRights())
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            int totalCounts;
            var predicate = PredicateBuilder.True<BuildingRequest>();

            if (!String.IsNullOrWhiteSpace(buildingName))
            {
                predicate = predicate.And(d => d.BuildingName.Contains(buildingName.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(contactName))
            {
                predicate = predicate.And(d => d.ContactName.Contains(contactName.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(userName))
            {
                predicate = predicate.And(d => d.UserName.Contains(userName.Trim()));
            }

            var query = this.unitOfWork.Repo<BuildingRequest>().Query();

            query = query
                .Where(predicate)
                .OrderByDescending(e => e.BuildingRequestId)
                .Take(10000);

            totalCounts = query.Count();

            List<BuildingRequest> returnValue = query
                .Skip(offset)
                .Take(limit)
                .ToList();

            StringBuilder sb = new StringBuilder();

            if (totalCounts >= 10000)
            {
                sb.Append("Има повече от 10000 резултата, моля, въведете допълнителни филтри.");
            }

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new
            {
                buildingRequests = returnValue,
                buildingRequestsCount = totalCounts,
                msg = sb.ToString(),
            });
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
                            u => u.Noms.Select(e => e.NomType),
                            u => u.MenuCategories.Select(e => e.Menus),
                            u => u.Albums.Select(e => e.AlbumPhotos),
                            u => u.Events
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

                    #region Manage Noms

                    bool newType;
                    bool oldType;
                    foreach (var buildingType in this.unitOfWork.Repo<Nom>().Query().Where(e => e.NomType.Alias == "BuildingTypes"))
                    {
                        newType = building.BuildingTypes.Contains(buildingType.NomId);
                        oldType = oldBuilding.Noms.Any(e => e.NomId == buildingType.NomId);

                        if (newType && !oldType)
                            oldBuilding.Noms.Add(this.unitOfWork.Repo<Nom>().Find(buildingType.NomId));

                        if (!newType && oldType)
                            oldBuilding.Noms.Remove(this.unitOfWork.Repo<Nom>().Find(buildingType.NomId));
                    }

                    foreach (var kitchenType in this.unitOfWork.Repo<Nom>().Query().Where(e => e.NomType.Alias == "KitchenTypes"))
                    {
                        newType = building.KitchenTypes.Contains(kitchenType.NomId);
                        oldType = oldBuilding.Noms.Any(e => e.NomId == kitchenType.NomId);

                        if (newType && !oldType)
                            oldBuilding.Noms.Add(this.unitOfWork.Repo<Nom>().Find(kitchenType.NomId));

                        if (!newType && oldType)
                            oldBuilding.Noms.Remove(this.unitOfWork.Repo<Nom>().Find(kitchenType.NomId));
                    }

                    foreach (var musicType in this.unitOfWork.Repo<Nom>().Query().Where(e => e.NomType.Alias == "MusicTypes"))
                    {
                        newType = building.MusicTypes.Contains(musicType.NomId);
                        oldType = oldBuilding.Noms.Any(e => e.NomId == musicType.NomId);

                        if (newType && !oldType)
                            oldBuilding.Noms.Add(this.unitOfWork.Repo<Nom>().Find(musicType.NomId));

                        if (!newType && oldType)
                            oldBuilding.Noms.Remove(this.unitOfWork.Repo<Nom>().Find(musicType.NomId));
                    }

                    foreach (var occasionType in this.unitOfWork.Repo<Nom>().Query().Where(e => e.NomType.Alias == "OccasionTypes"))
                    {
                        newType = building.OccasionTypes.Contains(occasionType.NomId);
                        oldType = oldBuilding.Noms.Any(e => e.NomId == occasionType.NomId);

                        if (newType && !oldType)
                            oldBuilding.Noms.Add(this.unitOfWork.Repo<Nom>().Find(occasionType.NomId));

                        if (!newType && oldType)
                            oldBuilding.Noms.Remove(this.unitOfWork.Repo<Nom>().Find(occasionType.NomId));
                    }

                    foreach (var paymentType in this.unitOfWork.Repo<Nom>().Query().Where(e => e.NomType.Alias == "PaymentTypes"))
                    {
                        newType = building.PaymentTypes.Contains(paymentType.NomId);
                        oldType = oldBuilding.Noms.Any(e => e.NomId == paymentType.NomId);

                        if (newType && !oldType)
                            oldBuilding.Noms.Add(this.unitOfWork.Repo<Nom>().Find(paymentType.NomId));

                        if (!newType && oldType)
                            oldBuilding.Noms.Remove(this.unitOfWork.Repo<Nom>().Find(paymentType.NomId));
                    }

                    foreach (var extra in this.unitOfWork.Repo<Nom>().Query().Where(e => e.NomType.Alias == "Extras"))
                    {
                        newType = building.Extras.Contains(extra.NomId);
                        oldType = oldBuilding.Noms.Any(e => e.NomId == extra.NomId);

                        if (newType && !oldType)
                            oldBuilding.Noms.Add(this.unitOfWork.Repo<Nom>().Find(extra.NomId));

                        if (!newType && oldType)
                            oldBuilding.Noms.Remove(this.unitOfWork.Repo<Nom>().Find(extra.NomId));
                    }

                    #endregion

                    #region Manage Menu Categories

                    foreach (var mc in building.MenuCategories)
                    {
                        if (!mc.IsNew && mc.IsDeleted)
                        {
                            MenuCategory mCat = this.unitOfWork.Repo<MenuCategory>().Find(mc.MenuCategoryId, u => u.Menus);
                            if (mCat != null)
                            {
                                foreach (var menu in mCat.Menus.ToList())
                                {
                                    Menu m = this.unitOfWork.Repo<Menu>().Find(menu.MenuId);
                                    this.unitOfWork.Repo<Menu>().Remove(m);
                                }

                                this.unitOfWork.Repo<MenuCategory>().Remove(mCat);
                            }
                        }
                        else if (!mc.IsNew && !mc.IsDeleted)
                        {
                            var oldMenuCat = oldBuilding.MenuCategories.FirstOrDefault(e => e.MenuCategoryId == mc.MenuCategoryId);
                            if (oldMenuCat != null)
                            {
                                oldMenuCat.Name = mc.Name;
                                foreach (var menu in mc.Menus)
                                {
                                    if (!menu.IsNew && menu.IsDeleted)
                                    {
                                        Menu m = this.unitOfWork.Repo<Menu>().Find(menu.MenuId);
                                        this.unitOfWork.Repo<Menu>().Remove(m);
                                    }
                                    else if (!menu.IsNew && !menu.IsDeleted)
                                    {
                                        var oldMenu = oldMenuCat.Menus.FirstOrDefault(e => e.MenuId == menu.MenuId);
                                        oldMenu.Name = menu.Name;
                                        oldMenu.Info = menu.Info;
                                        oldMenu.Price = menu.Price;
                                        oldMenu.ImagePath = menu.ImagePath;
                                        oldMenu.Size = menu.Size;
                                    }
                                    else
                                    {
                                        Menu m = new Menu();
                                        m.MenuCategoryId = menu.MenuCategoryId;
                                        m.Name = menu.Name;
                                        m.Info = menu.Info;
                                        m.Price = menu.Price;
                                        m.ImagePath = menu.ImagePath;
                                        m.Size = menu.Size;
                                        m.IsActive = true;

                                        this.unitOfWork.Repo<Menu>().Add(m);
                                    }
                                }
                            }
                        }
                        else
                        {
                            MenuCategory mCat = new MenuCategory();
                            mCat.BuildingId = building.BuildingId;
                            mCat.Name = mc.Name;
                            mCat.IsActive = true;
                            this.unitOfWork.Repo<MenuCategory>().Add(mCat);
                        }
                    }

                    #endregion

                    #region Manage Albums

                    foreach (var ab in building.Albums)
                    {
                        if (!ab.IsNew && ab.IsDeleted)
                        {
                            Album alb = this.unitOfWork.Repo<Album>().Find(ab.AlbumId, u => u.AlbumPhotos);
                            if (alb != null)
                            {
                                foreach (var ap in alb.AlbumPhotos.ToList())
                                {
                                    AlbumPhoto apD = this.unitOfWork.Repo<AlbumPhoto>().Find(ap.AlbumPhotoId);
                                    this.unitOfWork.Repo<AlbumPhoto>().Remove(apD);
                                }

                                this.unitOfWork.Repo<Album>().Remove(alb);
                            }
                        }
                        else if (!ab.IsNew && !ab.IsDeleted)
                        {
                            var oldAlbum = oldBuilding.Albums.FirstOrDefault(e => e.AlbumId == ab.AlbumId);
                            if (oldAlbum != null)
                            {
                                oldAlbum.Name = ab.Name;

                                foreach (var albumPhoto in ab.AlbumPhotos)
                                {
                                    if (albumPhoto.IsDeleted)
                                    {
                                        AlbumPhoto apD = this.unitOfWork.Repo<AlbumPhoto>().Find(albumPhoto.AlbumPhotoId);
                                        if (apD != null)
                                        {
                                            this.unitOfWork.Repo<AlbumPhoto>().Remove(apD);
                                        }
                                    }
                                    else if (albumPhoto.IsNew)
                                    {
                                        AlbumPhoto apA = new AlbumPhoto();
                                        apA.AlbumId = albumPhoto.AlbumId;
                                        apA.ImagePath = albumPhoto.ImagePath;
                                        apA.ImageThumbPath = albumPhoto.ImageThumbPath;

                                        this.unitOfWork.Repo<AlbumPhoto>().Add(apA);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Album aAdd = new Album();
                            aAdd.BuildingId = building.BuildingId;
                            aAdd.Name = ab.Name;
                            aAdd.IsActive = true;
                            this.unitOfWork.Repo<Album>().Add(aAdd);
                        }
                    }

                    #endregion

                    #region Manage Events

                    foreach (var ev in building.Events)
                    {
                        if (ev.IsDeleted)
                        {
                            Event evD = this.unitOfWork.Repo<Event>().Find(ev.EventId);
                            if (evD != null)
                            {
                                this.unitOfWork.Repo<Event>().Remove(evD);
                            }
                        }
                        else if (ev.IsEdited && !ev.IsNew)
                        {
                            Event oldEvent = this.unitOfWork.Repo<Event>().Find(ev.EventId);

                            if (oldEvent != null)
                            {
                                oldEvent.Name = ev.Name;
                                oldEvent.Info = ev.Info;
                                oldEvent.IsActive = ev.IsActive;
                                oldEvent.ImagePath = ev.ImagePath;
                                oldEvent.ImageThumbPath = ev.ImageThumbPath;
                                oldEvent.Date = ev.Date;
                            }
                        }
                        else if (ev.IsNew)
                        {
                            Event eAdd = new Event();

                            eAdd.BuildingId = building.BuildingId;
                            eAdd.Name = ev.Name;
                            eAdd.Info = ev.Info;
                            eAdd.IsActive = ev.IsActive;
                            eAdd.ImagePath = ev.ImagePath;
                            eAdd.ImageThumbPath = ev.ImageThumbPath;
                            eAdd.Date = ev.Date;

                            this.unitOfWork.Repo<Event>().Add(eAdd);
                        }
                    }

                    #endregion

                    #region Manage Comments

                    foreach (var cm in building.Comments)
                    {
                        if (cm.IsDeleted)
                        {
                            Comment cD = this.unitOfWork.Repo<Comment>().Find(cm.CommentId);
                            if (cD != null)
                            {
                                this.unitOfWork.Repo<Comment>().Remove(cD);
                            }
                        }
                    }

                    #endregion

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
            bool isAdmin = HasAdminRights();
            User user = unitOfWork.Repo<User>()
               .Query()
               .Include(e => e.Role)
               .Include(e => e.Buildings)
               .SingleOrDefault(e => e.UserId == this.userContext.UserId);

            if (!user.Buildings.Any(e => e.BuildingId == id && !e.IsDeleted) && !HasAdminRights())
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            var query = this.unitOfWork.Repo<Building>()
                .Find(id,
                    d => d.Settlement,
                    d => d.Noms.Select(e => e.NomType),
                    d => d.MenuCategories.Select(e => e.Menus),
                    d => d.Albums.Select(e => e.AlbumPhotos),
                    d => d.Events,
                    d => d.Comments
                );

            if (query == null)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NoContent);
            }

            var returnValue = new BuildingDO(query, HasAdminRights());

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        /// <summary>
        /// Генерира нова сграда
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetNewBuilding()
        {
            if (!HasAdminRights())
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            var returnValue = new NewBuildingDO();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        [HttpPost]
        public HttpResponseMessage PostBuilding(NewBuildingDO building, int? buildingRqId)
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

                    newBuilding.MenuCategories.Add(new MenuCategory { Name = "Предястия", IsActive = true });
                    newBuilding.MenuCategories.Add(new MenuCategory { Name = "Основни ястия", IsActive = true });
                    newBuilding.MenuCategories.Add(new MenuCategory { Name = "Десерти", IsActive = true });

                    newBuilding.Albums.Add(new Album { Name = "Главен албум", IsActive = true });
                    newBuilding.Albums.Add(new Album { Name = "Меню", IsActive = true });
                    newBuilding.Albums.Add(new Album { Name = "Интериор", IsActive = true });

                    this.unitOfWork.Repo<Building>().Add(newBuilding);

                    if (buildingRqId.HasValue)
                    {
                        BuildingRequest buildingRequest = this.unitOfWork.Repo<BuildingRequest>().Find(buildingRqId.Value);

                        if (buildingRequest != null)
                        {
                            buildingRequest.HasRegisteredBuilding = true;
                        }
                    }

                    this.unitOfWork.Save();
                    transactionScope.Complete();

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { bRq = buildingRqId });
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }

        }

        [HttpGet]
        public HttpResponseMessage GetBuildings(
            string buildingPage,
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
                predicate = predicate.And(d => d.Noms.Any(e => e.NomId == buildingTypeId.Value));
            }

            if (kitchenTypeId.HasValue)
            {
                predicate = predicate.And(d => d.Noms.Any(e => e.NomId == kitchenTypeId.Value));
            }

            if (musicTypeId.HasValue)
            {
                predicate = predicate.And(d => d.Noms.Any(e => e.NomId == musicTypeId.Value));
            }

            if (occasionTypeId.HasValue)
            {
                predicate = predicate.And(d => d.Noms.Any(e => e.NomId == occasionTypeId.Value));
            }

            if (extraId.HasValue)
            {
                predicate = predicate.And(d => d.Noms.Any(e => e.NomId == extraId.Value));
            }

            var query = this.unitOfWork.Repo<Building>().Query();

            if (buildingPage == "last")
            {
                query = query.OrderByDescending(e => e.ModifyDate);
            }
            else
            {
                query = query.OrderByDescending(e => e.BuildingId);
            }

            query = query
                .Where(predicate)
                .Take(10000);

            totalCounts = query.Count();

            List<BuildingsListItemDO> returnValue = query
               .Skip(offset)
               .Take(limit)
               .Include(e => e.District)
               .Include(e => e.Municipality)
               .Include(e => e.Settlement)
               .Include(e => e.Users)
               .Include(e => e.Noms.Select(k => k.NomType))
               .Include(e => e.ModifyUser)
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
