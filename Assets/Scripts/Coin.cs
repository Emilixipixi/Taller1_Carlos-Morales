using UnityEngine;

public class Coin : MonoBehaviour
{
    // Contador compartido por todas las monedas (static = una sola variable para todas)
    public static int totalMonedas = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            totalMonedas++;
            Destroy(gameObject);
        }
    }
}