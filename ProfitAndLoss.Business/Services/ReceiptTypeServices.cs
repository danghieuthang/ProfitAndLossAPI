using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface IReceiptTypeServices : IBaseServices<ReceiptType>
    {
    }
    public class ReceiptTypeServices : BaseServices<ReceiptType>, IReceiptTypeServices
    {
        public ReceiptTypeServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
