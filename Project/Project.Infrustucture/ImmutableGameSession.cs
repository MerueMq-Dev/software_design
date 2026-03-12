using System;

namespace Project.Infrastructure;


/// <summary>
/// Представляет одну игровую партию
/// </summary>
public sealed record ImmutableGameSession
{
    public string PlayerName { get; init; }
    public int Score { get; init; }
    public bool IsFinished { get; init; }
    public int MovesCount { get; init; }
    public int MaxMoves { get; init; }
    public int? TargetScore { get; init; }

    public ImmutableGameSession(
        string playerName,
        int score,
        bool isFinished,
        int movesCount,
        int maxMoves,
        int? targetScore)
    {
        PlayerName = playerName ?? "Игрок";
        Score = score;
        IsFinished = isFinished;
        MovesCount = movesCount;
        MaxMoves = maxMoves;
        TargetScore = targetScore;

        if (score < 0)
            throw new ArgumentException("Счёт не может быть отрицательным");

        if (movesCount < 0)
            throw new ArgumentException("Количество шагов не может быть отрицательным");

        if (maxMoves <= 0)
            throw new ArgumentException("Максимальное количество шагов должно быть больше нуля");
    }

    public static ImmutableGameSession Start(string playerName, int maxMoves = 30, int? targetScore = null)
    {
        return new ImmutableGameSession(playerName, 0, false, 0, maxMoves, targetScore);
    }

    public int GetRemainingMoves() => MaxMoves - MovesCount;

    public bool IsTargetReached() =>
        TargetScore.HasValue && Score >= TargetScore.Value;

    public bool IsOutOfMoves() =>
        MovesCount >= MaxMoves;

    public ImmutableGameSession AddScore(int value)
    {
        if (value < 0)
            throw new ArgumentException("Очки не могут быть отрицательными");

        return this with { Score = Score + value };
    }

    public ImmutableGameSession IncrementMoves()
    {
        return this with { MovesCount = MovesCount + 1 };
    }

    public ImmutableGameSession Finish()
    {
        return this with { IsFinished = true };
    }
}