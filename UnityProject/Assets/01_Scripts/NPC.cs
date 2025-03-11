using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Idle,
    Wandering,
}

public class NPC : MonoBehaviour
{
    [Header("Status")]
    public float walkSpeed;

    [Header("AI")]
    public float detectDistance;
    private AIState aIState;

    private NavMeshAgent agent;
    private float playerDistance;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        aIState = AIState.Wandering;   
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(transform.position, CharacterManager.Instance.Player.controller.body.transform.position);
        switch(aIState)
        {
            case AIState.Idle:
            case AIState.Wandering:
                PassiveUpdate();
                break;
            default:
                break;
        }
    }
    
    void PassiveUpdate()
    {
        if(aIState == AIState.Wandering && agent.remainingDistance < .1f)
        {
            Invoke(nameof(WanderToNewLocation), 1f);
        }
    }

    void WanderToNewLocation()
    {
        if (aIState == AIState.Idle) return;

        agent.SetDestination(GetWanderLocation());
    }

    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        int i = 0;

        do {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(5, 20)), out hit, 20, NavMesh.AllAreas);
            if(++i == 30) break;
        } while (Vector3.Distance(transform.position, hit.position) < detectDistance);

        return hit.position;
    }
}
