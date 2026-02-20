using UnityEngine;
using System.Collections.Generic;


namespace Scripts.Combat.States
{
    public class PlanState : CombatState
    {
        private List<Vector3> points = new List<Vector3>();
        private readonly List<GameObject> spawnedNodes = new List<GameObject>();
        private int remainingDashCount;
        private LineRenderer lineRenderer;

        public PlanState(CombatController CombatController, CombatBlackboard blackboard, LineRenderer lineRenderer) : base(CombatController, blackboard)
        {
            this.lineRenderer = lineRenderer;
        }

        public override void OnEnter()
        {
            Time.timeScale = 0.2f;
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
            ClearVisuals();
            CombatController.ClearNodes();
            Time.timeScale = 1f;
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
                    distance = hit.distance - 0.2f;
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
                Vector3 lastConfirmedPoint = points[points.Count - 1];
                Vector3 confirmedPoint = lineRenderer.GetPosition(points.Count);

                CombatBlackboard.DashPoints.Add(new DashCommand(lastConfirmedPoint, confirmedPoint));

                points.Add(confirmedPoint);
                CreateNode(confirmedPoint);
                remainingDashCount--;
            }

            if (remainingDashCount == 0)
            {
                //combatController changestate to dash state;
                CombatController.ChangeState(new DashState(CombatController, CombatBlackboard));
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

        void ClearVisuals()
        {
            lineRenderer.positionCount = 0;
            spawnedNodes.Clear();
        }

    }


}
