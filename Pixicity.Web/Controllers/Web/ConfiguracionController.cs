using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixicity.Data.Models.Web;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pixicity.Web.Controllers.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWebService _webService;

        public ConfiguracionController(IMapper mapper, IWebService webService)
        {
            _mapper = mapper;
            _webService = webService;
        }

        [HttpGet]
        [Route(nameof(GetConfiguracion))]
        public async Task<JSONObjectResult> GetConfiguracion()
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _webService.GetConfiguracion();
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpPut]
        [Route(nameof(UpdateConfiguracion))]
        public async Task<JSONObjectResult> UpdateConfiguracion([FromBody] Configuracion model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _webService.UpdateConfiguracion(model);
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
