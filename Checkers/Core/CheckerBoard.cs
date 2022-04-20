namespace Checkers.Core;

public class CheckerBoard
{
    public readonly static int Size = 8;

    public event Action<Position>? MakingKing;
    public event Action<Position>? RemovingChecker;
    public event Action<Position, Position>? ChangingCheckerPosition;

    public Color[] Colors = new Color[2];

    public Dictionary<Position, Checker> Checkers { get; private set; } = new();

    public CheckerBoard(Player player1, Player player2)
    {
        Colors[0] = player1.Color;
        Colors[1] = player2.Color;

        ArrangeCheckers();
    }

    public bool MakeKing(Position position)
    {
        var checker = GetChecker(position);

        if (checker is null || checker.IsKing)
            return false;

        if (
            (checker.Color == Colors[0] && position.X == Size - 1)
            ||
            (checker.Color == Colors[1] && position.X == 0)
            )
        {
            checker.IsKing = true;
            MakingKing?.Invoke(position);
            return true;
        }

        return false;
    }

    public bool RemoveChecker(Position position)
    {
        if (!IsCheckerOnPosition(position))
            return false;

        Checkers.Remove(position);
        RemovingChecker?.Invoke(position);
        return true;
    }

    public bool ChangeCheckerPosition(Position prev, Position next)
    {
        if (!IsCheckerOnPosition(prev) || IsCheckerOnPosition(next))
            return false;

        Checkers.Add(next, Checkers[prev]);
        Checkers.Remove(prev);
        ChangingCheckerPosition?.Invoke(prev, next);

        return true;
    }

    public bool IsCheckerOnPosition(Position position) => 
        Checkers.TryGetValue(position, out _);

    public bool IsPositionOnBoard(Position position) => 
        position.X >= 0 && position.X < Size && 
        position.Y >= 0 && position.Y < Size;

    public Checker? GetChecker(Position position)
    {
        Checkers.TryGetValue(position, out var checker);
        return checker;
    }

    private void ArrangeCheckers()
    {
        // Place black checkers
        for (int i = 0; i < 3; i++)
            for (int j = i % 2; j < Size; j += 2)
                Checkers.Add(new Position(i, j), new(Color.White));

        // Place white checkers
        for (int i = Size - 3; i < Size; i++)
            for (int j = i % 2; j < Size; j += 2)
                Checkers.Add(new Position(i, j), new(Color.Black));
    }
}