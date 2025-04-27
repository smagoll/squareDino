using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IMovable, IDamageable
{
    [SerializeField]
    private int _maxHp = 1;
    [SerializeField]
    private float _speed = 1;
    [SerializeField]
    private int _damage = 1;

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private NavMeshAgent _navMeshAgent;

    [SerializeField]
    private HealthBar _healthBar;

    private int _hp;

    private static readonly int Move = Animator.StringToHash("move");

    public bool IsDead { get; private set; }

    private void Start()
    {
        _navMeshAgent.speed = _speed;
        _healthBar.SetMaxHp(_maxHp);
        _hp = _maxHp;
    }

    public void MoveTo(Vector3 targetPosition)
    {
        _animator.SetBool(Move, true);
        _navMeshAgent.destination = targetPosition;

        StartCoroutine(WaitMove());
    }

    private IEnumerator WaitMove()
    {
        while (_navMeshAgent.pathPending)
            yield return null;

        while (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance)
            yield return null;

        _animator.SetBool(Move, false);
        TryAttack();
    }

    public void ApplyDamage(int damage)
    {
        _hp -= damage;
        _healthBar.UpdateBar(_hp);

        if (_hp <= 0)
        {
            Death();
        }
    }
    
    private void TryAttack()
    {
        Collider[] colliders = new Collider[10];
        var size = Physics.OverlapSphereNonAlloc(transform.position, 5f, colliders);
        
        for (int i = 0; i < size; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                if (colliders[i].TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(_damage);
                    break;
                }
            }
        }
    }

    private void Death()
    {
        IsDead = true;
        gameObject.SetActive(false);
    }
}