using UnityEngine;

public class BotonRampaManual : MonoBehaviour
{
    public RampaManual rampaManualScript;

    public void NotificarAbrirRampa()
    {
        rampaManualScript.SubirPuerta();
    }
}
