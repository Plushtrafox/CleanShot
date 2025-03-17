using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuFinal : MonoBehaviour
{
    public MenuPausa menuDePausa;
    public GameObject menuVictoria;

    public TextMeshProUGUI puntosFinalesUI;

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
    public void victoriaMenu(int puntosFinales)
    {
        menuVictoria.SetActive(true);
        puntosFinalesUI.text= puntosFinales.ToString();
        menuDePausa.Pausar();
        


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
