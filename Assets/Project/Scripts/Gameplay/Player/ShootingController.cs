using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShootingController : MonoBehaviour
{
    [SerializeField] 
    private Transform _spawnPoint;
    
    private Camera _camera;
    private IWeapon _weapon;
    private ObjectPool<Projectile> _projectilePool;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void Init(IWeapon weapon)
    {
        _weapon = weapon;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPosition;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPosition = hit.point;
            Debug.DrawLine(ray.origin, hit.point, Color.red, 1.0f);
        }
        else
        {
            targetPosition = ray.GetPoint(100f);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.blue, 1.0f);
        }
        
        _weapon.Shoot(targetPosition, _spawnPoint);
    }
}
