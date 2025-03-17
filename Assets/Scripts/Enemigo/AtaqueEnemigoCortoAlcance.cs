using UnityEngine;

public class AtaqueEnemigoCortoAlcance : MonoBehaviour
{
    [SerializeField]
    private float tiempoEntreAtaque = 1f;
    public GameObject jugador;
     public PlayerVida vidaJugador;
    [SerializeField]
    private int damageAtaque =10;

    private void Awake()
    {
        jugador = GameObject.Find("==Player==");
        vidaJugador = jugador.GetComponent<PlayerVida>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == jugador.name)
        {
            InvokeRepeating("damagePlayer", 0f, tiempoEntreAtaque);
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.name == jugador.name)
        {
            CancelInvoke();
        }

    }


    void damagePlayer()
    {
        vidaJugador.reducirVida(damageAtaque);

    }
}
