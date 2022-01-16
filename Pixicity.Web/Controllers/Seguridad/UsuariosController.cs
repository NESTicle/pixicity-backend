using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Domain.Extensions;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.Transversal;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Domain.ViewModels.Seguridad;
using Pixicity.Service.Interfaces;
using Pixicity.Web.Helpers;
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
        private readonly ISeguridadService _seguridadService;
        private readonly IMapper _mapper;
        private IAppPrincipal _currentUser { get; }

        public UsuariosController(ISeguridadService seguridadService, IMapper mapper, IAppPrincipal currentUser)
        {
            _seguridadService = seguridadService;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        [HttpGet]
        [Route(nameof(GetUsuarios))]
        public async Task<JSONObjectResult> GetUsuarios([FromQuery] QueryParamsHelper queryParameters)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _seguridadService.GetUsuarios(queryParameters, out long totalCount);

                var mapped = data.Select(x => new
                {
                    x.Id,
                    userName = x.UserName,
                    genero = x.Genero,
                    pais = x.Estado.Pais.Nombre,
                    iso2 = x.Estado.Pais.ISO2,
                    puntos = x.Puntos,
                    comentarios = x.CantidadComentarios,
                    activo = (DateTime.Now - x.Sessions?.OrderBy(x => x.Id)?.LastOrDefault()?.Activo)?.TotalMinutes,
                    posts = 0 // x.CantidadPosts
                });

                var paginationMetadata = new
                {
                    totalCount,
                    pageSize = queryParameters.PageCount,
                    currentPage = queryParameters.Page,
                    totalPages = queryParameters.GetTotalPages(totalCount)
                };

                result.Data = new
                {
                    usuarios = mapped,
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
        [Route(nameof(GetUsuarioInfo))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> GetUsuarioInfo(string userName)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _seguridadService.GetUsuarioInfo(userName);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpGet]
        [Route(nameof(GetSesiones))]
        public async Task<JSONObjectResult> GetSesiones([FromQuery] QueryParamsHelper queryParameters)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _seguridadService.GetSesiones(queryParameters, out long totalCount);
                var mapped = _mapper.Map<List<SessionViewModel>>(data);

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
        [Route(nameof(GetUserByUserName))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> GetUserByUserName([FromQuery] string userName)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _seguridadService.GetUsuarioInfoByUserName(userName);

                var mapped = new
                {
                    id = data?.Id,
                    userName = data?.UserName,
                    genero = data?.GeneroString,
                    pais = new {
                        nombre = data?.Estado?.Pais?.Nombre,
                        iso2 = data?.Estado?.Pais?.ISO2
                    },
                    puntos = data?.Puntos,
                    comentarios = data?.CantidadComentarios,
                    fechaRegistro = data?.FechaRegistro,
                    posts = 0 // x.CantidadPosts
                };

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
        [Route(nameof(UpdateUsuario))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> UpdateUsuario([FromBody] UsuarioViewModel model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                Usuario usuario = _mapper.Map<Usuario>(model);
                usuario.Id = _currentUser.Id;

                result.Data = _seguridadService.UpdateUsuario(usuario);
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
                {
                    Session session = new Session()
                    {
                        UsuarioId = loggedUser.Id,
                        Token = _seguridadService.GenerarJWT(loggedUser),
                        FechaExpiracion = DateTime.UtcNow.AddDays(15)
                    };

                    _seguridadService.SaveUserSession(session);

                    result.Data = new
                    {
                        Usuario = new
                        {
                            id = EncodingHelper.EncodeBase64(loggedUser.Id.ToString()),
                            loggedUser.UserName,
                            rango = loggedUser.Rango.Nombre
                        },
                        session.Token
                    };
                }
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpGet]
        [Route(nameof(GetLoggedUserByJwt))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> GetLoggedUserByJwt()
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var loggedUser = _seguridadService.GetLoggedUserByJwt();
                var mapped = _mapper.Map<UsuarioViewModel>(loggedUser);

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
        [Route(nameof(ChangeUserPassword))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> ChangeUserPassword([FromBody] ChangePasswordUsuarioViewModel model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _seguridadService.ChangeUserPassword(model);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpDelete]
        [Route(nameof(DeleteSesion))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> DeleteSesion([FromQuery] long id)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _seguridadService.DeleteSession(id);
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
