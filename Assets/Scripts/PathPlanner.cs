using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class PathPlanner : MonoBehaviour
{
    [Header("--- Settings ---")]
    [SerializeField] private int maxMoveCount = 3;
    [SerializeField] private float maxDashDistance = 6f;
    [SerializeField] private float dashDuration = .2f;

    [Header("--- Visuals ---")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject nodePrefab;

    //[Header("--- Layers ---")]
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask enemyLayer;

    private List<Vector3> pathPoints = new List<Vector3>();
    private List<GameObject> spawnedNodes = new List<GameObject>();
    private bool isDragging = false; // is mouse clicked??


    void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged += HandleStateChanged;
    }

    void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.Planning:
                ClearVisuals();
                PreparePlanning();
                break;
            case GameState.Roaming:
                ClearVisuals();
                break;

        }
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Planning) return;

        HandleDrawingInput();
    }

    void PreparePlanning()
    {
        pathPoints.Clear();
        spawnedNodes.Clear();

        // first point is main character
        pathPoints.Add(transform.position);

        lineRenderer.positionCount = 1;

        lineRenderer.SetPosition(0, transform.position);

        CreateNode(transform.position);
    }

    void HandleDrawingInput()
    {
        if (pathPoints.Count > maxMoveCount) return;

        // Referance point
        Vector3 lastFixedPoint = pathPoints[pathPoints.Count - 1];

        Vector3 cursorWorldPos = GetWorldPositionAtHeight(Input.mousePosition, lastFixedPoint.y);

        // first touch
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lineRenderer.positionCount = pathPoints.Count + 1;
            lineRenderer.SetPosition(pathPoints.Count, lastFixedPoint);
        }

        if (Input.GetMouseButton(0) && isDragging)
        {

            Vector3 direction = (cursorWorldPos - lastFixedPoint).normalized;
            float distance = Vector3.Distance(cursorWorldPos, lastFixedPoint);

            distance = Mathf.Clamp(distance, 0, maxDashDistance);

            Vector3 previewPoint = lastFixedPoint + (direction * distance);
            lineRenderer.SetPosition(pathPoints.Count, previewPoint);
        }


        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            Vector3 finalPoint = lineRenderer.GetPosition(pathPoints.Count);

            if (Vector3.Distance(lastFixedPoint, finalPoint) < 0.5f)
            {
                lineRenderer.positionCount = pathPoints.Count;
                return;
            }

            pathPoints.Add(finalPoint);
            CreateNode(finalPoint);
            if (pathPoints.Count > maxMoveCount)
            {

                StartAttackSequence(pathPoints);
                ClearVisuals();
            }
        }
    }

    void StartAttackSequence(List<Vector3> pathPoints)
    {
        GameManager.Instance.ChangeState(GameState.Attacking);

        Sequence attackSequence = DOTween.Sequence();

        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            Vector3 targetPos = pathPoints[i + 1];

            targetPos.y = transform.position.y;

            Tween moveTween = transform.DOMove(targetPos, dashDuration)
                .SetEase(Ease.InQuart)
                .OnStart(() => transform.LookAt(targetPos));
            attackSequence.Append(moveTween);
        }

        attackSequence.OnComplete(() =>
        {
            GameManager.Instance.ChangeState(GameState.Roaming);
        });
    }

    Vector3 GetWorldPositionAtHeight(Vector3 screenPos, float height)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        Plane dynamicPlane = new Plane(Vector3.up, new Vector3(0, height, 0));

        if (dynamicPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }

    void CreateNode(Vector3 pos)
    {
        GameObject node = Instantiate(nodePrefab, pos, Quaternion.identity);
        spawnedNodes.Add(node);
    }
    
    void ClearVisuals()
    {
        pathPoints.Clear();
        lineRenderer.positionCount = 0;
        foreach (var node in spawnedNodes) Destroy(node);
        spawnedNodes.Clear();
    }

}