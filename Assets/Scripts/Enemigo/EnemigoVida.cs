using UnityEngine;

public class EnemigoVida : MonoBehaviour
{

    public float vidaEnemigo = 100;


    
    public void recibirDamage(float damage)
    {
        vidaEnemigo -= damage;

        if (vidaEnemigo <= 0)
        {
            Destroy(gameObject);
                
        }
    }
}
