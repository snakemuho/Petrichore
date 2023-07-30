using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] AudioSource source;
    [SerializeField] float currentHP, maxHP;
    float healCooldown = 2;
    PlayerUI ui;
    void Awake()
    {

        source = GetComponent<AudioSource>();
        ui = GetComponent<PlayerUI>();
    }
    void Start()
    {
        currentHP = maxHP;
        ui.SetHealth(currentHP / maxHP);
    }

    void FixedUpdate()
    {
        if (currentHP < maxHP)
        {
            if (healCooldown == 2)
            {
                healCooldown -= Time.deltaTime;
                Heal(1);
            }
            if (healCooldown < 2)
                healCooldown -= Time.deltaTime;
            if (healCooldown < 0)
                healCooldown = 2;
        }
    }
    public float CurrentHealth()
    {
        return currentHP;
    }

    public float MaxHealth()
    {
        return maxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        ui.SetHealth(currentHP / maxHP);
        healCooldown = 1.95f;
        if (currentHP <= 0)
            Death();
        CamShake.Instance.Shake();
    }
    public void RainDamageSound(AudioClip clip)
    {
        source.PlayOneShot(clip, 0.7f);
    }    

    public void Heal(float amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
        ui.SetHealth(currentHP / maxHP);
    }
    void Death()
    {
        GameManager.Instance.ReloadLevel();
    }
}
