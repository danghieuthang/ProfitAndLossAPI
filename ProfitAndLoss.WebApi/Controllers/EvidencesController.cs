using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Utilities.DTOs;

namespace ProfitAndLoss.WebApi.Controllers
{
    [Route(RouteConstants.Evidence.PREFIX)]
    [ApiController]
    public class EvidencesController : BaseController
    {
        private readonly IEvidenceServices _evidenceService;
        public EvidencesController(IEvidenceServices evidenceService)
        {
            _evidenceService = evidenceService;
        }

        //[HttpPost]
        //public async Task<GenericResult> CreateEvidence([FromForm] EvidenceCreateModel model)
        //{
        //    return await _evidenceService.CreateEvidence(model);
        //}

        [HttpPost]
        public async Task<GenericResult> CreateEvidence([FromBody] List<EvidenceCreateModel> models)
        {
            return await _evidenceService.AddMultiEvidences(models);
        }

        [HttpGet]
        public async Task<GenericResult> GetEvidences([FromQuery] EvidenceSearchModel model)
        {
            return await _evidenceService.SearchEvidences(model);
        }
    }
}
