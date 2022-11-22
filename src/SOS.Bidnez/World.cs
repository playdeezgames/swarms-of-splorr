﻿using SOS.Persistence;
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
                Name = "Yer Swarm",
                CharacterType = CharacterType.Swarm,
                X=0.0,
                Y=0.0,
                Heading = 0.0,
                Speed = 1.0
            });
        }
    }
}
