using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Clase para el comportamiento de las paredes que bloquean el camino
public class WallBehaviour: MonoBehaviour {
  /// Método para desactivar la puerta
  public void DestroyDoor() {
    gameObject.SetActive(false); /// Desactiva la puerta
  }
}