using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class EnemigoVida : MonoBehaviour
{
    public PuntosScript puntosDeKill;

    public Slider vidaEnemigoSlider;

    public Transform jugador;

    public GameObject vidaEnemigoUI;

    public GameObject puntosDeKillGameObject;

    public int vidaEnemigo = 100;
    public int puntosPorEliminar = 25;

    private void Awake()
    {
        puntosDeKillGameObject = GameObject.Find("EscenaManager");
        if (puntosDeKillGameObject)
        {
            puntosDeKill = puntosDeKillGameObject.GetComponent<PuntosScript>();
        }
    }

    private void Update()
    {
        
    }


    public void recibirDamage(int damage)
    {
        
        vidaEnemigo -= damage;
        vidaEnemigoSlider.value = vidaEnemigo;
        print("recibio damage");

        if (vidaEnemigo <= 0)
        {
            Destroy(gameObject);
            puntosDeKill.SumarPuntos(puntosPorEliminar);
        }
    }
    public void mirarJugador()
    {
        vidaEnemigoUI.transform.forward=jugador.forward*-1;
    }
}
