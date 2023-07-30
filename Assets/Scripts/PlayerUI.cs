using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Image healthBar;

    public void SetHealth(float amount)
    {
        healthBar.fillAmount = amount;
    }
}
