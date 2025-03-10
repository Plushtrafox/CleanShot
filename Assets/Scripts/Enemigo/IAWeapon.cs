using UnityEngine;

public class IAWeapon : MonoBehaviour
{
    private GameObject target;
    public GameObject enemyBullet;
    public Transform enemyBulletPosition;
    private Transform playerPosition;
    public float bulletVelocity = 100f
    ;
    void Start()
    {
        target = GameObject.Find("Player");
        playerPosition = FindAnyObjectByType<PlayerController>().transform;
        Invoke("ShootPlayer",5);
    }

    
    void Update()
    {
        
    }

    void ShootPlayer()
    {
        Vector3 playerDirection = playerPosition.position - transform.position;
        GameObject newBullet;
        newBullet = Instantiate(enemyBullet,enemyBulletPosition.position,enemyBulletPosition.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(playerDirection*bulletVelocity,ForceMode.Force);
        Invoke("ShootPlayer",3);
    }
}
