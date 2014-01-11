using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EmsTU.Web.Common
{
    public class Statics
    {
        #region Public

        public static bool EnableActionLog
        {
            get
            {
                return GetAppConfigValue<bool>("EnableActionLog");
            }
        }
        public static string PathStorage
        {
            get
            {
                return GetAppConfigValue<string>("PathStorage");
            }
        }
        public static string PathImageThumb
        {
            get
            {
                return GetAppConfigValue<string>("PathImageThumb");
            }
        }
        public static string PathImage
        {
            get
            {
                return GetAppConfigValue<string>("PathImage");
            }
        }
        public static string PathImageBuilding
        {
            get
            {
                return GetAppConfigValue<string>("PathImageBuilding");
            }
        }

        #endregion

        #region Private

        private static ConcurrentDictionary<string, object> _valueCache = new ConcurrentDictionary<string, object>();
        private static object _syncRoot = new object();

        private static T GetAppConfigValue<T>(string appConfigKey)
        {
            if (!_valueCache.ContainsKey(appConfigKey))
            {
                lock (_syncRoot)
                {
                    if (!_valueCache.ContainsKey(appConfigKey))
                    {
                        string appConfigValue = System.Configuration.ConfigurationManager.AppSettings[appConfigKey];

                        T configValue = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(appConfigValue);

                        _valueCache.TryAdd(appConfigKey, configValue);
                    }
                }
            }

            return (T)_valueCache[appConfigKey];
        }

        #endregion
    }
}