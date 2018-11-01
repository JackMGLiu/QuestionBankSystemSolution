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

        Task<List<SysUserViewModel>> GetUserPageList(int page,int size);
    }
}