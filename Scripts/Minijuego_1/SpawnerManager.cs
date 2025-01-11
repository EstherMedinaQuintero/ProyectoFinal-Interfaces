using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Clase para gestionar la lógica de los spawners y detectar cuándo se completa el minijuego.
public class SpawnerManager: MonoBehaviour {
  /// Indica si el minijuego ha sido completado.
  public bool gameCompleted { get; private set; } = false;

  /// Referencia al GameManager para suscribirse a eventos del juego.
  private GameManager gameManager;

  /// Configura el SpawnerManager al inicio, incluyendo la suscripción al evento de finalización del minijuego.
  void Start() {
    /// Busca el GameManager en la escena.
    gameManager = FindObjectOfType<GameManager>();
    if (gameManager == null) {
      Debug.LogError("No se encontró el GameManager en la escena.");
      return;
    }

    /// Suscríbete al evento OnGameCompleted del GameManager.
    gameManager.OnGameCompleted += HandleGameCompleted;
  }

  /// Maneja la lógica cuando el minijuego se completa.
  /// Se activa a través del evento OnGameCompleted.
  private void HandleGameCompleted() {
    gameCompleted = true;
    Debug.Log("SpawnerManager detectó que el minijuego ha sido completado.");
    /// Aquí puedes añadir lógica para proceder con la siguiente fase o evento en el juego.
  }

  /// Limpia las suscripciones al evento al destruir este objeto.
  /// Evita errores si el objeto se destruye antes de completar el minijuego.
  private void OnDestroy() {
    if (gameManager != null) {
      /// Desuscríbete del evento OnGameCompleted.
      gameManager.OnGameCompleted -= HandleGameCompleted;
    }
  }
}
