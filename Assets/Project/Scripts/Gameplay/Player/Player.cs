using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private ShootingController _shootingController;
    
    public void Init(IWeapon weapon)
    {
        _shootingController.Init(weapon);
    }
}
