using AnimeWebApi.Models;

namespace AnimeWebApi.Dto
{
    public class AnimeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Details { get; set; }

        public string Genre { get; set; }

        public int CharacterId { get; set; }



        public int directorID { get; set; }

    }


}    
