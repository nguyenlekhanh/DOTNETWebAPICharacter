global using AutoMapper;

namespace WebAPICharacter.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character { Id = 1, Name = "Sam" }
        };
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper) 
        { 
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceRepsonse = new ServiceResponse<List<GetCharacterDto>>();
            characters.Add(_mapper.Map<Character>(newCharacter));
            serviceRepsonse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceRepsonse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceRepsonse = new ServiceResponse<List<GetCharacterDto>>();
            serviceRepsonse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceRepsonse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceRepsonse = new ServiceResponse<GetCharacterDto>();
            
            var character = characters.FirstOrDefault(c => c.Id == id);

            serviceRepsonse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceRepsonse;
        }
    }
}
