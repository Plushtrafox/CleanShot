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

    public bool menuAbierto=false;

    public MenuArmas menuDeArmas;

    public int poderActual = 0;


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
    }


    public void abrirMenuArmas()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if(mira && menuArmas)
        {
            mira.SetActive(false);
            menuArmas.SetActive(true);
            menuAbierto = true;
            pausarMenu();
        }



    }
    public void cerrarMenuArmas()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (mira && menuArmas)
        {
            mira.SetActive(true);
            menuArmas.SetActive(false);
            menuAbierto = false;
            resumirMenu();
        }
            

    }
    public void pausarMenu()
    {
        Time.timeScale = 0f;
    }
    public void resumirMenu()
    {
        Time.timeScale = 1f;
    }
}
