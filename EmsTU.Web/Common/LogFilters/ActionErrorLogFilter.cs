using EmsTU.Common.Data;
using EmsTU.Model.Data;
using EmsTU.Model.Infrastructure;
using EmsTU.Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Net.Http;

namespace EmsTU.Web.Common.LogFilters
{
    public class ActionErrorLogFilter : ExceptionFilterAttribute, System.Web.Mvc.IExceptionFilter
    {
        private IUnitOfWork unitOfWork;

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            ProcessException(null, actionExecutedContext);
        }

        void System.Web.Mvc.IExceptionFilter.OnException(ExceptionContext filterContext)
        {
            ProcessException(filterContext, null);
        }

        public void ProcessException(ExceptionContext filterContext, HttpActionExecutedContext actionExecutedContext)
        {
            try
            {
                if (Statics.EnableActionLog)
                {
                    using (this.unitOfWork = new EmsTUUnitOfWork())
                    {
                        StringBuilder requestInfo = new StringBuilder();
                        WriteRequest(requestInfo, filterContext, actionExecutedContext);

                        StringBuilder errorInfo = new StringBuilder();

                        Exception exception = null;

                        if (filterContext != null)
                            exception = filterContext.Exception;
                        else if (actionExecutedContext != null)
                            exception = actionExecutedContext.Exception;

                        WriteException(errorInfo, exception);

                        Guid? requestId = null;

                        if (filterContext != null)
                            requestId = (Guid?)filterContext.HttpContext.Items[ActionLogFilter.MvcRequestIdKey];
                        else if (actionExecutedContext != null)
                            requestId = actionExecutedContext.ActionContext.Request.GetCorrelationId();

                        ActionErrorLog actionLogErrorRecord = new ActionErrorLog();
                        actionLogErrorRecord.RequestId = requestId;
                        actionLogErrorRecord.RequestInfo = requestInfo.ToString();
                        actionLogErrorRecord.ErrorInfo = errorInfo.ToString();
                        actionLogErrorRecord.ActionErrorDate = DateTime.Now;

                        this.unitOfWork.Repo<ActionErrorLog>().Add(actionLogErrorRecord);
                        this.unitOfWork.Save();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private void WriteException(StringBuilder stringBuilder, Exception exception)
        {
            stringBuilder.AppendFormat("Exception type: {0}\n", exception.GetType().FullName);
            stringBuilder.AppendFormat("Message: {0}\n", exception.Message);
            stringBuilder.AppendFormat("Stack trace:\n{0}\n", exception.StackTrace);
            if (exception.InnerException != null)
            {
                stringBuilder.AppendFormat("\n\nInner Exception:\n");
                WriteException(stringBuilder, exception.InnerException);
            }
        }

        private void WriteRequest(StringBuilder stringBuilder, ExceptionContext filterContext, HttpActionExecutedContext actionExecutedContext)
        {
            if (filterContext != null)
            {
                var request = filterContext.RequestContext.HttpContext.Request;

                stringBuilder.AppendLine("Request data:");
                stringBuilder.AppendFormat("UserHostAddress: {0}\n", request.UserHostAddress);
                stringBuilder.AppendFormat("RawUrl: {0}\n", request.RawUrl);
                stringBuilder.AppendFormat("Form: {0}\n", GetFormString(request.Form));
                stringBuilder.AppendFormat("UserAgent: {0}\n", request.UserAgent);
            }
            else if (actionExecutedContext != null)
            {
                //TODO: Implement
            }
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