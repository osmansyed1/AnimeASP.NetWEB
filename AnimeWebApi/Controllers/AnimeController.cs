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

        [HttpGet("anime&viewer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetAnimeWithViewer()
        {
            var animeviewer = _animeRepository.GetAnimeWithViewer(); //here I dont need to map because I want All Anime poco class records without showing in AnimeDto class
            //below is nested Query ,I created new Oject and Storing anime data along with Viewer data to show in Swagger
            var res = animeviewer.Select(a => new {

               AnimeId = a.Id,
               AnimeTitle=a.Title,
               AnimeDetails=a.Details,
                AnimeGenre=a.Genre,
                   Viewer= a.AnimeViewers.Select(av=> new
                    {
                      vId =av.Viewer.Id,
                     Viewername =av.Viewer.Name,
                   critic = av.Viewer.Critic
                    }).ToList()

            }).ToList();
            
            
            return Ok(res);



            
        }



        [HttpGet("viewer/{animeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetViewerByAnime(int animeId)
        {
            var viewer = _mapper.Map<List<ViewerDto>>(_animeRepository.GetViewerByAnime(animeId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                return Ok(viewer);  
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
            
            //Here I did  not use _mapper,To insert the value of Two Foreign key i.e CharacterId and 
            //directorId I am creating an object of Anime to manually store two foreign key
            //oytherwise it will give error FK_anime_characterId mismatch in original table
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

            return StatusCode(500, "A problem happened while handling  request");
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

