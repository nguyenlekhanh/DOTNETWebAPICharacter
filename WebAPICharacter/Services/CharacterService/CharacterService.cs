namespace WebAPICharacter.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character { Id = 1, Name = "Sam" }
        };

        public async Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter)
        {
            var serviceRepsonse = new ServiceResponse<List<Character>>();
            characters.Add(newCharacter);
            serviceRepsonse.Data = characters;
            return serviceRepsonse;
        }

        public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
        {
            var serviceRepsonse = new ServiceResponse<List<Character>>();
            serviceRepsonse.Data = characters;
            return serviceRepsonse;
        }

        public async Task<ServiceResponse<Character>> GetCharacterById(int id)
        {
            var serviceRepsonse = new ServiceResponse<Character>();
            
            var character = characters.FirstOrDefault(c => c.Id == id);

            serviceRepsonse.Data = character;
            return serviceRepsonse;
        }
    }
}
