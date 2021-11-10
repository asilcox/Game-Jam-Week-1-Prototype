using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    [SerializeField]
    private Health playerHealth;

    private void Start()
    {
        slider.maxValue = playerHealth.GetHealth();
        fill.color = gradient.Evaluate(.9f);
    }

    private void Update()
    {
        slider.value = playerHealth.GetHealth();
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    //public void SetMaxHealth(int health)
    //{
    //    slider.maxValue = health;
    //    slider.value = health;

    //    //use .9f to start the health bar at green. will be utilized for enemy bars
    //    fill.color = gradient.Evaluate(.9f);

    //}

    //public void SetHealth(int health)
    //{
    //    slider.value = health;

    //    fill.color = gradient.Evaluate(slider.normalizedValue);
    //}
}
