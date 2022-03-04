using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixicity.Domain.Extensions;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Domain.ViewModels.Seguridad;
using Pixicity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pixicity.Web.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class RangosController : ControllerBase
    {
        private readonly ISeguridadService _seguridadService;
        private readonly IMapper _mapper;

        public RangosController(ISeguridadService seguridadService, IMapper mapper)
        {
            _seguridadService = seguridadService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(nameof(GetRangosUsuarios))]
        public async Task<JSONObjectResult> GetRangosUsuarios([FromQuery] QueryParamsHelper queryParameters)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _seguridadService.GetRangosUsuarios(queryParameters, out long totalCount);
                var mapped = _mapper.Map<List<RangoViewModel>>(data);

                var paginationMetadata = new
                {
                    totalCount,
                    pageSize = queryParameters.PageCount,
                    currentPage = queryParameters.Page,
                    totalPages = queryParameters.GetTotalPages(totalCount)
                };

                result.Data = new
                {
                    rangos = mapped,
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
    }
}
