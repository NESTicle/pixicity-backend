using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pixicity.Data.Models.Parametros;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.ViewModels;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Domain.ViewModels.Import;
using Pixicity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
                var mapped = data.Select(x => new {
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
