using UnityEngine;

public class RampaManual : MonoBehaviour
{

    public float velocidadRotacion = 5f;
    public float tiempoPuertaQuieta = 2f;


    public Quaternion rotacionPuertaAbierta = Quaternion.Euler(0, 0, 90);
    public Quaternion rotacionPuertaCerrada = Quaternion.Euler(0, 0, 0);

    public float velocidadDeActualizacion = 0.1f;

    public bool abriendo = false;
    public bool cerrando = false;
    public bool abierto = false;


    private void Update()
    {
        if (abriendo == true)
        {
            abrirPuerta();
        }
        else if (cerrando==true)
        {
            cerrarPuerta();
        }
    }

    public void SubirPuerta()
    {
        if (!abriendo && !abierto)
        {
            abriendo = true;

        }

    }
    

    void abrirPuerta()
    {

        transform.rotation = Quaternion.Lerp(transform.rotation, rotacionPuertaAbierta, velocidadRotacion*Time.deltaTime);
        if (transform.rotation == rotacionPuertaAbierta)
        { 
            Invoke("notificarPuertaAbierta",tiempoPuertaQuieta);
            abriendo = false;
            abierto = true;

        }



    }
    void notificarPuertaAbierta()
    {
        cerrando = true;
        abierto = false;
    }
    void cerrarPuerta()
    {
        
        transform.rotation = Quaternion.Lerp(transform.rotation, rotacionPuertaCerrada, velocidadRotacion * Time.deltaTime);
        if (transform.rotation == rotacionPuertaCerrada)
        {
            cerrando = false;

        }
    }



}
