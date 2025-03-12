using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuFinal : MonoBehaviour
{
    public MenuPausa menuDePausa;
    
    public void ReintentarJuego()
    {
        SceneManager.LoadScene("Testing");
        menuDePausa.Resumir();
       
    }

}
