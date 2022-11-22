using SOS.Persistence;
namespace SOS.Bidnez
{
    public class World: IWorld
    {
        private WorldData _worldData;
        public World()
        {
            _worldData = new WorldData();
        }

        public ICharacter? PlayerCharacter => Character.FromId(_worldData, _worldData.PlayerCharacterId);

        public void Start()
        {
            _worldData.PlayerCharacterId = _worldData.Characters.Count;
            _worldData.Characters.Add(new CharacterData()
            {
                Name = "You"
            });
        }
    }
}
