using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PearlTeleport : MonoBehaviour {

  public AudioClip teleportSound; // Sonido al teletransportarse
  public float soundVolume = 0.5f; // Volumen base del sonido
  public float pitchVariation = 0.2f; // Variación del tono del sonido
  public GameObject particleSystemPrefab; // Prefab del sistema de partículas

  void Awake() {
    if (particleSystemPrefab != null) {
      GameObject currentParticleSystem = Instantiate(particleSystemPrefab); // Instanciar el sistema de partículas
      currentParticleSystem.SetActive(false); // Desactivarlo por defecto
      particleSystemPrefab = currentParticleSystem;
    }
  }

  private void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.CompareTag("Ground")) {
      GameObject player = GameObject.FindGameObjectWithTag("Player");
      PlayRandomizedSound();
      if (player != null) {
        particleSystemPrefab.transform.position = transform.position;
        particleSystemPrefab.SetActive(true);
        Vector3 perlaPos = transform.position;
        perlaPos.y += 1;
        player.transform.position = perlaPos;
      }
      particleSystemPrefab.SetActive(false);
      Destroy(gameObject);
    }
    if (collision.gameObject.CompareTag("Water")) {
      Destroy(gameObject);
    }
  }
  void PlayRandomizedSound() {
    if (teleportSound != null) {
      GameObject soundObject = new GameObject("TempAudio");
      AudioSource audioSource = soundObject.AddComponent<AudioSource>();
      audioSource.clip = teleportSound;
      audioSource.volume = soundVolume * Random.Range(0.9f, 1.1f);
      audioSource.pitch = 1.0f + Random.Range(-pitchVariation, pitchVariation);
      audioSource.Play();
      Destroy(soundObject, teleportSound.length + 0.1f); // Destruir el objeto cuando termine el sonido
    }
  }
}
