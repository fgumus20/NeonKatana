using UnityEngine;
using UnityEngine.AI;

public class NavMeshChaseTest : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    void Awake() => agent = GetComponent<NavMeshAgent>();

    void Update()
    {
        if (target != null)
            agent.SetDestination(target.position);
    }
}
