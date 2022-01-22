using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixicity.Data.Models.Posts;
using Pixicity.Data.Models.Web;
using Pixicity.Domain.Extensions;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Domain.ViewModels.Posts;
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
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;

        public PostsController(IMapper mapper, IPostService postService)
        {
            _mapper = mapper;
            _postService = postService;
        }

        [HttpGet]
        [Route(nameof(GetPosts))]
        public async Task<JSONObjectResult> GetPosts([FromQuery] QueryParamsHelper queryParameters)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _postService.GetPosts(queryParameters, out long totalCount);
                var mapped = data.Select(x => new
                {
                    x.Id,
                    x.Titulo,
                    categoria = new {
                        icono = x.Categoria.Icono,
                        nombre = x.Categoria.Nombre,
                        seo = x.Categoria.SEO
                    },
                    x.Sticky,
                    x.EsPrivado
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
        [Route(nameof(GetPostsAdmin))]
        public async Task<JSONObjectResult> GetPostsAdmin([FromQuery] QueryParamsHelper queryParameters)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _postService.GetPostsAdmin(queryParameters, out long totalCount);
                var mapped = data.Select(x => new
                {
                    x.Id,
                    x.Titulo,
                    x.FechaRegistro,
                    x.Puntos,
                    categoria = new
                    {
                        icono = x.Categoria.Icono,
                        nombre = x.Categoria.Nombre,
                        seo = x.Categoria.SEO
                    },
                    x.Etiquetas,
                    x.Sticky,
                    x.EsPrivado,
                    x.Eliminado,
                    x.Usuario.UserName
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
        [Route(nameof(GetPostsByLoggedUser))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> GetPostsByLoggedUser([FromQuery] QueryParamsHelper queryParameters)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _postService.GetPostsByLoggedUser(queryParameters, out long totalCount);
                var mapped = data.Select(x => new
                {
                    x.Id,
                    x.Titulo,
                    x.FechaRegistro,
                    x.Puntos,
                    categoria = new
                    {
                        icono = x.Categoria.Icono,
                        nombre = x.Categoria.Nombre,
                        seo = x.Categoria.SEO
                    },
                    x.Etiquetas,
                    x.Sticky,
                    x.EsPrivado
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
        [Route(nameof(GetStickyPosts))]
        public async Task<JSONObjectResult> GetStickyPosts()
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _postService.GetStickyPosts();
                var mapped = data.Select(x => new
                {
                    x.Id,
                    x.Titulo,
                    categoria = new
                    {
                        icono = x.Categoria.Icono,
                        nombre = x.Categoria.Nombre,
                        seo = x.Categoria.SEO
                    },
                    x.Sticky
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
        [Route(nameof(GetPostById))]
        public async Task<JSONObjectResult> GetPostById([FromQuery] long postId)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _postService.GetPostById(postId);
                var mapped = _mapper.Map<PostViewModel>(data);

                if (mapped != null)
                {
                    result.Data = new
                    {
                        post = mapped,
                        usuario = new
                        {
                            userName = data.Usuario.UserName,
                            genero = data.Usuario.GeneroString,
                            pais = data.Usuario.Estado.Pais.ISO2
                        }
                    };
                }
                else
                {
                    result.Data = null;
                }
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpPost]
        [Route(nameof(NextPost))]
        public async Task<JSONObjectResult> NextPost([FromBody] Post model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _postService.NextPost(model.Id);
                var mapped = _mapper.Map<PostViewModel>(data);

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
        [Route(nameof(PreviousPost))]
        public async Task<JSONObjectResult> PreviousPost([FromBody] Post model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _postService.PreviousPost(model.Id);
                var mapped = _mapper.Map<PostViewModel>(data);

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
        [Route(nameof(RandomPost))]
        public async Task<JSONObjectResult> RandomPost([FromBody] Post model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _postService.PreviousPost(model.Id);
                var mapped = _mapper.Map<PostViewModel>(data);

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
        [Route(nameof(SavePost))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> SavePost([FromBody] Post model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _postService.SavePost(model);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpPut]
        [Route(nameof(UpdatePost))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> UpdatePost([FromBody] Post model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _postService.UpdatePost(model);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpPut]
        [Route(nameof(ChangeStickyPost))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> ChangeStickyPost([FromBody] Post model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _postService.ChangeStickyPost(model.Id);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpDelete]
        [Route(nameof(DeletePost))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> DeletePost([FromQuery] long postId)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _postService.DeletePost(postId);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpPost]
        [Route(nameof(ReportPost))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> ReportPost([FromBody] Denuncia model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _postService.DenunciaPost(model);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpPost]
        [Route(nameof(AddFavoritePost))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> AddFavoritePost([FromBody] Post model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _postService.AddFavoritePost(model.Id);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpGet]
        [Route(nameof(GetRelatedPosts))]
        public async Task<JSONObjectResult> GetRelatedPosts([FromQuery] long postId)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _postService.GetRelatedPosts(postId);
                var mapped = _mapper.Map<List<PostViewModel>>(data);

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
        [Route(nameof(GetPostsByUserId))]
        public async Task<JSONObjectResult> GetPostsByUserId([FromQuery] QueryParamsHelper queryParameters)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _postService.GetPostsByUserId(queryParameters, out long totalCount);
                var mapped = data.Select(x => new
                {
                    x.Id,
                    x.Titulo,
                    categoria = new
                    {
                        icono = x.Categoria.Icono,
                        nombre = x.Categoria.Nombre,
                        seo = x.Categoria.SEO
                    },
                    x.Sticky,
                    x.EsPrivado
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
    }
}
