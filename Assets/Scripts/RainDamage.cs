using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDamage : MonoBehaviour
{

    [SerializeField] AudioClip[] damageSound;

    Ray rayDown;
    RaycastHit rayHit;
    [SerializeField] float damage;
    [SerializeField] PlayerHealth playerHealth;
    float cooldown = 0.25f;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (cooldown == 0.25f)
        {
            cooldown -= Time.deltaTime;
            rayDown = new Ray(transform.position, Vector3.down * 12);
            Debug.DrawRay(transform.position, Vector3.down * 12);
            if (Physics.Raycast(rayDown, out rayHit))
            {
                if (rayHit.collider.gameObject.tag == "Cover")
                    return;
                else if (rayHit.collider.gameObject.tag == "Player")
                {
                    playerHealth.TakeDamage(damage);
                    playerHealth.RainDamageSound(damageSound[Random.Range(0, damageSound.Length - 1)]);
                }
            }
        }
        if (cooldown < 0.25f)
            cooldown -= Time.deltaTime;
        if (cooldown < 0)
            cooldown = 0.25f;
    }
}
