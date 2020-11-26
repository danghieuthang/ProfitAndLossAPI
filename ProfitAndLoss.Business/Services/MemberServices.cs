using Microsoft.EntityFrameworkCore;
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
        Task<StoreViewModel> GetStoreByMemberId(string memberId);
    }

    public class MemberServices : BaseServices<Member>, IMemberServices
    {
        private readonly IBrandServices _brandServices;
        private readonly IStoreServices _storeServices;

        public MemberServices(IUnitOfWork unitOfWork,
            IBrandServices brandServices,
            IStoreServices storeServices) : base (unitOfWork)
        {
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
            Member member = model.ToEntity();
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

        public async Task<StoreViewModel> GetStoreByMemberId(string memberId)
        {
            var entity = _unitOfWork.MemberRepository.GetAll( m => m.Id.Equals(new Guid(memberId))).Include(x => x.Store).Include(x => x.Store.Brand).FirstOrDefault();
            if (entity == null || entity.Store == null) return null;
            var brandModel = new BrandViewModel();
            var storeModel = new StoreViewModel();
            // get brand model    
            brandModel.ToModel(entity.Store.Brand);
            // get store model
            storeModel.ToModel(entity.Store);
            storeModel.Brand = brandModel;
            return storeModel;
        }
    }
}
