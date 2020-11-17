using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface IReceiptHistoryServices : IBaseServices<ReceiptHistory>
    {

    }
    public class ReceiptHistoryServices : BaseServices<ReceiptHistory>, IReceiptHistoryServices
    {
        public ReceiptHistoryServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        private void PrepareCreateEntity()
        {

        }

    }
}
