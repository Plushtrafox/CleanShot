using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PuntosScript : MonoBehaviour
{
    public TextMeshProUGUI puntosActualesUI;


    public int puntosActuales = 0;


    public void SumarPuntos(int puntosPorSumar)
    {
        puntosActuales += puntosPorSumar;
        puntosActualesUI.text = puntosActuales.ToString();
    }

}
