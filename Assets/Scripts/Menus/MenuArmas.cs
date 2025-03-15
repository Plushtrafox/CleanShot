using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuArmas : MonoBehaviour
{

    public int iD;
    public string nombrePoder;
    public TextMeshProUGUI nombrePoderTexto;
    public Image poderSeleccionado;
    public bool seleccionado = false;
    public Sprite iconoPoder;


    public CamaraController camaraControl;

    public PowerSystem sistemaPoder;






    public void Seleccionar()
    {
        seleccionado = true;
        poderSeleccionado.sprite = iconoPoder;
        sistemaPoder.poderActual = iD;



        if (camaraControl)
        {
            camaraControl.cerrarMenuArmas();

        }


    }



    public void HoverEnter()
    {
        if(nombrePoderTexto)
        {
            nombrePoderTexto.text = nombrePoder;

        }

    }
    public void HoverExit()
    {
        if (nombrePoderTexto)
        {
            nombrePoderTexto.text = "";
        }

    }
}
