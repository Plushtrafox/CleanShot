using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public PlayerVida vidaJugador;
    public int damageBala=20;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            vidaJugador = collision.gameObject.GetComponent<PlayerVida>();

            vidaJugador.reducirVida(damageBala);
            Destroy(gameObject);
           

        }
    }

}
