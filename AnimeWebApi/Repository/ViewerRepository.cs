using AnimeWebApi.Data;
using AnimeWebApi.Interface;
using AnimeWebApi.Models;

namespace AnimeWebApi.Repository
{
    public class ViewerRepository :IViewerRepository
    {
        private readonly DataContext _context;

        public ViewerRepository(DataContext context)
        {
            _context = context;
        }
    
        public ICollection<Anime> GetAnimeByViewer(int viewerId)
        {
            return _context.animeviews.Where(e=>e.ViewerId == viewerId).Select(v=>v.Anime).ToList();
        }

        public Viewer GetViewer(int id)
        {
            return _context.viewers.Where(v => v.Id == id).FirstOrDefault();
        }

        public ICollection<Viewer> GetViewers()
        {
            return _context.viewers.ToList();   
        }

        public bool isViewerExists(int id)
        {
            return _context.viewers.Any(v=>v.Id == id); 
        }
    }
}
