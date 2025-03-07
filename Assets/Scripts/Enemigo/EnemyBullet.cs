using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject enemyBullet;
    void Start()
    {
        Destroy(enemyBullet,6);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(enemyBullet);
        }
    }

}
