using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] AudioClip _splashSound;
    [SerializeField] GameObject _deathParticle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<AudioSource>().PlayOneShot(_splashSound, 0.5f);
            GameObject hitParticle = Instantiate(_deathParticle, other.transform.position + Vector3.up * .5f, Quaternion.identity);
            hitParticle.GetComponent<ParticleSystem>().Play();
            GameManager.Instance.ReloadLevel();
        }
    }
}
