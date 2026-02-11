using UnityEngine;
using System.Collections.Generic;
using Scripts.Combat;


public class CombatController : MonoBehaviour
{
    private enum CombatSubState {None, Planning, Executing}

    [Header("--- Modules ---")]
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerStatsSO stats;

    //[Header("--- Focus Settings ---")]
    //[SerializeField] private float planningTimeScale = 0.2f;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject nodePrefab;

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

        if(state == GameState.Combat)
        {
            ChangeState(new PathPlanner(this, blackboard, lineRenderer));
        }
    }

    public GameObject SpawnNode(Vector3 position)
    {
        return Instantiate(nodePrefab, position, Quaternion.identity);
    }
}