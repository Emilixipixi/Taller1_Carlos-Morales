using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Jugar(){
        SceneManager.LoadScene("Nivel 1");
    }

    public void Salir(){
        Debug.Log("Salir del juego----");
        Application.Quit();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
