using UnityEngine;

public class ObjetoSostenidoDisparado : MonoBehaviour
{
    public bool estaDisparado=false;
    public EnemigoVida vidaDeEnemigo;
    public int damageDisparo = 20;


    public void objetoDisparo(){
        estaDisparado=true;
    }
    void OnCollisionEnter(Collision collision)
    {
        
        if(!estaDisparado)return;
        
        Collider objetoCollider = collision.collider;
        
        vidaDeEnemigo.recibirDamage(damageDisparo);
        
        bool esEnemigo= objetoCollider.GetComponent<EnemigoVida>();
            
        if (esEnemigo)
        {
            EnemigoVida vidaEnemigo = objetoCollider.GetComponent<EnemigoVida>();
            vidaEnemigo.recibirDamage(damageDisparo/2);
               

        }
        estaDisparado=false;

        
        
    }
}
