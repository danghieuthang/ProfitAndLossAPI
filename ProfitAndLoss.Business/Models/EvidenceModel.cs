using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    public class RequestCreateEvidenceModel : BaseCreateModel<Evidence>
    {
        public RequestCreateEvidenceModel()
        {

        }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Actived { get; set; }

        public string ImUrl { get; set; }

        public Guid ReceptId { get; set; }

    }

    public class RequestUpdateEvidenceModel : BaseUpdateModel<Evidence>
    {
        public RequestUpdateEvidenceModel()
        {

        }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Actived { get; set; }

        public string ImUrl { get; set; }
    }

    public class RequestSearchEvidenceModel : BaseSearchModel<Evidence>
    {
        public RequestSearchEvidenceModel()
        {

        }

        public string Name { get; set; }

    }
}
