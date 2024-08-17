using AnimeWebApi.Models;

namespace AnimeWebApi.Interface
{
    public interface IAnimeRepository
    {
       ICollection<Anime> GetAnimes();
        Anime GetAnime(int animeId);

        ICollection<Viewer> GetViewerByAnime(int animeId);

        ICollection<Anime> GetAnimeWithViewer();

        bool isAnimeExists(int animeId);  
        
        Anime GetAnimeByName(string animeName);

        bool isAnimeExistByName(string animeName);

        bool CreateAnimes(Anime anime);

        bool UpdateAnime(Anime anime);  

        bool DeleteAnime(Anime anime);

        Character GetCharacterById(int id);


        Director GetDirectorById(int id);

        bool Save();


    }
}
