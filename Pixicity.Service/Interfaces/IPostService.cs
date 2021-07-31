using Pixicity.Data.Models.Posts;
using Pixicity.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Service.Interfaces
{
    public interface IPostService
    {
        /// <summary>
        /// Obtiene la lista de posts paginada
        /// </summary>
        /// <param name="queryParameters">Helper utilizado para la paginación</param>
        /// <param name="totalCount">Numero total de Posts en el Sistema</param>
        /// <returns></returns>
        List<Post> GetPosts(QueryParamsHelper queryParameters, out long totalCount);

        /// <summary>
        /// Guarda el Post en el Sistema
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long SavePost(Post model);
    }
}
