using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PowerSystem : MonoBehaviour
{
    public Transform camaraJugador;
    public float fuerzaEmpuje = 5f;
    public float rangoEmpuje = 5f;

    public float fuerzaJalo = 5f;

    public int damageExplosion=120;
    public int damageEmpuje=30;

    public MenuPausa menuDePausa;

    public Transform lugarSostenerObjeto;

    public int poderActual = 0;

    public Collider objetoSostenido;

    public bool estaSosteniendo = false;

    public float fuerzaDisparoObjetoSostenido = 10f;


    public float escalarDisparoObjetivoSostenido=2f;


    public Shot disparoScript;

    public MenuArmas menuDeArmas;

    public bool enemigoActualEsCortoAlcance = false;
    public bool enemigoActualEsLargoAlcance = false;

    public float fuerzaMareo = 10f;

    [Header("Referencias al UI poderes")]
    public Slider empujarPoderCargaUI;
    public Slider sostenerPoderCargaUI;
    public Slider atraerPoderCargaUI;
    public Slider explotarPoderCargaUI;
    public Slider marearPoderCargaUI;
    public Slider barraPoderActualUI;


    [Header("Poderes Cooldown")]
    public float empujeCooldown = 2f;
    public float atraerCooldown = 1f;
    public float sostenerCooldown = 3f;
    public float explotarCooldown = 10f;
    public float marearCooldown = 6f;


    [Header("Poderes Cooldown porcentaje")]
    public float empujeCooldownPorcentaje = 100f;
    public float atraerCooldownPorcentaje = 100f;
    public float sostenerCooldownPorcentaje = 100f;
    public float explotarCooldownPorcentaje = 100f;
    public float marearCooldownPorcentaje = 100f;



    [Header("intervalo para actualizar UI de cooldown")]
    public float cooldownIntervaloActualizacionUI = 0.2f;

    [Header("porcentaje para actualizar UI de cooldown en cada intervalo")]
    public float empujeCooldownPorcentajeActualizacionUI;
    public float atraerCooldownPorcentajeActualizacionUI;
    public float sostenerCooldownPorcentajeActualizacionUI;
    public float explotarCooldownPorcentajeActualizacionUI;
    public float marearCooldownPorcentajeActualizacionUI;


    [Header("Estado de poderes")]
    public bool empujeActivo = true;
    public bool atraerActivo = true;
    public bool sostenerActivo = true;
    public bool explotarActivo = true;
    public bool mareoActivo=true;




    //poderes
    // 0 Empujar
    // 1 Atraer
    // 2 Explotar
    // 3 Sostener
    // 4 Marear



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
                if (Input.GetButtonDown("Fire1"))
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
                        if(empujeActivo)empujeObjectoPoder();
                        break;
                    case 1:
                        if (atraerActivo) jaloObjectoPoder();

                        break;
                    case 2:
                        if (explotarActivo) ExplosionPoder();

                        break;
                    case 3:
                        if (sostenerActivo) sostenerObjectoPoder();
                        break;

                    case 4:
                        if(mareoActivo) marearPoder();
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

            // Calcula el incremento porcentual
            empujeCooldownPorcentajeActualizacionUI = (100f / empujeCooldown) * cooldownIntervaloActualizacionUI;
            empujeCooldownPorcentaje = 0f;//reinicia el cooldown
            empujeActivo = false;

            // Inicia la actualización repetitiva
            InvokeRepeating("ActualizarBarraCooldownEmpuje", 0f, cooldownIntervaloActualizacionUI);
        


            if (esEnemigo)
            {
                EnemigoVida vidaEnemigo = objetivo.collider.GetComponent<EnemigoVida>();
                vidaEnemigo.recibirDamage(damageEmpuje);


               

            }
        }
    }
    void ActualizarBarraCooldownEmpuje()
    {
        // Aumenta el porcentaje actual
        empujeCooldownPorcentaje += empujeCooldownPorcentajeActualizacionUI;
        if (poderActual == 0)
        {
            barraPoderActualUI.value = empujeCooldownPorcentaje / 100f;
        
        }

        // Limita el porcentaje a 100%
        if (empujeCooldownPorcentaje >= 100f)
        {
            empujeActivo = true;
            empujeCooldownPorcentaje = 100f;
            CancelInvoke("ActualizarBarraCooldownEmpuje"); // Detiene la actualización repetitiva
        }

        // Actualiza la barra de UI
        if (empujarPoderCargaUI != null)
        {
            empujarPoderCargaUI.value = empujeCooldownPorcentaje / 100f; // Convierte a un valor entre 0 y 1
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

            objetoSostenido.gameObject.TryGetComponent(out IAEnemyPart2 enemigoLargoAlcance);
            objetoSostenido.gameObject.TryGetComponent(out EnemigoCortoAlcanceScript enemigoCortoAlcance);
            if (enemigoLargoAlcance != null)
            {

                enemigoLargoAlcance.estaInactivo = true;
                enemigoActualEsLargoAlcance = true;
            }
            else if (enemigoCortoAlcance!=null)
            {
                enemigoCortoAlcance.estaInactivo = true;
                enemigoActualEsCortoAlcance = true;
            }

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
                


            if (enemigoActualEsCortoAlcance)
            {
                objetoSostenido.gameObject.TryGetComponent(out EnemigoCortoAlcanceScript enemigoCortoAlcance);
                enemigoCortoAlcance.estaInactivo = false;
                enemigoActualEsCortoAlcance = false;

            }
            else if (enemigoActualEsLargoAlcance)
            {
                objetoSostenido.gameObject.TryGetComponent(out IAEnemyPart2 enemigoLargoAlcance);
                enemigoLargoAlcance.estaInactivo = false;
                enemigoActualEsLargoAlcance = false;
            }


            }


            rb.AddForce(targetDisparo * fuerzaDisparoObjetoSostenido, ForceMode.Impulse);

        // Calcula el incremento porcentual
        sostenerCooldownPorcentajeActualizacionUI = (100f / sostenerCooldown) * cooldownIntervaloActualizacionUI;
        sostenerCooldownPorcentaje = 0f;//reinicia el cooldown
        sostenerActivo = false;

        // Inicia la actualización repetitiva
        InvokeRepeating("ActualizarBarraCooldownSostener", 0f, cooldownIntervaloActualizacionUI);



        objetoSostenido = null;
            estaSosteniendo = false;
            disparoScript.estaSosteniendooObjetoPoder = false;





    }
    void ActualizarBarraCooldownSostener()
    {
        // Aumenta el porcentaje actual
        sostenerCooldownPorcentaje += sostenerCooldownPorcentajeActualizacionUI;

        if (poderActual == 3)
        {
            barraPoderActualUI.value = sostenerCooldownPorcentaje / 100f;

        }

        // Limita el porcentaje a 100%
        if (sostenerCooldownPorcentaje >= 100f)
        {
            sostenerActivo = true;
            sostenerCooldownPorcentaje = 100f;
            CancelInvoke("ActualizarBarraCooldownSostener"); // Detiene la actualización repetitiva
        }

        // Actualiza la barra de UI
        if (sostenerPoderCargaUI != null)
        {
            sostenerPoderCargaUI.value = sostenerCooldownPorcentaje / 100f; // Convierte a un valor entre 0 y 1
        }
    }


    //habilidad de atraer
    private void jaloObjectoPoder()
    {

        RaycastHit objetivo;
        Physics.Raycast(camaraJugador.position, camaraJugador.forward, out objetivo, 30f);

        if (objetivo.rigidbody)
        {
            objetivo.rigidbody.AddForce((camaraJugador.forward * fuerzaJalo)*-1, ForceMode.Impulse);

            // Calcula el incremento porcentual
            atraerCooldownPorcentajeActualizacionUI = (100f / atraerCooldown) * cooldownIntervaloActualizacionUI;
            atraerCooldownPorcentaje = 0f;//reinicia el cooldown
            atraerActivo = false;

            // Inicia la actualización repetitiva
            InvokeRepeating("ActualizarBarraCooldownAtraer", 0f, cooldownIntervaloActualizacionUI);
        }
    }
    void ActualizarBarraCooldownAtraer()
    {
        // Aumenta el porcentaje actual
        atraerCooldownPorcentaje += atraerCooldownPorcentajeActualizacionUI;

        if (poderActual == 1)
        {
            barraPoderActualUI.value = atraerCooldownPorcentaje / 100f;

        }

        // Limita el porcentaje a 100%
        if (atraerCooldownPorcentaje >= 100f)
        {
            atraerActivo = true;
            atraerCooldownPorcentaje = 100f;
            CancelInvoke("ActualizarBarraCooldownAtraer"); // Detiene la actualización repetitiva
        }

        // Actualiza la barra de UI
        if (atraerPoderCargaUI != null)
        {
            atraerPoderCargaUI.value = atraerCooldownPorcentaje / 100f; // Convierte a un valor entre 0 y 1
        }
    }




    // Habilidad de explosion
    private void ExplosionPoder()
    {
        RaycastHit objetivo;
        Physics.Raycast(camaraJugador.position, camaraJugador.forward, out objetivo, 30f);


        Vector3 explosionPosition = objetivo.point; // Posici�n de la explosi�n (2 unidades hacia adelante)
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, distanciaExplosion); // Obtener objetos en el radio

        // Calcula el incremento porcentual
        explotarCooldownPorcentajeActualizacionUI = (100f / explotarCooldown) * cooldownIntervaloActualizacionUI;
        explotarCooldownPorcentaje = 0f;//reinicia el cooldown
        explotarActivo = false;

        // Inicia la actualización repetitiva
        InvokeRepeating("ActualizarBarraCooldownExplotar", 0f, cooldownIntervaloActualizacionUI);

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
    void ActualizarBarraCooldownExplotar()
    {
        // Aumenta el porcentaje actual
        explotarCooldownPorcentaje += explotarCooldownPorcentajeActualizacionUI;

        if (poderActual == 2)
        {
            barraPoderActualUI.value = explotarCooldownPorcentaje / 100f;

        }

        // Limita el porcentaje a 100%
        if (explotarCooldownPorcentaje >= 100f)
        {
            explotarActivo = true;
            explotarCooldownPorcentaje = 100f;
            CancelInvoke("ActualizarBarraCooldownExplotar"); // Detiene la actualización repetitiva
        }

        // Actualiza la barra de UI
        if (explotarPoderCargaUI != null)
        {
            explotarPoderCargaUI.value = explotarCooldownPorcentaje / 100f; // Convierte a un valor entre 0 y 1
        }
    }
    
    
    //habilidad mareo
    void marearPoder()
    {
        Physics.Raycast(camaraJugador.position, camaraJugador.forward, out RaycastHit objetivo, 30f);
        if (objetivo.collider.TryGetComponent(out Rigidbody rbObjetivo))
        {
            rbObjetivo.AddTorque(transform.up * fuerzaMareo);

        }

        
        
        if(objetivo.collider.TryGetComponent(out IAEnemyPart2 enemigoLargoAlcance))
        {
            enemigoLargoAlcance.mareoEnemigo();
            // Calcula el incremento porcentual
            marearCooldownPorcentajeActualizacionUI = (100f / marearCooldown) * cooldownIntervaloActualizacionUI;
            marearCooldownPorcentaje = 0f;//reinicia el cooldown
            mareoActivo = false;

            // Inicia la actualización repetitiva
            InvokeRepeating("ActualizarBarraCooldownMarear", 0f, cooldownIntervaloActualizacionUI);
        }
        else if(objetivo.collider.TryGetComponent(out EnemigoCortoAlcanceScript enemigoCortoAlcance))
        {
            enemigoCortoAlcance.mareoEnemigo();
            // Calcula el incremento porcentual
            marearCooldownPorcentajeActualizacionUI = (100f / marearCooldown) * cooldownIntervaloActualizacionUI;
            marearCooldownPorcentaje = 0f;//reinicia el cooldown
            mareoActivo = false;

            // Inicia la actualización repetitiva
            InvokeRepeating("ActualizarBarraCooldownMarear", 0f, cooldownIntervaloActualizacionUI);

        }




    }
    void ActualizarBarraCooldownMarear()
    {
        // Aumenta el porcentaje actual
        marearCooldownPorcentaje += marearCooldownPorcentajeActualizacionUI;

        if (poderActual == 4)
        {
            barraPoderActualUI.value = marearCooldownPorcentaje / 100f;

        }

        // Limita el porcentaje a 100%
        if (marearCooldownPorcentaje >= 100f)
        {
            mareoActivo = true;
            marearCooldownPorcentaje = 100f;
            CancelInvoke("ActualizarBarraCooldownMarear"); // Detiene la actualización repetitiva
        }

        // Actualiza la barra de UI
        if (marearPoderCargaUI != null)
        {
            marearPoderCargaUI.value = marearCooldownPorcentaje / 100f; // Convierte a un valor entre 0 y 1
        }
    }


}
