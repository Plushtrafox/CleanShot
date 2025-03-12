using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamaraController : MonoBehaviour
{
    float moverEnX;
    float moverEnY;
    public float sensibilidadCamara = 5.0f;
    public Transform jugador;

    float xRotation;

    public GameObject mira;
    public GameObject menuArmas;

    public MenuArmas menuDeArmas;

    public MenuPausa menuDePausa;

  


    void Awake()
    {
        Cursor.lockState=CursorLockMode.Locked;

    }


    void Update()
    {
        moverEnX = Input.GetAxis("Mouse X") * sensibilidadCamara * Time.deltaTime;
        moverEnY = Input.GetAxis("Mouse Y") * sensibilidadCamara * Time.deltaTime;

        xRotation -= moverEnY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar la rotación vertical
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        jugador.Rotate(Vector3.up * moverEnX);

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            abrirMenuArmas();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            cerrarMenuArmas();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            menuDePausa.PausarMenu();
        }
    }


    public void abrirMenuArmas()
    {

        if(mira && menuArmas)
        {
            mira.SetActive(false);
            menuArmas.SetActive(true);
            menuDePausa.Pausar();
        }



    }
    public void cerrarMenuArmas()
    {

        if (mira && menuArmas)
        {
            mira.SetActive(true);
            menuArmas.SetActive(false);
            menuDePausa.Resumir();
        }
            

    }

}
