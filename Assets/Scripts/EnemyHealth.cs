using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float _currentHP, _maxHP;
    [SerializeField] GameObject _deathParticle;
    [SerializeField] Renderer _rend;
    AudioSource _audioSource;
    [SerializeField] AudioClip[] _hitSound, _deathSound;
    

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        _currentHP = _maxHP;
    }

    public float CurrentHealth()
    {
        return _currentHP;
    }

    public float MaxHealth()
    {
        return _maxHP;
    }

    public void TakeDamage(float damage)
    {
        _audioSource.PlayOneShot(_hitSound[Random.Range(0, _hitSound.Length - 1)], 3);
        _currentHP -= damage;
        _rend.material.SetFloat("_ShakeFXTime", Time.time);
        if (_currentHP <= 0)
            StartCoroutine(DeathTimer());
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(Random.Range(0.02f, 0.05f));
        _audioSource.PlayOneShot(_deathSound[Random.Range(0, _deathSound.Length - 1)], 3);
        GameObject hitParticle = Instantiate(_deathParticle, transform.position, Quaternion.identity);
        hitParticle.GetComponent<ParticleSystem>().Play();
        gameObject.SetActive(false);
    }
}
