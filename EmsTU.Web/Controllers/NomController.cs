using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmsTU.Common.Data;
using EmsTU.Model.Models;
using EmsTU.Model.Utils;

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

        [HttpGet]
        public HttpResponseMessage GetYesNoOptions()
        {
            var yes = new { Name = "Да", Value = 1, IsActive = true };
            var no = new { Name = "Не", Value = 2, IsActive = true };

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

    }
}
