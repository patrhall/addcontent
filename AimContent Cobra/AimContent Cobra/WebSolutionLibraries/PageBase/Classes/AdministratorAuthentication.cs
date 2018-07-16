using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIM.Business.WebSolution;
using System.Data.Linq;
using System.Xml.Linq;

namespace AIM.Base.Classes
{
    /// <summary>
    /// Used to permit / deny access to pages
    /// </summary>
    public class AdministratorAuthentication
    {        
        private AIM.Business.WebSolution.DataObjectsDataContext db;
        private Config config;

        public AdministratorAuthentication(Config config)
        {
            this.config = config;
            db = new AIM.Business.WebSolution.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);
        }        

        /// <summary>
        /// Checks if administrator ahs access to page.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public bool HasAccessPermission(int ObjectId)
        {            
            //If no roles is used, permit access
            if (!config.useAdminRoles)
                return true;
            
            //use first if there is any users with multiple roles (based on old standard)
            var adminRole = (from o in db.AdminRoles
                             where o.AdminID == config.adminuserid
                             select o.Role.RoleName).First();

            //Always permit super admin
            if (adminRole.ToLower() == Globals.Configuration.SuperAdminRole)
                return true;

            return (from o in db.Object_X_AdminRoles
                    where o.Role.AdminRoles.Any(p => p.AdminID == config.adminuserid) &&
                    o.ObjectId == ObjectId
                    select o).Any();
        }

        public bool HasAccessPermission(List<string> rolesToPermit)
        {
            //If no roles is used, permit access
            if (!config.useAdminRoles)
                return true;

            //use first if there is any users with multiple roles (based on old standard)
            var adminRole = (from o in db.AdminRoles
                             where o.AdminID == config.adminuserid
                             select o.Role.RoleName).First();

            //Always permit super admin
            if (adminRole.ToLower() == Globals.Configuration.SuperAdminRole)
                return true;

            return rolesToPermit.Contains(adminRole);            
        }

        public bool HasAccessPermission(string roleToPermit)
        {
            List<string> roles = new List<string>();
            roles.Add(roleToPermit);

            return HasAccessPermission(roles);
        }

        public bool HasModuleAccessPermission(int moduleId)
        {
            //If no roles is used, permit access
            if (!config.useAdminRoles)
                return true;

            //use first if there is any users with multiple roles (based on old standard)
            var adminRole = (from o in db.AdminRoles
                             where o.AdminID == config.adminuserid
                             select o.Role.RoleName).First();

            //Always permit super admin
            if (adminRole.ToLower() == Globals.Configuration.SuperAdminRole)
                return true;

            var adminRoleId = (from o in db.AdminRoles
                             where o.AdminID == config.adminuserid
                             select o.RoleID).First();

            return (from o in db.Roles_X_Modules
                    where o.SiteID == config.siteID &&
                    o.Module_FK == moduleId &&
                    o.Roles_FK == adminRoleId
                    select o).Any();
        }

        public bool IsUserAdmin
        {
            get
            {
                List<string> roles = new List<string>();
                roles.Add(Globals.Configuration.SuperAdminRole);

                return HasAccessPermission(roles);
            }
        } 
    }
}
