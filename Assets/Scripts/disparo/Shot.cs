using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class Shot : MonoBehaviour
{   


    public TextMeshProUGUI balasDisponiblesUI;

    public GameObject recargandoUI;

    public MenuPausa menuDePausa;

    public int magSize = 10;

    //public float shotForce = 1500f;
    public float shotRate = 0.1f;
    private float shotRateTime = 0f;


    public float tiempoEntreRecarga = 1f;
    //private float tiempoTotalRecarga = 0f;

    private bool reloading;

    public bool estaSosteniendooObjetoPoder=false;

    public ParticleSystem disparoBalaVFX;
    public ParticleSystem choqueBalaVFX;

    public GameObject arma;
    public Transform camara;

    //public LayerMask enemigoObjetivo;

    // disparo 2.0
    public float distanciaMaximaDisparo = 70f;
    public int cantidadBalasActuales;
    public float fuerzaChoqueDisparo = 50f;
    public int damageShot = 50;

    EnemigoVida vidaDeEnemigo;

    public GameObject choqueBalaVFXGameObject;


    private void Awake()
    {
        cantidadBalasActuales = magSize;

    }


    void Update()
    {
        if (menuDePausa.estaPausado == false)
        {
            if (!estaSosteniendooObjetoPoder)
            {


                if (Input.GetKeyDown(KeyCode.R)&& cantidadBalasActuales!= magSize || cantidadBalasActuales == 0)
                {
                    Reload();
                }

                if (Input.GetButton("Fire1") && !reloading)
                {
                    if (cantidadBalasActuales > 0)
                    {
                        shotRateTime -= Time.deltaTime;

                        if (shotRateTime <= 0)
                        {
                            Shoot2();

                            shotRateTime = shotRate;
                        }
                    }
                }
                else
                {
                    shotRateTime = 0;
                }
            }

        }

    }

    private void Shoot2()
    {
        RaycastHit objetivo;
        Physics.Raycast(camara.position, camara.forward, out objetivo, distanciaMaximaDisparo);

       


        if (objetivo.collider!=null && objetivo.collider.gameObject.TryGetComponent(out Rigidbody rbObjetivo))
        {
            rbObjetivo.AddForce(camara.forward * fuerzaChoqueDisparo, ForceMode.Impulse);



        }

        disparoBalaVFX.Play();

        



        if (choqueBalaVFX && choqueBalaVFXGameObject)
        {
            choqueBalaVFXGameObject.transform.position = objetivo.point;
            choqueBalaVFX.Play();
        }

        if (objetivo.collider != null &&  objetivo.collider.gameObject.TryGetComponent(out BotonRampaManual botonRampa)
)
        {
            botonRampa.NotificarAbrirRampa();

        }



        if (objetivo.collider != null && objetivo.collider.gameObject.TryGetComponent(out EnemigoVida vidaObjetivo)
)
        {
            vidaObjetivo.recibirDamage(damageShot);

        }
        cantidadBalasActuales -= 1;

        actualizarBalasUI();


    }





    private void Reload()
    {
        Invoke("TerminoRecarga", 1f);

        recargandoUI.SetActive(true);



        reloading = true;
        //tiempoTotalRecarga = tiempoEntreRecarga;

        cantidadBalasActuales = magSize;

    }

    void TerminoRecarga()
    {
        recargandoUI.SetActive(false);
        reloading = false;
        actualizarBalasUI();


    }





    public void actualizarBalasUI()
    {
        balasDisponiblesUI.text = cantidadBalasActuales.ToString();
    }
}
