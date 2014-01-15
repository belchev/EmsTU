using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using EmsTU.Common.Data;
using EmsTU.Model.Infrastructure;
using EmsTU.Model.Models;

namespace EmsTU.Web.Controllers
{
    /// <summary>
    /// Базов WebApi контролер
    /// </summary>
    public class BaseController : ApiController
    {
        protected IUnitOfWork unitOfWork;
        protected IUserContextProvider userContextProvider;
        protected UserContext userContext;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="unitOfWork">Базов интерфейс за достъп до базата данни</param>
        /// <param name="userContextProvider">Интерфейс за достъп до потребителските данни</param>
        public BaseController(IUnitOfWork unitOfWork, IUserContextProvider userContextProvider)
        {
            this.unitOfWork = unitOfWork;
            this.userContextProvider = userContextProvider;
            this.userContext = userContextProvider.GetCurrentUserContext();
        }

        private User _systemUser;

        /// <summary>
        /// Достъп до системен потребител
        /// </summary>
        protected User SystemUser
        {
            get
            {
                if (_systemUser == null)
                {
                    _systemUser = this.unitOfWork.Repo<User>().Query().FirstOrDefault(e => e.Username == "systemUser");
                }

                return _systemUser;
            }
        }

        /// <summary>
        /// Метод, който проверява дали текущият потребител е администратор
        /// </summary>
        /// <param name="u">Идентификатор на потребител</param>
        /// <returns></returns>
        protected bool HasAdminRights()
        {
            return this.userContext.Permissions.Contains("sys#admin");
        }

    }
}
