using UnityEngine;
using System.Collections.Generic;
using Scripts.Combat;


public class PathPlanner : CombatState
{
    private List<Vector3> points = new List<Vector3>();
    private readonly List<GameObject> spawnedNodes = new List<GameObject>();
    private int remainingDashCount;
    private LineRenderer lineRenderer;

    public PathPlanner(CombatController controller, CombatBlackboard blackboard, LineRenderer lineRenderer) : base(controller, blackboard) {
        this.lineRenderer = lineRenderer;
    }

    public override void OnEnter()
    {
        remainingDashCount = CombatBlackboard.PlayerStats.dashMoveCount;
        points.Clear();
        points.Add(CombatBlackboard.PlayerTransform.position);
        UpdateLineRenderer();
    }

    public override void Update()
    {
        HandleDrawing();
    }

    public override void OnExit()
    {
        //throw new System.NotImplementedException();
    }

    private void HandleDrawing()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 lastPoint = points[points.Count - 1];
            Vector3 currentPos = GetWorldMousePosition(lastPoint.y);

            Vector3 direction = (currentPos - lastPoint).normalized;

            float distance = Vector3.Distance(lastPoint, currentPos);
            distance = Mathf.Clamp(distance, CombatBlackboard.PlayerStats.minDashDistance, CombatBlackboard.PlayerStats.maxDashDistance);

            if (Physics.Raycast(lastPoint, direction, out RaycastHit hit, distance, CombatBlackboard.PlayerStats.obstacleLayer))
            {
                // Eðer bir duvara çarptýysak, mesafeyi çarpma noktasýna göre kýsaltýyoruz
                distance = hit.distance - 0.2f; // Duvarýn içine girmemesi için ufak bir offset
            }

            Vector3 previewPoint = lastPoint + (direction * distance);

            lineRenderer.positionCount = points.Count + 1;
            lineRenderer.SetPosition(points.Count, previewPoint);
        }

        if (Input.GetMouseButtonUp(0))
        {
            ConfirmPoint();
        }
    }

    private void ConfirmPoint()
    {
        if (lineRenderer.positionCount > points.Count)
        {
            Vector3 confirmedPoint = lineRenderer.GetPosition(points.Count);
            points.Add(confirmedPoint);
            CreateNode(confirmedPoint);
            remainingDashCount--;
        }

        if (remainingDashCount == 0)
        {
            //combatController.StartAttack();
        }
    }

    private void UpdateLineRenderer()
    {
        lineRenderer.positionCount = points.Count;
        for (int i = 0; i < points.Count; i++) lineRenderer.SetPosition(i, points[i]);
    }

    private Vector3 GetWorldMousePosition(float height)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0, height, 0));
        plane.Raycast(ray, out float enter);
        return ray.GetPoint(enter);
    }



    void CreateNode(Vector3 pos)
    {
        GameObject node = CombatController.SpawnNode(pos);
        spawnedNodes.Add(node);
    }

}