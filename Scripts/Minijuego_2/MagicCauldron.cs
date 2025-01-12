using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Clase que gestiona la prueba 1
public class MagicCauldron: MonoBehaviour {
  /// Ingredientes necesarios (cristal, flor, runa)
  public GameObject[] ingredients;

  /// Palabra mágica que activa el evento
  public string magicWord = "Mystic";

  /// Efecto visual de explosión mágica
  public GameObject magicExplosion;

  /// Array para controlar si cada ingrediente está dentro de la zona de la cacerola
  private bool[] ingredientsInZone;

  /// Indica si todos los ingredientes están listos
  private bool ingredientsReady = false;

  /// Objeto que representa la puerta
  public GameObject door;

  /// Indica si la puerta ya ha sido destruida
  public bool isDoorDestroyed = false;

  void Start() {
    /// Inicializa el array que controla los ingredientes en la zona
    ingredientsInZone = new bool[ingredients.Length];

    /// Desactiva el efecto de explosión mágica al inicio
    magicExplosion.SetActive(false);
  }

  void Update() {
    /// Comprueba si todos los ingredientes están dentro de la zona
    ingredientsReady = true;
    foreach (bool inZone in ingredientsInZone) {
      if (!inZone) {
        ingredientsReady = false;
        break;
      }
    }

    /// Si todos los ingredientes están listos y la puerta no ha sido destruida, activa el evento mágico
    if (ingredientsReady && !isDoorDestroyed) {
      TriggerMagicEvent();
    }
  }

  /// Detecta cuando un ingrediente entra en la zona de la cacerola
  /// <param name="other"> El collider del objeto que entra. </param>
  void OnTriggerEnter(Collider other) {
    for (int i = 0; i < ingredients.Length; i++) {
      if (other.gameObject == ingredients[i]) {
        /// Marca el ingrediente como presente en la zona
        ingredientsInZone[i] = true;
        Debug.Log(ingredients[i].name + " está en la zona.");
        break;
      }
    }
  }

  /// Detecta cuando un ingrediente sale de la zona de la cacerola
  /// <param name="other"> El collider del objeto que sale. </param>
  void OnTriggerExit(Collider other) {
    for (int i = 0; i < ingredients.Length; i++) {
      if (other.gameObject == ingredients[i]) {
        /// Marca el ingrediente como fuera de la zona
        ingredientsInZone[i] = false;
        Debug.Log(ingredients[i].name + " ha salido de la zona.");
        break;
      }
    }
  }

  /// Activa el evento mágico cuando todos los ingredientes están listos
  private void TriggerMagicEvent() {
    /// Activa la explosión mágica
    ActivateMagicExplosion();

    /// Destruye o desactiva la puerta
    DestroyDoor();

    /// Muestra un mensaje en la consola indicando que el conjuro se ha completado
    Debug.Log("¡Conjuro completado! El camino está abierto.");
  }

  /// Método para destruir o desactivar la puerta
  public void DestroyDoor() {
    door.GetComponent<WallBehaviour>().DestroyDoor();
  }

  /// Método para activar el efecto de explosión mágica
  public void ActivateMagicExplosion() {
    magicExplosion.SetActive(true);
  }
}
