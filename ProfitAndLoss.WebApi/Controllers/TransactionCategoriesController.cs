using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Utilities.DTOs;

namespace ProfitAndLoss.WebApi.Controllers
{
    [Route(RouteConstants.TransactionCategories.PREFIX)]
    [ApiController]
    public class TransactionCategoriesController : ControllerBase
    {
        private readonly ITransactionCategoryServices _transactionCategoryServices;
        public TransactionCategoriesController(ITransactionCategoryServices transactionCategoryServices)
        {
            _transactionCategoryServices = transactionCategoryServices;
        }

        [HttpGet]
        public async Task<GenericResult> GetAll()
        {
            return await _transactionCategoryServices.GetAll();
        }

        /// <summary>
        /// Get all transaction categorioes by  transaction type id
        /// </summary>
        /// <param name="id">The transaction type id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<GenericResult> GetTransactionCategoriesByTypeId(Guid id)
        {
            return await _transactionCategoryServices.GetTransactionCategoriesByTypeId(id);
        }
    }
}
