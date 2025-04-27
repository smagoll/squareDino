using System;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 1f;

    private Rigidbody rb;
    
    private Vector3 _direction;
    private int _damage;
    private bool _isActive;

    private Pistol _pistol;
    private ObjectPool<Projectile> _pool;
    private IShooter _shooter;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Init(ObjectPool<Projectile> pool, Pistol pistol)
    {
        _pool = pool;
        _damage = pistol.Damage;
    }
    
    public void Launch(Vector3 startPosition, Vector3 targetPosition, IShooter shooter)
    {
        _direction = (targetPosition - startPosition).normalized;
        transform.position = startPosition;
        transform.forward = _direction;
        _shooter = shooter;
        _isActive = true;
    }
    
    private void FixedUpdate()
    {
        if (!_isActive) return;

        rb.MovePosition(transform.position + _direction * (_speed * Time.deltaTime));

        if (transform.position.magnitude > 1000f)
        {
            _isActive = false;
            gameObject.SetActive(false);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive) return;

        if (_shooter.IsChild(other.gameObject)) // игнорирование попадания в самого себя
        {
            return;
        }

        var damageableObject = other.GetComponentInParent<IDamageable>();
        
        if (damageableObject != null)
        {
            damageableObject.ApplyDamage(_damage);
        }
        
        _isActive = false;
        _pool.Release(this);
    }
}