using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Clase que gestiona la activación de un audio cuando el jugador entra en contacto con un objeto.
/// Usa un sistema de transición para verificar si es la primera vez que se reproduce el audio.
public class ManagerActivateaudio: MonoBehaviour {
  /// Objeto que contiene el audio a reproducir.
  public GameObject audioToPlay;

  /// Indica si es la primera vez que se activa el audio.
  public bool explicationOne;

  /// Inicializa el estado del script y verifica si es la primera vez que debe reproducirse el audio.
  void Start() {
    /// Busca el objeto TransitionManager en la escena
    GameObject element = GameObject.Find("TransitionManager");
    if (element != null) {
      /// Intenta obtener el script ManagerTransition del objeto encontrado
      ManagerTransition managerTransition = element.GetComponent<ManagerTransition>();
      if (managerTransition != null) {
        /// Asigna el valor de FirstTime a explicationOne
        explicationOne = managerTransition.FirstTime;
      } else {
        Debug.LogError("No se encontró el script ManagerTransition en el objeto TransitionManager.");
      }
    } else {
      Debug.LogError("No se encontró el objeto TransitionManager en la escena.");
    }
  }

  /// Método que se ejecuta cuando el jugador entra en el área del trigger.
  /// <param name="other"> El collider del objeto que entra al trigger. </param>
  public void OnTriggerEnter(Collider other) {
    /// Verifica si el objeto que entra tiene la etiqueta "Player"
    if (other.gameObject.CompareTag("Player")) {
      /// Reproduce el audio solo si es la primera vez
      if (explicationOne) {
        /// Espera antes de reproducir el audio
        StartCoroutine(WaitForAudio());
      }
    }
  }

  /// Corrutina que espera 2 segundos antes de reproducir el audio.
  private IEnumerator WaitForAudio() {
    yield return new WaitForSeconds(2.0f); /// Espera 2 segundos
    PlayAudio(); /// Reproduce el audio
    explicationOne = false; /// Marca que el audio ya fue reproducido
  }

  /// Reproduce el audio asociado al objeto especificado.
  public void PlayAudio() {
    /// Obtiene el componente AudioSource del objeto audioToPlay
    AudioSource audioSource = audioToPlay.GetComponent<AudioSource>();

    if (audioSource != null) {
      /// Reproduce el audio si el componente AudioSource está presente
      audioSource.Play();
    } else {
      Debug.LogError("No se encontró un componente AudioSource en el objeto audioToPlay.");
    }
  }
}
