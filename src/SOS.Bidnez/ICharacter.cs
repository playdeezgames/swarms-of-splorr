namespace SOS.Bidnez
{
    public interface ICharacter
    {
        int Id { get; }
        string Name { get; }
        double X { get; }
        double Y { get; }
        double Heading { get; }
        double Speed { get; }
    }
}
