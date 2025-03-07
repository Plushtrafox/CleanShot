using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public Transform lugarMunicion;
    public Queue<GameObject> municionDisponible = new Queue<GameObject>();
    public List<GameObject> municionUsada = new List<GameObject>();

    public int magSize = 10;

    public float shotForce = 1500f;
    public float shotRate = 0.5f;
    private float shotRateTime = 0f;


    public float tiempoEntreRecarga = 5f;
    private float tiempoTotalRecarga = 0f;

    private bool reloading;

    private void Awake()
    {
        for (int i=0; i < magSize; i++)
        {
            GameObject newBullet = Instantiate(bullet, lugarMunicion.position, Quaternion.identity);
            
            ReloadBullet(newBullet);
        }
    }


    void Update()
    {
        if(reloading)
        {
            tiempoTotalRecarga -= Time.deltaTime;

            if(tiempoTotalRecarga <= 0) reloading = false;
        }

        if(Input.GetKeyDown(KeyCode.R) || municionDisponible.Count == 0)
        {
            Reload();
        }

        if (Input.GetButton("Fire1") && !reloading)
        {
            if(municionDisponible.Count > 0)
            {
                shotRateTime -= Time.deltaTime;

                if(shotRateTime <= 0)
                {
                    Shoot();

                    shotRateTime = shotRate;
                }
            }
        }
        else
        {
            shotRateTime = 0;
        }
    }

    private void Shoot()
    {
        GameObject newBullet = municionDisponible.Dequeue();

        municionUsada.Add(newBullet);
        
        UseBullet(newBullet);

        newBullet.transform.position = spawnPoint.position;

        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
                
        rb.AddForce(spawnPoint.forward * shotForce);
    }

    private void Reload()
    {
        reloading = true;
        tiempoTotalRecarga = tiempoEntreRecarga;

        foreach(GameObject bullet in municionUsada)
        {
            ReloadBullet(bullet);
        }

        municionUsada.Clear();
    }

    private void UseBullet(GameObject targetBullet)
    {
        targetBullet.SetActive(true);
    }

    private void ReloadBullet(GameObject targetBullet)
    {
        targetBullet.SetActive(false);
        municionDisponible.Enqueue(targetBullet);
        targetBullet.transform.position = lugarMunicion.position;
    }
}
