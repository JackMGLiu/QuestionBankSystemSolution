using System.Collections.Generic;
using System.Threading.Tasks;
using Project.QuestionBank.Core.Domain;
using Project.QuestionBank.Core.ViewModel.SysUser;
using Project.QuestionBank.Infrastructure.Service.Interface;

namespace Project.QuestionBank.Core.Service.Interface
{
    public interface ISysUserService : IBaseService<SysUser>
    {
        List<SysUserViewModel> GetUserList();

        /// <summary>
        /// 查询用户给分页数据
        /// </summary>
        /// <param name="keyword">查询条件</param>
        /// <param name="page">起始页</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        Task<List<SysUserViewModel>> GetUserPageList(string keyword,int page,int size);
    }
}