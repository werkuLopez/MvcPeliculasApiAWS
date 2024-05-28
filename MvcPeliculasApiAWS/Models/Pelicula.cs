using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcPeliculasApiAWS.Models
{
    public class Pelicula
    {
        public int IdPelicula { get; set; }
        public string Genero { get; set; }
        public string Titulo { get; set; }
        public string Argumento { get; set; }
        public string Actor { get; set; }
        public string Foto { get; set; }
        public int Precio { get; set; }
        public string Youtube { get; set; }
    }
}
