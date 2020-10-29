using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface ITransactionCategoryServices : IBaseServices<TransactionCategory>
    {
        Task<GenericResult> GetTransactionCategoriesByTypeId(Guid id);
    }
    public class TransactionCategoryServices : BaseServices<TransactionCategory>, ITransactionCategoryServices
    {
        public TransactionCategoryServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<GenericResult> GetTransactionCategoriesByTypeId(Guid id)
        {
            var categories = BaseRepository.GetAll(x => x.TransactionTypeId == id);
            var listResult = new List<TransactionCategoryViewModel>();
            Global.Mapper.Map(categories, listResult);
            return new GenericResult
            {
                Data = listResult,
                StatusCode = System.Net.HttpStatusCode.OK,
                Success = true
            };
        }
    }
}
