using UnityEngine;

public interface IWeapon
{
    void Shoot(Vector3 targetPosition, Transform spawnPoint);
    void Claim(IShooter shooter);
}

public interface IShooter
{
    bool IsChild(GameObject go);
}