using UnityEngine;

public class Shot : MonoBehaviour
{



    public GameObject bullet;
    public Transform spawnPoint;

    public float shotForce = 1500f;
    public float shotRate = 0.5f;

    private float shotRateTime = 0f;


    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {

            if (Time.time > shotRateTime)
            {
                GameObject newBullet;

                newBullet = Instantiate(bullet,spawnPoint.position,spawnPoint.rotation);

                newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);

                shotRateTime = shotRate + shotRateTime;

                Destroy(newBullet,2);
               
            }


        }


    }
}
