﻿using AnimeWebApi.Models;

namespace AnimeWebApi.Dto
{
    public class CharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public ICollection<Anime> anime { get; set; }
    }
}
