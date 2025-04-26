using System;
using UnityEngine;
using UnityEngine.Pool;

[Serializable]
public class Pistol : IWeapon
{
    private ObjectPool<Projectile> _projectilePool;
    
    public int Damage { get; private set; }

    public Pistol(int damage)
    {
        Damage = damage;
    }

    public void Init(ObjectPool<Projectile> projectilePool)
    {
        _projectilePool = projectilePool;
    }
    
    public void Shoot(Vector3 targetPosition, Transform spawnPoint)
    {
        Projectile projectile = _projectilePool.Get();
        projectile.Init(_projectilePool, this);
        projectile.Launch(spawnPoint.position, targetPosition);
    }
}