using AnimeWebApi.Models;

namespace AnimeWebApi.Interface
{
    public interface IDirectorRepository
    {
        Director GetDirectorById(int dirId);
        ICollection<Director> GetDirectors();   

        bool isDirectorExists(int dirId);

        ICollection<Anime> GetAnimeByDirector(int dirId);

        bool CreateDirector(Director director);

        bool UpdateDirector(Director director);

        bool DeleteDirector( Director director);

        bool Save();


    }
}
