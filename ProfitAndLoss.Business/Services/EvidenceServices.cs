using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.Constant;
using ProfitAndLoss.Utilities.DTOs;
using ProfitAndLoss.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface IEvidenceServices : IBaseServices<Evidence>
    {
        Task<GenericResult> AddMultiEvidences(List<EvidenceCreateModel> evidences);
        Task<GenericResult> CreateEvidence(EvidenceCreateModel model);
        Task<string> WriteFile(IFormFile file);
        Task<GenericResult> SearchEvidences(EvidenceSearchModel model);
    }
    public class EvidenceServices : BaseServices<Evidence>, IEvidenceServices
    {
        private static string ApiKey = "AIzaSyB0bAvWYtuR-EP0YiultKtT2yhdW40HgMw";
        private static string Bucket = "swdk13.appspot.com";
        private static string AuthEmail = "dhthang1998@gmail.com";
        private static string AuthPassword = "anhthangdepZai123";
        public EvidenceServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<GenericResult> AddMultiEvidences(List<EvidenceCreateModel> evidences)
        {
            var result = new List<Evidence>();
            foreach (var evidence in evidences)
            {
                //evidence.ImgUrl = WriteFile(evidence.Image).Result;
                result.Add(BaseRepository.Add(evidence.ToEntity()));
            }
            _unitOfWork.Commit();
            return new GenericResult
            {
                Data = result,
                Success = true,
                ResultCode = Utilities.AppResultCode.Success,
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }

        private async Task<string> PutImageToFireBase(IFormFile file)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            // you can use CancellationTokenSource to cancel the upload midway
            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true //Exception is thrown when cancel upload
                })
            .Child("Evidence")
            .Child(file.FileName)
            .PutAsync(file.OpenReadStream(), cancellation.Token);
            var imgUrl = await task;
            return imgUrl;
        }
        private void PrepareCreateEvidence(EvidenceCreateModel model)
        {
            model.Name = model.Image.Name;
        }
        /// <summary>
        /// Create evidence
        /// </summary>
        /// <param name="model">The Request Create Evidence Model</param>
        /// <returns>Evidence created</returns>
        public async Task<GenericResult> CreateEvidence(EvidenceCreateModel model)
        {
            PrepareCreateEvidence(model);
            if (!model.Image.FileName.IsImageFile())
            {
                return new GenericResult
                {
                    Data = null,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Success = false,
                    ResultCode = Utilities.AppResultCode.FailValidation
                };
            }
            model.ImgUrl = await PutImageToFireBase(model.Image);
            var data = BaseRepository.Add(model.ToEntity());
            _unitOfWork.Commit();
            return new GenericResult
            {
                Data = data,
                StatusCode = System.Net.HttpStatusCode.Created,
                Success = true,
                ResultCode = Utilities.AppResultCode.Success
            };
        }

        public async Task<GenericResult> SearchEvidences(EvidenceSearchModel model)
        {
            //
            var entities = BaseRepository.GetAll(x => x.ReceiptId == model.ReceiptId);
            //
            var pageSize = model.PageSize > 0 ? model.PageSize : CommonConstants.DEFAULT_PAGESIZE;
            var currentPage = model.Page > 0 ? model.Page : 1;
            var strOrder = model.SortBy;
            //
            var result = new PageResult<Evidence>
            {
                PageIndex = currentPage,
                TotalCount = entities.Count()
            };

            result.Results = entities.OrderBy(x => x.CreatedDate).Skip((currentPage - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            //
            return new GenericResult
            {
                Data = result,
                Success = true
            };
        }

        public async Task<string> WriteFile(IFormFile file)
        {
            string result = null;
            string fileName;
            try
            {
                // Get extension of file
                var extension = "." + file.FileName.Split('.')[^1];

                // Create new file name
                fileName = file.FileName.Split('.')[0] + "_" + DateTime.Now.Ticks + extension;

                //
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files", fileName);

                using var stream = new FileStream(filePath, FileMode.Create);

                await file.CopyToAsync(stream);

                result = filePath;

            }
            catch (Exception e)
            {

            }
            return result;
        }
    }
}
