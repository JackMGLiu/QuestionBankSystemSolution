using Project.QuestionBank.Core.Domain;
using Project.QuestionBank.Core.Repository.Interface;
using Project.QuestionBank.Core.Service.Interface;
using Project.QuestionBank.Infrastructure.Service.Impl;

namespace Project.QuestionBank.Core.Service.Impl
{
    public class DictItemService : BaseService<DictItem>, IDictItemService
    {
        private readonly IDictItemRepository _dal;

        public DictItemService(IDictItemRepository dal)
        {
            this._dal = dal;
            base.BaseDao = dal;
        }
    }
}