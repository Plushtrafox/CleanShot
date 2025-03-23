using System.Numerics;
using Unity.Mathematics;
using UnityEngine;

public class EnemigoCortoAlcanceScript : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public UnityEngine.Quaternion angulo;
    public float grado;

    public float velocidad = 5f;
    public float velocidadPersecucion = 10f;

    public GameObject target;
    public bool ataque;

    public float distanciaVerJugador = 20f;
    public float distanciaCortoAlcance = 2f;
    public float distanciaAtaqueCortoAlcance = 1f;

    public bool puedoAtacar = true;

    public float tiempoEntreAtaque = 1f;

    public Rigidbody enemigoRB;

    public float fuerzaRetrocesoAtaque = 2f;


    void Awake()
    {
        target = GameObject.Find("==Player==");

    }

    public void Comportamiento_Enemigo()
    {
        if (UnityEngine.Vector3.Distance(transform.position, target.transform.position) > distanciaVerJugador)
        {

            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = UnityEngine.Random.Range(0, 5);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    grado = UnityEngine.Random.Range(0, 360);
                    angulo = UnityEngine.Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;

                case 1:
                    transform.rotation = UnityEngine.Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(UnityEngine.Vector3.forward * velocidad * Time.deltaTime);
                    break;
            }
        }
        else if (UnityEngine.Vector3.Distance(transform.position, target.transform.position) > distanciaAtaqueCortoAlcance && puedoAtacar == true)
        {
            
                var lookpos = target.transform.position - transform.position;
                lookpos.y = 0;
                var rotation = UnityEngine.Quaternion.LookRotation(lookpos);
                transform.rotation = UnityEngine.Quaternion.RotateTowards(transform.rotation, rotation, 4);

                transform.Translate(UnityEngine.Vector3.forward * velocidadPersecucion * Time.deltaTime);

           
        }
    }
    

    private void Update()
    {
        Comportamiento_Enemigo();
    }

    public void CancelarAtacarJugador()
    {
        puedoAtacar = false;
        enemigoRB.AddForce((transform.forward*fuerzaRetrocesoAtaque)*-1,ForceMode.Impulse);
       
        Invoke("ActivarAtacarJugador", tiempoEntreAtaque);


    }
    public void ActivarAtacarJugador()
    {
        puedoAtacar = true;
    }
}
