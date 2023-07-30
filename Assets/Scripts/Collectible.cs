using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(0, 10, 0);   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<ItemCollection>().ItemGet();
            gameObject.SetActive(false);
        }
    }
}
