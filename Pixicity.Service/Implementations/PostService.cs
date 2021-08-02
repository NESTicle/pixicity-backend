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

namespace Pixicity.Service.Implementations
{
    public class PostService : IPostService
    {
        private readonly PixicityDbContext _dbContext;
        private IAppPrincipal _currentUser { get; }

        public PostService(PixicityDbContext dbContext, IAppPrincipal currentUser)
        {
            _dbContext = dbContext;
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

        public bool DeletePost(long postId)
        {
            try
            {
                Post post = GetPostSimpleById(postId);

                if (post == null)
                    throw new Exception("Un error ha ocurrido al tratar de eliminar un post");

                if(post.UsuarioId != _currentUser.Id) // TODO: Poder eliminar los posts si eres un Administrador
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
    }
}
