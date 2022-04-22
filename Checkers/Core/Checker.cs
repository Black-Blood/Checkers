namespace Checkers.Core;

public class Checker
{
    public bool IsKing { get; set; } = false;

    public Color Color { get; private set; }

    public Position Position { get; internal set; }

    public List<Position> Moves { get; internal set; } = new();

    public List<Checker> UnderAttackCheckers { get; internal set; } = new();

    public Checker(Position position, Color color)
    {
        Position = position;
        Color = color;
    }
}
