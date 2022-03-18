using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pixicity.Data;
using Pixicity.Data.Models.Parametros;
using Pixicity.Data.Models.Posts;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Data.Models.Web;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.Transversal;
using Pixicity.Domain.ViewModels.Posts;
using Pixicity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Pixicity.Domain.Enums.Enums;

namespace Pixicity.Service.Implementations
{
    public class PostService : IPostService
    {
        private readonly PixicityDbContext _dbContext;
        private readonly ISeguridadService _seguridadService;
        private readonly ILogsService _logsService;
        private readonly IMapper _mapper;
        private IAppPrincipal _currentUser { get; }

        public PostService(PixicityDbContext dbContext, ISeguridadService seguridadService, ILogsService logsService, IMapper mapper, IAppPrincipal currentUser)
        {
            _dbContext = dbContext;
            _seguridadService = seguridadService;
            _logsService = logsService;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public long CountPosts()
        {
            try
            {
                return _dbContext.Post.Where(x => x.Eliminado == false && x.EsBorrador == false).Count();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Post> GetPosts(QueryParamsHelper queryParameters, out long totalCount)
        {
            try
            {
                var posts = _dbContext.Post
                    .AsNoTracking()
                    .Include(x => x.Categoria)
                    .Where(x => x.Eliminado == false && x.Sticky == false && x.EsBorrador == false);

                if (!string.IsNullOrEmpty(queryParameters.Query))
                    posts = posts.Where(x => x.Categoria.SEO == queryParameters.Query);

                totalCount = posts.Count();

                return posts
                    .OrderByDescending(x => x.FechaRegistro)
                    .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                    .Take(queryParameters.PageCount)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Post> SearchPosts(QueryParamsHelper queryParameters, PostViewModel search, out long totalCount)
        {
            try
            {
                var posts = _dbContext.Post
                    .AsNoTracking()
                    .Include(x => x.Categoria)
                    .Include(x => x.Comentarios)
                    .Include(x => x.Usuario)
                    .Where(x => x.Eliminado == false && x.EsBorrador == false);

                if (search != null)
                {
                    search.Search = search.Search?.Trim()?.ToLower();

                    switch (search.SearchType)
                    {
                        case "titulo":
                            posts = posts.Where(x => x.Titulo.ToLower().Contains(search.Search));
                            break;

                        case "contenido":
                            posts = posts.Where(x => x.Contenido.ToLower().Contains(search.Search));
                            break;

                        case "tags":
                            posts = posts.Where(x => x.Etiquetas.ToLower().Contains(search.Search));
                            break;
                    }

                    if (search.CategoriaId.HasValue)
                        posts = posts.Where(x => x.CategoriaId == search.CategoriaId.Value);

                    if (!string.IsNullOrEmpty(search.Autor))
                        posts = posts.Where(x => x.Usuario.UserName.ToLower().Contains(search.Autor));
                }

                totalCount = posts.Count();

                return posts
                    .OrderByDescending(x => x.FechaRegistro)
                    .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                    .Take(queryParameters.PageCount)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Post> GetPostsByUserId(QueryParamsHelper queryParameters, out long totalCount)
        {
            try
            {
                long userId = long.Parse(queryParameters.Query);

                var posts = _dbContext.Post
                    .AsNoTracking()
                    .Include(x => x.Categoria)
                    .Where(x => x.UsuarioId == userId && x.Eliminado == false && x.EsBorrador == false);

                totalCount = posts.Count();

                return posts
                    .OrderByDescending(x => x.FechaRegistro)
                    .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                    .Take(queryParameters.PageCount)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Post> GetPostsAdmin(QueryParamsHelper queryParameters, out long totalCount)
        {
            try
            {
                var posts = _dbContext.Post
                    .AsNoTracking()
                    .Include(x => x.Usuario)
                    .Include(x => x.Categoria)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(queryParameters.Query))
                {
                    string query = queryParameters.Query.Trim().ToLower();
                    posts = posts.Where(x => x.Categoria.Nombre.ToLower().Contains(query) || x.Titulo.ToLower().Contains(query) || x.Contenido.ToLower().Contains(query));
                }

                totalCount = posts.Count();

                return posts
                    .OrderByDescending(x => x.FechaRegistro)
                    .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                    .Take(queryParameters.PageCount)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Post> GetPostsByLoggedUser(QueryParamsHelper queryParameters, out long totalCount)
        {
            try
            {
                var posts = _dbContext.Post
                    .AsNoTracking()
                    .Include(x => x.Categoria)
                    .Where(x => x.Eliminado == false && x.UsuarioId == _currentUser.Id && x.EsBorrador == false);

                if (!string.IsNullOrEmpty(queryParameters.Query))
                {
                    string query = queryParameters.Query.Trim().ToLower();
                    posts = posts.Where(x => x.Categoria.Nombre.ToLower().Contains(query) || x.Titulo.ToLower().Contains(query) || x.Contenido.ToLower().Contains(query));
                }

                totalCount = posts.Count();

                return posts
                    .OrderByDescending(x => x.FechaRegistro)
                    .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                    .Take(queryParameters.PageCount)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Post> GetStickyPosts()
        {
            try
            {
                return _dbContext.Post
                    .AsNoTracking()
                    .Include(x => x.Categoria)
                    .Where(x => x.Eliminado == false && x.Sticky == true && x.EsBorrador == false)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Post GetPostById(long postId)
        {
            try
            {
                return _dbContext.Post
                    .AsNoTracking()
                    .Include(x => x.Categoria)
                    .Include(x => x.FavoritosPosts)
                    .Include(x => x.Usuario.Estado.Pais)
                    .Include(x => x.Usuario.Rango)
                    .FirstOrDefault(x => x.Id == postId && x.Eliminado == false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Post NextPost(long postId)
        {
            try
            {
                return _dbContext.Post
                    .AsNoTracking()
                    .Include(x => x.Categoria)
                    .Include(x => x.Usuario.Estado.Pais)
                    .FirstOrDefault(x => x.Id > postId && x.Eliminado == false && x.EsBorrador == false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Post PreviousPost(long postId)
        {
            try
            {
                return _dbContext.Post
                    .AsNoTracking()
                    .Include(x => x.Categoria)
                    .Include(x => x.Usuario.Estado.Pais)
                    .Where(x => x.Id < postId && x.Eliminado == false && x.EsBorrador == false)
                    .OrderByDescending(y => y.Id)
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Post RandomPost(long postId)
        {
            try
            {
                Random rand = new Random();
                int toSkip = rand.Next(0, _dbContext.Post.Count());

                return _dbContext.Post
                    .AsNoTracking()
                    .Include(x => x.Categoria)
                    .Include(x => x.Usuario.Estado.Pais)
                    .Where(x => x.Id != postId && x.Eliminado == false && x.EsBorrador == false)
                    .Skip(toSkip)
                    .Take(1)
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Post GetPostSimpleById(long postId)
        {
            try
            {
                return _dbContext.Post
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == postId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long SavePost(Post model)
        {
            try
            {
                model.UsuarioId = _currentUser.Id;
                model.Puntos = 0;
                model.IP = _currentUser.IP;
                model.UsuarioRegistra = _currentUser.UserName;

                _dbContext.Post.Add(model);
                _dbContext.SaveChanges();

                _seguridadService.SaveActividadUsuario(new Actividad()
                {
                    UsuarioId = model.UsuarioId,
                    ObjId1 = model.Id,
                    TipoActividad = TipoActividad.PostNuevo
                });

                return model.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long UpdatePost(Post model)
        {
            try
            {
                Post post = GetPostSimpleById(model.Id);

                if (_currentUser.IsAdmin == false && _currentUser.IsModerador == false)
                    if (post.UsuarioId != _currentUser.Id)
                        throw new Exception("Oye cerebrito!, no puedes hacer eso aquí");

                post.Titulo = model.Titulo;
                post.Contenido = model.Contenido;
                post.CategoriaId = model.CategoriaId;
                post.EsPrivado = model.EsPrivado;
                post.Smileys = model.Smileys;
                post.SinComentarios = model.SinComentarios;
                post.EsBorrador = model.EsBorrador;

                post.FechaActualiza = DateTime.Now;
                post.UsuarioActualiza = _currentUser.UserName;

                _dbContext.Update(post);
                _dbContext.SaveChanges();

                if (_currentUser.IsAdmin || _currentUser.IsModerador)
                {
                    SaveHistorial(new Historial()
                    {
                        Accion = "Actualizado",
                        Razon = "-",
                        TipoId = post.Id,
                        Tipo = TipoHistorial.Post,
                        IP = _currentUser.IP
                    });
                }

                return post.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long UpdatePostEF(Post model)
        {
            try
            {
                _dbContext.Update(model);
                _dbContext.SaveChanges();

                return model.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long ChangeStickyPost(long postId)
        {
            try
            {
                Post post = GetPostSimpleById(postId);

                if (post == null)
                    throw new Exception("Un error ha ocurrido obteniendo el post");

                post.Sticky = !post.Sticky;
                _dbContext.Update(post);
                _dbContext.SaveChanges();

                return postId;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DeletePost(long postId)
        {
            try
            {
                Post post = GetPostSimpleById(postId);

                if (post == null)
                    throw new Exception("Un error ha ocurrido al tratar de eliminar un post");

                if (_currentUser.IsAdmin == false && _currentUser.IsModerador == false)
                    if (post.UsuarioId != _currentUser.Id)
                        throw new Exception("Oye cerebrito!, no puedes hacer eso aquí");

                post.Eliminado = !post.Eliminado;

                _dbContext.Update(post);
                _dbContext.SaveChanges();

                if (_currentUser.IsAdmin || _currentUser.IsModerador)
                {
                    SaveHistorial(new Historial()
                    {
                        Accion = "Eliminado",
                        Razon = "Pendiente",
                        TipoId = post.Id,
                        Tipo = TipoHistorial.Post,
                        IP = _currentUser.IP
                    });
                }

                return post.Eliminado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long AddComentario(Comentario model)
        {
            try
            {
                model.UsuarioId = _currentUser.Id;
                model.IP = _currentUser.IP;

                _dbContext.Comentario.Add(model);
                _dbContext.SaveChanges();

                Post post = GetPostSimpleById(model.PostId);

                if (post.UsuarioId != _currentUser.Id)
                {
                    _logsService.SaveMonitor(new Data.Models.Logs.Monitor()
                    {
                        UsuarioId = post.UsuarioId,
                        UsuarioQueHaceAccionId = _currentUser.Id,
                        Tipo = TipoMonitor.Comentario,
                        Mensaje = $"Comentó tu post",
                        PostId = model.PostId
                    });
                }

                // Notificar a todos los usuarios que siguen el post
                var listaUsuarios = GetUsuariosQueSiguenPost(model.PostId);

                if (listaUsuarios != null && listaUsuarios.Count > 0)
                {
                    foreach (var usuario in listaUsuarios.Where(x => x.Id != _currentUser.Id))
                    {
                        _logsService.SaveMonitor(new Data.Models.Logs.Monitor()
                        {
                            UsuarioId = usuario.Id,
                            UsuarioQueHaceAccionId = _currentUser.Id,
                            Tipo = TipoMonitor.ComentarioSiguePost,
                            Mensaje = $"Agregó un comentario en un post que sigues",
                            PostId = model.PostId
                        });
                    }
                }

                // Agregar comentario a la actividad del usuario

                _seguridadService.SaveActividadUsuario(new Actividad()
                {
                    UsuarioId = model.UsuarioId,
                    ObjId1 = post.Id,
                    TipoActividad = TipoActividad.ComentarioNuevo
                });

                return model.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int DeleteComentario(long comentarioId)
        {
            try
            {
                if (comentarioId < 1)
                    throw new Exception("No se ha podido eliminar el comentario");

                Comentario comentario = _dbContext.Comentario.Where(x => x.Id == comentarioId).FirstOrDefault();

                if (comentario == null)
                    throw new Exception("No se ha podido eliminar el comentario");

                if (comentario.UsuarioId != _currentUser.Id)
                    if (!_currentUser.IsModerador && !_currentUser.IsAdmin)
                        throw new Exception("Oye cerebrito!, no puedes hacer eso aquí");

                comentario.Eliminado = !comentario.Eliminado;

                _dbContext.Update(comentario);
                return _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Comentario> GetComentarios(QueryParamsHelper queryParameters, out long totalCount)
        {
            try
            {
                var posts = _dbContext.Comentario
                    .Include(x => x.Usuario)
                    .Include(x => x.Post.Categoria)
                    .AsNoTracking()
                    .AsQueryable();

                totalCount = posts.Count();

                return posts
                    .OrderByDescending(x => x.FechaComentario)
                    .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                    .Take(queryParameters.PageCount)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Comentario> GetComentariosByUserId(QueryParamsHelper queryParameters, out long totalCount)
        {
            try
            {
                if (string.IsNullOrEmpty(queryParameters.Query))
                    throw new Exception("El parámetro query es requerido para esta consulta");

                long userId = long.Parse(queryParameters.Query);

                var posts = _dbContext.Comentario
                    .Include(x => x.Usuario)
                    .Include(x => x.Post.Categoria)
                    .AsNoTracking()
                    .Where(x => x.UsuarioId == userId && x.Eliminado == false);

                totalCount = posts.Count();

                return posts
                    .OrderByDescending(x => x.FechaComentario)
                    .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                    .Take(queryParameters.PageCount)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Comentario> GetComentariosRecientes()
        {
            try
            {
                return _dbContext.Comentario
                    .AsNoTracking()
                    .Include(x => x.Usuario)
                    .Include(x => x.Post.Categoria)
                    .Where(x => x.Eliminado == false)
                    .OrderByDescending(x => x.Id)
                    .Take(10)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Comentario> GetComentariosByPostId(long postId)
        {
            try
            {
                return _dbContext.Comentario
                    .AsNoTracking()
                    .Include(x => x.Usuario)
                    .Where(x => x.Eliminado == false && x.PostId == postId)
                    .OrderBy(x => x.FechaComentario)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long CountComentarios()
        {
            try
            {
                return _dbContext.Comentario.Count();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long SetVoto(Voto model)
        {
            try
            {
                model.UsuarioId = _currentUser.Id;

                if (GetAvailableVotos(VotosTypeEnum.Posts) <= 0)
                    throw new Exception("Ya no tienes puntos para sumar");

                var post = GetPostSimpleById(model.TypeId);

                if (post.UsuarioId == model.UsuarioId)
                    throw new Exception("No es posible votar a tu mismo post");

                var votos = GetVotosByUsuarioTypeId(model);

                if (votos != null && votos.Count > 0)
                    throw new Exception("No es posible votar a un mismo post más de una vez");

                Voto voto = new Voto()
                {
                    UsuarioId = model.UsuarioId,
                    Cantidad = model.Cantidad,
                    TypeId = model.TypeId,
                    VotosType = model.VotosType
                };

                _dbContext.Voto.Add(voto);
                _dbContext.SaveChanges();

                if (model.VotosType == VotosTypeEnum.Posts)
                {
                    if (post != null)
                    {
                        post.Puntos += model.Cantidad;
                        UpdatePostEF(post);

                        _logsService.SaveMonitor(new Data.Models.Logs.Monitor()
                        {
                            UsuarioId = post.UsuarioId,
                            UsuarioQueHaceAccionId = model.UsuarioId,
                            Tipo = TipoMonitor.Puntos,
                            Mensaje = $"Dejó <b>{model.Cantidad}</b> puntos en tu post",
                            PostId = post.Id
                        });

                        _seguridadService.SumarPuntosUsuario(post.UsuarioId, model.Cantidad);

                        _seguridadService.SaveActividadUsuario(new Actividad()
                        {
                            UsuarioId = model.UsuarioId,
                            ObjId1 = post.UsuarioId,
                            Datos = model.Cantidad.ToString(),
                            TipoActividad = TipoActividad.PostVotado
                        });
                    }
                }

                return voto.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Voto> GetVotosByUsuarioId(long usuarioId)
        {
            try
            {
                return _dbContext.Voto.Where(x => x.UsuarioId == usuarioId).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Voto> GetCurrentVotosByUsuarioId(long usuarioId, string type)
        {
            try
            {
                var finalDia = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
                var inicioDia = DateTime.Now.Date; // .AddTicks(-1)

                var votos = _dbContext.Voto.Where(x => x.UsuarioId == usuarioId && x.TipoString == type && x.Fecha.Date >= inicioDia && x.Fecha.Date <= finalDia);
                return votos.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int GetAvailableVotos(VotosTypeEnum type)
        {
            try
            {
                var votos = GetCurrentVotosByUsuarioId(_currentUser.Id, type.ToString());

                if (votos == null || votos.Count < 1)
                    return 10; // TODO: 10 puntos disponibles por dias, seria interesante poderlo parametrizar desde el panel administrativo

                int sumVotos = votos.Sum(x => x.Cantidad);
                int currentVotos = 10 - sumVotos;

                return currentVotos < 0 ? 0 : currentVotos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Denuncia> GetDenuncias(QueryParamsHelper queryParameters, out long totalCount)
        {
            try
            {
                var query = _dbContext.Denuncia
                    .AsNoTracking()
                    .Include(x => x.Post.Categoria)
                    .Include(x => x.Usuario)
                    .Where(x => x.Eliminado == false);

                totalCount = query.Count();

                return query
                    .OrderByDescending(x => x.FechaRegistro)
                    .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                    .Take(queryParameters.PageCount)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long DeleteDenuncia(long id)
        {
            try
            {
                if (id < 1)
                    throw new Exception("Hubo un error al eliminar una denuncia");

                Denuncia denuncia = _dbContext.Denuncia.FirstOrDefault(x => x.Id == id);

                if (denuncia == null)
                    throw new Exception($"No se ha podido eliminar la denuncia con id {id}");

                denuncia.Eliminado = true;

                _dbContext.Update(denuncia);
                _dbContext.SaveChanges();

                return denuncia.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Voto> GetVotosByUsuarioTypeId(Voto voto)
        {
            try
            {
                var votos = _dbContext.Voto.Where(x => x.UsuarioId == voto.UsuarioId && x.TypeId == voto.TypeId);
                return votos.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long DenunciaPost(Denuncia model)
        {
            try
            {
                Denuncia entity = _dbContext.Denuncia.FirstOrDefault(x => x.PostId == model.PostId && x.UsuarioRegistra == _currentUser.UserName && x.Eliminado == false);

                if (entity != null)
                    throw new Exception("No puedes denunciar un post más de una vez");

                Denuncia denuncia = new Denuncia()
                {
                    UsuarioDenunciaId = _currentUser.Id,
                    PostId = model.PostId,
                    RazonDenunciaId = model.RazonDenunciaId,
                    Comentarios = model.Comentarios,
                    UsuarioRegistra = _currentUser.UserName
                };

                _dbContext.Denuncia.Add(denuncia);
                _dbContext.SaveChanges();

                EliminarPostDenunciado(model.PostId, 8, 2);

                return denuncia.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public FavoritoPost SearchFavoritoPost(FavoritoPost model)
        {
            try
            {
                var query = _dbContext.FavoritoPost.AsQueryable();

                if (model.PostId > 0)
                    query = query.Where(x => x.PostId == model.PostId);

                if (model.UsuarioId > 0)
                    query = query.Where(x => x.UsuarioId == model.UsuarioId);

                return query.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long AddFavoritePost(long postId)
        {
            try
            {
                FavoritoPost favoritoPost = new FavoritoPost()
                {
                    PostId = postId,
                    UsuarioId = _currentUser.Id
                };

                FavoritoPost search = SearchFavoritoPost(favoritoPost);

                if (search != null)
                {
                    if (search.Eliminado == false)
                        return 0;
                    else
                    {
                        ChangeDeleteFavorito(search.Id);
                        return search.Id;
                    }
                }

                _dbContext.FavoritoPost.Add(favoritoPost);
                _dbContext.SaveChanges();

                _seguridadService.SaveActividadUsuario(new Actividad()
                {
                    UsuarioId = _currentUser.Id,
                    ObjId1 = postId,
                    TipoActividad = TipoActividad.PostFavorito
                });

                return favoritoPost.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<FavoritoPost> GetFavoritos(QueryParamsHelper queryParameters, long categoriaId, out long totalCount)
        {
            try
            {
                var posts = _dbContext.FavoritoPost
                    .AsNoTracking()
                    .Include(x => x.Post.Categoria)
                    .Where(x => x.UsuarioId == _currentUser.Id && x.Post.Eliminado == false && x.Eliminado == false);

                if (!string.IsNullOrEmpty(queryParameters.Query))
                    posts = posts.Where(x => x.Post.Titulo.ToLower().Contains(queryParameters.Query.ToLower()));

                if (categoriaId > 0)
                    posts = posts.Where(x => x.Post.Categoria.Id == categoriaId);

                totalCount = posts.Count();

                return posts
                    .OrderByDescending(x => x.FechaRegistro)
                    .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                    .Take(queryParameters.PageCount)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<FavoritoPost> GetLastFavoritos(int count)
        {
            try
            {
                var posts = _dbContext.FavoritoPost
                    .AsNoTracking()
                    .Include(x => x.Post.Categoria)
                    .Where(x => x.UsuarioId == _currentUser.Id && x.Post.Eliminado == false && x.Eliminado == false);

                if (count > 50)
                    count = 50;

                return posts
                    .OrderByDescending(x => x.FechaRegistro)
                    .Take(count)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ChangeDeleteFavorito(long favoritoId)
        {
            try
            {
                FavoritoPost favorito = _dbContext.FavoritoPost.FirstOrDefault(x => x.Id == favoritoId);

                if (favorito == null)
                    throw new Exception("No se ha podido cambiar el estado al favorito ya que no existe el registro");

                if (favorito.UsuarioId != _currentUser.Id)
                    throw new Exception("Oye cerebrito!, no puedes hacer eso aquí");

                favorito.Eliminado = !favorito.Eliminado;
                favorito.FechaElimina = DateTime.Now;
                favorito.UsuarioElimina = _currentUser.UserName;

                if (favorito.Eliminado == false)
                    favorito.FechaRegistro = DateTime.Now;

                _dbContext.Update(favorito);
                _dbContext.SaveChanges();

                return favorito.Eliminado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Post> GetRelatedPosts(long postId)
        {
            try
            {
                var post = _dbContext.Post.FirstOrDefault(x => x.Id == postId);

                if (post == null)
                    return new List<Post>();

                var relatedPosts = _dbContext.Post
                    .AsNoTracking()
                    .Include(x => x.Categoria)
                    .Where(x => x.Id != post.Id && x.Titulo.ToLower().Contains(post.Titulo.ToLower()))
                    .Take(5)
                    .ToList();

                return relatedPosts;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int PostsCountByUserId(long userId)
        {
            try
            {
                return _dbContext.Post.Count(x => x.UsuarioId == userId && x.Eliminado == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TopsViewModel GetTopPosts(string date, long? categoria)
        {
            try
            {
                TopsViewModel topPosts = new TopsViewModel();

                var topPuntos = _dbContext.Post
                    .AsNoTracking()
                    .Include(x => x.Categoria)
                    .Where(x => x.Eliminado == false && x.EsBorrador == false)
                    .AsQueryable();

                topPuntos = FilterTopPosts(date, topPuntos);

                if (categoria.HasValue)
                    topPuntos = topPuntos.Where(x => x.CategoriaId == categoria.Value);

                topPuntos = topPuntos
                    .OrderByDescending(s => s.Puntos)
                    .Take(10);

                topPosts.PostsConMasPuntos = _mapper.Map<List<PostViewModel>>(topPuntos.ToList());

                var topFavoritos = _dbContext.Post
                    .AsNoTracking()
                    .Include(x => x.Categoria)
                    .Include(x => x.FavoritosPosts)
                    .Where(x => x.Eliminado == false && x.EsBorrador == false && x.FavoritosPosts.Any(x => x.Eliminado == false)) // && x.FavoritosPosts.Count > 0
                    .AsQueryable();

                topFavoritos = FilterTopPosts(date, topFavoritos);

                if (categoria.HasValue)
                    topFavoritos = topFavoritos.Where(x => x.CategoriaId == categoria.Value);

                topFavoritos = topFavoritos
                    .OrderByDescending(x => x.FavoritosPosts.Count)
                    .Take(10);

                topPosts.PostsConMasFavoritos = _mapper.Map<List<PostViewModel>>(topFavoritos.ToList());

                var topComentarios = _dbContext.Post
                    .AsNoTracking()
                    .Include(x => x.Categoria)
                    .Include(x => x.Comentarios)
                    .Where(x => x.Eliminado == false && x.EsBorrador == false)
                    .AsQueryable();

                topComentarios = FilterTopPosts(date, topComentarios);

                if (categoria.HasValue)
                    topComentarios = topComentarios.Where(x => x.CategoriaId == categoria.Value);

                topComentarios = topComentarios
                    .OrderByDescending(o => o.Comentarios.Count)
                    .Take(10);

                topPosts.PostsConMasComentarios = _mapper.Map<List<PostViewModel>>(topComentarios.ToList());

                var topPostSeguidores = _dbContext.Post
                    .AsNoTracking()
                    .Include(x => x.Categoria)
                    .Include(x => x.SeguirPosts)
                    .Where(x => x.Eliminado == false && x.EsBorrador == false && x.SeguirPosts.Any(x => x.Eliminado == false)) // && x.SeguirPosts.Count > 0
                    .AsQueryable();

                topPostSeguidores = FilterTopPosts(date, topPostSeguidores);

                if (categoria.HasValue)
                    topPostSeguidores = topPostSeguidores.Where(x => x.CategoriaId == categoria.Value);

                topPostSeguidores = topPostSeguidores
                    .OrderByDescending(x => x.SeguirPosts.Count)
                    .Take(10);

                topPosts.PostsConMasSeguidores = _mapper.Map<List<PostViewModel>>(topPostSeguidores.ToList());

                return topPosts;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long SeguirPost(long postId)
        {
            try
            {
                if (postId <= 0)
                    throw new Exception("El id del post no es válido");

                if (_currentUser == null || _currentUser.Id <= 0)
                    throw new Exception("Necesitas estar logeado para poder realizar esta acción");

                SeguirPost seguirPost = _dbContext.SeguirPost.FirstOrDefault(x => x.PostId == postId && x.UsuarioId == _currentUser.Id);

                if (seguirPost == null)
                {
                    seguirPost = new SeguirPost()
                    {
                        PostId = postId,
                        UsuarioId = _currentUser.Id,
                        Eliminado = false
                    };

                    _dbContext.SeguirPost.Add(seguirPost);

                    _seguridadService.SaveActividadUsuario(new Actividad()
                    {
                        UsuarioId = _currentUser.Id,
                        ObjId1 = postId,
                        TipoActividad = TipoActividad.SiguiendoPost
                    });
                }
                else
                {
                    seguirPost.Eliminado = !seguirPost.Eliminado;
                    seguirPost.FechaRegistro = DateTime.Now;

                    _dbContext.Update(seguirPost);
                }

                _dbContext.SaveChanges();

                return seguirPost.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool IsFollowingPost(long postId, string userName)
        {
            try
            {
                if (postId <= 0)
                    return false;

                if (string.IsNullOrEmpty(userName))
                    return false;

                SeguirPost seguirPost = _dbContext.SeguirPost
                    .Include(x => x.Usuario)
                    .FirstOrDefault(x => x.PostId == postId && x.Eliminado == false && x.Usuario.UserName.ToLower() == userName.ToLower().Trim());

                return seguirPost != null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CloudTagsViewModel> GetCloudTags()
        {
            try
            {
                var posts = _dbContext.Post
                    .Where(x => x.Eliminado == false)
                    .OrderByDescending(x => x.Id)
                    .Take(20);

                var tags = posts.Select(x => x.Etiquetas).ToList();

                if (tags.Count <= 0)
                    return new List<CloudTagsViewModel>();

                List<string> stringTags = string.Join(",", tags).Split(",").ToList();

                var group = stringTags.GroupBy(x => x).Select(x => new CloudTagsViewModel()
                {
                    Tag = x.Key,
                    Count = x.Count()
                }).OrderByDescending(x => x.Count)
                .Take(15)
                .OrderBy(x => x.Tag)
                .ToList();

                return group;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Visitas GetVisitaPost(long postId, string IP, long? usuarioId)
        {
            try
            {
                var visita = _dbContext.Visitas
                    .Where(x => x.Eliminado == false && x.TypeId == postId);

                if (usuarioId >= 0)
                    visita = visita.Where(x => x.UsuarioId == usuarioId);
                else
                    visita = visita.Where(x => x.IP == IP);

                return visita.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long SetVisitaToPostUsuario(long postId, string IP, string userName)
        {
            try
            {
                if (postId <= 0)
                    return 0;

                long? usuarioId = null;

                if (!string.IsNullOrEmpty(userName))
                {
                    Usuario usuario = _seguridadService.GetUsuarioByUserName(userName);
                    usuarioId = usuario?.Id;
                }

                if (GetVisitaPost(postId, IP, usuarioId) != null)
                    return 1;

                Visitas visita = new Visitas()
                {
                    TipoVisitas = VisitasTypeEnum.Posts,
                    TypeId = postId,
                    IP = IP,
                    UsuarioId = usuarioId,
                };

                _dbContext.Visitas.Add(visita);

                Post post = _dbContext.Post.FirstOrDefault(x => x.Id == postId);
                post.Visitantes++;

                _dbContext.Update(post);
                _dbContext.SaveChanges();

                return visita.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Usuario> GetUsuariosQueSiguenPost(long postId)
        {
            try
            {
                var usuarios = _dbContext.SeguirPost
                    .Include(x => x.Usuario)
                    .Where(x => x.PostId == postId && x.Eliminado == false)
                    .Select(x => x.Usuario)
                    .ToList();

                return usuarios;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int GetCountUsuariosQueSiguenPost(long postId)
        {
            try
            {
                return _dbContext.SeguirPost.Count(x => x.PostId == postId && x.Eliminado == false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IQueryable<Post> FilterTopPosts(string date, IQueryable<Post> posts)
        {
            try
            {
                if (string.IsNullOrEmpty(date) || date == "all")
                    return posts;

                DateTime today = DateTime.Today;
                DateTime yesterday = today.AddDays(-1);

                switch (date)
                {
                    case "ayer":
                        return posts.Where(x => x.FechaRegistro >= yesterday && x.FechaRegistro < today);
                    case "hoy":
                        DateTime endDateTime = today.AddDays(1).AddTicks(-1);
                        return posts.Where(x => x.FechaRegistro >= today && x.FechaRegistro <= endDateTime);
                    case "ultimos7dias":
                        DateTime last7Days = today.AddDays(-7);
                        return posts.Where(x => x.FechaRegistro >= today && x.FechaRegistro <= last7Days);
                    case "mes":
                        DateTime last30Days = today.AddDays(-30);
                        return posts.Where(x => x.FechaRegistro >= today && x.FechaRegistro <= last30Days);
                    default:
                        return posts;
                }
            }
            catch (Exception)
            {
                return posts;
            }
        }

        public long SaveHistorial(Historial model)
        {
            try
            {
                if (model == null)
                    return 0;

                model.Id = 0;
                model.FechaRegistro = DateTime.Now;
                model.UsuarioRegistra = _currentUser.UserName;

                _dbContext.Historial.Add(model);
                _dbContext.SaveChanges();

                return model.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool EliminarPostDenunciado(long postId, int cantidadDenuncias, int dias = 1)
        {
            try
            {
                if (postId <= 0)
                    return false;

                DateTime today = DateTime.Today.AddDays(1).AddTicks(-1);
                DateTime days = today.AddDays(-dias); // Rango de fecha desde donde se va a evaluar si se elimina o no los posts

                List<Denuncia> denuncias = _dbContext.Denuncia
                    .AsNoTracking()
                    .Where(x => x.PostId == postId && x.Eliminado == false && x.FechaRegistro >= days && x.FechaRegistro < today)
                    .ToList();

                if (denuncias.Count > cantidadDenuncias)
                {
                    Post post = _dbContext.Post.FirstOrDefault(x => x.Id == postId);

                    if (post == null)
                        return false;

                    post.Eliminado = true;

                    _dbContext.Update(post);
                    _dbContext.SaveChanges();

                    SaveHistorial(new Historial()
                    {
                        Accion = "Eliminado",
                        Razon = "Por acumulación de denuncias",
                        TipoId = post.Id,
                        Tipo = TipoHistorial.Post,
                        IP = _currentUser.IP
                    });

                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Post> GetBorradores(QueryParamsHelper queryParameters, long categoriaId, out long totalCount)
        {
            try
            {
                var posts = _dbContext.Post
                    .AsNoTracking()
                    .Include(x => x.Categoria)
                    .Where(x => x.Eliminado == false && x.UsuarioId == _currentUser.Id && x.EsBorrador == true);

                if (categoriaId > 0)
                {
                    posts = posts.Where(x => x.CategoriaId == categoriaId);
                }

                if (!string.IsNullOrEmpty(queryParameters.Query))
                {
                    string query = queryParameters.Query.Trim().ToLower();
                    posts = posts.Where(x => x.Titulo.ToLower().Contains(query) || x.Contenido.ToLower().Contains(query));
                }

                totalCount = posts.Count();

                return posts
                    .OrderByDescending(x => x.FechaRegistro)
                    .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                    .Take(queryParameters.PageCount)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
