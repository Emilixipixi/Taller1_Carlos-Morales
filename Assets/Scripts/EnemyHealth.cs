using System.Collections; // <-- NUEVO
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [Header ("Salud del Enemigo")]
    public int MaxHealth = 3;
    [SerializeField] private int currentHealth;

    [Header("Retroceso (knockback)")] // <-- NUEVO
    public float knockbackForceX = 5f; // <-- NUEVO
    public float knockbackForceY = 2f; // <-- NUEVO
    public float knockbackDuration = 0.2f; // <-- NUEVO

    // EnemyPatrol lee esta variable para no pisar el empujón
    [HideInInspector] public bool enKnockback; // <-- NUEVO

    private Rigidbody2D rb; // <-- NUEVO
    private bool muerto; // <-- NUEVO

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = MaxHealth;
        rb = GetComponent<Rigidbody2D>(); // <-- NUEVO
    }

    public void TakeDamage(int damageAmount){

        currentHealth -= damageAmount;
        if(currentHealth <= 0){
            Die();
        }

    }

    // Empuja al enemigo en dirección contraria al atacante // <-- NUEVO
    public void Knockback(Transform attacker) // <-- NUEVO
    { // <-- NUEVO
        if (muerto || rb == null || attacker == null) return; // <-- NUEVO

        float direccion = Mathf.Sign(transform.position.x - attacker.position.x); // <-- NUEVO
        if (direccion == 0) direccion = 1; // <-- NUEVO

        StopAllCoroutines(); // <-- NUEVO (si lo golpean otra vez, reinicia el retroceso)
        StartCoroutine(KnockbackRoutine(direccion)); // <-- NUEVO
    } // <-- NUEVO

    IEnumerator KnockbackRoutine(float direccion) // <-- NUEVO
    { // <-- NUEVO
        enKnockback = true; // <-- NUEVO

        rb.linearVelocity = Vector2.zero; // <-- NUEVO
        rb.AddForce(new Vector2(direccion * knockbackForceX, knockbackForceY), ForceMode2D.Impulse); // <-- NUEVO

        yield return new WaitForSeconds(knockbackDuration); // <-- NUEVO

        enKnockback = false; // <-- NUEVO
    } // <-- NUEVO

    public void Die(){
        muerto = true; // <-- NUEVO
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}