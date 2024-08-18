using AnimeWebApi.Models;

namespace AnimeWebApi.Interface
{
    public interface IViewerRepository
    {
        ICollection<Viewer> GetViewers();
        Viewer GetViewer(int id);

        bool isViewerExists(int id);

        bool CreateViewer( Viewer viewer);

        bool updateViewer(Viewer viewer);   

        bool deleteViewer(Viewer viewer);

        ICollection<Viewer> GetViewerAnime();

        bool Save();

        public ICollection<Anime> GetAnimeByViewer(int viewerId);
    }
}
