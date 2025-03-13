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








    //ejemplo


    //public int iD;
    //public string nombrePoder;
    //public TextMeshProUGUI nombrePoderTexto;
    //public Image poderSeleccionado;
    //public bool seleccionado = false;
    //public Sprite iconoPoder;
    //public CamaraController camaraControl;
    //public PowerSystem sistemaPoder;
    //public void Seleccionar()
    //{
    //    seleccionado = true;
    //    poderSeleccionado.sprite = iconoPoder;
    //    sistemaPoder.poderActual = iD;
    //    if (camaraControl)
    //    {
    //        camaraControl.cerrarMenuArmas();
    //    }
    //}
    //public void HoverEnter()
    //{
    //    if (nombrePoderTexto)
    //    {
    //        nombrePoderTexto.text = nombrePoder;
    //    }
    //}
    //public void HoverExit()
    //{
    //    if (nombrePoderTexto)
    //    {
    //        nombrePoderTexto.text = "";
    //    }
    //}
}
