using Microsoft.AspNetCore.Mvc;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Domain.ViewModels.Seguridad;
using Pixicity.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace Pixicity.Web.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ISeguridadService _seguridadService;

        public UsuariosController(ISeguridadService seguridadService)
        {
            _seguridadService = seguridadService;
        }

        [HttpPost]
        [Route(nameof(RegistrarUsuario))]
        public async Task<JSONObjectResult> RegistrarUsuario([FromBody] UsuarioViewModel model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _seguridadService.RegistrarUsuario(model);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<JSONObjectResult> Login([FromBody] UsuarioViewModel model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                Usuario loggedUser = _seguridadService.LoginUsuario(model);

                if (loggedUser == null)
                    result.Data = "error";
                else
                    result.Data = new
                    {
                        Usuario = new
                        {
                            id = EncodingHelper.EncodeBase64(loggedUser.Id.ToString()),
                            loggedUser.UserName,
                            rango = loggedUser.Rango.Nombre
                        },
                        Token = _seguridadService.GenerarJWT(loggedUser)
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
