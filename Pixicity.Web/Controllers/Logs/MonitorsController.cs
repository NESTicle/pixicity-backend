using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixicity.Domain.Extensions;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Domain.ViewModels.Logs;
using Pixicity.Service.Interfaces;
using Pixicity.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pixicity.Web.Controllers.Logs
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogsService _logsService;

        public MonitorsController(IMapper mapper, ILogsService logsService)
        {
            _mapper = mapper;
            _logsService = logsService;
        }

        [HttpGet]
        [Route(nameof(GetNotificaciones))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> GetNotificaciones([FromQuery] QueryParamsHelper queryParameters, [FromQuery] ActividadFiltrarViewModel search)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _logsService.GetNotificacionesByCurrentUser(queryParameters, search, out long totalCount);
                var mapped = _mapper.Map<List<MonitorViewModel>>(data);

                var paginationMetadata = new
                {
                    totalCount,
                    pageSize = queryParameters.PageCount,
                    currentPage = queryParameters.Page,
                    totalPages = queryParameters.GetTotalPages(totalCount)
                };

                result.Data = new
                {
                    data = mapped,
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

        [HttpGet]
        [Route(nameof(GetLastNotificaciones))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> GetLastNotificaciones()
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _logsService.GetLastNotificacionesByCurrentUser();
                var mapped = _mapper.Map<List<MonitorViewModel>>(data);

                result.Data = mapped;
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpGet]
        [Route(nameof(SetNotificacionesAsReaded))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> SetNotificacionesAsReaded()
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _logsService.SetNotificacionesAsReaded();
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpGet]
        [Route(nameof(GetStats))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> GetStats()
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _logsService.GetCurrentUserStats();
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
