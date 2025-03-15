using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuDePausaUI;
    public bool estaPausado = false;


    public void PausarMenu()
    {
        if (menuDePausaUI)
        {
            menuDePausaUI.SetActive(true);
            Pausar();
        }

        

    }
    public void Pausar()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        estaPausado = true;
    }
    public void ResumirMenu()
    {
        menuDePausaUI.SetActive(false);

        Resumir();
    }
    public void Resumir()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        estaPausado = false;


    }


}
