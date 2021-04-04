using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thitipong.Upload.Api.Areas.V1.Models;
using Thitipong.Upload.Service;
using Thitipong.Upload.Service.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Thitipong.Upload.Api.Areas.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly FileService _fileService;
        private readonly App _app;

        public TransactionController(FileService fileService, App app)
        {
            this._fileService = fileService;
            this._app = app;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionResponse>>> GetAllAsync([FromHeader(Name = "X-OrderBy")] string orderBy = "date")
        {

            var query = _app.Transactions.All();

            if (orderBy == "date")
            {
                query = query.OrderBy(q => q.TransactionDate);
            }
            else if (orderBy == "currency")
            {
                query = query.OrderBy(q => q.CurrencyCode);
            }
            else if (orderBy == "status")
            {
                query = query.OrderBy(q => q.TransactionStatus);
            }

            var items = await query.ToListAsync();
            var output = items.ConvertAll(TransactionResponse.FromModel);
            return output;
        }


        [HttpPost("upload")]
        public ActionResult Upload(IFormFile formFile)
        {
            try
            {
                var filePath = _fileService.Upload(formFile);
                if (Path.GetExtension(filePath).ToLower() == ".csv")
                {
                    _app.Transactions.ImportCsvFile(filePath);
                }

                if (Path.GetExtension(filePath).ToLower() == ".xml")
                {
                    _app.Transactions.ImportXmlFile(filePath);
                }

                return Ok();
            }
            catch (Exception ex )
            {
                return BadRequest(ex.Message); 
            }

        }

    }
}
