using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _zapSound;

    [SerializeField] float _damage;
    float _damageCD = 0.5f;
    private void FixedUpdate()
    {
        if (_damageCD < 0.5f)
            _damageCD -= Time.deltaTime;
        if (_damageCD <= 0)
            _damageCD = 0.5f;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && _damageCD == 0.5f)
        {
            _damageCD -= Time.deltaTime;
            other.GetComponent<IHealth>().TakeDamage(_damage);
            _audioSource.PlayOneShot(_zapSound[Random.Range(0, _zapSound.Length - 1)]);
        }
    }
}
