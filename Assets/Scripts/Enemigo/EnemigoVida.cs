using UnityEngine;

public class EnemigoVida : MonoBehaviour
{
    public PuntosScript puntosDeKill;

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

    public void recibirDamage(int damage)
    {
        vidaEnemigo -= damage;

        if (vidaEnemigo <= 0)
        {
            puntosDeKill.SumarPuntos(puntosPorEliminar);
            Destroy(gameObject);     
        }
    }
}
