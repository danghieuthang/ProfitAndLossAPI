using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface IMemberService : IDisposable
    {
        Task<GenericResult> LoginAsync(MemberLoginModel model);
        Task<GenericResult> CreateMemberAsync(MemberCreateModel model);
        Task<GenericResult> UpdateMemberAsync(MemberUpdateModel model);
        Task<GenericResult> DeleteMemberAsync(Guid id);
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

        public async Task<GenericResult> CreateMember(MemberCreateModel model)
        {
            Member member = new Member { UserName = model.UserName, Email = model.Email };
            _unitOfWork.MemberRepository.Add(member);
            _unitOfWork.Commit();
            return new GenericResult { Success = true, Message = "Created member success!" };
        }

        public Task<GenericResult> LoginAsync(MemberLoginModel model)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResult> CreateMemberAsync(MemberCreateModel model)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResult> UpdateMemberAsync(MemberUpdateModel model)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResult> DeleteMemberAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
