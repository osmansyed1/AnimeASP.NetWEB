using AnimeWebApi.Models;

namespace AnimeWebApi.Interface
{
    public interface IViewerRepository
    {
        ICollection<Viewer> GetViewers();
        Viewer GetViewer(int id);

        bool isViewerExists(int id);

        public ICollection<Anime> GetAnimeByViewer(int viewerId);
    }
}
