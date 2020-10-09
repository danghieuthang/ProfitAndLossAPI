using ProfitAndLoss.Data.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProfitAndLoss.Business.Models
{
    public class RequestCreateBrandModel : BaseCreateModel<Brand>
    {
        public RequestCreateBrandModel()
        {

        }
        public bool Actived { get; set; }

    }

    public class RequestUpdateBrandModel : BaseUpdateModel<Brand>
    {
        public RequestUpdateBrandModel()
        {

        }
        public bool Actived { get; set; }
    }

    public class RequestSearchBrandModel : BaseSearchModel<Brand>
    {
        public bool Actived { get; set; }
    }
    public class BrandExportModel : BaseExportModel<Brand>
    {
        public BrandExportModel() : base()
        {

        }

        [Display(Name = "No.")]
        public int No { get; set; }

        [Display(Name = "ID")]
        public string Id { get; set; }

        [Display(Name = "Modified Date")]

        public string ModifiedDate { get; set; }

        [Display(Name = "Created Date")]
        public string CreatedDate { get; set; }

        [DefaultValue("Active")]
        public string Status { get; set; }
    }
}
