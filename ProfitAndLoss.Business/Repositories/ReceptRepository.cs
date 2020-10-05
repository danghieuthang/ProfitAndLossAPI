using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Repositories
{
    public interface IReceptRepository : IBaseRepository<Recept, Guid>
    {

    }
    public class ReceptRepository : BaseRepository<Recept, Guid>, IReceptRepository
    {
        public ReceptRepository(DataContext context) : base(context)
        {

        }
    }
}
