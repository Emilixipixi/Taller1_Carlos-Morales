using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [Header("Referencias")]
    public TMP_Text textoMonedas; // El texto "Monedas: 0" del Canvas

    private int ultimoValor = -1;

    void Start()
    {
        // Reinicia el contador al empezar el nivel.
        // Es necesario porque una variable static NO se reinicia sola al recargar la escena.
        Coin.totalMonedas = 0;
    }

    void Update()
    {
        // Solo actualiza el texto cuando el valor cambia
        if (Coin.totalMonedas == ultimoValor) return;
        ultimoValor = Coin.totalMonedas;

        textoMonedas.text = "Monedas: " + ultimoValor;
    }
}