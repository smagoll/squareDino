﻿using UnityEngine;

public interface IWeapon
{
    void Shoot(Vector3 targetPosition, Transform spawnPoint);
}