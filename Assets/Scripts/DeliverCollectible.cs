using UnityEngine;

public class DeliverCollectible : MonoBehaviour
{
    [SerializeField] AudioClip _errorSound;
    [SerializeField] GameObject _bringText, _thankText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<ItemCollection>().UmbrellasInInventory > 0)
            {
                _thankText.SetActive(true);
                other.GetComponent<ItemCollection>().ItemRemove();
                GameManager.Instance.AddCollected();
                gameObject.SetActive(false);
            }
            else
            {
                _bringText.SetActive(true);
                other.GetComponent<AudioSource>().PlayOneShot(_errorSound);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _thankText.SetActive(false);
        _bringText.SetActive(false);
    }
}
