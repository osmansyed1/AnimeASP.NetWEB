using AnimeWebApi.Data;
using AnimeWebApi.Interface;
using AnimeWebApi.Models;

namespace AnimeWebApi.Repository
{
    public class DirectorRepository :IDirectorRepository
    {
        private readonly DataContext _context;

        public DirectorRepository(DataContext context)
        {
            _context = context;
        }

        public Director GetDirectorById(int dirId)
        {
           return _context.directors.Where(d=>d.Id==dirId).FirstOrDefault();  
        }

        public ICollection<Director> GetDirectors()
        {
            return _context.directors.ToList(); 
        }

        public ICollection<Anime> GetAnimeByDirector(int dirId)
        {
            return _context.animes.Where(a=>a.director.Id==dirId).ToList(); //here we going to animes table then director table
            //here return gype is anime obj
        }

        public bool isDirectorExists(int dirId)
        {
           return _context.directors.Any(d=>d.Id==dirId);   
        }

        public bool CreateDirector(Director director)
        {
            // _context.directors.Add(director);
            _context.Add(director);
            return Save();  
        }

        public bool Save()
        {
          var saved =_context.SaveChanges();  
            return  saved>0 ?true : false;  
        }

        public bool UpdateDirector(Director director)
        {
            _context.Update(director);
            return Save();  
        }

        public bool DeleteDirector(Director director)
        {
           _context.Remove(director);
            return Save();  
        }
    }
}
