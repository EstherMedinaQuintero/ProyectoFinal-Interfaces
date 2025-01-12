using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portalController : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;  // Nombre de la escena a cargar
    public int levelIndex; // Índice del nivel a cargar

    // Cuando el jugador entra en el portal, se activa la transición de escena
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Encuentra el TransitionManager y manda la solicitud de carga de escena
            ManagerTransition transitionManager = FindObjectOfType<ManagerTransition>();
            if (transitionManager != null)
            {
                // Llama al método StartSceneTransition con el nombre de la escena
                Debug.Log("Cambiando de escena");
                transitionManager.StartSceneTransition(sceneToLoad, levelIndex);
            }
            else
            {
                Debug.LogError("No se encontró el TransitionManager");
            }
        }
    }

}
