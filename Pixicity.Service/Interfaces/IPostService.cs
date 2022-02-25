using Pixicity.Data.Models.Parametros;
using Pixicity.Data.Models.Posts;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Data.Models.Web;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.ViewModels.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Buscar posts
        /// </summary>
        /// <param name="queryParameters">Helper utilizado para la paginación</param>
        /// <param name="search">Parámetros de búsqueda</param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<Post> SearchPosts(QueryParamsHelper queryParameters, PostViewModel search, out long totalCount);

        /// <summary>
        /// Obtener la lista de usuarios filtrado por el id del usuario
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<Post> GetPostsByUserId(QueryParamsHelper queryParameters, out long totalCount);

        /// <summary>
        /// Obtiene la lista de posts paginada para la sección de /administracion/posts
        /// </summary>
        /// <param name="queryParameters">Helper utilizado para la paginación</param>
        /// <param name="totalCount">Número total de Posts en el Sistema</param>
        /// <returns></returns>
        List<Post> GetPostsAdmin(QueryParamsHelper queryParameters, out long totalCount);

        /// <summary>
        /// Obtiene la lista de posts paginada filtrado por el usuario logeado
        /// </summary>
        /// <param name="queryParameters">Helper utilizado para la paginación</param>
        /// <param name="totalCount">Número total de Posts</param>
        /// <returns></returns>
        List<Post> GetPostsByLoggedUser(QueryParamsHelper queryParameters, out long totalCount);

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
        /// Obtiene la lista de todos los comentarios para visualizar en el dashboard
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<Comentario> GetComentarios(QueryParamsHelper queryParameters, out long totalCount);

        /// <summary>
        /// Obtiene la lista de comentarios filtrado por el id del usuario
        /// </summary>
        /// <param name="queryParameters">Filtros de búsqueda</param>
        /// <param name="totalCount">Cantidad total de comentarios del usuario</param>
        /// <returns></returns>
        List<Comentario> GetComentariosByUserId(QueryParamsHelper queryParameters, out long totalCount);

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
        /// Obtener la cantidad de comentarios realizados en la comunidad
        /// </summary>
        /// <returns></returns>
        long CountComentarios();

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
        /// Obtiene la lista de denuncias de posts para visualizarlas en el dashboard
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<Denuncia> GetDenuncias(QueryParamsHelper queryParameters, out long totalCount);

        /// <summary>
        /// Registrar una nueva denuncia para el post
        /// </summary>
        /// <param name="denuncia">Entidad de denuncia</param>
        /// <returns></returns>
        long DenunciaPost(Denuncia denuncia);

        /// <summary>
        /// Buscar un favorito post
        /// </summary>
        /// <param name="model">Entidad de FavoritoPost</param>
        /// <returns></returns>
        FavoritoPost SearchFavoritoPost(FavoritoPost model);

        /// <summary>
        /// Agrega el post a favoritos
        /// </summary>
        /// <param name="postId">Id del post</param>
        /// <returns></returns>
        long AddFavoritePost(long postId);

        /// <summary>
        /// Obtener la lista de posts en favoritos del usuario
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<FavoritoPost> GetFavoritos(QueryParamsHelper queryParameters, long categoriaId, out long totalCount);

        /// <summary>
        /// Obtener la lista de favoritos
        /// </summary>
        /// <param name="count">La cantidad de favoritos que se van a devolver</param>
        /// <returns></returns>
        List<FavoritoPost> GetLastFavoritos(int count);

        /// <summary>
        /// Eliminar o restaura el favorito del 
        /// </summary>
        /// <param name="favoritoId"></param>
        /// <returns></returns>
        bool ChangeDeleteFavorito(long favoritoId);

        /// <summary>
        /// Obtener la lista de posts relacionados
        /// </summary>
        /// <param name="postId">Id del post</param>
        /// <returns></returns>
        List<Post> GetRelatedPosts(long postId);

        /// <summary>
        /// Obtiene la cantidad de posts publicados por el usuario
        /// </summary>
        /// <param name="userId">Id del usuario</param>
        /// <returns></returns>
        int PostsCountByUserId(long userId);

        TopsViewModel GetTopPosts(string date, long? categoria);

        /// <summary>
        /// Seguir post
        /// </summary>
        /// <param name="postId">Id del post</param>
        /// <returns></returns>
        long SeguirPost(long postId);

        /// <summary>
        /// Revisar si está siguiendo el post
        /// </summary>
        /// <param name="postId">Id del post</param>
        /// <param name="userName">UserName</param>
        /// <returns></returns>
        bool IsFollowingPost(long postId, string userName);

        /// <summary>
        /// Obtener la lista de top tags
        /// </summary>
        /// <returns></returns>
        List<CloudTagsViewModel> GetCloudTags();

        /// <summary>
        /// Obtener visita por post y usuario
        /// </summary>
        /// <param name="postId">Id del post</param>
        /// <param name="usuarioId">Id del usuario</param>
        /// <returns></returns>
        Visitas GetVisitaPost(long postId, string IP, long? usuarioId);

        /// <summary>
        /// Sumar una visita al post y asignarle el usuario visitante
        /// </summary>
        /// <param name="postId">Id del post</param>
        /// <param name="IP">IP del usuario logeado</param>
        /// <param name="userName">Username del usuario logeado</param>
        /// <returns></returns>
        long SetVisitaToPostUsuario(long postId, string IP, string userName);

        /// <summary>
        /// Obtener la lista de usuarios que siguen un post
        /// </summary>
        /// <param name="postId">Id del post</param>
        /// <returns></returns>
        List<Usuario> GetUsuariosQueSiguenPost(long postId);

        /// <summary>
        /// Obtener la cantidad de seguidores que tiene un post
        /// </summary>
        /// <param name="postId">Id del post</param>
        /// <returns></returns>
        int GetCountUsuariosQueSiguenPost(long postId);

        IQueryable<Post> FilterTopPosts(string date, IQueryable<Post> posts);

        /// <summary>
        /// Crear un nuevo historial de cambios en la comunidad
        /// </summary>
        /// <param name="model">Entidad Historial</param>
        /// <returns></returns>
        long SaveHistorial(Historial model);

        /// <summary>
        /// Eliminar posts que tengan denuncias continuas en la comunidad
        /// </summary>
        /// <param name="postId">Id del post</param>
        /// <param name="cantidadDenuncias">Cantidad de denuncias para determinar si se elimina el post de manera automática</param>
        /// <param name="dias">Días que se analizarán para determinar si se elimina o no el post</param>
        /// <returns></returns>
        bool EliminarPostDenunciado(long postId, int cantidadDenuncias, int dias = 1);
    }
}
