using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    [SerializeField]
    private EnemyCharge enemyCharge;

    private void Start()
    {
        slider.maxValue = enemyCharge.maxCharge;
        slider.value = enemyCharge.currentCharge;
    }

    private void Update()
    {
        slider.value = enemyCharge.currentCharge;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    //public void SetMaxHealth(int health)
    //{
    //    slider.maxValue = health;
    //    slider.value = health;

    //}

    //public void SetHealth(int health)
    //{
    //    slider.value = health;

    //    fill.color = gradient.Evaluate(slider.normalizedValue);
    //}
}
