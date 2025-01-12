using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Clase que desactiva un objeto de texto después de un tiempo determinado.
public class textDisappear: MonoBehaviour {
  /// Tiempo en segundos antes de que el texto desaparezca.
  public float timeToDisappear;

  /// Inicializa el comportamiento y comienza la corrutina que desactiva el objeto.
  void Start() {
    /// Inicia la corrutina que desactiva el objeto después del tiempo especificado.
    StartCoroutine(Disappear());
  }

  /// Corrutina que espera el tiempo especificado y luego desactiva el objeto.
  private IEnumerator Disappear() {
    /// Espera el tiempo indicado.
    yield return new WaitForSeconds(timeToDisappear);

    /// Desactiva el objeto.
    gameObject.SetActive(false);
  }
}
