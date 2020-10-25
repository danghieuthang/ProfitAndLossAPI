using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using ProfitAndLoss.Business.Helpers;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public interface IDemoExcelService : IBaseServices<Brand>
    {
        Task<byte[]> ExportBrands();
    }

    public class DemoExcelService : BaseServices<Brand>, IDemoExcelService
    {
        public DemoExcelService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }


        public async Task<byte[]> ExportBrands()
        {
            // ToList trước khi select có index
            var data = BaseRepository.GetAll().ToList().Select((x, i) => new BrandExportModel
            {
                No = i + 1,
                Id = x.Id.ToString(),
                ModifiedDate = x.ModifiedDate.ToFormal(),
                CreatedDate = x.CreatedDate.ToFormal()
            }).ToList();

            return ExcelHelper<BrandExportModel>.Export(data, "Brand Report");
        }
    }
}
