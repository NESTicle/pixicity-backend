using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pixicity.Data.Models.Web;
using Pixicity.Domain.Extensions;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Service.Interfaces;
using Pixicity.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pixicity.Web.Controllers.General
{
    [Route("api/[controller]")]
    [ApiController]
    public class AfiliadosController : ControllerBase
    {
        private readonly IWebService _webService;
        private readonly IMapper _mapper;

        public AfiliadosController(IWebService webService, IMapper mapper)
        {
            _webService = webService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(nameof(GetAfiliados))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Moderador" })]
        public async Task<JSONObjectResult> GetAfiliados([FromQuery] QueryParamsHelper queryParameters)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _webService.GetAfiliados(queryParameters, out long totalCount);

                var paginationMetadata = new
                {
                    totalCount,
                    pageSize = queryParameters.PageCount,
                    currentPage = queryParameters.Page,
                    totalPages = queryParameters.GetTotalPages(totalCount)
                };

                result.Data = new
                {
                    data,
                    pagination = paginationMetadata
                };
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpPost]
        [Route(nameof(SaveAfiliacion))]
        public async Task<JSONObjectResult> SaveAfiliacion([FromBody] Afiliado model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _webService.SaveAfiliado(model);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpPut]
        [Route(nameof(UpdateAfiliacion))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Moderador" })]
        public async Task<JSONObjectResult> UpdateAfiliacion([FromBody] Afiliado model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _webService.UpdateAfiliacion(model);
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
