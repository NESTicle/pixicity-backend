using Microsoft.EntityFrameworkCore;
using Pixicity.Data;
using Pixicity.Data.Models.Parametros;
using Pixicity.Data.Models.Posts;
using Pixicity.Data.Models.Web;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.Transversal;
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
        private IAppPrincipal _currentUser { get; }

        public PostService(PixicityDbContext dbContext, ISeguridadService seguridadService, ILogsService logsService, IAppPrincipal currentUser)
        {
            _dbContext = dbContext;
            _seguridadService = seguridadService;
            _logsService = logsService;
            _currentUser = currentUser;
        }

        public long CountPosts()
        {
            try
            {
                return _dbContext.Post.Where(x => x.Eliminado == false).Count();
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
                    .Where(x => x.Eliminado == false && x.Sticky == false);

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
                    .Where(x => x.Eliminado == false && x.UsuarioId == _currentUser.Id);

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
                    .Where(x => x.Eliminado == false && x.Sticky == true)
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
                    .Include(x => x.Usuario.Estado.Pais)
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
                    .FirstOrDefault(x => x.Id > postId && x.Eliminado == false);
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
                    .Where(x => x.Id < postId && x.Eliminado == false)
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
                    .Where(x => x.Id != postId && x.Eliminado == false)
                    .OrderBy(r => Guid.NewGuid())
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
                return _dbContext.Post.AsNoTracking().FirstOrDefault(x => x.Id == postId);
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

                _dbContext.Post.Add(model);
                _dbContext.SaveChanges();

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
                post.Contenido = model.Contenido;
                post.CategoriaId = model.CategoriaId;
                post.EsPrivado = model.EsPrivado;
                post.Smileys = model.Smileys;
                post.Titulo = model.Titulo;
                post.FechaActualiza = DateTime.Now;
                post.UsuarioActualiza = _currentUser.UserName;

                _dbContext.Update(post);
                _dbContext.SaveChanges();

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

                if (post.UsuarioId != _currentUser.Id) // TODO: Poder eliminar los posts si eres un Administrador
                    throw new Exception("Oye cerebrito!, no puedes hacer eso aquí");

                post.Eliminado = !post.Eliminado;

                _dbContext.Update(post);
                _dbContext.SaveChanges();

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

                return model.Id;
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

                if (GetPostSimpleById(model.UsuarioId).UsuarioId == model.UsuarioId)
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
                    Post post = GetPostSimpleById(model.TypeId);

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
                    }
                }

                _seguridadService.SumarPuntosUsuario(model.UsuarioId, model.Cantidad);

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

                if(search != null)
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

                if(post == null)
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
    }
}
