﻿namespace SOS.Bidnez
{
    public interface ICharacter
    {
        int Id { get; }
        string Name { get; }
        double X { get; }
        double Y { get; }
        double Heading { get; set; }
        double Speed { get; }
    }
}
