using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;

namespace Framework.Core
{
    public class UserTicket
    {
        private ClaimsPrincipal User { get => ((ClaimsPrincipal)Thread.CurrentPrincipal); }
        private IEnumerable<Claim> UserClames { get => User.Claims; }

        public string this[string key]
        {
            get
            {
                var clame = UserClames.FirstOrDefault(a => a.Type == key);
                if (clame == null) return string.Empty;
                return clame.Value;
            }
        }

        public string UserName
        {
            get
            {
                return User.Identity.Name;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return User.Identity.IsAuthenticated;
            }
        }

        public string AuthenticationType
        {
            get
            {
                return User.Identity.AuthenticationType;
            }
        }

        public long Inspection
        {
            //edit by minoo.check for null user
            get
            {
                var inspection = UserClames.FirstOrDefault(a => a.Type == "Inspection");
                if (inspection == null) return -1;
                long.TryParse(inspection.Value, out long inspectionId);
                return inspectionId;
            }
        }

        public long Id
        {
            get
            {
                var clame = UserClames.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier);
                if (clame == null) return -1;
                long.TryParse(clame.Value, out long userId);
                return userId;
            }
        }

        public long EntityId
        {
            get
            {
                var clame = UserClames.FirstOrDefault(a => a.Type == "EntityId");
                if (clame == null) return -1;
                long.TryParse(clame.Value, out long response);
                return response;
            }
        }

        public bool HasPermission(Enum permission)
        {
            var list = UserClames.Where(a => a.Type == "Permissions").ToList();
            var permissionId = (permission.GetType().GetField("value__").GetValue(permission)).ToString();
            //var x=permission as type;
            return list.Any(a => a.Value == permissionId);
        }

        public List<T> GetPermissions<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            var permissionList = UserClames.Where(a => a.Type == "Permissions").Select(a => a.Value).ToList();
            var allEnumvalue = typeof(T).GetEnumValues();

            var newInts = new List<int>();
            var permissionIds = new List<int>();

            foreach (var collection in allEnumvalue)
            {
                newInts.Add((int)collection);
            }

            foreach (var permission in permissionList)
            {
                int permissionValue;
                if (Int32.TryParse(permission, out permissionValue) && newInts.Contains(permissionValue))
                {
                    permissionIds.Add(permissionValue);
                }
            }

            return permissionIds.Select(a => (T)(object)a).ToList();

            //var x=permission as type;
        }


        public List<long> BusinessRoles
        {
            get
            {
                return UserClames.Where(a => a.Type == "BusinessRole").Select(a => Convert.ToInt64(a.Value)).ToList();
            }
        }


        public List<T> GetBusinessRoles<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            var BusinessRoles = UserClames.Where(a => a.Type == "BusinessRole").Select(a => a.Value).ToList();
            var allEnumvalue = typeof(T).GetEnumValues();

            var newInts = new List<int>();
            var permissionIds = new List<int>();

            foreach (var collection in allEnumvalue)
            {
                newInts.Add((int)collection);
            }

            foreach (var item in BusinessRoles)
            {
                if (Int32.TryParse(item, out int businessRoleValue) && newInts.Contains(businessRoleValue))
                {
                    permissionIds.Add(businessRoleValue);
                }
            }

            return permissionIds.Select(a => (T)(object)a).ToList();

            //var x=permission as type;
        }
    }
}