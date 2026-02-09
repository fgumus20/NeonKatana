using UnityEngine;

public readonly struct DashCommand
{
    public Vector3 StartPos { get; }
    public Vector3 EndPos { get; }
    public PlayerStatsSO Stats { get; }

    public DashCommand(Vector3 start, Vector3 end, PlayerStatsSO stats)
    {
        start.y = end.y;

        StartPos = start;
        EndPos = end;
        Stats = stats;
    }
}
