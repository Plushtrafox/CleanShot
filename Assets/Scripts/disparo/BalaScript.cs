using UnityEngine;

public class BalaScript : MonoBehaviour
{
    public bool estaDisparado=false;
    public float damageShot=5f;

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
