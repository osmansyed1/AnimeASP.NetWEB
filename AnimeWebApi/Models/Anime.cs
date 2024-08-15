using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeWebApi.Models
{
    public class Anime
    {
        [Key]
      
        public int Id { get; set; }
        public string Title { get; set; }

        public string Details { get; set; }

        public string Genre { get; set; }

      

        

        public int  CharacterId { get; set; }

        public Character Character { get; set; }

        
        public int directorID { get; set; }
        public Director director { get; set; }   

        public ICollection<AnimeViewer> AnimeViewers { get; set; }
       
    }
}
