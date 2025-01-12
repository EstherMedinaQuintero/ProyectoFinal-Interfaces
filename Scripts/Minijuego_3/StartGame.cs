using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Clase para gestionar la secuencia inicial del juego, incluyendo reproducción de clips de audio,
/// activación de paneles visuales con una animación de caída, y activación de scripts.
public class StartGame: MonoBehaviour {
  /// Fuente de audio utilizada para reproducir los clips de la secuencia.
  public AudioSource audioSource;

  /// Array de clips de audio que se reproducirán en secuencia.
  public AudioClip[] audioClips;

  /// Array de paneles que se activarán después de la secuencia de audio.
  public GameObject[] panels;

  /// Script que se activará al finalizar la secuencia de audio.
  public MonoBehaviour scriptToActivate;

  /// Índice actual del clip de audio que se está reproduciendo.
  private int currentClipIndex = 0;

  /// Inicializa el estado del juego al iniciar el componente.
  private void Start() {
    /// Desactiva todos los paneles al inicio
    foreach (GameObject panel in panels) {
      panel.SetActive(false);
    }

    /// Desactiva el script específico si está asignado
    if (scriptToActivate != null) {
      scriptToActivate.enabled = false;
    }

    /// Comienza la reproducción de la secuencia de audio
    PlayNextClip();
  }

  /// Reproduce el siguiente clip de audio en la secuencia.
  /// Cuando todos los clips han sido reproducidos, llama a `OnAudioSequenceComplete`.
  private void PlayNextClip() {
    if (currentClipIndex < audioClips.Length) {
      /// Configura el clip actual y reproduce
      audioSource.clip = audioClips[currentClipIndex];
      audioSource.volume = 0.5f; // Configura el volumen al 50%
      audioSource.Play();

      /// Incrementa el índice y programa la reproducción del siguiente clip
      currentClipIndex++;
      Invoke(nameof(PlayNextClip), audioSource.clip.length);
    } else {
      /// Finaliza la secuencia de audio
      OnAudioSequenceComplete();
    }
  }

  /// Lógica que se ejecuta cuando termina la secuencia de audio.
  /// Activa los paneles con una animación de caída y habilita el script asignado.
  private void OnAudioSequenceComplete() {
    foreach (GameObject panel in panels) {
      panel.SetActive(true); // Activa el panel
      StartCoroutine(DropFromAbove(panel.transform)); /// Aplica la animación de caída
    }

    /// Activa el script especificado si está asignado
    if (scriptToActivate != null) {
      scriptToActivate.enabled = true;
    }
  }

  /// Corrutina que anima los paneles cayendo desde arriba hasta su posición final.
  /// <param name="panelTransform"> Transform del panel a animar. </param>
  private IEnumerator DropFromAbove(Transform panelTransform) {
    float duration = 1f; /// Duración de la animación
    float elapsedTime = 0f;

    /// Define la posición inicial (500 unidades arriba) y la posición final
    Vector3 startPos = panelTransform.position + new Vector3(0, 500, 0);
    Vector3 endPos = panelTransform.position;

    /// Configura la posición inicial
    panelTransform.position = startPos;

    /// Realiza una interpolación lineal para animar la posición
    while (elapsedTime < duration) {
      panelTransform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
      elapsedTime += Time.deltaTime;
      yield return null;
    }

    /// Asegura que la posición final sea exacta
    panelTransform.position = endPos;
  }
}
