using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AnimeWebApi.Models
{
    public class Viewer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public string Name { get; set; } 
        
        public string Critic { get; set; }

       public ICollection<AnimeViewer> AnimeViewers { get; set; } 

       
    }
}
