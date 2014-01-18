using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmsTU.Model.Models;
using EmsTU.Model.Utils;
using EmsTU.Model.Data.RepositoryExtensions;
using EmsTU.Model.DataObjects;
using System.Transactions;
using EmsTU.Common.Data;
using EmsTU.Model.Infrastructure;

namespace EmsTU.Web.Controllers
{
    /// <summary>
    /// Контролер за извличане на номенклатури
    /// </summary>
    public class NomController : BaseController
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="unitOfWork">Базов интерфейс за достъп до базата данни</param>
        /// <param name="userContextProvider">Интерфейс за достъп до потребителските данни</param>
        public NomController(IUnitOfWork unitOfWork, IUserContextProvider userContextProvider)
            : base(unitOfWork, userContextProvider)
        {
        }

        [HttpGet]
        public HttpResponseMessage GetNoms(int nomTypeId, string name, string alias, bool? isActive, int limit, int offset)
        {
            if (!HasAdminRights())
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            var query =
                this.unitOfWork.Repo<Nom>()
                .FindByNomTypeIdNameAliasAndIsActive(nomTypeId, name, alias, isActive);

            var returnValue = query
                .Skip(offset)
                .Take(limit)
                .ToList()
                .Select(e => new NomDO(e))
                .ToList();

            int totalCount = query.Count();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new
            {
                returnValue = returnValue,
                totalCount = totalCount
            });
        }

        [HttpPut]
        public HttpResponseMessage PutNom(int nomId, NomDO nom)
        {
            if (!HasAdminRights())
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    Nom oldNom = this.unitOfWork.Repo<Nom>().Find(nomId);
                    if (oldNom == null)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                    if (!oldNom.Version.SequenceEqual(nom.Version))
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "Съществува нова версия на статуса на документ." });
                    }

                    oldNom.Name = nom.Name;
                    oldNom.Alias = nom.Alias;
                    oldNom.IsActive = nom.IsActive;

                    this.unitOfWork.Save();

                    transactionScope.Complete();

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "" });
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        [HttpPost]
        public HttpResponseMessage PostNom(NomDO nom)
        {
            if (!HasAdminRights())
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    Nom newNom = new Nom();

                    newNom.NomTypeId = nom.NomTypeId;
                    newNom.Name = nom.Name;
                    newNom.Alias = nom.Alias;
                    newNom.IsActive = nom.IsActive;

                    this.unitOfWork.Repo<Nom>().Add(newNom);

                    this.unitOfWork.Save();

                    transactionScope.Complete();

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "" });
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteNom(int nomId)
        {
            if (!HasAdminRights())
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {

                    Nom nom = this.unitOfWork.Repo<Nom>().Find(nomId);
                    if (nom != null)
                    {
                        this.unitOfWork.Repo<Nom>().Remove(nom);

                        this.unitOfWork.Save();

                        transactionScope.Complete();

                        return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "" });
                    }
                    else
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    }
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        [HttpGet]
        public HttpResponseMessage GetNom(int nomId)
        {
            if (!HasAdminRights())
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            Nom nom = this.unitOfWork.Repo<Nom>().Find(
                nomId,
                u => u.NomType
            );

            if (nom != null)
            {
                NomDO returnValue = new NomDO(nom);

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
            }
            else
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        public HttpResponseMessage GetNomTypes()
        {
            if (!HasAdminRights())
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            var noms = new List<NomLinkDO>() 
            {
                new NomLinkDO { Name = "Тип заведения", Url = "#/n/bt" },
                new NomLinkDO { Name = "Тип кухни", Url = "#/n/kt" },
                new NomLinkDO { Name = "Тип музика", Url = "#/n/mt" },
                new NomLinkDO { Name = "Поводи", Url = "#/n/ot" },
                new NomLinkDO { Name = "Начини на плащане", Url = "#/n/pt" },
                new NomLinkDO { Name = "Екстри", Url = "#/n/e" },
            };

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new
            {
                noms = noms
            });
        }

        [HttpGet]
        public HttpResponseMessage GetSelectOptionNoms(string type)
        {
            var returnValue =
                this.unitOfWork.Repo<Nom>().Query()
                .Where(e => e.IsActive && e.NomType.Alias == type)
                .Select(n => new
                {
                    nomId = n.NomId,
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
