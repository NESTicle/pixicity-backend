using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixicity.Data.Models.Web;
using Pixicity.Domain.Extensions;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Domain.ViewModels.Web;
using Pixicity.Service.Interfaces;
using Pixicity.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pixicity.Web.Controllers.Posts
{
    [Route("api/[controller]")]
    [ApiController]
    public class DenunciasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;

        public DenunciasController(IMapper mapper, IPostService postService)
        {
            _mapper = mapper;
            _postService = postService;
        }

        [HttpGet]
        [Route(nameof(GetDenuncias))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Administrador" })]
        public async Task<JSONObjectResult> GetDenuncias([FromQuery] QueryParamsHelper queryParameters)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _postService.GetDenuncias(queryParameters, out long totalCount);
                var mapped = _mapper.Map<List<DenunciaViewModel>>(data);

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

        [HttpDelete]
        [Route(nameof(DeleteDenuncia))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Administrador" })]
        public async Task<JSONObjectResult> DeleteDenuncia([FromBody] Denuncia model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _postService.DeleteDenuncia(model.Id);
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
