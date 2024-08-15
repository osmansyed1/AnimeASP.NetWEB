using AnimeWebApi.Models;

namespace AnimeWebApi.Interface
{
    public interface ICharacterRepository
    {
        ICollection<Character> GetCharacters();

        Character GetCharacter(int charId); 

        bool isCharacterExists(int charID);

        bool CreateCharacter(Character character);

        bool UpdaterCharacter(Character character); 

        bool DeleteCharacter(Character character);  
        bool Save();
    }
}
;