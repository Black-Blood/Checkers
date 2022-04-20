namespace Checkers.Core;

public class Checker
{
    public Color Color { get; private set; }

    public bool IsKing { get; set; } = false;

    public Checker(Color color)
    {
        Color = color;
    }
}
