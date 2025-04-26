using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IMovable
{
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private NavMeshAgent _navMeshAgent;
    
    public bool IsDead { get; private set; }

    private void Start()
    {
        _navMeshAgent.speed = speed;
    }

    public void MoveTo(Vector3 targetPosition)
    {
        _navMeshAgent.destination = targetPosition;

        StartCoroutine(WaitMove());
    }

    private IEnumerator WaitMove()
    {
        while (_navMeshAgent.pathPending)
            yield return null;

        while (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance)
            yield return null;

        Debug.Log("Враг остановился перед игроком!");
    }
}