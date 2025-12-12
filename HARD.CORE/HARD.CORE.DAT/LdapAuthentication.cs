using HARD.CORE.DAT.Interfaces;

using System;
using System.DirectoryServices;
using System.Text;

namespace HARD.CORE.DAT
{
    public class LdapAuthentication : ILdapAuthentication
    {
        private string _path = "";

        private string _filterAttribute;
        public LdapAuthentication()
        {
        }

        public bool IsAutheticated(string domain, string username, string pwd)
        {

            string domainAndUsername = domain + "@\\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            try
            {

                object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                if (result == null)
                {
                    return false;
                }

                _path = result.Path;
                _filterAttribute = result.Properties["cn"][0].ToString();

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

        }
        public string GetGroups()
        {
            DirectorySearcher search = new DirectorySearcher(_path);
            search.Filter = "(cn=" + _filterAttribute + ")";
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();

            try
            {
                SearchResult result = search.FindOne();
                int propertyCount = result.Properties["memberOf"].Count;
                string dn = null;
                int equalsIndex = 0;
                int commaIndex = 0;
                int propertyCounter = 0;

                for (propertyCounter = 0; propertyCounter <= propertyCount - 1; propertyCounter += 1)
                {
                    dn = result.Properties["memberOf"][propertyCounter].ToString();

                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);

                    if ((equalsIndex == -1))
                    {
                        return null;
                    }

                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el grupo del usuario. <br />" + ex.Message);
            }

            return groupNames.ToString();

        }
    }

}
