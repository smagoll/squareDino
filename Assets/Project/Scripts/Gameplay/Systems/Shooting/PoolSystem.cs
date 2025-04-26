using System;
using UnityEngine;
using UnityEngine.Pool;

public class PoolSystem : MonoBehaviour
{
    [SerializeField]
    private Projectile _projectile;
    [SerializeField]
    private Transform _projectileTransform;

    public ObjectPool<Projectile> CreatePool()
    {
        ObjectPool<Projectile> pool = new(() => 
                Instantiate(_projectile, _projectileTransform), 
            item => {
                item.gameObject.SetActive(true);
            }, item => {
                item.gameObject.SetActive(false);
            }, item => {
                Destroy(item.gameObject);
            }, false);

        return pool;
    }
}