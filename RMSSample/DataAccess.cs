//---------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
//---------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using Windows.Storage;

namespace Microsoft.RightsManagement.Apps.RMSSample
{
    /// <summary>
    /// Class for accessing app data
    /// </summary>
    internal class DataAccess
    {
        /// <summary>
        /// Stores a given key-value pair in the local store
        /// </summary>        
        public static void StoreValue(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Invalid key specified.");
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Invalid value specified.");
            }

            var localSettings = ApplicationData.Current.LocalSettings;

            if (localSettings.Values.ContainsKey(key))
            {
                localSettings.Values[key] = value;
            }
            else
            {
                localSettings.Values.Add(new KeyValuePair<string, object>(key, value));
            }
        }

        /// <summary>
        /// Retrieves a value for a given key from the local store
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>value</returns>
        public static string RetrieveValue(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Invalid key specified.");
            }

            var localSettings = ApplicationData.Current.LocalSettings;

            if (localSettings.Values.ContainsKey(key))
            {
                return localSettings.Values[key].ToString();
            }

            return null;
        }
    }
}
