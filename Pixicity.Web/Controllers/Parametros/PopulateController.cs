using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixicity.Data.Models.Parametros;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace Pixicity.Web.Controllers.Parametros
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopulateController : ControllerBase
    {
        private readonly IParametrosService _parametrosService;

        public PopulateController(IParametrosService parametrosService)
        {
            _parametrosService = parametrosService;
        }

        [HttpGet]
        [Route(nameof(PopulateCategorias))]
        public async Task<JSONObjectResult> PopulateCategorias()
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _parametrosService.PopulateCategorias();
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }
    }
}
