using UnityEngine;

[RequireComponent(typeof(ShootingController), typeof(PlayerController))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float hp = 1;
    
    private ShootingController _shootingController;
    private PlayerController _playerController;
    
    private void Awake()
    {
        _shootingController = GetComponent<ShootingController>();
        _playerController = GetComponent<PlayerController>();
    }
    
    public void Init(IWeapon weapon)
    {
        _shootingController.Init(weapon);
    }

    public void ApplyDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            GameEvents.RaiseLose();
        }
    }
    
    public void EnableControls()
    {
        _shootingController.enabled = true;
        _playerController.enabled = true;
    }

    public void DisableControls()
    {
        _shootingController.enabled = false;
        _playerController.enabled = false;
    }
}
