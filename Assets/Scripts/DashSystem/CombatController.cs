using UnityEngine;
using Scripts.Combat.States;
using System.Collections.Generic;

namespace Scripts.Combat
{
    public class CombatController : MonoBehaviour
    {

        [Header("--- Modules ---")]
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerStatsSO stats;

        //[Header("--- Focus Settings ---")]
        //[SerializeField] private float planningTimeScale = 0.2f;

        [SerializeField] private LineRenderer lineRenderer;
        
        [SerializeField] private GameObject nodePrefab;//TO DO: create node pool to manage plan state
        private readonly List<GameObject> spawnedNodes = new List<GameObject>();

        private CombatState currentState;
        private CombatBlackboard blackboard;


        private void Start()
        {
            GameManager.Instance.OnStateChanged += HandleStateChanged;
            Rigidbody rb = GetComponent<Rigidbody>();
            blackboard = new CombatBlackboard(transform, rb, animator, stats);
        }



        private void Update()
        {
            if (GameManager.Instance.CurrentState == GameState.Combat)
            {
                currentState.Update();
            }

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
    }

}
