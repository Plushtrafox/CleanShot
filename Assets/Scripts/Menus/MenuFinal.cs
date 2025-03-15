using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuFinal : MonoBehaviour
{
    public MenuPausa menuDePausa;
    
    public void ReintentarJuego()
    {
        menuDePausa.ResumirMenu();
        SceneManager.LoadScene("Testing");

       
    }

    public void IniciarJuego()
    {

        menuDePausa.Resumir();

        SceneManager.LoadScene("Testing");

    }
    public void MenuPrincipal()
    {
        menuDePausa.Pausar();
        SceneManager.LoadScene("MenuPrincipal");

    }

    public void SalirJuego()
    {
        Application.Quit();

    }

}
