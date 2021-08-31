using Pixicity.Data.Models.Posts;
using Pixicity.Data.Models.Web;
using Pixicity.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static Pixicity.Domain.Enums.Enums;

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
        /// Obtener el siguiente post a partir de un id
        /// </summary>
        /// <param name="postId">Id del post</param>
        /// <returns></returns>
        Post NextPost(long postId);

        /// <summary>
        /// Obtener el anterior post a partir de un id
        /// </summary>
        /// <param name="postId">Id del post</param>
        /// <returns></returns>
        Post PreviousPost(long postId);

        /// <summary>
        /// Obtener un post en aleatorio
        /// </summary>
        /// <param name="postId">Id del post</param>
        /// <returns></returns>
        Post RandomPost(long postId);

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
        /// Cambiar el estado de sticky para 
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        long ChangeStickyPost(long postId);

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

        /// <summary>
        /// Asignar votos al post o a las fotos
        /// </summary>
        /// <returns></returns>
        long SetVoto(Voto model);

        List<Voto> GetVotosByUsuarioId(long usuarioId);

        /// <summary>
        /// Obtiene los votos del día de hoy del usuario actual
        /// </summary>
        /// <param name="usuarioId">Id del Usuario</param>
        /// <param name="type">Tipo de Voto (Posts o Fotos)</param>
        /// <returns></returns>
        List<Voto> GetCurrentVotosByUsuarioId(long usuarioId, string type);

        /// <summary>
        /// Obtiene los votos que le quedan por realizar al día al usuario
        /// </summary>
        /// <param name="type">Tipo de Voto (Posts o Fotos)</param>
        /// <returns></returns>
        int GetAvailableVotos(VotosTypeEnum type);

        /// <summary>
        /// Registrar una nueva denuncia para el post
        /// </summary>
        /// <param name="denuncia">Entidad de denuncia</param>
        /// <returns></returns>
        long DenunciaPost(Denuncia denuncia);
    }
}
