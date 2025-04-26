using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private PoolSystem _poolSystem;
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;
    [SerializeField]
    private Level _level;
    [SerializeField]
    private Player _playerPrefab;

    private void Start()
    {
        IWeapon weapon = new Pistol(1);
        var pool = _poolSystem.CreatePool();
        weapon.Init(pool);
        
        var player = Instantiate(_playerPrefab, _level.WayPoints[0].transform.position, Quaternion.identity, _level.transform);
        player.Init(weapon);

        _virtualCamera.Follow = player.transform;
        _virtualCamera.LookAt = player.transform;
        
        LevelController levelController = new LevelController(_level, player);
        
        StartCoroutine(WaitForScreenTap(levelController));
    }
    
    private IEnumerator WaitForScreenTap(LevelController levelController)
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        levelController.Launch();
    }
}
