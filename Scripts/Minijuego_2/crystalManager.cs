using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Clase que gestiona la activación de cristales y la destrucción de una puerta
public class crystalManager: MonoBehaviour {
  /// Array que contiene los cristales que deben activarse
  public GameObject[] crystals;

  /// Array que indica si cada cristal está activado
  private bool[] crystalsInZone;

  /// Objeto que representa la puerta que se destruirá cuando todos los cristales estén activados
  public GameObject door;

  /// Indica si la puerta ya ha sido destruida
  public bool isDoorDestroyed = false;

  void Start() {
    /// Inicializa el array para controlar la activación de los cristales
    crystalsInZone = new bool[crystals.Length];
  }

  /// Método para activar un cristal específico
  /// <param name="crystal"> El cristal que ha sido activado. </param>
  public void ActivateCrystal(GameObject crystal) {
    /// Encuentra el índice del cristal basado en su nombre
    int index = System.Array.FindIndex(crystals, c => c.name == crystal.name);

    /// Si el cristal está en la lista, márcalo como activado
    if (index != -1) {
      crystalsInZone[index] = true;
    }

    /// Verifica si todos los cristales están activados y la puerta no ha sido destruida
    if (System.Array.TrueForAll(crystalsInZone, activated => activated) && !isDoorDestroyed) {
      /// Destruye la puerta llamando al método correspondiente en el componente de la puerta
      door.GetComponent<WallBehaviour>().DestroyDoor();
    }
  }
}
