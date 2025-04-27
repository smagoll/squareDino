using System;
using UnityEngine;
using UnityEngine.Pool;

[Serializable]
public class Pistol : IWeapon
{
    [SerializeField] 
    private Projectile _projectilePrefab;
    [SerializeField]
    private int _damage;

    public int Damage => _damage;
    
    public void Shoot(Vector3 targetPosition, Transform spawnPoint)
    {
        var pool = PoolSystem.Instance.GetOrCreatePool(_projectilePrefab);
        Projectile projectile = pool.Get();
        
        projectile.Init(pool, this);
        projectile.Launch(spawnPoint.position, targetPosition);
    }
}