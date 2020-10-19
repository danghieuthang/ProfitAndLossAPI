using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface IEvidenceRepository : IBaseRepository<Evidence, Guid>
    {

    }
    public class EvidenceRepository : BaseRepository<Evidence, Guid>, IEvidenceRepository
    {
        public EvidenceRepository(DataContext context) : base(context)
        {

        }
    }
}
