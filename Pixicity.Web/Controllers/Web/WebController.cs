using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixicity.Data.Models.Web;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Service.Interfaces;
using Pixicity.Web.Helpers;
using System;
using System.Threading.Tasks;

namespace Pixicity.Web.Controllers.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWebService _webService;

        public WebController(IMapper mapper, IWebService webService)
        {
            _mapper = mapper;
            _webService = webService;
        }

        [HttpGet]
        [Route(nameof(GetAdsByType))]
        public async Task<JSONObjectResult> GetAdsByType([FromQuery] string type)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _webService.GetAdsByType(type);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpGet]
        [Route(nameof(GetAfiliados))]
        public async Task<JSONObjectResult> GetAfiliados()
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _webService.GetAfiliados();
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpPost]
        [Route(nameof(ChangeAfiliadoActive))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> ChangeAfiliadoActive([FromBody] Afiliado model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _webService.ChangeAfiliadoActive(model);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpGet]
        [Route(nameof(HitAfiliado))]
        public async Task<JSONObjectResult> HitAfiliado([FromQuery] string codigo)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _webService.HitAfiliado(codigo);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpGet]
        [Route(nameof(GetHistorialModeracion))]
        public async Task<JSONObjectResult> GetHistorialModeracion()
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _webService.GetHistorialModeracion();
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
