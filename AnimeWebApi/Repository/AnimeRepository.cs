using AnimeWebApi.Data;
using AnimeWebApi.Interface;
using AnimeWebApi.Models;

namespace AnimeWebApi.Repository
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly DataContext _context;

        public AnimeRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateAnimes(Anime anime)
        {
            _context.Add(anime);
            return Save();
        }

        public bool DeleteAnime(Anime anime)
        {
           _context.Remove(anime);
            return Save();  
        }

        public Anime GetAnime(int animeId)
        {
            return _context.animes.Where(a => a.Id == animeId).FirstOrDefault();
        }

        public Anime GetAnimeByName(string animeName)
        {
            return _context.animes.Where(a => a.Title.Trim().ToUpper() == animeName.TrimEnd().ToUpper()).FirstOrDefault();
        }
       

        public ICollection<Anime> GetAnimes()
        {
            return _context.animes.ToList();
        }

        public Character GetCharacterById(int id)
        {
            return _context.characters.Where(c => c.Id == id).FirstOrDefault();
        }

        public Director GetDirectorById(int id)
        {
            return _context.directors.Where(d => d.Id == id).FirstOrDefault();
        }

        public bool isAnimeExistByName(string animeName)
        {
            
          
          return _context.animes.Any(a => a.Title.Trim().ToUpper() == animeName.TrimEnd().ToUpper());
           
        }

        public bool isAnimeExists(int animeId)
        {
            return _context.animes.Any(a => a.Id == animeId);
        }

        public bool Save()
        {
            var saved= _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAnime(Anime anime)
        {
            _context.animes.Update(anime);  
            return Save();
        }
    }
}





/* [HttpPost]
public IActionResult CreateAnime([FromBody] AnimeDto createAnime)
{
    if (createAnime == null)
    {
        return BadRequest("AnimeDto is null");
    }

    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    // Check if the related entities exist
    var character = _context.Characters.FirstOrDefault(c => c.Id == 1); // Example
    var director = _context.Directors.FirstOrDefault(d => d.Id == 1); // Example

    if (character == null)
    {
        ModelState.AddModelError("CharacterId", "Character not found.");
        return BadRequest(ModelState);
    }

    if (director == null)
    {
        ModelState.AddModelError("DirectorId", "Director not found.");
        return BadRequest(ModelState);
    }

    var anime = new Anime
    {
        Title = createAnime.Title,
        Genre = createAnime.Genre,
        Details = createAnime.Details,
        CharacterId = character.Id, // Set foreign key
        DirectorId = director.Id    // Set foreign key
    };

    _context.Animes.Add(anime);
    _context.SaveChanges();

    return Ok("Successfully Created");
}
*/
