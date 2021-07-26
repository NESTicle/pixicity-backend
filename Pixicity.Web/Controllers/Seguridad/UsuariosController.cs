using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Domain.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pixicity.Web.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        [HttpPost]
        [Route(nameof(RegistrarUsuario))]
        public async Task<JSONObjectResult> RegistrarUsuario([FromBody] Usuario model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = model;
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
