using UnityEngine;

public readonly struct DashCommand
{
    public readonly Vector3 StartPos;
    public readonly Vector3 EndPos;
    public readonly PlayerStatsSO Stats;

    public DashCommand(Vector3 start, Vector3 end, PlayerStatsSO stats)
    {
        StartPos = start;
        EndPos = end;
        Stats = stats;

        StartPos.y = EndPos.y;
    }
}