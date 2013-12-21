using EmsTU.Model.Data;
using EmsTU.Model.Infrastructure;
using EmsTU.Model.Models;
using EmsTU.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Text;
using System.Net.Http;

namespace EmsTU.Web.Common.LogFilters
{
    public class ActionLogFilter : System.Web.Http.Filters.ActionFilterAttribute, System.Web.Mvc.IActionFilter
    {
        public static readonly string MvcRequestIdKey = "__ActionLogRequestId__";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ProcessExecuting(null, actionContext);
        }

        void System.Web.Mvc.IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            ProcessExecuting(filterContext, null);
        }

        public void ProcessExecuting(ActionExecutingContext filterContext, HttpActionContext actionContext)
        {
            try
            {
                if (Statics.EnableActionLog)
                {
                    Guid? requestId = null;
                    if (filterContext != null)
                    {
                        requestId = Guid.NewGuid();
                        filterContext.HttpContext.Items.Add(ActionLogFilter.MvcRequestIdKey, requestId);
                    }
                    else if (actionContext != null)
                    {
                        requestId = requestId = actionContext.Request.GetCorrelationId();
                    }

                    using (var unitOfWork = new EmsTUUnitOfWork())
                    {
                        RouteValueDictionary routeValues = null;
                        HttpRequestBase request = null;

                        if (filterContext != null)
                        {
                            routeValues = filterContext.RouteData.Values;
                            request = filterContext.RequestContext.HttpContext.Request;
                        }
                        else
                        {
                            routeValues = (RouteValueDictionary)actionContext.Request.GetRouteData().Values;
                        }

                        IUserContextProvider userContextProvider = new UserContextProviderImpl(new HttpContextWrapper(HttpContext.Current));
                        UserContext userContext = userContextProvider.GetCurrentUserContext();

                        ActionLog actionLogRecord = new ActionLog();
                        actionLogRecord.ActionDate = DateTime.Now;
                        actionLogRecord.IP = request != null ? request.UserHostAddress.LimitLength(50) : actionContext.Request.Headers.Host;
                        actionLogRecord.Action = request != null ? String.Format("{0} {1}/{2}", request.HttpMethod, routeValues["controller"], routeValues["action"]).LimitLength(200) : actionContext.Request.RequestUri.AbsolutePath;
                        actionLogRecord.ObjectId = routeValues.ContainsKey("id") ? ((string)routeValues["id"]).LimitLength(200) : null;
                        actionLogRecord.RawUrl = request != null ? request.RawUrl.LimitLength(500) : actionContext.Request.RequestUri.AbsoluteUri;
                        actionLogRecord.Form = request != null && request.Form.HasKeys() ? GetFormString(request.Form).LimitLength(500) : null;
                        actionLogRecord.BrowserInfo = request != null && !String.IsNullOrEmpty(request.UserAgent) ? request.UserAgent.LimitLength(200) : actionContext.Request.Headers.UserAgent.ToString();
                        actionLogRecord.UserId = userContext != null ? userContext.UserId : (int?)null;
                        actionLogRecord.LoginName = userContext != null ? userContext.FullName : null;
                        actionLogRecord.RequestId = requestId;

                        unitOfWork.Repo<ActionLog>().Add(actionLogRecord);
                        unitOfWork.Save();
                    }
                }
            }
            catch (Exception e)
            {
                //WriteExceptionToEventLog("Unable to write to ActionLog. See exception for details.", e, filterContext.RequestContext.HttpContext.Request);
                Debug.WriteLine(e.Message);
            }

        }

        public override void OnActionExecuted(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
        }

        void System.Web.Mvc.IActionFilter.OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {
        }

        private string GetFormString(NameValueCollection form)
        {
            int i = 0;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string key in form.AllKeys)
            {
                string value = form[key];

                if (key.ToLower().Contains("password"))
                    continue;

                //if (String.Compare(key.ToLower(), "uin") == 0 ||
                //    String.Compare(key.ToLower(), "idnumber") == 0)
                //{
                //    value = SecurityUtils.HashGraoData(value);
                //}

                if (i == 0)
                {
                    stringBuilder.AppendFormat("{0}={1}", key, value);
                }
                else
                {
                    stringBuilder.AppendFormat("&{0}={1}", key, value);
                }

                i++;
            }

            return stringBuilder.ToString();
        }
    }
}