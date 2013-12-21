﻿using System;
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