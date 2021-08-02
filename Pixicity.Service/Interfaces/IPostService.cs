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
        /// Obtiene la cantidad de posts publicados en la Comunidad
        /// </summary>
        /// <returns></returns>
        long CountPosts();

        /// <summary>
        /// Obtiene la lista de posts paginada
        /// </summary>
        /// <param name="queryParameters">Helper utilizado para la paginación</param>
        /// <param name="totalCount">Numero total de Posts en el Sistema</param>
        /// <returns></returns>
        List<Post> GetPosts(QueryParamsHelper queryParameters, out long totalCount);

        /// <summary>
        /// Obtiene una lista de posts fijados por los administradores
        /// </summary>
        /// <returns></returns>
        List<Post> GetStickyPosts();

        /// <summary>
        /// Obtiene un post filtrado por el Id
        /// </summary>
        /// <param name="postId">Id del Post</param>
        /// <returns></returns>
        Post GetPostById(long postId);

        /// <summary>
        /// Guarda el Post en el Sistema
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long SavePost(Post model);

        /// <summary>
        /// Actualizar el Post en el Sistema
        /// </summary>
        /// <param name="model">Entidad Post</param>
        /// <returns></returns>
        long UpdatePost(Post model);

        /// <summary>
        /// Cambia el estado de Eliminado de un Post
        /// </summary>
        /// <param name="postId">Id del Post</param>
        /// <returns></returns>
        bool DeletePost(long postId);

        /// <summary>
        /// Método para agregar comentario al Post
        /// </summary>
        /// <param name="model">Entidad Comentario</param>
        /// <returns></returns>
        long AddComentario(Comentario model);

        /// <summary>
        /// Obtener la lista de los últimos comentarios
        /// </summary>
        /// <returns></returns>
        List<Comentario> GetComentariosRecientes();

        /// <summary>
        /// Obtener la lista de comentarios por post
        /// </summary>
        /// <param name="postId">Id del Post</param>
        /// <returns></returns>
        List<Comentario> GetComentariosByPostId(long postId);
    }
}
