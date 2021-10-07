using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pixicity.Data.Models.Parametros;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Service.Interfaces;
using Pixicity.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pixicity.Web.Controllers.Parametros
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IParametrosService _parametrosService;

        public CategoriasController(IParametrosService parametrosService, IMapper mapper)
        {
            _parametrosService = parametrosService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(nameof(GetCategoriasDropdown))]
        public async Task<JSONObjectResult> GetCategoriasDropdown()
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _parametrosService.GetCategoriasDropdown();
                var mapped = data.Select(x => new
                {
                    x.Id,
                    x.Nombre,
                    x.SEO,
                    x.Icono
                });

                result.Data = mapped;
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpPost]
        [Route(nameof(SaveCategoria))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> SaveCategoria([FromBody] Categoria model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _parametrosService.SaveCategoria(model);
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
