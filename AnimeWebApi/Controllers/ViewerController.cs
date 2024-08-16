using AnimeWebApi.Dto;
using AnimeWebApi.Interface;
using AnimeWebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewerController :Controller
    {
        private readonly IViewerRepository _viewerRepository;
        private readonly IMapper _mapper;

        public ViewerController(IViewerRepository viewerRepository,IMapper mapper)
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

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(viewer);  
            
        }

        [HttpGet("{viewerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetViewerById(int viewerId)
        {

            if(viewerId < 0)
            {
                return BadRequest(ModelState);  
            }

            if(!_viewerRepository.isViewerExists(viewerId))
            {
                return StatusCode(StatusCodes.Status404NotFound);   
            }
            var viewerpoco=_viewerRepository.GetViewer(viewerId); 
            var viewerMapDto=_mapper.Map<ViewerDto>(viewerpoco);    

            if(!ModelState.IsValid)
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

        public IActionResult GetActionAnimeByViewer(int viewerId)
        {
            var animes = _mapper.Map<List<AnimeDto>>(_viewerRepository.GetAnimeByViewer(viewerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(animes);
        }

    }
}
