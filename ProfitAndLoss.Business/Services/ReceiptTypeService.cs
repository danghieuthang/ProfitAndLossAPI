using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface IReceiptTypeService : IBaseService<ReceiptType>
    {
        Task<GenericResult> GetAll();
    }

    public class ReceiptTypeService : BaseService<ReceiptType>, IReceiptTypeService
    {
        public ReceiptTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<GenericResult> GetAll()
        {
            return new GenericResult { Data = BaseRepository.GetAll(), Success = true };
        }
    }
}
