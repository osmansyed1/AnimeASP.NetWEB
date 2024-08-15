namespace AnimeWebApi.Models
{
    public class AnimeViewer
    {
        public int AnimeId { get; set; }
        public Anime Anime { get; set; }

        public int ViewerId { get; set; }

        public Viewer Viewer { get; set; }
    }
}
