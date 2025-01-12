using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// Clase que gestiona el comportamiento de un portal para cargar una nueva escena cuando el jugador lo atraviesa.
public class portalController: MonoBehaviour {
  /// Nombre de la escena a cargar cuando se activa el portal.
  [SerializeField] private string sceneToLoad;

  /// Índice del nivel asociado al portal.
  public int levelIndex;

  /// Método que se ejecuta cuando un objeto entra en el área del trigger del portal.
  /// <param name="other"> El collider del objeto que entra en el área del portal. </param>
  private void OnTriggerEnter(Collider other) {
    /// Verifica si el objeto que entra tiene el tag "Player"
    if (other.CompareTag("Player")) {
      /// Busca el ManagerTransition en la escena
      ManagerTransition transitionManager = FindObjectOfType<ManagerTransition>();
      if (transitionManager != null) {
        /// Llama al método StartSceneTransition para iniciar la transición
        Debug.Log("Cambiando de escena a: " + sceneToLoad);
        transitionManager.StartSceneTransition(sceneToLoad, levelIndex);
      } else {
        Debug.LogError("No se encontró el TransitionManager en la escena.");
      }
    }
  }
}
