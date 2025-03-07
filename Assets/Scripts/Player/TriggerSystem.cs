using UnityEngine;

public class TriggerSystem : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("EnemyHitbox"))
        {
            print("Damage");
        }
    }
}
