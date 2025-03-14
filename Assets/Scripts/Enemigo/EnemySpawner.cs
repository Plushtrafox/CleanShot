using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //
    public GameObject enemigoCortoAlcanze;
    public GameObject enemigoLargoAlcanze;

    public List<Transform> SpawnerList = new List<Transform>();
    public bool enemies = false;
    public int cantidadDeEnemigos = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
       for (int i = 0;i < cantidadDeEnemigos; i++)
        {
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
