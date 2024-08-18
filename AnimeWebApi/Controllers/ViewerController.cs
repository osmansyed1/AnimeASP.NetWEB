using AnimeWebApi.Dto;
using AnimeWebApi.Interface;
using AnimeWebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewerController : Controller
    {
        private readonly IViewerRepository _viewerRepository;
        private readonly IMapper _mapper;

        public ViewerController(IViewerRepository viewerRepository, IMapper mapper)
        {
            _viewerRepository = viewerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult GetViewer()
        {
            var viewer = _mapper.Map<List<ViewerDto>>(_viewerRepository.GetViewers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(viewer);

        }

        [HttpGet("viewer&anime")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult GetViewerWithAnime()
        {
            var vieweranime=_viewerRepository.GetViewerAnime();

            var res = vieweranime.Select(v => new
            {
                viewerName = v.Name,
                viewerId = v.Id,
                viewerCritic = v.Critic,
                anime = v.AnimeViewers.Select(av => new
                {
                    animTitle = av.Anime.Title,
                    animeId = av.Anime.Id,
                    animeDetails = av.Anime.Details,
                    animGenre = av.Anime.Genre,

                }).ToList()
            }).ToList();

            return Ok(res);
                
        }



        [HttpGet("{viewerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetViewerById(int viewerId)
        {

            if (viewerId < 0)
            {
                return BadRequest(ModelState);
            }

            if (!_viewerRepository.isViewerExists(viewerId))
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            var viewerpoco = _viewerRepository.GetViewer(viewerId);
            var viewerMapDto = _mapper.Map<ViewerDto>(viewerpoco);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(viewerMapDto);

        }

        [HttpGet("anime/{viewerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetAnimeByViewer(int viewerId)
        {
            var animes = _mapper.Map<List<AnimeDto>>(_viewerRepository.GetAnimeByViewer(viewerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(animes);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult CreateViewer([FromBody] ViewerDto createViewer)
        {
            if (createViewer == null)
            {
                return BadRequest(ModelState);
            }

            var viewer = _viewerRepository.GetViewers().Where(c => c.Name.Trim().ToUpper() == createViewer.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if (viewer != null)
            {
                ModelState.AddModelError("", "Viewer allready exist");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var viewerMap = new Viewer()
            {
                Name = createViewer.Name,
                Critic = createViewer.Critic,

            };
            var res = _viewerRepository.CreateViewer(viewerMap);

            if (!res)
            {
                ModelState.AddModelError("", "Something wrong while Saving");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok("creted");

        }

        [HttpPut("viewerId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult UpdateViewer(int viewerId, [FromBody] ViewerDto updateViewer)
        {
            if (updateViewer == null)
            {
                return BadRequest();
            }
            if (viewerId != updateViewer.Id)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            if (!_viewerRepository.isViewerExists(viewerId))
            {
                return NotFound();

            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var viewerMap = _mapper.Map<Viewer>(updateViewer);
            var res = _viewerRepository.updateViewer(viewerMap);
            if (!res)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok("Updated");

        }

        [HttpDelete("viewerId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeleteViewer(int viewerId)

        {
            if (!_viewerRepository.isViewerExists(viewerId))
            {
                return NotFound(ModelState);
            }
            var viewerToDelete = _viewerRepository.GetViewer(viewerId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Viewer to delete

            var viewerDeleted = _viewerRepository.deleteViewer(viewerToDelete);

            if (!viewerDeleted)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok("Deleted");




        }







    }
}
