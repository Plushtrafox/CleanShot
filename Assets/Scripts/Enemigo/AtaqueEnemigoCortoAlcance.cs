using UnityEngine;

public class AtaqueEnemigoCortoAlcance : MonoBehaviour
{

    public GameObject jugador;
    public PlayerVida vidaJugador;

    private int damageAtaque =10;

    public EnemigoCortoAlcanceScript movimientoEnemigoCortoAlcance;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == jugador.name)
        {
            
            movimientoEnemigoCortoAlcance.CancelarAtacarJugador();
            damagePlayer();

        }

    }


    void damagePlayer()
    {
        vidaJugador.reducirVida(damageAtaque);

    }
}
