using Microsoft.FSharp.Core;
using SOS.Persistence;
using System;

namespace SOS.Bidnez
{
    internal class Character : ICharacter
    {
        private WorldData _worldData;
        private int _id;
        internal Character(WorldData worldData, int id)
        {
            _worldData = worldData;
            _id = id;
        }

        public string Name => _worldData.Characters[_id].Name;

        public double X => _worldData.Characters[_id].X;

        public double Y => _worldData.Characters[_id].Y;

        public int Id => _id;

        internal static ICharacter? FromId(WorldData worldData, FSharpOption<int> id)
        {
            return (id is null) ? (null) : new Character(worldData, id.Value);
        }
    }
}