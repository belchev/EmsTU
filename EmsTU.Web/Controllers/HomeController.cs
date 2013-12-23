using EmsTU.Common.Data;
using EmsTU.Model.Infrastructure;
using EmsTU.Model.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Newtonsoft.Json;

namespace EmsTU.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork unitOfWork;
        private IUserContextProvider userContextProvider;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="unitOfWork">Базов интерфейс за достъп до базата данни</param>
        /// <param name="userContextProvider">Интерфейс за достъп до потребителските данни</param>
        public HomeController(
            IUnitOfWork unitOfWork,
            IUserContextProvider userContextProvider
            )
        {
            this.unitOfWork = unitOfWork;
            this.userContextProvider = userContextProvider;
        }

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            UserContext userContext = this.userContextProvider.GetCurrentUserContext();
            User user = this.unitOfWork.Repo<User>().Query()
                //.Include(e => e.Unit)
                .SingleOrDefault(e => e.UserId == userContext.UserId);


            string config = string.Empty;

            config =
            JsonConvert.SerializeObject(
                new
                {
                    App = "EmsTU",
                    UserId = userContext.UserId,
                    UserFullName = userContext.FullName,
                    //UserUnitName = user.Unit.Name,
                    //UserUnitPosition = userUnitPosition,
                    UserHasPassword = true,
                    //Permissions = userContext.Permissions,
                },

                System.Web.Http.GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings);

            IDictionary<string, object> model = new ExpandoObject();
            model.Add("Config", config);
            model.Add("Version", "0.7.0.0#000000");
            model.Add("VersionHash", "000000");

            return View(model);
        }

    }
}
