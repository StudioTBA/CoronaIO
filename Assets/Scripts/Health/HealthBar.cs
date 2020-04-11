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

    private void SetHealth(int health)
    {
        slider.value = health;

        // Set color to equivalent percentage in gradient scale
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public int GetHealth()
    {
        return Mathf.RoundToInt(slider.value);
    }

    /// <summary>
    /// Used to infict damage on agents.
    /// </summary>
    /// <param name="damagePercentage">If human has 100 units of health
    /// and damagePercentage is 20 (20%), 20 units will be removed.</param>
    public void TakeDamage(int damagePercentage)
    {
        SetHealth(Mathf.RoundToInt(slider.value) - Mathf.RoundToInt(slider.maxValue * damagePercentage / 100.0f));
    }
}