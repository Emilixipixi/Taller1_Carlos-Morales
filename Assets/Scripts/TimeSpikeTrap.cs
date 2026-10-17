using UnityEngine;
using System.Collections;

public class TimeSpikeTrap : MonoBehaviour
{

    [Header("Ajustes del Tiempo")]
    public float waitTime=2f;
    public float activeTime=1f;

    [Header("Daño de la trampa")]
    public int damageAmount=1;

    [Header("Estado Trampa")]
    public bool isDangerous=false;

    private Animator animator;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(TrapRoutine());
        
    }

    IEnumerator TrapRoutine(){

        while(true){

            isDangerous=false;
            yield return new WaitForSeconds(waitTime);

            if(animator != null){
                animator.SetTrigger("Active");
            }

            isDangerous=true;
            yield return new WaitForSeconds(activeTime);

            isDangerous=false;
        }
   }


   private void OnTriggerEnter2D(Collider2D other){
    if(isDangerous){
        PlayerHealt playerHealth = other.GetComponent<PlayerHealt>(); // <-- CORREGIDO (el tipo es PlayerHealt)

        if(playerHealth != null){
            playerHealth.RecibirDanio(transform);
        }
    }
   }

   // Daña también al Player que ya está encima cuando los pinchos se activan // <-- NUEVO
   private void OnTriggerStay2D(Collider2D other){ // <-- NUEVO
    if(isDangerous){ // <-- NUEVO
        PlayerHealt playerHealth = other.GetComponent<PlayerHealt>(); // <-- NUEVO

        if(playerHealth != null){ // <-- NUEVO
            playerHealth.RecibirDanio(transform); // <-- NUEVO
        } // <-- NUEVO
    } // <-- NUEVO
   } // <-- NUEVO

    // Update is called once per frame
    void Update()
    {
        
    }
}