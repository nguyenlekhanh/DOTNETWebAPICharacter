﻿using Microsoft.AspNetCore.Mvc;

namespace WebAPICharacter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character { Name = "Sam" }
        };

        [HttpGet("GetAll")]
        public ActionResult<List<Character>> Get()
        {
            return Ok(characters);
        }

        [HttpGet("GetSingle")]
        public ActionResult<Character> GetSingle()
        {
            return Ok(characters[0]);
        }
    }
}
