using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface IMemberRepository : IBaseRepository<Member, Guid>
    {

    }
    public class MemberRepository : BaseRepository<Member, Guid>, IMemberRepository
    {
        public MemberRepository(DataContext context) : base(context)
        {

        }
    }
}
