using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Repositories
{
    public interface IMemberRepository : IBaseRepository<Member, Guid>
    {

    }
    public class MemberRepository : BaseReponsitory<Member, Guid>, IMemberRepository
    {
        public MemberRepository(DataContext context) : base(context)
        {

        }
    }
}
