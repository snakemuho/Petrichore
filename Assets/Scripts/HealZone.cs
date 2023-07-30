using UnityEngine;

public class HealZone : MonoBehaviour
{
    PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
            _playerHealth.Heal(0.25f);
    }
}
