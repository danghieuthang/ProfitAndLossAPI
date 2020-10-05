using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface IMemberService : IDisposable
    {
        Task<GenericResult> Login(MemberLoginModel model);
        Task<GenericResult> CreateMember(RequestCreateMemberModel model);
    }

    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<GenericResult> Login(MemberLoginModel model)
        {
            return new GenericResult
            {
                Data = new
                {
                    UserName = "user name login"
                },
                Success = true
            };
        }

        public async Task<GenericResult> CreateMember(RequestCreateMemberModel model)
        {
            Member member = new Member { UserName = model.UserName, Email = model.Email };
            _unitOfWork.MemberRepository.Add(member);
            _unitOfWork.Commit();
            return new GenericResult { Success = true, Message = "Created member success!" };
        }

    }
}
