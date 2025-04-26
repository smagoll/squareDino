using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 1f;
    
    private Vector3 _direction;
    private int _damage;
    private bool _isActive;

    private Pistol _pistol;
    private ObjectPool<Projectile> _pool;

    public void Init(ObjectPool<Projectile> pool, Pistol pistol)
    {
        _pool = pool;
        _damage = pistol.Damage;
    }
    
    public void Launch(Vector3 startPosition, Vector3 targetPosition)
    {
        _direction = (targetPosition - startPosition).normalized;
        transform.position = startPosition;
        transform.forward = _direction;
        _isActive = true;
    }
    
    private void Update()
    {
        if (!_isActive) return;

        transform.position += _direction * (_speed * Time.deltaTime);

        if (transform.position.magnitude > 1000f)
        {
            _isActive = false;
            gameObject.SetActive(false);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            if (other.TryGetComponent<IDamageable>(out var damageableObject))
            {
                damageableObject.ApplyDamage(_damage);
            }
        }
        
        _isActive = false;
        _pool.Release(this);
    }
}