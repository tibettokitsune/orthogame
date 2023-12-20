using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public int Health { get; private set; }
    public int HealthMax { get; private set; } = 10;

    public void Damage(int value)
    {
        if (Health - value > 0)
        { 
            return;
        }
    }

    public void AddHealth(int value)
    {
        if (Health + value > HealthMax)
        {
            Health = HealthMax;
            return;
        }
        Health += value;
    }
}
