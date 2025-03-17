using NUnit.Framework;
using UnityEngine;

public class EnemigoBalaSpawnManager : MonoBehaviour
{
    public GameObject projectile;

    public int cantidadBalas = 15;

    public Transform lugarBalas;




    private void Awake()
    {
        for (int i = 0; i < cantidadBalas; i++)
        {
            GameObject balaNueva=Instantiate(projectile);
            balaNueva.transform.position = lugarBalas.position;
            

        }
    }

}
