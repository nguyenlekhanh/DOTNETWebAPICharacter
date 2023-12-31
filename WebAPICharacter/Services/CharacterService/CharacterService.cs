﻿global using AutoMapper;
using WebAPICharacter.Models;

namespace WebAPICharacter.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character { Id = 1, Name = "Sam" }
        };
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper, DataContext context) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceRepsonse = new ServiceResponse<List<GetCharacterDto>>();

            var addAsync = await _context.Character.AddAsync(_mapper.Map<Character>(newCharacter));
            _context.SaveChanges();

            var dbCharacters = await _context.Character.ToListAsync();
            serviceRepsonse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            
            //characters.Add(_mapper.Map<Character>(newCharacter));
            //serviceRepsonse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceRepsonse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                //var character = characters.FirstOrDefault(c => c.Id == id);
                _context.Remove(_context.Character.Single(c => c.Id == id));
                _context.SaveChanges();

                //if (character is null)
                //{
                //    throw new Exception($"Character with Id '{id}' not found");
                //}

                //characters.Remove(character);

                //serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                var dbCharacters = await _context.Character.ToListAsync();
                serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }

            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceRepsonse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Character.ToListAsync();
            serviceRepsonse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceRepsonse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceRepsonse = new ServiceResponse<GetCharacterDto>();

            var character = await _context.Character.FirstOrDefaultAsync(c => c.Id == id);

            serviceRepsonse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceRepsonse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {

            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try { 
                var character = await _context.Character.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

                if(character is null)
                {
                    throw new Exception($"Character with Id '{updatedCharacter.Id}' not found");
                }

                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;
                _context.SaveChanges();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);

            } catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
