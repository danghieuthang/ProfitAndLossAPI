using ProfitAndLoss.Data.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProfitAndLoss.Business.Models
{
    public class BrandCreateModel : BaseCreateModel<Brand>
    {
        public BrandCreateModel()
        {

        }
        public bool Actived { get; set; }

    }

    public class BrandUpdateModel : BaseUpdateModel<Brand>
    {
        public BrandUpdateModel()
        {

        }
        public bool Actived { get; set; }
    }

    public class BrandSearchModel : BaseSearchModel<Brand>
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

    public class BrandViewModel: BaseViewModel<Brand>
    {
        public BrandViewModel()
        {

        }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
