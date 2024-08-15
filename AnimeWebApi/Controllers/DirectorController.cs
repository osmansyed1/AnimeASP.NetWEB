using AnimeWebApi.Dto;
using AnimeWebApi.Interface;
using AnimeWebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController :Controller
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly IMapper _mapper;

        public DirectorController(IDirectorRepository directorRepository,IMapper mapper)
        {
            _directorRepository = directorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDirectors()
        {
            var directorDtoMap = _mapper.Map<List<DirectorDto>>(_directorRepository.GetDirectors());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(directorDtoMap);  
        }

        [HttpGet("{dirId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult GetDirectorByID(int dirId)
        {

            if(!_directorRepository.isDirectorExists(dirId))
            {
                return NotFound();
            }
        var directordto=_mapper.Map<DirectorDto>(_directorRepository.GetDirectorById(dirId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(directordto);

        }

        [HttpGet("{dirId}/animes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult GetAnimeByDirector(int dirId)
        {
            if (!_directorRepository.isDirectorExists(dirId))
                { 
                return NotFound();
            
            }
            var animeDto=_mapper.Map<List<AnimeDto>>(_directorRepository.GetAnimeByDirector(dirId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(animeDto);    


        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateDirectors([FromBody] DirectorDto createDirector)
        {
            if(createDirector== null)
            {
                return BadRequest(ModelState);
            }
            var director = _directorRepository.GetDirectors()
                .Where(d => d.Name.Trim().ToUpper() == createDirector.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
            
            if(director != null)
            {
                ModelState.AddModelError("", "Alreday exists");
                return StatusCode(StatusCodes.Status422UnprocessableEntity);
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var DirectorDtoMap=_mapper.Map<Director>(createDirector);

            var result=_directorRepository.CreateDirector(DirectorDtoMap);

            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong while Creating");
                return StatusCode(500, "Error");
            }

            return Ok("Successfully created");

            



        }

        [HttpPut("{dirId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateDirector(int dirId,[FromBody] DirectorDto updateDirector)
        {
            if (updateDirector == null)
            { 
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if(dirId!=updateDirector.Id)
            {
                return BadRequest(ModelState);
            }
            if(!_directorRepository.isDirectorExists(dirId))
            {
                return NotFound();
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }

             var directorDto=_mapper.Map<Director>(updateDirector); 

            var res=_directorRepository.UpdateDirector(directorDto);
            if(!res)
            {
                ModelState.AddModelError("", "Something went wrong While updating");
                return StatusCode(500, "Error");
            }

            return Ok("Successfully updated");
        }

        [HttpDelete("{directorID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteDirector(int directorID)
        { 
            if(!_directorRepository.isDirectorExists(directorID))
            {
                return NotFound(ModelState);
            }

            var directorToDelete=_directorRepository.GetDirectorById(directorID);   

            if(!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            var res=_directorRepository.DeleteDirector(directorToDelete); 
            
            if(!res)
            {
                return StatusCode(500, "Error");
            }
            return Ok("Deleted");
        }

    }

}
