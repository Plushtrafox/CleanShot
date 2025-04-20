using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PuntosScript : MonoBehaviour
{
    public TextMeshProUGUI puntosActualesUI;
    public EnemySpawner enemySpawnerScript;
    public MenuFinal menuFinalScript;

    public int puntosActuales = 0;

    public void SumarPuntos(int puntosPorSumar)
    {
        puntosActuales += puntosPorSumar;
        puntosActualesUI.text = puntosActuales.ToString();

        Invoke("condicionPerdida", 1f);
    }

    public void condicionPerdida()
    {
        enemySpawnerScript.enemyList.RemoveAll(item => item == null);

        if (enemySpawnerScript.enemyList.Count == 0)
        {
            menuFinalScript.victoriaMenu(puntosActuales);
        }
    }

}
