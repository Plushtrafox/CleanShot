using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PuntosScript : MonoBehaviour
{
    public TextMeshProUGUI puntosActualesUI;
    public TextMeshProUGUI puntosTotalesUI;



    public int puntosActuales = 0;


    public void SumarPuntos(int puntosPorSumar)
    {
        puntosActuales += puntosPorSumar;
        puntosActualesUI.text = puntosActuales.ToString();
        puntosTotalesUI.text = puntosActuales.ToString();
    }

}
