using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pixicity.Data.Models.Parametros;
using Pixicity.Domain.Extensions;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Domain.ViewModels.Import;
using Pixicity.Domain.ViewModels.Parametros;
using Pixicity.Service.Interfaces;
using Pixicity.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pixicity.Web.Controllers.Parametros
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IParametrosService _parametrosService;
        private readonly IMapper _mapper;

        public PaisesController(IWebHostEnvironment webHostEnvironment, IParametrosService parametrosService, IMapper mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            _parametrosService = parametrosService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(nameof(GetPaises))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> GetPaises([FromQuery] QueryParamsHelper queryParameters)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _parametrosService.GetPaises(queryParameters, out long totalCount);

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

        [HttpGet]
        [Route(nameof(GetPaisesDropdown))]
        public async Task<JSONObjectResult> GetPaisesDropdown()
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _parametrosService.GetPaisesDropdown();
                var mapped = data.Select(x => new
                {
                    id = x.Id,
                    iso2 = x.ISO2,
                    nombre = x.Nombre
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

        [HttpGet]
        [Route(nameof(GetEstadosByPais))]
        public async Task<JSONObjectResult> GetEstadosByPais([FromQuery] long IdPais)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _parametrosService.GetEstadosByPaisId(IdPais);
                var mapped = _mapper.Map<List<DropdownViewModel>>(data);

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
        [Route(nameof(SavePais))]
        public async Task<JSONObjectResult> SavePais([FromBody] Pais model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _parametrosService.SavePais(model);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpPut]
        [Route(nameof(UpdatePais))]
        public async Task<JSONObjectResult> UpdatePais([FromBody] Pais model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _parametrosService.UpdatePais(model);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpGet]
        [Route(nameof(Importar))]
        public async Task<JSONObjectResult> Importar()
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var myJsonString = System.IO.File.ReadAllText("Populate/paises.json");
                var myJsonObject = JsonConvert.DeserializeObject<List<PaisImport>>(myJsonString);

                foreach (var currentPais in myJsonObject)
                {
                    Pais pais = new Pais()
                    {
                        Nombre = currentPais.country,
                        ISO2 = currentPais.country.Substring(0, 2),
                        ISO3 = currentPais.country.Substring(0, 3),
                        UsuarioRegistra = "Pixicity"
                    };

                    pais.Id = _parametrosService.SavePais(pais);

                    foreach (var currentEstado in currentPais.states)
                    {
                        Estado estado = new Estado()
                        {
                            Nombre = currentEstado,
                            IdPais = pais.Id,
                            UsuarioRegistra = "Pixicity"
                        };

                        _parametrosService.SaveEstado(estado);
                    }
                }
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
