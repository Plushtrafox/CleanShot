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


    void Awake()
    {
        target = GameObject.Find("==Player==");

    }

    public void Comportamiento_Enemigo()
    {
        if (UnityEngine.Vector3.Distance(transform.position, target.transform.position) > 20)
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
        else if (UnityEngine.Vector3.Distance(transform.position, target.transform.position) > 1 )
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
}
