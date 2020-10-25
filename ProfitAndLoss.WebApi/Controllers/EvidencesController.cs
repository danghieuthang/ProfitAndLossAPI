using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Utilities.DTOs;

namespace ProfitAndLoss.WebApi.Controllers
{
    [Route(RouteConstants.Evidence.PREFIX)]
    [ApiController]
    public class EvidencesController : ControllerBase
    {
        private readonly IEvidenceServices _evidenceService;
        public EvidencesController(IEvidenceServices evidenceService)
        {
            _evidenceService = evidenceService;
        }

        [HttpPost]
        public async Task<GenericResult> CreateEvidence([FromForm] EvidenceCreateModel model)
        {
            return await _evidenceService.CreateEvidence(model);
        }

        [HttpGet]
        public async Task<GenericResult> GetEvidences([FromQuery] EvidenceSearchModel model)
        {
            return await _evidenceService.SearchEvidences(model);
        }
    }
}
