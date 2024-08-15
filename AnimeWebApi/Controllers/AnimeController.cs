using AnimeWebApi.Dto;
using AnimeWebApi.Interface;
using AnimeWebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeController :Controller
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IMapper _mapper;
        private readonly ICharacterRepository _characterRepository;
        private readonly IDirectorRepository _directorRepository;

        public AnimeController(IAnimeRepository animeRepository,IMapper mapper,
            ICharacterRepository characterRepository,IDirectorRepository directorRepository)
        {    
            
        
            _animeRepository = animeRepository;
            _mapper = mapper;
            _characterRepository = characterRepository;
            _directorRepository = directorRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllAnimes()
        {
            var anime = _mapper.Map<List<AnimeDto>>(_animeRepository.GetAnimes());

            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);  
            }

            return Ok(anime);
        }

        [HttpGet("{animeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetAnime(int animeId)
        {
            if(animeId <= 0)
            {
                return StatusCode(StatusCodes.Status404NotFound);

            }
            if(!_animeRepository.isAnimeExists(animeId))
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            var anime = _mapper.Map<AnimeDto>(_animeRepository.GetAnime(animeId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
                
                return Ok(anime);

        }

       // [HttpGet("{name}")]
        [HttpGet("by-name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetAnimesByName(string name)
        {
            //here name represent Title of POCO class or Table

            if (name == null)
            {
                return BadRequest();

            }
            if(!_animeRepository.isAnimeExistByName(name))
            {
                ModelState.AddModelError("", "Not found");
                return StatusCode(StatusCodes.Status404NotFound);
            }

            // var anime=_mapper.Map<AnimeDto>(_animeRepository.GetAnimeByName(name)); // transformation from poco class to Dto clASS

            var anime = _animeRepository.GetAnimeByName(name);
            var animeDto = _mapper.Map<AnimeDto>(anime);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           // return Ok(anime);

            return Ok(animeDto);
        }

        /*   
         *  ALTERnate
         *  [HttpGet]   
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult GetAnimesByName([FromQuery] string name)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return BadRequest("Name cannot be null or empty.");
                }

                if (!_animeRepository.isAnimeExistByName(name))
                {
                    return NotFound("Anime not found.");
                }

                var anime = _animeRepository.GetAnimeByName(name);
                var animeDto = _mapper.Map<AnimeDto>(anime);

                return Ok(animeDto);
            }*/  //Something is here

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpPost]
        public  IActionResult CreateAnime([FromBody] AnimeDto createAnimeDto)
        {
            if (createAnimeDto == null)
            {
                return BadRequest("AnimeDto is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the related entities exist
            

            var anime = new Anime
            {
                Title = createAnimeDto.Title,
                Genre = createAnimeDto.Genre,
                Details = createAnimeDto.Details,
                CharacterId = createAnimeDto.CharacterId,
                directorID = createAnimeDto.directorID
            };

            var result = _animeRepository.CreateAnimes(anime);

            if (result)
            {
                return Ok("Successfully Created");
            }

            return StatusCode(500, "A problem happened while handling your request.");
        }

        [HttpPut("{anId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateAnime(int anId,[FromBody] AnimeDto updateAnime)
        {

            if (updateAnime == null)
            {
                return BadRequest();

            }
            if (updateAnime.Id != anId)
            {
                return BadRequest();
            }

            var animeExist = _animeRepository.isAnimeExists(anId);

            if(!animeExist)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            { 
                return BadRequest();
            }

            var animeDtoMap = _mapper.Map<Anime>(updateAnime);
            var res=_animeRepository.UpdateAnime(animeDtoMap);

            if(!res)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok("Updated");




        }

        [HttpDelete("{anId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteAnime(int anId)
        {
            if(!_animeRepository.isAnimeExists(anId))
            {
                return NotFound();
            }

           var AnimeToDeleted=_animeRepository.GetAnime(anId);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(!_animeRepository.DeleteAnime(AnimeToDeleted))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok("Deleted");
        }


    }



}

