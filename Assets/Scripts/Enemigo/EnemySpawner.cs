using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public EnemigoBalaSpawnManager balaManager;

    public GameObject enemigoCortoAlcanze;
    public GameObject enemigoLargoAlcanze;

    public List<Transform> spawnerList = new List<Transform>();

    public bool enemies = false;
    public int cantidadDeEnemigos = 10;

    public PlayerVida jugadorVida;

    public GameObject jugador;

    //nuevos

    public GameObject managerDeEscena;

    public PuntosScript scriptDePuntos;
    
    

    public List<GameObject> enemyList = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform lugar in spawnerList)
        {
            Vector3 spawnPosition = lugar.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(spawnPosition, out hit, 5.0f, NavMesh.AllAreas);
            lugar.position = hit.position;
            

        }


        for (int i = 0; i < cantidadDeEnemigos; i++)
        {




            int enemigoPorCrearAleatorio = Random.Range(0, 10);
            int lugarAleatorio = Random.Range(0, spawnerList.Count - 1);
            if (enemigoPorCrearAleatorio == 0 || enemigoPorCrearAleatorio == 1 || enemigoPorCrearAleatorio == 2)
            {
                GameObject newEnemy = Instantiate(enemigoLargoAlcanze);
                newEnemy.transform.position = spawnerList[lugarAleatorio].position;
                IAEnemyPart2 enemigoLargoAlcance = newEnemy.GetComponent<IAEnemyPart2>();
                enemigoLargoAlcance.balasManager = balaManager;
                enemyList.Add(newEnemy);

                EnemigoVida vidaNewEnemigo = newEnemy.GetComponent<EnemigoVida>();
                vidaNewEnemigo.puntosDeKillGameObject = managerDeEscena;
                vidaNewEnemigo.puntosDeKill = scriptDePuntos;

                enemigoLargoAlcance.player = jugador.transform;



            }
            else
            {
                GameObject newEnemy = Instantiate(enemigoCortoAlcanze);
                newEnemy.transform.position = spawnerList[lugarAleatorio].position;
                enemyList.Add(newEnemy);

                AtaqueEnemigoCortoAlcance enemigoCortoAlcance = newEnemy.GetComponent<AtaqueEnemigoCortoAlcance>();
                EnemigoCortoAlcanceScript scriptEnemigoCortoAlcance = newEnemy.GetComponent<EnemigoCortoAlcanceScript>();

                enemigoCortoAlcance.vidaJugador = jugadorVida;
                enemigoCortoAlcance.jugador = jugador;

                EnemigoVida vidaNewEnemigo = newEnemy.GetComponent<EnemigoVida>();
                vidaNewEnemigo.puntosDeKillGameObject = managerDeEscena;
                vidaNewEnemigo.puntosDeKill = scriptDePuntos;
                scriptEnemigoCortoAlcance.target = jugador;


            }

        }
    }

}
