using UnityEngine;

public class BalaScript : MonoBehaviour
{
    public bool estaDisparado=false;
    public int damageShot=5;

    public void objetoDisparo(){
        estaDisparado=true;
    }
    void OnCollisionEnter(Collision collision)
    {
        
        if(!estaDisparado)return;
        
        Collider objetoCollider = collision.collider;
        
        bool esEnemigo= objetoCollider.GetComponent<EnemigoVida>();
            
        if (esEnemigo)
        {
            EnemigoVida vidaEnemigo = objetoCollider.GetComponent<EnemigoVida>();
            vidaEnemigo.recibirDamage(damageShot);
               

        }
        estaDisparado=false;

        
        
    }

}
