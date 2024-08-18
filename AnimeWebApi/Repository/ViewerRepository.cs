using AnimeWebApi.Data;
using AnimeWebApi.Interface;
using AnimeWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeWebApi.Repository
{
    public class ViewerRepository :IViewerRepository
    {
        private readonly DataContext _context;

        public ViewerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateViewer(Viewer viewer)
        {
           
            _context.Add(viewer);
            return Save();  
        }

        public bool deleteViewer(Viewer viewer)
        {
            _context.Remove(viewer);
            return Save();  
        }

        public ICollection<Anime> GetAnimeByViewer(int viewerId)
        {
            return _context.animeviews.Where(e=>e.ViewerId == viewerId).Select(v=>v.Anime).ToList();
        }

        public Viewer GetViewer(int id)
        {
            return _context.viewers.Where(v => v.Id == id).FirstOrDefault();
        }

        public ICollection<Viewer> GetViewerAnime()
        {
            return _context.viewers.Include(v=>v.AnimeViewers).ThenInclude(av=>av.Anime).ToList();
        }

        public ICollection<Viewer> GetViewers()
        {
            return _context.viewers.ToList();   
        }

        public bool isViewerExists(int id)
        {
            return _context.viewers.Any(v=>v.Id == id); 
        }

        public bool Save()
        {
            var saved =_context.SaveChanges(); 
            return saved>0 ?true : false;

            
        }

        public bool updateViewer(Viewer viewer)
        {
            _context.viewers.Update(viewer); 
            return Save();  
        }
    }
}
