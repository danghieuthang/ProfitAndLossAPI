using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface IMemberServices : IBaseServices<Member>
    {
        Task<GenericResult> LoginAsync(MemberLoginModel model);
        Task<GenericResult> CreateMemberAsync(MemberCreateModel model);
        Task<GenericResult> UpdateMemberAsync(MemberUpdateModel model);
        Task<GenericResult> DeleteMemberAsync(Guid id);
    }

    public class MemberServices : BaseServices<Member>, IMemberServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBrandServices _brandServices;
        private readonly IStoreServices _storeServices;

        public MemberServices(IUnitOfWork unitOfWork,
            IBrandServices brandServices,
            IStoreServices storeServices) : base (unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _brandServices = brandServices;
            _storeServices = storeServices;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<GenericResult> LoginAsync(MemberLoginModel model)
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

        public async Task<GenericResult> CreateMemberAsync(MemberCreateModel model)
        {
            /* add default brand id */
            if (model.StoreId == null)
            {
                model.StoreId = _storeServices.GetEntity().FirstOrDefault().Id;
            }
            Member member = new Member { Id = model.Id,FirstName = model.FirstName,  UserName = model.UserName, Email = model.Email };
            _unitOfWork.MemberRepository.Add(member);
            _unitOfWork.Commit();
            return new GenericResult { Success = true, Message = "Created member success!" };
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
