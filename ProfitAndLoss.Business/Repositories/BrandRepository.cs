using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Repositories
{
    public interface IBrandRepository : IBaseRepository<Brand, Guid>
    {
    }
    public class BrandRepository : BaseRepository<Brand, Guid>, IBrandRepository
    {
        public BrandRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
