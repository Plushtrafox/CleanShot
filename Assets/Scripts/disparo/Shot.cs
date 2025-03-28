using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class Shot : MonoBehaviour
{   //intento 2 de varias armas
    //public enum TipoDisparo { Normal, Escopeta, Rafaga }
    //public TipoDisparo tipoDisparo;

    //public GameObject bullet;
   /// public Transform spawnPoint;
    //public Transform lugarMunicion;
    //public Queue<GameObject> municionDisponible = new Queue<GameObject>();
    //public List<GameObject> municionUsada = new List<GameObject>();

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
 //    private void Disparar()
 //   {
 //       switch (tipoDisparo)
 //       {
 //           case TipoDisparo.Normal:
 //               DisparoNormal();
 //               break;
 //           case TipoDisparo.Escopeta:
 //               DisparoEscopeta();
 //               break;
 //           case TipoDisparo.Rafaga:
 //               StartCoroutine(DisparoRafaga());
 //               break;
 //       }
 //   }
 //private void DisparoNormal()
 //   {
 //       if (municionDisponible.Count == 0) return;
 //       DispararBala(spawnPoint.position, spawnPoint.forward);
 //   }
 //    private void DisparoEscopeta()
 //   {
 //       int cantidadPerdigones = 6;
 //       float dispersion = 10f;

 //       for (int i = 0; i < cantidadPerdigones; i++)
 //       {
 //           Vector3 direccion = spawnPoint.forward;
 //           direccion.x += Random.Range(-dispersion, dispersion) * 0.01f;
 //           direccion.y += Random.Range(-dispersion, dispersion) * 0.01f;
 //           DispararBala(spawnPoint.position, direccion.normalized);
 //       }
 //   }
 //     private IEnumerator DisparoRafaga()
 //   {
 //       int cantidadDisparos = 3;
 //       float delayEntreDisparos = 0.1f;

 //       for (int i = 0; i < cantidadDisparos; i++)
 //       {
 //           if (municionDisponible.Count > 0)
 //           {
 //               DispararBala(spawnPoint.position, spawnPoint.forward);
 //               yield return new WaitForSeconds(delayEntreDisparos);
 //           }
 //       }
 //   }
 //    private void DispararBala(Vector3 posicion, Vector3 direccion)
 //   {
 //       if (municionDisponible.Count == 0) return;

 //       GameObject newBullet = municionDisponible.Dequeue();
 //       municionUsada.Add(newBullet);

 //       newBullet.transform.position = posicion;
 //       newBullet.transform.rotation = Quaternion.LookRotation(direccion);
 //       newBullet.SetActive(true);

 //       Rigidbody rb = newBullet.GetComponent<Rigidbody>();
 //       rb.linearVelocity = Vector3.zero;
 //       rb.angularVelocity = Vector3.zero;
 //       rb.AddForce(direccion * shotForce);

 //       BalaScript scriptDeBala = newBullet.GetComponent<BalaScript>();
 //       scriptDeBala.objetoDisparo();
 //   }

    //private void Shoot()
    //{
    //    //ajustarAnguloArma();
    //    GameObject newBullet = municionDisponible.Dequeue();


    //    municionUsada.Add(newBullet);


    //    UseBullet(newBullet);

    //    newBullet.transform.position = spawnPoint.position;
    //    newBullet.transform.rotation = spawnPoint.rotation;

    //    Rigidbody rb = newBullet.GetComponent<Rigidbody>();
    //    rb.linearVelocity = Vector3.zero; // Reiniciar la velocidad
    //    rb.angularVelocity = Vector3.zero; // Reiniciar la velocidad angular

    //    rb.AddForce(spawnPoint.forward * shotForce);

    //    disparoBalaVFX.Play();

    //    BalaScript scriptDeBala=newBullet.GetComponent<BalaScript>();
    //    scriptDeBala.objetoDisparo();
    //    actualizarBalasUI();

    //}
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





        if (objetivo.collider.gameObject.TryGetComponent(out EnemigoVida vidaObjetivo)
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
