using Unity.VisualScripting;
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

    public MenuPausa menuDePausa;

    public Transform lugarSostenerObjeto;

    public int poderActual = 0;

    public Collider objetoSostenido;

    public bool estaSosteniendo = false;

    public float fuerzaDisparoObjetoSostenido = 1f;

    public float maxFuerzaDisparoObjetoSostenido = 7f;

    public float rapidezCargaDisparoObjetoSostenido = 2; 

    public float escalarDisparoObjetivoSostenido=2f;
    

    public Shot disparoScript;
        //poderes
        // 0 Empujar
        // 1 Atraer
        // 2 Explotar
        // 3 Sostener



    // Habilidad de explosi�n (AddExplosionForce)
    public float fuerzaExplosion = 10f; // Fuerza de la explosi�n
    public float distanciaExplosion = 5f; // Radio de la explosi�n
    public float levantadoExplosion = 0.5f; // Modificador de fuerza hacia arriba




    // Update is called once per frame
    void Update()
    {


        if (menuDePausa.estaPausado == false)
        {
            if (estaSosteniendo)
            {
                objetoSostenido.transform.position = lugarSostenerObjeto.position;
                if (Input.GetButton("Fire1") && fuerzaDisparoObjetoSostenido < maxFuerzaDisparoObjetoSostenido)
                {
                    fuerzaDisparoObjetoSostenido = fuerzaDisparoObjetoSostenido + rapidezCargaDisparoObjetoSostenido * Time.deltaTime;

                }
                if (Input.GetButtonUp("Fire1"))
                {
                    dispararObjetoSostenidoPoder();
                }
            }
            // Habilidad de empuje (AddForce)
            if (Input.GetKeyDown(KeyCode.E) && !estaSosteniendo) // Activar con la tecla E
            {
                switch (poderActual)
                {
                    case 0:
                        empujeObjectoPoder();
                        break;
                    case 1:
                        jaloObjectoPoder();

                        break;
                    case 2:
                        ExplosionPoder();

                        break;
                    case 3:
                        sostenerObjectoPoder();
                        break;
                }

            }


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


//habilidad de sostener
    private void sostenerObjectoPoder()
    {

        RaycastHit objetivo;
        Physics.Raycast(camaraJugador.position, camaraJugador.forward, out objetivo, 30f);

        if (objetivo.rigidbody != false)
        {
            estaSosteniendo = true;
            objetoSostenido = objetivo.collider;
            objetoSostenido.transform.position = lugarSostenerObjeto.position;

            disparoScript.estaSosteniendooObjetoPoder = true;

        }

    }
    private void dispararObjetoSostenidoPoder()
    {
        

            Rigidbody rb = objetoSostenido.GetComponent<Rigidbody>();
            bool esEnemigo = objetoSostenido.GetComponent<ObjetoSostenidoDisparado>();

            Vector3 targetDisparo = camaraJugador.forward;
            targetDisparo.y = +escalarDisparoObjetivoSostenido;

            if (esEnemigo)
            {
                ObjetoSostenidoDisparado objetoDisparado = objetoSostenido.GetComponent<ObjetoSostenidoDisparado>();
                objetoDisparado.objetoDisparo();
            }


            rb.AddForce(targetDisparo * fuerzaDisparoObjetoSostenido, ForceMode.Impulse);

            objetoSostenido = null;
            estaSosteniendo = false;
            disparoScript.estaSosteniendooObjetoPoder = false;
        fuerzaDisparoObjetoSostenido = 0f;




    }

//habilidad de atraer
    private void jaloObjectoPoder()
    {

        RaycastHit objetivo;
        Physics.Raycast(camaraJugador.position, camaraJugador.forward, out objetivo, 30f);

        if (objetivo.rigidbody)
        {
            objetivo.rigidbody.AddForce((camaraJugador.forward * fuerzaJalo)*-1, ForceMode.Impulse);
        }
    }




    // Habilidad de explosion
    private void ExplosionPoder()
    {
        RaycastHit objetivo;
        Physics.Raycast(camaraJugador.position, camaraJugador.forward, out objetivo, 30f);


        Vector3 explosionPosition = objetivo.point; // Posici�n de la explosi�n (2 unidades hacia adelante)
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
