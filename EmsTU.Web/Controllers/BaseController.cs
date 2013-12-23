using EmsTU.Common.Data;
using EmsTU.Model.Infrastructure;
using EmsTU.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace EmsTU.Web.Controllers
{
    /// <summary>
    /// Базов WebApi контролер
    /// </summary>
    public class BaseController : ApiController
    {
        protected IUnitOfWork unitOfWork;
        protected IUserContextProvider userContextProvider;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="unitOfWork">Базов интерфейс за достъп до базата данни</param>
        /// <param name="userContextProvider">Интерфейс за достъп до потребителските данни</param>
        /// <param name="documentSerializer">Интерфейс за Xml сериализация на документи</param>
        public BaseController(IUnitOfWork unitOfWork, IUserContextProvider userContextProvider)
        {
            this.unitOfWork = unitOfWork;
            this.userContextProvider = userContextProvider;
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


    }
}
