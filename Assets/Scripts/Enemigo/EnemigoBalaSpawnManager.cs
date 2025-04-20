using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoBalaSpawnManager : MonoBehaviour
{
    public GameObject projectile;

    public int cantidadBalas = 15;
    public int aumentoCantidadBalas = 10;

    public Transform lugarBalas;

    public Queue<GameObject> listaBalasDisponibles = new Queue<GameObject>();

    public List<GameObject> listaBalasUsadas = new List<GameObject>();


    private void Awake()
    {
        for (int i = 0; i < cantidadBalas; i++)
        {
            GameObject balaNueva=Instantiate(projectile);
            balaNueva.transform.position = lugarBalas.position;
            EnemyBullet balaEnemigoScript = balaNueva.GetComponent<EnemyBullet>();
            balaEnemigoScript.balaManager = this;

            listaBalasDisponibles.Enqueue(balaNueva);
        }
    }

    public GameObject DispararBala()
    {
        GameObject balaDisparada = listaBalasDisponibles.Dequeue();
        listaBalasUsadas.Add(balaDisparada);

        return (balaDisparada);
    }



    public void RecargarBala(GameObject balaRecargar)
    {
        listaBalasUsadas.Remove(balaRecargar);
        listaBalasDisponibles.Enqueue(balaRecargar);
        Rigidbody rb = balaRecargar.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        balaRecargar.transform.position = lugarBalas.position;
        balaRecargar.SetActive(false);
    }






}
