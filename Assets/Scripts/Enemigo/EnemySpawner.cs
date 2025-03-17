using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemigoCortoAlcanze;
    public GameObject enemigoLargoAlcanze;

    public List<Transform> spawnerList = new List<Transform>();
    public bool enemies = false;
    public int cantidadDeEnemigos = 10;

    public List<GameObject> enemyList = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
       for (int i = 0;i < cantidadDeEnemigos; i++)
        {
            int enemigoPorCrearAleatorio = Random.Range(0, 10);
            int lugarAleatorio = Random.Range(0, spawnerList.Count-1);
            if (enemigoPorCrearAleatorio == 0 || enemigoPorCrearAleatorio ==1 || enemigoPorCrearAleatorio ==2 )
            {
                GameObject newEnemy = Instantiate(enemigoLargoAlcanze);
                newEnemy.transform.position = spawnerList[lugarAleatorio].position;
                enemyList.Add(newEnemy);


            }
            else
            {
                GameObject newEnemy = Instantiate(enemigoCortoAlcanze);
                newEnemy.transform.position = spawnerList[lugarAleatorio].position;
                enemyList.Add(newEnemy);

            }
           
        }
    }

}
