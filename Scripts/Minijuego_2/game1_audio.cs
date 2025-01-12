using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Clase que gestiona la reproducción de un audio cuando el jugador entra en una zona específica
public class game1_audio: MonoBehaviour {
  /// Objeto que contiene el audio a reproducir
  public GameObject audioToPlay;

  /// Variable que indica si es la primera vez que se activa la explicación
  public bool explicationOne;

  /// Inicializa la variable para permitir que el audio se reproduzca la primera vez
  void Start() {
    explicationOne = true;
  }

  /// Método que se ejecuta cuando otro objeto entra en el área del trigger
  /// <param name="other"> El collider del objeto que entra al trigger. </param>
  public void OnTriggerEnter(Collider other) {
    /// Verifica si el objeto que entra tiene la etiqueta "Player"
    if (other.gameObject.tag == "Player") {
      /// Reproduce el audio solo si es la primera vez que se activa
      if (explicationOne == true) {
        /// Espera un segundo antes de reproducir el audio
        StartCoroutine(WaitForAudio());
      }
    }
  }

  /// Método para esperar un segundo antes de reproducir el audio
  private IEnumerator WaitForAudio() {
    yield return new WaitForSeconds(3.0f);
    PlayAudio();
    /// Marca que la explicación ya ha sido reproducida
    explicationOne = false;
  }

  /// Método para reproducir el audio asociado al objeto
  public void PlayAudio() {
    /// Accede al componente AudioSource del objeto que contiene el audio
    AudioSource audioSource = audioToPlay.GetComponent<AudioSource>();

    /// Reproduce el audio
    audioSource.Play();
  }
}
