using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PathPlanner : MonoBehaviour
{
    [Header("--- Modules ---")]
    [SerializeField] private PlayerStatsSO stats;
    [SerializeField] private DashExecutor executor;

    [Header("--- Visuals ---")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject nodePrefab;

    private readonly List<Vector3> pathPoints = new List<Vector3>();
    private readonly List<GameObject> spawnedNodes = new List<GameObject>();
    private bool isDragging = false;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

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
        if (newState == GameState.Planning)
        {
            ClearVisuals();
            PreparePlanning();
        }
        else if (newState == GameState.Roaming)
        {
            ClearVisuals();
        }
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Planning) return;
        HandleInput();
    }

    void PreparePlanning()
    {
        pathPoints.Clear();
        spawnedNodes.Clear();

        Vector3 startPos = transform.position;
        pathPoints.Add(startPos);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, startPos);
        CreateNode(startPos);
    }

    void HandleInput()
    {
        if (pathPoints.Count > stats.maxMoveCount) return;

        if (Input.GetMouseButtonDown(0))
        {

            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.touchCount > 0 && EventSystem.current != null &&
                EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;
        }

        Vector3 lastPoint = pathPoints[pathPoints.Count - 1];

        Vector2 screenPos = GetPointerScreenPosition();
        Vector3 cursorWorldPos = GetWorldPositionAtHeight(screenPos, lastPoint.y);

        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lineRenderer.positionCount = pathPoints.Count + 1;
            lineRenderer.SetPosition(pathPoints.Count, lastPoint);
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 delta = cursorWorldPos - lastPoint;
            if (delta.sqrMagnitude < 0.0001f)
            {
                lineRenderer.SetPosition(pathPoints.Count, lastPoint);
                return;
            }

            Vector3 dir = delta.normalized;
            float dist = Mathf.Clamp(delta.magnitude, 0f, stats.maxDashDistance);

            if (Physics.Raycast(lastPoint, dir, out RaycastHit hit, dist, stats.obstacleLayer))
            {
                dist = Mathf.Max(0f, hit.distance - 0.5f);
            }

            Vector3 previewPoint = lastPoint + (dir * dist);
            lineRenderer.SetPosition(pathPoints.Count, previewPoint);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            Vector3 finalPoint = lineRenderer.GetPosition(pathPoints.Count);

            if (Vector3.Distance(lastPoint, finalPoint) < 0.5f)
            {
                lineRenderer.positionCount = pathPoints.Count;
                return;
            }

            pathPoints.Add(finalPoint);
            CreateNode(finalPoint);

            if (pathPoints.Count > stats.maxMoveCount)
            {
                SendToExecutor();
            }
        }
    }

    void SendToExecutor()
    {
        List<DashCommand> commands = new List<DashCommand>();

        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            commands.Add(new DashCommand(pathPoints[i], pathPoints[i + 1], stats));
        }

        executor.Execute(commands, () =>
        {
            ClearVisuals();
        });
    }

    private Vector2 GetPointerScreenPosition()
    {
        if (Input.touchCount > 0)
            return Input.GetTouch(0).position;

        return Input.mousePosition;
    }

    private Vector3 GetWorldPositionAtHeight(Vector2 screenPos, float height)
    {
        if (cam == null) cam = Camera.main;
        if (cam == null) return Vector3.zero;

        Ray ray = cam.ScreenPointToRay(screenPos);
        Plane plane = new Plane(Vector3.up, new Vector3(0f, height, 0f));
        return plane.Raycast(ray, out float d) ? ray.GetPoint(d) : Vector3.zero;
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

        foreach (var node in spawnedNodes)
            if (node != null) Destroy(node);

        spawnedNodes.Clear();
    }
}
