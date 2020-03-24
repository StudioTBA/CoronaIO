using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Exclusively manages an agent's health bar including its color
/// This script is not involved with an object's actual health value
/// </summary>
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        // Set color to max in gradient
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        // Set color to equivalent percentage in gradient scale
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
