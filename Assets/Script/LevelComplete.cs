using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Enter
        {
            SceneManager.LoadScene("SampleScene"); // Reemplaza con el nombre de tu nivel principal
        }
        if (Input.GetKeyDown(KeyCode.Escape)) // Escape = salir del juego
        {
            Application.Quit();
            Debug.Log("Saliendo del juego..."); // Solo se verá en el editor
        }
    }
}