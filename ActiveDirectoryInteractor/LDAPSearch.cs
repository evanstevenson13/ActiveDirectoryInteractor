

using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;


namespace ActiveDirectoryInteractor{
    public static class LDAPSearch{
        private static string path = string.Empty;
        private static string username = string.Empty;
        private static string password = string.Empty;
        private static Dictionary<string, object> allProperties = new Dictionary<string, object>();
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


        public static void Initialize(string path, string username, string password){
            LDAPSearch.path = path;
            LDAPSearch.username =  username;
            LDAPSearch.password = password;
        }


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
