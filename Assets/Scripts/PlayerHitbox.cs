using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] GameObject hitPartic;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().TakeDamage(playerData.damage);
            GameObject hitParticle = Instantiate(hitPartic, other.transform.position, Quaternion.identity);
            hitParticle.GetComponent<ParticleSystem>().Play();
        }
    }
}
