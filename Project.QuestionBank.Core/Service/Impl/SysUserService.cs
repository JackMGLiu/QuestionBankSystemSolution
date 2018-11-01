using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.QuestionBank.Core.AutoMapper;
using Project.QuestionBank.Core.Domain;
using Project.QuestionBank.Core.Repository.Interface;
using Project.QuestionBank.Core.Service.Interface;
using Project.QuestionBank.Core.ViewModel.SysRole;
using Project.QuestionBank.Core.ViewModel.SysUser;
using Project.QuestionBank.Infrastructure.Service.Impl;
using SqlSugar;

namespace Project.QuestionBank.Core.Service.Impl
{
    public class SysUserService : BaseService<SysUser>, ISysUserService
    {
        private readonly ISysUserRepository _dal;

        public SysUserService(ISysUserRepository dal)
        {
            this._dal = dal;
            base.BaseDao = dal;
        }

        public List<SysUserViewModel> GetUserList()
        {
            var data = _dal.CurrentDb.Queryable<SysUser, SysRole>((u, r) => new object[]
            {
                JoinType.Left, u.RoleId == r.Id
            }).Select((u, r) => u).ToList();
            var res = data.MapTo<List<SysUser>, List<SysUserViewModel>>();
            return res;
        }

        public async Task<List<SysUserViewModel>> GetUserPageList(int page, int size)
        {
            var data = await Task.Run(() => _dal.CurrentDb.Queryable<SysUser, SysRole>((u, r) => new object[]
            {
                JoinType.Left, u.RoleId == r.Id
            }).Select((u, r) => new SysUserViewModel
            {
                UserId = u.Id,
                UserName = u.UserName,
                //RealName = u.RealName,
                //Age = u.Age,
                //Email = u.Email,
                //Remark = u.Remark,               
                RoleViewModel = new SysRoleViewModel
                {
                    RoleName = r.RoleName
                }
            }).ToPageList(page, size)); 
            return data;
        }
    }
}