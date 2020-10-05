using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Repositories
{
    public interface IFeedbackRepository : IBaseRepository<Feedback, Guid>
    {

    }
    public class FeedbackRepository : BaseRepository<Feedback, Guid>, IFeedbackRepository
    {
        public FeedbackRepository(DataContext context) : base(context)
        {

        }
    }
}
