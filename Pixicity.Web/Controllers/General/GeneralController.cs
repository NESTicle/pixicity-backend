using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixicity.Data.Models.Web;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pixicity.Web.Controllers.General
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private readonly IWebService _webService;
        private readonly IPostService _postService;
        private readonly ISeguridadService _seguridadService;

        public GeneralController(IWebService webService, IPostService postService, ISeguridadService seguridadService)
        {
            _webService = webService;
            _postService = postService;
            _seguridadService = seguridadService;
        }

        [HttpGet]
        [Route(nameof(GetEstadisticas))]
        public async Task<JSONObjectResult> GetEstadisticas()
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = new
                {
                    usuariosOnline = _seguridadService.GetOnlineUsersCount(),
                    usuarios = _seguridadService.CountUsuarios(),
                    posts = _postService.CountPosts(),
                    comentarios = _postService.CountComentarios(),
                    fotos = 0,
                    comentariosFotos = 0,
                    recordUsers = _seguridadService.GetRecordUsersOnline()
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
        [Route(nameof(Contacto))]
        public async Task<JSONObjectResult> Contacto([FromBody] Contacto model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _webService.SaveContacto(model);
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
