using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class space_inv_stats
{
    [Range(1,5)]
    public int maxHealth;
    [HideInInspector]
    public int currentHealth;
    [HideInInspector]
    public int maxLives = 3;
    [HideInInspector]
    public int currentLives;

    public float shipSpeed;
    public float fireRate;
}
