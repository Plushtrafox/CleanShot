using UnityEngine;

public class RampaScript : MonoBehaviour
{
    public int estadoPuerta = 0;
    //0=abajo
    //1=normal
    //2=sin movimiento
    public float velocidadRotacion = 5f;
    public float tiempoPuertaQuieta = 2f;


    public Quaternion rotacionPuertaAbierta = Quaternion.Euler(0, 0, -90);
    public Quaternion rotacionPuertaCerrada = Quaternion.Euler(0, 0, 0);

    // 0 en rotacion en z para que se pueda pasar
    //90 en rotacion en z para que no se pueda pasar
    //>” (mayor) y “<”
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        CambioRampa();


    }

    void CambioRampa()
    {

        if (estadoPuerta==0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotacionPuertaAbierta, velocidadRotacion * Time.deltaTime);
        }
        else if(estadoPuerta==1)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotacionPuertaCerrada, velocidadRotacion * Time.deltaTime);

        }

        if (gameObject.transform.rotation == rotacionPuertaAbierta)
        {
            estadoPuerta = 2;
            Invoke("cerrarPuertaNotificacion", tiempoPuertaQuieta);

        }
        else if (gameObject.transform.rotation == rotacionPuertaCerrada)
        {
            estadoPuerta = 2;
            Invoke("abrirPuertaNotificacion", tiempoPuertaQuieta);

        }

    }

    void cerrarPuertaNotificacion()
    {
        estadoPuerta = 1;

    }
    void abrirPuertaNotificacion()
    {
        estadoPuerta = 0;
    }




}
