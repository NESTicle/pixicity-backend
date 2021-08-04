using Microsoft.EntityFrameworkCore;
using Pixicity.Data;
using Pixicity.Data.Models.Posts;
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
        private IAppPrincipal _currentUser { get; }

        public PostService(PixicityDbContext dbContext, ISeguridadService seguridadService, IAppPrincipal currentUser)
        {
            _dbContext = dbContext;
            _seguridadService = seguridadService;
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

                post.Eliminado = true;

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

                return model.Id;
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
    }
}
