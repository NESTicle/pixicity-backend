using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Domain.ViewModels.Posts
{
    public class TopsViewModel
    {
        public List<PostViewModel> PostsConMasPuntos { get; set; }
        public List<PostViewModel> PostsConMasFavoritos { get; set; }
        public List<PostViewModel> PostsConMasComentarios { get; set; }
        public List<PostViewModel> PostsConMasSeguidores { get; set; }
    }
}
