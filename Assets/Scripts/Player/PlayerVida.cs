using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerVida : MonoBehaviour
{

    public int vida = 100;
    public GameObject menuPerdida;
    public MenuPausa menuDePausa;
    public TextMeshProUGUI valorVida;

    public void reducirVida(int damage)
    {
        vida -= damage;
        valorVida.text=vida.ToString();


        if (vida <= 0)
        {
            menuPerdida.SetActive(true);

            menuDePausa.Pausar();

        }

    }
}
