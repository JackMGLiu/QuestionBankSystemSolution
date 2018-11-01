using AutoMapper;
using Project.QuestionBank.Core.Domain;
using Project.QuestionBank.Core.ViewModel.SysRole;
using Project.QuestionBank.Core.ViewModel.SysUser;

namespace Project.QuestionBank.Core.AutoMapper
{
    /// <summary>
    /// AutoMapper的全局实体映射配置静态类
    /// </summary>
    public static class AutoMapperConfiguration
    {
        public static void Init()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                #region SysUser

                //将领域实体映射到视图实体
                cfg.CreateMap<SysUser, SysUserViewModel>()
                    .ForMember(v => v.UserId, v => v.MapFrom(s => s.Id))
                    .ForMember(v => v.RoleViewModel, v => v.MapFrom(s => s.Role));

                //将视图实体映射到领域实体
                cfg.CreateMap<SysUserViewModel, SysUser>();

                #endregion

                #region SysRole

                //将领域实体映射到视图实体
                cfg.CreateMap<SysRole, SysRoleViewModel>()
                    .ForMember(v => v.RoleId, v => v.MapFrom(s => s.Id));

                //将视图实体映射到领域实体
                cfg.CreateMap<SysRoleViewModel, SysRole>();

                #endregion
            });

            Mapper = MapperConfiguration.CreateMapper();
        }

        public static IMapper Mapper { get; private set; }

        public static MapperConfiguration MapperConfiguration { get; private set; }
    }
}