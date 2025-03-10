using UnityEngine;

public class ObjetoSostenidoDisparado : MonoBehaviour
{
    public bool estaDisparado=false;
    public EnemigoVida vidaDeEnemigo;


    public void objetoDisparo(){
        estaDisparado=true;
    }
    void OnCollisionEnter(Collision collision)
    {
        if(!estaDisparado)return;
        
        Collider objetoCollider = collision.collider;
        
        vidaDeEnemigo.recibirDamage(20f);
        
        bool esEnemigo= objetoCollider.GetComponent<EnemigoVida>();
            
        if (esEnemigo)
        {
            EnemigoVida vidaEnemigo = objetoCollider.GetComponent<EnemigoVida>();
            vidaEnemigo.recibirDamage(10f);
               

        }
        estaDisparado=false;

        
        
    }
}
