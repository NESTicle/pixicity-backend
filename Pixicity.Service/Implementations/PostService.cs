using Microsoft.EntityFrameworkCore;
using Pixicity.Data;
using Pixicity.Data.Models.Posts;
using Pixicity.Domain.Helpers;
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

        public PostService(PixicityDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public long SavePost(Post model)
        {
            try
            {
                _dbContext.Post.Add(model);
                _dbContext.SaveChanges();

                return model.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
