

using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;


namespace ActiveDirectoryInteractor{
    /// <summary>
    /// Class used to ldap query active directory
    /// </summary>
    public static class LDAPSearch{
        private static string path = string.Empty;
        private static string username = string.Empty;
        private static string password = string.Empty;
        private static Dictionary<string, object> allProperties = new Dictionary<string, object>();
        /// <summary>
        /// Converts a domain string into active directory search path
        /// </summary>
        public static string fullPath{
            get{
                if(string.IsNullOrEmpty(path)){return string.Empty;}
                string domain = string.Join(".", path.Split('.'));
                string completePath = string.Empty;
                completePath += "LDAP://";
                completePath += domain + "/DC=";
                completePath += string.Join(",DC=", path.Split('.'));
                return completePath;
            }
            set{}
        }


        /// <summary>
        /// Set up properties to perform active directory searches
        /// </summary>
        /// <param name="path">Path to search in</param>
        /// <param name="username">Username to access active directory with</param>
        /// <param name="password">Password to access active directory with</param>
        public static void Initialize(string path, string username, string password){
            LDAPSearch.path = path;
            LDAPSearch.username =  username;
            LDAPSearch.password = password;
        }


        /// <summary>
        /// Gets the properties for a computer in active directory
        /// </summary>
        /// <param name="computerName">Computer name to search for</param>
        /// <returns>Dictionary of property names and values for the provided computer</returns>
        public static Dictionary<string, object> GetComputerProperties(string computerName){
            //if(!DirectoryEntry.Exists(fullPath)){}
            DirectoryEntry de = new DirectoryEntry(fullPath, username, password, AuthenticationTypes.Secure);
            DirectorySearcher deSearch = new DirectorySearcher(de);

            deSearch.SearchScope = SearchScope.Subtree;
            deSearch.Filter = "(&(objectClass=computer)(name=" + computerName + "))";
            //deSearch.PropertiesToLoad.Add("ms-Mcs-AdmPwd");
            deSearch.ClientTimeout = new TimeSpan(0, 0, 5);
            SearchResult results = deSearch.FindOne();

            ResultPropertyCollection properties = results.Properties;

            allProperties.Clear();
            foreach(DictionaryEntry property in results.Properties){
                allProperties.Add(property.Key.ToString(), property.Value);
            }

            de.Dispose();
            return allProperties;
        }

    }// Class

}// Namespace
