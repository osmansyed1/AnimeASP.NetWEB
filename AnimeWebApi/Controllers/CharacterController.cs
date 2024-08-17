using AnimeWebApi.Dto;
using AnimeWebApi.Interface;
using AnimeWebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController :Controller
    {
        private readonly ICharacterRepository _characterRepository;
       private readonly IMapper _mapper;

        public CharacterController(ICharacterRepository characterRepository,IMapper mapper)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

          [HttpGet]
          [ProducesResponseType(StatusCodes.Status200OK)]
          [ProducesResponseType(StatusCodes.Status400BadRequest)]
          [ProducesResponseType(StatusCodes.Status500InternalServerError)]

          public IActionResult GetCharacters()
        {

            var characters = _mapper.Map<List<CharacterDto>>(_characterRepository.GetCharacters());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(characters);
        }


        [HttpGet("withAnime")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCharacterWithAnime()
        {
            var charracterAnimeJoin = _mapper.Map<List<CharacterDto>>(_characterRepository.GetCharacterAnime());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(charracterAnimeJoin);
        }

        [HttpGet("{charID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult GetCharacter(int charID)
        {
            if(!_characterRepository.isCharacterExists(charID))
            { 
                return NotFound(ModelState);
            }

            var character= _mapper.Map<CharacterDto>(_characterRepository.GetCharacter(charID));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }

            return Ok(character);   

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateCharacter([FromBody] CharacterDto characterCreate)
        {
            if(characterCreate == null)
            {
                return BadRequest(ModelState);  
            }

            var character = _characterRepository.GetCharacters()
               .Where(c => c.Name.Trim().ToUpper() == characterCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if(character !=null)
            {
                ModelState.AddModelError("", "Character already Exists");
                return StatusCode(422,ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }
            var characterMap = _mapper.Map<Character>(characterCreate); // Mapping Poco Character || CharcateDto

            var result = _characterRepository.CreateCharacter(characterMap); // here actual new object/row/field creatred  

            if(!result)
            {
                ModelState.AddModelError("", "Something went wrong While Saving");
                return StatusCode(500,ModelState);  
            }

            return Ok("Successfully Created");



        }

        [HttpPut("{charID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult UpdateCharacter(int charID, [FromBody] CharacterDto updateCharacter)
        {
            if (updateCharacter == null)
            {
                return BadRequest(ModelState);
            }

            // remember here ID ===>both capital

            if(charID!=updateCharacter.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_characterRepository.isCharacterExists(charID))
            { 
                return StatusCode(statusCode: StatusCodes.Status404NotFound);   
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var charcterMap = _mapper.Map<Character>(updateCharacter);

            var result=_characterRepository.UpdaterCharacter(charcterMap);

            if (!result)
            {
                 ModelState.AddModelError("", "Something went wrong while updating");
                 return StatusCode(500,ModelState); 
            }
            return Ok("Updated Successfully");
        }

        [HttpDelete("{charId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeleteCharacter(int charId)
        {
            if (charId <= 0)
            {
                return BadRequest(ModelState);
            }

            //remember here Id==>I capital and d is smaller

            if (!_characterRepository.isCharacterExists(charId))
            { 
                return StatusCode(StatusCodes.Status404NotFound);
            }
            
            var characterToDelete =_characterRepository.GetCharacter(charId); //Fetching one record or object from table

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result=_characterRepository.DeleteCharacter(characterToDelete); //Deleting particular record
            
            if(!result)
            {

                ModelState.AddModelError("", "Something went wrong while deleting record");
                return StatusCode(500,ModelState);  
            }
            return Ok("Successfully deleted");
        }




    }
}
