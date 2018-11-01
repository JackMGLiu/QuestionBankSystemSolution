using System.Collections;
using System.Collections.Generic;
using Project.QuestionBank.Core.Domain;
using Project.QuestionBank.Core.ViewModel.SysUser;

namespace Project.QuestionBank.Core.AutoMapper
{
    /// <summary>
    /// 数据库表-实体映射静态扩展类
    /// </summary>
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }

        #region SysUser

        public static SysUserViewModel ToModel(this SysUser entity)
        {
            return entity.MapTo<SysUser, SysUserViewModel>();
        }

        public static IEnumerable<SysUserViewModel> ToModelList(this IEnumerable<SysUser> entitylist)
        {
            return entitylist.MapTo<IEnumerable<SysUser>, IEnumerable<SysUserViewModel>>();
        }

        public static SysUser ToEntity(this SysUserViewModel model)
        {
            return model.MapTo<SysUserViewModel, SysUser>();
        }

        #endregion
    }
}