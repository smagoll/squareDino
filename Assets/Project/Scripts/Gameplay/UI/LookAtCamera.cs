using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main; // Получаем основную камеру
    }

    private void Update()
    {
        if (mainCamera != null)
        {
            transform.LookAt(mainCamera.transform); // Поворачиваем объект к камере
            transform.Rotate(0, 180, 0);
        }
    }
}
