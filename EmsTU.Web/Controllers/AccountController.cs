using EmsTU.Common.Data;
using EmsTU.Model.Infrastructure;
using EmsTU.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmsTU.Model.Data.RepositoryExtensions;

namespace EmsTU.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserContextProvider userContextProvider;

        private IUnitOfWork unitOfWork;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="unitOfWork">Базов интерфейс за достъп до базата данни</param>
        /// <param name="userContextProvider">Интерфейс за достъп до потребителските данни</param>
        public AccountController(IUnitOfWork unitOfWork, IUserContextProvider userContextProvider)
        {
            this.unitOfWork = unitOfWork;
            this.userContextProvider = userContextProvider;
        }

        /// <summary>
        /// Вход в системата
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            this.userContextProvider.ClearCurrentUserContext();

            return View();
        }

        /// <summary>
        /// Обработка на данните при опит за вход в системата
        /// </summary>
        /// <param name="username">Въведено потребителско име</param>
        /// <param name="password">Въведена парола</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Login(string username, string password)
        {
            User user = this.unitOfWork.Repo<User>().GetByUsername(username);

            if (user != null && user.IsActive && user.VerifyPassword(password))
            {
                this.userContextProvider.SetCurrentUserContext(new UserContext(user));

                return Redirect("~/");
            }
            else
            {
                ViewBag.ErrorMessage = "Невалидно потребителско име или парола.";
                return View();
            }
        }

        /// <summary>
        /// Изпълнява изход от системата за текущия потребител
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Logout()
        {
            this.userContextProvider.ClearCurrentUserContext();

            return Redirect("~/");
        }

    }
}
