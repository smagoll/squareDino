using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] 
    private Slider _slider;

    public void SetMaxHp(int maxHealth)
    {
        _slider.maxValue = maxHealth;
        UpdateBar(maxHealth);
    }
    
    public void UpdateBar(int health)
    {
        _slider.value = health;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
