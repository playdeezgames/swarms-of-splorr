namespace SOS.Bidnez
{
    public interface IWorld
    {
        ICharacter? PlayerCharacter { get; }

        void Start();
    }
}
