using UnityEngine;

public class TriggerSystem : MonoBehaviour
{
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("EnemyHitbox"))
        {
            print("Daño");
        }
    }
}
