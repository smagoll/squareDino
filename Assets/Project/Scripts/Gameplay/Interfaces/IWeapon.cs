using UnityEngine;
using UnityEngine.Pool;

public interface IWeapon
{
    void Init(ObjectPool<Projectile> projectilePool);
    void Shoot(Vector3 targetPosition, Transform spawnPoint);
}