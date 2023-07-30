using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

public class ItemCollection : MonoBehaviour
{
    [FormerlySerializedAs("pickupGet")] private AudioClip[] _pickupGet;
    [FormerlySerializedAs("pickupLost")] private AudioClip[] _pickupLost;
    AudioSource _audioSource;
    [FormerlySerializedAs("countUI")][SerializeField] private TextMeshProUGUI _countUI;
    public int UmbrellasInInventory { get; private set; } = 0;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ItemGet()
    {
        _audioSource.PlayOneShot(_pickupGet[Random.Range(0, _pickupGet.Length - 1)]);
        UmbrellasInInventory++;
        _countUI.text = "x " + UmbrellasInInventory;
    }
    public void ItemRemove()
    {
        _audioSource.PlayOneShot(_pickupLost[Random.Range(0, _pickupLost.Length - 1)]);
        UmbrellasInInventory--;
        _countUI.text = "x " + UmbrellasInInventory;
    }
}
