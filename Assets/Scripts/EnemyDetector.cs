using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] LightningAI _enemy;

    private void OnTriggerStay(Collider other)
    {
        _enemy.TriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _enemy.TriggerExit(other);
    }
}
