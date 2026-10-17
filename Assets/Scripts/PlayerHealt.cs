using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealt : MonoBehaviour
{
    [Header("Vidas")]
    public int vidasMaximas = 3;
    private int vidasActuales;
    public int VidasActuales => vidasActuales; // <-- NUEVO (solo lectura para el UI)

    [Header("Invulnerabilidad")]
    public float duracionInvulnerabilidad = 1f;
    private bool esInvulnerable;

    [Header("Rechazo (knockback)")]
    public float fuerzaRechazoX = 6f;
    public float fuerzaRechazoY = 4f;
    public float duracionRechazo = 0.25f;

    // El PlayerController lee esta variable para no pisar el empujón
    [HideInInspector] public bool enRechazo;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool muerto;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        vidasActuales = vidasMaximas;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            RecibirDanio(collision.transform);
        }
    }

    // Si el jugador sigue pegado al enemigo cuando termina la invulnerabilidad, vuelve a recibir daño
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            RecibirDanio(collision.transform);
        }
    }

    public void RecibirDanio(Transform enemigo)
    {
        if (esInvulnerable || muerto) return;

        vidasActuales--;
        animator.SetTrigger("Hurt");

        if (vidasActuales <= 0)
        {
            muerto = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        // Dirección del empujón: lejos del enemigo
        float direccion = Mathf.Sign(transform.position.x - enemigo.position.x);
        if (direccion == 0) direccion = 1;

        StartCoroutine(Rechazo(direccion));
        StartCoroutine(Invulnerabilidad());
    }

    IEnumerator Rechazo(float direccion)
    {
        enRechazo = true;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(direccion * fuerzaRechazoX, fuerzaRechazoY), ForceMode2D.Impulse);

        yield return new WaitForSeconds(duracionRechazo);

        enRechazo = false;
    }

    IEnumerator Invulnerabilidad()
    {
        esInvulnerable = true;

        // Parpadeo visual durante el segundo de invulnerabilidad
        float tiempo = 0f;
        while (tiempo < duracionInvulnerabilidad)
        {
            if (sprite != null) sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(0.1f);
            tiempo += 0.1f;
        }

        if (sprite != null) sprite.enabled = true;
        esInvulnerable = false;
    }
}