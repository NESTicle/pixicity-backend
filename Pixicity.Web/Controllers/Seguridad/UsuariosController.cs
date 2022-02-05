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
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private IAppPrincipal _currentUser { get; }

        public UsuariosController(ISeguridadService seguridadService, IPostService postService, IMapper mapper, IAppPrincipal currentUser)
        {
            _seguridadService = seguridadService;
            _postService = postService;
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
        public async Task<JSONObjectResult> GetUserByUserName([FromQuery] string userName)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var user = _seguridadService.GetUsuarioInfoByUserName(userName);

                if (user == null)
                    throw new Exception($"No se ha encontrado el usuario con el nombre de usuario ${user.UserName}");

                var mapped = _mapper.Map<PerfilUsuarioViewModel>(user);

                mapped.ComentariosCount = _seguridadService.CommentsCountByUserId(user.Id);
                mapped.SeguidoresCount = _seguridadService.SeguidoresCountByUserId(user.Id);
                mapped.SiguiendoCount = _seguridadService.SiguiendoCountByUserId(user.Id);
                mapped.PostsCount = _postService.PostsCountByUserId(user.Id);

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

        [HttpPost]
        [Route(nameof(SeguirUsuario))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> SeguirUsuario([FromBody] UsuarioSeguidores model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                if (model.SeguidorId == 0 && string.IsNullOrEmpty(model.UserName))
                    throw new Exception("Faltan datos para realizar la petición");

                result.Data = _seguridadService.SeguirUsuario(model);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpGet]
        [Route(nameof(IsFollowingTheUser))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> IsFollowingTheUser([FromQuery] string userName)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _seguridadService.IsFollowingTheUser(userName);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpGet]
        [Route(nameof(GetFollowingUsersByUserId))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> GetFollowingUsersByUserId([FromQuery] QueryParamsHelper queryParameters)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _seguridadService.GetFollowingUsersByUserId(queryParameters, out long totalCount);
                var mapped = data.Select(x => new
                {
                    userName = x.UserName,
                    genero = x.GeneroString,
                    pais = new
                    {
                        nombre = x.Estado?.Pais?.Nombre,
                        iso2 = x.Estado?.Pais?.ISO2?.ToLower()
                    },
                    puntos = x.Puntos
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
        [Route(nameof(GetFollowersByUserId))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> GetFollowersByUserId([FromQuery] QueryParamsHelper queryParameters)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _seguridadService.GetFollowersByUserId(queryParameters, out long totalCount);
                var mapped = data.Select(x => new
                {
                    userName = x.UserName,
                    genero = x.GeneroString,
                    pais = new
                    {
                        nombre = x.Estado?.Pais?.Nombre,
                        iso2 = x.Estado?.Pais?.ISO2?.ToLower()
                    },
                    puntos = x.Puntos
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
        [Route(nameof(GetCurrentPerfilInfo))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> GetCurrentPerfilInfo()
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _seguridadService.GetCurrentPerfilInfo();
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpPut]
        [Route(nameof(SavePerfilInfo))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> SavePerfilInfo([FromBody] UsuarioPerfil usuario)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _seguridadService.SavePerfilInfo(usuario);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpGet]
        [Route(nameof(GetSocialMediaByUsuarioId))]
        public async Task<JSONObjectResult> GetSocialMediaByUsuarioId([FromQuery] long usuarioId)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _seguridadService.GetSocialMediaByUsuarioId(usuarioId);
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
