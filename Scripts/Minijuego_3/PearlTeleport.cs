using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

/// Clase que gestiona el teletransporte del jugador al colisionar con la Ender Pearl.
/// Incluye efectos de partículas y sonidos.
public class PearlTeleport: MonoBehaviour {
  /// Sonido que se reproduce al teletransportarse.
  public AudioClip teleportSound;

  /// Volumen base del sonido al teletransportarse.
  public float soundVolume = 0.5f;

  /// Variación aleatoria del tono (pitch) del sonido.
  public float pitchVariation = 0.2f;

  /// Prefab del sistema de partículas que se activa al teletransportarse.
  public GameObject particleSystemPrefab;

  /// Inicializa el sistema de partículas, desactivándolo por defecto.
  void Awake() {
    if (particleSystemPrefab != null) {
      GameObject currentParticleSystem = Instantiate(particleSystemPrefab);
      currentParticleSystem.SetActive(false); /// Desactivar partículas por defecto
      particleSystemPrefab = currentParticleSystem; /// Asignar la instancia al prefab
    }
  }

  /// Maneja las colisiones de la Ender Pearl con otros objetos.
  /// <param name="collision"> Información sobre la colisión. </param>
  private void OnCollisionEnter(Collision collision) {
    /// Si la Ender Pearl colisiona con el suelo
    if (collision.gameObject.CompareTag("Ground")) {
      /// Encuentra al jugador en la escena
      GameObject player = GameObject.FindGameObjectWithTag("Player");

      /// Reproduce el sonido de teletransporte
      PlayRandomizedSound();

      /// Si se encuentra al jugador
      if (player != null) {
        /// Posicionar y activar las partículas
        particleSystemPrefab.transform.position = transform.position;
        particleSystemPrefab.SetActive(true);

        /// Ajustar la posición del jugador a la posición de la Ender Pearl
        Vector3 perlaPos = transform.position;
        perlaPos.y += 1; /// Ajuste para evitar que el jugador quede dentro del suelo
        player.transform.position = perlaPos;

        /// Desactivar partículas después del teletransporte
        particleSystemPrefab.SetActive(false);
      }

      /// Destruir la Ender Pearl
      Destroy(gameObject);
    }

    /// Si la Ender Pearl colisiona con el agua
    if (collision.gameObject.CompareTag("Water")) {
      Destroy(gameObject); /// Destruir la Ender Pearl sin teletransportar al jugador
    }
  }

  /// Reproduce el sonido de teletransporte con variaciones aleatorias de volumen y tono.
  void PlayRandomizedSound() {
    if (teleportSound != null) {
      /// Crear un objeto temporal para reproducir el sonido
      GameObject soundObject = new GameObject("TempAudio");
      AudioSource audioSource = soundObject.AddComponent<AudioSource>();
      audioSource.clip = teleportSound;

      /// Aplicar variación aleatoria al volumen y pitch
      audioSource.volume = soundVolume * Random.Range(0.9f, 1.1f);
      audioSource.pitch = 1.0f + Random.Range(-pitchVariation, pitchVariation);

      /// Reproducir el sonido
      audioSource.Play();

      /// Destruir el objeto temporal después de que el sonido termine
      Destroy(soundObject, teleportSound.length + 0.1f);
    }
  }
}
