using UnityEngine;
using UnityEngine.UI;

public class HealtUI : MonoBehaviour
{
    [Header("Referencias")]
    public PlayerHealt jugador;   // El personaje que tiene PlayerHealt
    public Image[] corazones;     // Los 3 corazones del Canvas, de izquierda a derecha

    private int ultimasVidas = -1;

    void Update()
    {
        if (jugador == null) return;

        int vidas = jugador.VidasActuales;

        // Solo actualiza la UI cuando las vidas cambian
        if (vidas == ultimasVidas) return;
        ultimasVidas = vidas;

        for (int i = 0; i < corazones.Length; i++)
        {
            // El corazón i se muestra solo si todavía quedan más de i vidas
            corazones[i].enabled = i < vidas;
        }
    }
}