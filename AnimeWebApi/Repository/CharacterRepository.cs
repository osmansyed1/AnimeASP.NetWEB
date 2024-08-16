using AnimeWebApi.Data;
using AnimeWebApi.Interface;
using AnimeWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeWebApi.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly DataContext _context;

        public CharacterRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateCharacter(Character character)
        {
            _context.Add(character);
            return Save();  
        }

        public bool DeleteCharacter(Character character)
        {
            _context.Remove(character); 
            return Save();
        }

        public Character GetCharacter(int charId)
        {
            return _context.characters.Where(c => c.Id == charId).FirstOrDefault();

        }

        public List<Character> GetCharacterAnime()
        {
            return _context.characters.Include(e=>e.anime).ToList();
                
        }

        public ICollection<Character> GetCharacters()
        {
           return _context.characters.ToList();
        }

        public bool isCharacterExists(int charID)
        {
            return _context.characters.Any(c=>c.Id== charID);              
        }

        public bool Save()
        {
            var saved= _context.SaveChanges();  

            return saved>0 ?true : false;   
        }

        public bool UpdaterCharacter(Character character)
        {
             _context.Update(character);

            return Save();

        }
    }
}
