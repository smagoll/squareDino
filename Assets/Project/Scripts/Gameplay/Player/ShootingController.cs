using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShootingController : MonoBehaviour, IShooter
{
    [SerializeField] 
    private Transform _spawnPoint;
    
    private Camera _camera;
    private IWeapon _weapon;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void Init(IWeapon weapon)
    {
        _weapon = weapon;
        _weapon.Claim(this);
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
        }
        else
        {
            targetPosition = ray.GetPoint(100f);
        }
        
        _weapon.Shoot(targetPosition, _spawnPoint);
    }

    public bool IsChild(GameObject go)
    {
        return go.transform.IsChildOf(transform);
    }
}
