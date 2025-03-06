using UnityEngine;
using UnityEngine.UIElements;

public class PowerSystem : MonoBehaviour
{
    public Transform camaraJugador;
    public float fuerzaEmpuje = 5f;
    public float rangoEmpuje = 5f;

    public float fuerzaJalo = 5f;

    public float damageExplosion=120;
    public float damageEmpuje=30;




    // Habilidad de explosión (AddExplosionForce)
    public float fuerzaExplosion = 10f; // Fuerza de la explosión
    public float distanciaExplosion = 5f; // Radio de la explosión
    public float levantadoExplosion = 0.5f; // Modificador de fuerza hacia arriba




    // Update is called once per frame
    void Update()
    {

        // Habilidad de empuje (AddForce)
        if (Input.GetKeyDown(KeyCode.E)) // Activar con la tecla E
        {
            empujeObjectoPoder();
        }

        // Habilidad de explosión (AddExplosionForce)
        if (Input.GetKeyDown(KeyCode.Q)) // Activar con la tecla Q
        {
            ExplosionPoder();
        }
        if (Input.GetKeyDown(KeyCode.R)) // Activar con la tecla Q
        {
            jaloObjectoPoder();
        }



    }




    // Habilidad de empuje (AddForce)
    private void empujeObjectoPoder()
    {

        RaycastHit objetivo;
        Physics.Raycast(camaraJugador.position, camaraJugador.forward, out objetivo, 30f);

        if (objetivo.rigidbody != false)
        {
            objetivo.rigidbody.AddForce(camaraJugador.forward * fuerzaEmpuje, ForceMode.Impulse);
            bool esEnemigo= objetivo.collider.GetComponent<EnemigoVida>();
            
            
            if (esEnemigo)
            {
                EnemigoVida vidaEnemigo = objetivo.collider.GetComponent<EnemigoVida>();
                vidaEnemigo.recibirDamage(damageEmpuje);
               

            }
        }
    }

    private void jaloObjectoPoder()
    {

        RaycastHit objetivo;
        Physics.Raycast(camaraJugador.position, camaraJugador.forward, out objetivo, 30f);

        if (objetivo.rigidbody != false)
        {
            objetivo.rigidbody.AddForce((camaraJugador.forward * fuerzaJalo)*-1, ForceMode.Impulse);
        }
    }




    // Habilidad de explosión (AddExplosionForce)
    private void ExplosionPoder()
    {
        RaycastHit objetivo;
        Physics.Raycast(camaraJugador.position, camaraJugador.forward, out objetivo, 30f);


        Vector3 explosionPosition = objetivo.point; // Posición de la explosión (2 unidades hacia adelante)
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, distanciaExplosion); // Obtener objetos en el radio

        foreach (Collider col in colliders)
        {
            




            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
            {
                bool esEnemigo = col.GetComponent<EnemigoVida>();
                rb.AddExplosionForce(fuerzaExplosion, explosionPosition, distanciaExplosion, levantadoExplosion, ForceMode.Impulse); // Aplicar fuerza explosiva

                if (esEnemigo)
                {
                    EnemigoVida vidaEnemigo = col.GetComponent<EnemigoVida>();
                    vidaEnemigo.recibirDamage(damageExplosion);


                }
            }
        }
    }
}
