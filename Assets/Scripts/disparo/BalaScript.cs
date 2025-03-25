using UnityEngine;

public class BalaScript : MonoBehaviour
{
    public bool estaDisparado=false;
    public int damageShot=50;

    public ParticleSystem efectoChoqueDisparo;

    public void objetoDisparo(){
        estaDisparado=true;
        
    }

    void OnTriggerEnter(Collider collision)
    {
        efectoChoqueDisparo.Play();
        
        if(!estaDisparado)return;
        
        Collider objetoCollider = collision;
        
        bool esEnemigo= objetoCollider.GetComponent<EnemigoVida>();
        print(collision.name);


        if (esEnemigo)
        {
            EnemigoVida vidaEnemigo = objetoCollider.GetComponent<EnemigoVida>();
            vidaEnemigo.recibirDamage(damageShot);


            estaDisparado = false;
        }
        

        
        
    }

}
