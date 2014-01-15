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
using System.Text.RegularExpressions;


namespace EmsTU.Web.Controllers
{
    public class UserController : BaseController
    {
        private Regex usernameRegex = new Regex(@"^[\w\.]{5,}$", RegexOptions.Singleline);

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="unitOfWork">Базов интерфейс за достъп до базата данни</param>
        /// <param name="userContextProvider">Интерфейс за достъп до потребителските данни</param>
        public UserController(IUnitOfWork unitOfWork, IUserContextProvider userContextProvider)
            : base(unitOfWork, userContextProvider)
        {
        }

        [HttpGet]
        public HttpResponseMessage GetRoles(string name)
        {
            var roles = this.unitOfWork.Repo<Role>().FindByName(name);

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, roles);
        }

        [HttpGet]
        public HttpResponseMessage GetUsers(string username, string fullname, bool exact, bool? showActive)
        {
            if (!HasAdminRights())
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            var returnValue =
                this.unitOfWork.Repo<User>()
                .FindByUsernameAndFullname(username, fullname, exact, showActive);

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        [HttpGet]
        public HttpResponseMessage GetUser(int userId)
        {
            if (!HasAdminRights())
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            User user = this.unitOfWork.Repo<User>().Find(userId, u => u.Buildings);
            if (user != null)
            {
                UserDO returnResult = new UserDO(user);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnResult);
            }
            else
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpPut]
        public HttpResponseMessage PutUser(int userId, UserDO user)
        {
            if (!string.IsNullOrEmpty(user.Password) && user.Password.Length < 8)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, "'Password' should be at least 8 characters long.");
            }

            User oldUser = this.unitOfWork.Repo<User>().Find(userId, u => u.Role, c => c.Buildings);
            if (oldUser == null)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }

            oldUser.Fullname = user.Fullname;
            oldUser.IsActive = user.IsActive;
            oldUser.Notes = user.Notes;
            oldUser.Version = user.Version;
            oldUser.Email = user.Email;
            oldUser.Role = this.unitOfWork.Repo<Role>().Find(user.RoleId);

            bool newBuilding;
            bool oldBuilding;

            foreach (var building in this.unitOfWork.Repo<Building>().Query())
            {
                newBuilding = user.Buildings.Any(e => e.BuildingId == building.BuildingId && !e.IsDeleted);
                oldBuilding = oldUser.Buildings.Any(e => e.BuildingId == building.BuildingId);

                if (newBuilding && !oldBuilding)
                    oldUser.Buildings.Add(this.unitOfWork.Repo<Building>().Find(building.BuildingId));

                if (!newBuilding && oldBuilding)
                    oldUser.Buildings.Remove(this.unitOfWork.Repo<Building>().Find(building.BuildingId));
            }


            if (!String.IsNullOrEmpty(user.Password))
            {
                oldUser.SetPassword(user.Password);
            }

            this.unitOfWork.Save();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage PostUser(UserDO user, int? buildingRqId)
        {
            if (!HasAdminRights())
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            if (string.IsNullOrWhiteSpace(user.Username) ||
                !this.usernameRegex.IsMatch(user.Username))
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, "'Username' should be at least 5 character long and contain only letters, numbers, underscores('_') and dots('.').");
            }

            if ((string.IsNullOrEmpty(user.Password) || user.Password.Length < 5))
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, "'Password' should be at least 8 characters long.");
            }

            User newUser = new User();

            newUser.Username = user.Username;
            newUser.Fullname = user.Fullname;
            newUser.IsActive = user.IsActive;
            newUser.Notes = user.Notes;
            newUser.Version = user.Version;
            newUser.Email = user.Email;
            newUser.Role = this.unitOfWork.Repo<Role>().Find(user.RoleId);

            newUser.SetPassword(user.Password);

            foreach (var building in user.Buildings)
            {
                newUser.Buildings.Add(this.unitOfWork.Repo<Building>().Find(building.BuildingId));
            }

            this.unitOfWork.Repo<User>().Add(newUser);

            if (buildingRqId.HasValue)
            {
                BuildingRequest buildingRequest = this.unitOfWork.Repo<BuildingRequest>().Find(buildingRqId.Value);
                
                if (buildingRequest != null)
                {
                    buildingRequest.HasRegisteredUser = true;
                }
            }

            this.unitOfWork.Save();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { bRq = buildingRqId });
        }

        [HttpPost]
        public HttpResponseMessage PostNewUserPassword(PasswordsDO passwords)
        {
            string newPassword = passwords.NewPassword;
            string oldPassword = passwords.OldPassword;
            User user = this.unitOfWork.Repo<User>().Find(this.userContext.UserId);
            if (user.VerifyPassword(oldPassword))
            {
                user.SetPassword(newPassword);
                this.unitOfWork.Save();
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.MethodNotAllowed, "The provided current password is wrong!");
            }
        }
    }
}
