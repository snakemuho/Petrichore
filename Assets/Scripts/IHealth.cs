using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth 
{
    public float MaxHealth();
    public float CurrentHealth();
    public void TakeDamage(float damage);
    

}
