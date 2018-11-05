using Project.QuestionBank.Core.Domain;
using Project.QuestionBank.Core.Repository.Interface;
using Project.QuestionBank.Core.Service.Interface;
using Project.QuestionBank.Infrastructure.Service.Impl;

namespace Project.QuestionBank.Core.Service.Impl
{
    public class DictTypeService : BaseService<DictType>, IDictTypeService
    {
        private readonly IDictTypeRepository _dal;

        public DictTypeService(IDictTypeRepository dal)
        {
            this._dal = dal;
            base.BaseDao = dal;
        }
    }
}