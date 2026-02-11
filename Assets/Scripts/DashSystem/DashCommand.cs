using UnityEngine;

public readonly struct DashCommand
{
    public Vector3 StartPos { get; }
    public Vector3 EndPos { get; }

    public DashCommand(Vector3 start, Vector3 end)
    {
        start.y = end.y;
        StartPos = start;
        EndPos = end;
    }
}
