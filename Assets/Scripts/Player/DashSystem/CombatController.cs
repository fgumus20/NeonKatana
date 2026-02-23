using UnityEngine;
using Scripts.Combat.States;
using System.Collections.Generic;
using Scripts.Combat.Vfx;
using System;

namespace Scripts.Combat
{
    public class CombatController : MonoBehaviour
    {

        [Header("--- Modules ---")]
        [SerializeField] private PlayerStatsSO stats;
        [SerializeField] private LineRenderer lineRenderer;
        
        [SerializeField] private GameObject nodePrefab;
        private readonly List<GameObject> spawnedNodes = new List<GameObject>();

        private CombatState currentState;
        private CombatBlackboard blackboard;

        public event Action<int> OnDashSegmentStarted;
        public event Action OnDashSequenceCompleted;

        private void Start()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            blackboard = new CombatBlackboard(transform, rb, stats);
        }

        private void OnEnable()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.OnStateChanged += HandleStateChanged;
        }

        private void OnDisable()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.OnStateChanged -= HandleStateChanged;
        }

        private void Update()
        {
            if (GameManager.Instance == null) return;
            if (GameManager.Instance.CurrentState != GameState.Combat) return;

            currentState?.Update();

        }

        public void ChangeState(CombatState newState)
        {
            currentState?.OnExit();
            currentState = newState;
            currentState.OnEnter();
        }

        private void HandleStateChanged(GameState state)
        {

            if (state == GameState.Combat)
            {
                ChangeState(new PlanState(this, blackboard, lineRenderer));
            }
        }

        public GameObject SpawnNode(Vector3 position)
        {
            var node  = Instantiate(nodePrefab, position, Quaternion.identity);
            spawnedNodes.Add(node);
            return node;
        }

        public void ClearNodes()
        {
            foreach (var node in spawnedNodes)
                if (node != null) Destroy(node);

            spawnedNodes.Clear();
        }

        public void NotifyDashSegment(int index)
        {
            OnDashSegmentStarted?.Invoke(index);
        }

        public void NotifyDashEnded()
        {
            OnDashSequenceCompleted?.Invoke();
        }

    }

}
