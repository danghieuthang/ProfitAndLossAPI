using Microsoft.AspNetCore.Http;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.Constant;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface IEvidenceService : IBaseService<Evidence>
    {
        Task<List<Evidence>> AddMultiEvidences(List<RequestCreateEvidenceModel> evidences);
        Task<GenericResult> CreateEvidence(RequestCreateEvidenceModel model);
        Task<string> WriteFile(IFormFile file);
        Task<GenericResult> SearchEvidences(RequestSearchEvidenceModel model);
    }
    public class EvidenceService : BaseService<Evidence>, IEvidenceService
    {
        public EvidenceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<List<Evidence>> AddMultiEvidences(List<RequestCreateEvidenceModel> evidences)
        {
            var result = new List<Evidence>();
            foreach (var evidence in evidences)
            {
                evidence.ImgUrl = WriteFile(evidence.Image).Result;
                result.Add(BaseRepository.Add(evidence.ToEntity()));
            }
            _unitOfWork.CommitAsync();
            return result;
        }

        /// <summary>
        /// Create evidence
        /// </summary>
        /// <param name="model">The Request Create Evidence Model</param>
        /// <returns>Evidence created</returns>
        public async Task<GenericResult> CreateEvidence(RequestCreateEvidenceModel model)
        {
            model.ImgUrl = WriteFile(model.Image).Result;
            var data = BaseRepository.Add(model.ToEntity());
            _unitOfWork.Commit();
            return new GenericResult
            {
                Data = data,
                StatusCode = System.Net.HttpStatusCode.Created,
                Success = true
            };
        }

        public async Task<GenericResult> SearchEvidences(RequestSearchEvidenceModel model)
        {
            //
            var entities = BaseRepository.GetAll(x => x.ReceptId == model.ReceptId);
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
