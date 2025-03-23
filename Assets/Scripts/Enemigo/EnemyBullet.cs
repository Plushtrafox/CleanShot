using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public PlayerVida vidaJugador;
    public int damageBala=20;

    public EnemigoBalaSpawnManager balaManager;

    public void Disparar()
    {
        Invoke("RecargarBalaUsada", 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            vidaJugador = collision.gameObject.GetComponent<PlayerVida>();

            vidaJugador.reducirVida(damageBala);
            balaManager.RecargarBala(gameObject);


        }
        else if (collision.gameObject.CompareTag("Escenario"))
        {

             balaManager.RecargarBala(gameObject);
        }
    }

    void RecargarBalaUsada()
    {
        balaManager.RecargarBala(gameObject);
    }

}
