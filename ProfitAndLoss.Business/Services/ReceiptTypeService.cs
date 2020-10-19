using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface IReceiptTypeService : IBaseService<ReceiptType>
    {
    }

    public class ReceiptTypeService : BaseService<ReceiptType>, IReceiptTypeService
    {
        public ReceiptTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
