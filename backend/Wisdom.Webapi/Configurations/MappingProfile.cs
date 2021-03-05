using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sugar.Enties;
using Wisdom.Webapi.ViewModels.Systems.Mneu;
using Wisdom.Webapi.ViewModels.Systems.Role;
using Wisdom.Webapi.ViewModels.Systems.Permission;

namespace Wisdom.Webapi.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region System
            // menu
            CreateMap<sys_menus, MenuNode>();
            // role
            CreateMap<sys_roles, RoleJsonModel>();
            // permission
            //CreateMap<sys_permissions, PermissionJsonModel>();
            #endregion

            #region MyRegion

            #endregion
        }
    }
}
