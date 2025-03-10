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
    public float shotRate = 0.1f;
    private float shotRateTime = 0f;


    public float tiempoEntreRecarga = 1f;
    private float tiempoTotalRecarga = 0f;

    private bool reloading;

    public bool estaSosteniendooObjetoPoder=false;

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
        if (!estaSosteniendooObjetoPoder)
        {
            if (reloading)
            {
                tiempoTotalRecarga -= Time.deltaTime;

                if (tiempoTotalRecarga <= 0) reloading = false;
            }

            if (Input.GetKeyDown(KeyCode.R) || municionDisponible.Count == 0)
            {
                Reload();
            }

            if (Input.GetButton("Fire1") && !reloading)
            {
                if (municionDisponible.Count > 0)
                {
                    shotRateTime -= Time.deltaTime;

                    if (shotRateTime <= 0)
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
    

    }

    private void Shoot()
    {
        GameObject newBullet = municionDisponible.Dequeue();

        municionUsada.Add(newBullet);


        UseBullet(newBullet);

        newBullet.transform.position = spawnPoint.position;
        newBullet.transform.rotation = spawnPoint.rotation;

        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero; // Reiniciar la velocidad
        rb.angularVelocity = Vector3.zero; // Reiniciar la velocidad angular

        rb.AddForce(spawnPoint.forward * shotForce);
    }

    private void Reload()
    {
        print("recargando empieza");
        reloading = true;
        tiempoTotalRecarga = tiempoEntreRecarga;

        foreach(GameObject bullet in municionUsada)
        {
            ReloadBullet(bullet);
        }

        municionUsada.Clear();
        print("recargando termina");
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
