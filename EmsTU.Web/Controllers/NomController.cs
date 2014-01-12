using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmsTU.Common.Data;
using EmsTU.Model.Models;
using EmsTU.Model.Utils;
using EmsTU.Model.DataObjects;

namespace EmsTU.Web.Controllers
{
    /// <summary>
    /// Контролер за извличане на номенклатури
    /// </summary>
    public class NomController : ApiController
    {
        private IUnitOfWork unitOfWork;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="unitOfWork">Базов интерфейс за достъп до базата данни</param>
        public NomController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region Load Noms

        [HttpGet]
        public HttpResponseMessage GetNomBuildingTypes(string type, string name, int? limit, int? offset)
        {
            var query = this.unitOfWork.Repo<BuildingType>().Query()
                .Where(e => e.IsActive);
            
            query = query.OrderByDescending(e => e.BuildingTypeId);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            int totalCounts = query.Count();

            if (limit.HasValue && offset.HasValue)
            {
                query = query.Skip(offset.Value).Take(limit.Value);
            }

            List<NomDO> returnValue = query
                .ToList()
                .Select(e => new NomDO(e, type))
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new
            {
                noms = returnValue,
                nomsCount = totalCounts
            });
        }

        [HttpGet]
        public HttpResponseMessage GetNomKitchenTypes(string type, string name, int? limit, int? offset)
        {
            var query = this.unitOfWork.Repo<KitchenType>().Query()
                .Where(e => e.IsActive);

            query = query.OrderByDescending(e => e.KitchenTypeId);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            int totalCounts = query.Count();

            if (limit.HasValue && offset.HasValue)
            {
                query = query.Skip(offset.Value).Take(limit.Value);
            }

            List<NomDO> returnValue = query
                .ToList()
                .Select(e => new NomDO(e, type))
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new
            {
                noms = returnValue,
                nomsCount = totalCounts
            });
        }

        [HttpGet]
        public HttpResponseMessage GetMusicTypes(string type, string name, int? limit, int? offset)
        {
            var query = this.unitOfWork.Repo<MusicType>().Query()
                .Where(e => e.IsActive);

            query = query.OrderByDescending(e => e.MusicTypeId);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            int totalCounts = query.Count();

            if (limit.HasValue && offset.HasValue)
            {
                query = query.Skip(offset.Value).Take(limit.Value);
            }

            List<NomDO> returnValue = query
                .ToList()
                .Select(e => new NomDO(e, type))
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new
            {
                noms = returnValue,
                nomsCount = totalCounts
            });
        }

        [HttpGet]
        public HttpResponseMessage GetNomOccasionTypes(string type, string name, int? limit, int? offset)
        {
            var query = this.unitOfWork.Repo<OccasionType>().Query()
                .Where(e => e.IsActive);

            query = query.OrderByDescending(e => e.OccasionTypeId);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            int totalCounts = query.Count();

            if (limit.HasValue && offset.HasValue)
            {
                query = query.Skip(offset.Value).Take(limit.Value);
            }

            List<NomDO> returnValue = query
                .ToList()
                .Select(e => new NomDO(e, type))
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new
            {
                noms = returnValue,
                nomsCount = totalCounts
            });
        }

        [HttpGet]
        public HttpResponseMessage GetNomPaymentTypes(string type, string name, int? limit, int? offset)
        {
            var query = this.unitOfWork.Repo<PaymentType>().Query()
                .Where(e => e.IsActive);

            query = query.OrderByDescending(e => e.PaymentTypeId);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            int totalCounts = query.Count();

            if (limit.HasValue && offset.HasValue)
            {
                query = query.Skip(offset.Value).Take(limit.Value);
            }

            List<NomDO> returnValue = query
                .ToList()
                .Select(e => new NomDO(e, type))
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new
            {
                noms = returnValue,
                nomsCount = totalCounts
            });
        }

        [HttpGet]
        public HttpResponseMessage GetNomExtras(string type, string name, int? limit, int? offset)
        {
            var query = this.unitOfWork.Repo<Extra>().Query()
                .Where(e => e.IsActive);

            query = query.OrderByDescending(e => e.ExtraId);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            int totalCounts = query.Count();

            if (limit.HasValue && offset.HasValue)
            {
                query = query.Skip(offset.Value).Take(limit.Value);
            }

            List<NomDO> returnValue = query
                .ToList()
                .Select(e => new NomDO(e, type))
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new
            {
                noms = returnValue,
                nomsCount = totalCounts
            });
        }

        #endregion

        [HttpGet]
        public HttpResponseMessage GetBuildingTypes()
        {
            var returnValue =
                this.unitOfWork.Repo<BuildingType>().Query()
                .Where(e => e.IsActive)
                .Select(n => new
                {
                    buildingTypeId = n.BuildingTypeId,
                    name = n.Name,
                    isActive = n.IsActive
                }).ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        [HttpGet]
        public HttpResponseMessage GetKitchenTypes()
        {
            var returnValue =
                this.unitOfWork.Repo<KitchenType>().Query()
                .Where(e => e.IsActive)
                .Select(n => new
                {
                    kitchenTypeId = n.KitchenTypeId,
                    name = n.Name,
                    isActive = n.IsActive
                }).ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        [HttpGet]
        public HttpResponseMessage GetMusicTypes()
        {
            var returnValue =
                this.unitOfWork.Repo<MusicType>().Query()
                .Where(e => e.IsActive)
                .Select(n => new
                {
                    musicTypeId = n.MusicTypeId,
                    name = n.Name,
                    isActive = n.IsActive
                }).ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        [HttpGet]
        public HttpResponseMessage GetOccasionTypes()
        {
            var returnValue =
                this.unitOfWork.Repo<OccasionType>().Query()
                .Where(e => e.IsActive)
                .Select(n => new
                {
                    occasionTypeId = n.OccasionTypeId,
                    name = n.Name,
                    isActive = n.IsActive
                }).ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        [HttpGet]
        public HttpResponseMessage GetExtras()
        {
            var returnValue =
                this.unitOfWork.Repo<Extra>().Query()
                .Where(e => e.IsActive)
                .Select(n => new
                {
                    extraId = n.ExtraId,
                    name = n.Name,
                    isActive = n.IsActive
                }).ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        [HttpGet]
        public HttpResponseMessage GetYesNoOptions()
        {
            var yes = new { Name = "Да", Value = "1", IsActive = true };
            var no = new { Name = "Не", Value = "2", IsActive = true };

            var returnValue = new[] { yes, no }.ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        [HttpGet]
        public HttpResponseMessage GetDistricts()
        {
            var returnValue =
                this.unitOfWork.Repo<District>().Query()
                .Where(e => e.IsActive)
                .Select(n => new
                {
                    districtId = n.DistrictId,
                    name = n.Name,
                    isActive = n.IsActive
                }).ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        [HttpGet]
        public HttpResponseMessage GetMunicipalities(int? districtId)
        {
            var predicate = PredicateBuilder.True<Municipality>().And(e => e.IsActive);

            if (districtId.HasValue)
            {
                predicate = predicate.And(e => e.DistrictId == districtId.Value);
            }

            var returnValue =
                this.unitOfWork.Repo<Municipality>().Query()
                .Where(predicate)
                .Select(n => new
                {
                    municipalityId = n.MunicipalityId,
                    name = n.Name,
                    isActive = n.IsActive
                }).ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        [HttpGet]
        public HttpResponseMessage GetSettlements(int? municipalityId)
        {
            var predicate = PredicateBuilder.True<Settlement>().And(e => e.IsActive);

            if (municipalityId.HasValue)
            {
                predicate = predicate.And(e => e.MunicipalityId == municipalityId.Value);
            }

            var returnValue =
                this.unitOfWork.Repo<Settlement>().Query()
                .Where(predicate)
                .Select(n => new
                {
                    settlementId = n.SettlementId,
                    name = n.Name,
                    isActive = n.IsActive
                }).ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        [HttpGet]
        public HttpResponseMessage GetRoles()
        {
            var returnValue =
                this.unitOfWork.Repo<Role>().Query()
                .Where(e => e.IsActive)
                .Select(n => new
                {
                    roleId = n.RoleId,
                    name = n.Name,
                    isActive = n.IsActive
                }).ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        [HttpGet]
        public HttpResponseMessage GetBuildings(string name, int? limit, int? offset)
        {
            var query = this.unitOfWork.Repo<Building>().Query();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            query = query.OrderByDescending(e => e.BuildingId);

            int totalCounts = query.Count();

            if (limit.HasValue && offset.HasValue)
            {
                query = query.Skip(offset.Value).Take(limit.Value);
            }

            List<BuildingsListItemDO> returnValue = query
                .Include(e => e.Settlement)
                .ToList()
                .Select(e => new BuildingsListItemDO(e))
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new
            {
                buildings = returnValue,
                buildingsCount = totalCounts
            });
        }

    }
}
