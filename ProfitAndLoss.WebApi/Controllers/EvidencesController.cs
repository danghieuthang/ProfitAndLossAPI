using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;

namespace ProfitAndLoss.WebApi.Controllers
{
    [Route(RouteConstants.Evidence.PREFIX)]
    [ApiController]
    public class EvidencesController : BaseController
    {
        private readonly IEvidenceServices _evidenceService;
        public EvidencesController(IEvidenceServices evidenceService, IdentityServices identityServices) : base(identityServices)
        {
            _evidenceService = evidenceService;
        }
        /// <summary>
        /// Create a evidence
        /// </summary>
        /// <param name="model">The evidence create model</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GenericResult> CreateEvidence([FromForm] EvidenceCreateModel model)
        {
            return await _evidenceService.CreateEvidence(model);
        }
        
        /// <summary>
        /// Add list evidences
        /// </summary>
        /// <param name="models">The list evidence create model</param>
        /// <returns></returns>
        [HttpPost("add-multi")]
        public async Task<GenericResult> CreateEvidence([FromBody] List<EvidenceCreateModel> models)
        {
            return await _evidenceService.AddMultiEvidences(models);
        }

        /// <summary>
        /// Search evidences
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<GenericResult> GetEvidences([FromQuery] EvidenceSearchModel model)
        {
            return await _evidenceService.SearchEvidences(model);
        }

    }
}
