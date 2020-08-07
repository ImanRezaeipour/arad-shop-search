using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.AccountsManagementIdentity.UserHelper
{
    public static class CurrentUser
    {
        private static string getValue(string claimKey)
        {
            try
            {
                var identity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
                var claim = identity.Claims.FirstOrDefault(a => a.Type == claimKey);
                if (claim != null)
                    return claim.Value;

                return "";
            }
            catch (System.Exception)
            {

                return "";
            }
        }
        public static string GetClaim(string clameName)
        {
            return getValue(clameName);
        }



        public static long GetId(this IPrincipal user)
        {
            var identity = (ClaimsIdentity)user.Identity;
            var claim = identity.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier);
            if (claim != null)
                return Convert.ToInt64(claim.Value); 
            return 0;
        }

        public static T GetClaim<T>(this IPrincipal user, string claimKey)
        {
            var identity = (ClaimsIdentity)user.Identity;
            var claim = identity.Claims.FirstOrDefault(a => a.Type == claimKey);
            if (claim != null)
                return (T)Convert.ChangeType(claim.Value, typeof(T)); ;
            return default(T);
        }
    }
}
