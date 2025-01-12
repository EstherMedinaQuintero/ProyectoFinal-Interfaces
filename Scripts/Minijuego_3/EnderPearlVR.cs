using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

/// Clase para gestionar la creación y teletransporte mediante una "Ender Pearl" en un entorno VR.
/// Incluye efectos de animación, sonido y partículas.
public class EnderPearlVR: MonoBehaviour {
  /// Prefab de la Ender Pearl.
  public GameObject pearlPrefab;

  /// Transform que indica la posición inicial donde aparecerá la Ender Pearl (mano del jugador).
  public Transform handTransform;

  /// Acción de entrada para activar la creación de la Ender Pearl.
  public InputActionReference accionActivar;

  /// Sonido que se reproduce al crear la Ender Pearl.
  public AudioClip spawnSound;

  /// Sonido que se reproduce al teletransportarse.
  public AudioClip teleportSound;

  /// Duración de la animación de aparición de la Ender Pearl.
  public float spawnDuration = 0.5f;

  /// Volumen base de los sonidos.
  public float soundVolume = 0.5f;

  /// Variación aleatoria del tono (pitch) para los sonidos.
  public float pitchVariation = 0.2f;

  /// Material que se aplicará a la Ender Pearl cuando sea interactuable.
  public Material interactableMaterial;

  /// Prefab del sistema de partículas que se activa al aparecer la Ender Pearl.
  public GameObject particleSystemPrefab;

  /// Prefab del sistema de partículas que se activa al teletransportarse.
  public GameObject arrivalParticleSystemPrefab;

  private GameObject currentPearl;
  private GameObject currentParticleSystem;

  /// Escala inicial de la Ender Pearl.
  private Vector3 originalScale = new Vector3(0.15147f, 0.15147f, 0.15147f);

  /// Configuración inicial de la clase. Asigna la acción de entrada y prepara las partículas.
  void Awake() {
    if (particleSystemPrefab != null) {
      currentParticleSystem = Instantiate(particleSystemPrefab);
      currentParticleSystem.SetActive(false); /// Desactivar partículas por defecto
    }

    if (accionActivar != null) {
      accionActivar.action.Enable();
      accionActivar.action.performed += ctx => SpawnPearl();
    }
  }

  /// Método para crear la Ender Pearl. Inicia su animación y configuración.
  void SpawnPearl() {
    if (currentPearl == null && pearlPrefab != null) {
      /// Instanciar la Ender Pearl
      currentPearl = Instantiate(pearlPrefab, handTransform.position, handTransform.rotation);

      /// Configuración inicial: desactivar físicas y agarre
      Rigidbody rb = currentPearl.GetComponent<Rigidbody>();
      XRGrabInteractable grabInteractable = currentPearl.GetComponent<XRGrabInteractable>();
      if (rb != null) rb.isKinematic = true;
      if (grabInteractable != null) grabInteractable.enabled = false;

      /// Configurar tamaño inicial y reproducir animación
      currentPearl.transform.localScale = Vector3.zero;
      StartCoroutine(AnimateSpawn(currentPearl));

      /// Reproducir sonido con tono aleatorio
      PlayRandomizedSound();

      /// Añadir comportamiento de teletransporte a la Ender Pearl
      currentPearl.AddComponent<PearlTeleport>();
      PearlTeleport pearlTeleport = currentPearl.GetComponent<PearlTeleport>();
      pearlTeleport.teleportSound = teleportSound;
      pearlTeleport.soundVolume = soundVolume;
      pearlTeleport.pitchVariation = pitchVariation;
      pearlTeleport.particleSystemPrefab = arrivalParticleSystemPrefab;
    }
  }

  /// Reproduce el sonido de aparición con variaciones aleatorias de volumen y tono.
  void PlayRandomizedSound() {
    if (spawnSound != null) {
      GameObject soundObject = new GameObject("TempAudio");
      AudioSource audioSource = soundObject.AddComponent<AudioSource>();
      audioSource.clip = spawnSound;
      audioSource.volume = soundVolume * Random.Range(0.9f, 1.1f); /// Variar volumen
      audioSource.pitch = 1.0f + Random.Range(-pitchVariation, pitchVariation); /// Variar tono
      audioSource.Play();
      Destroy(soundObject, spawnSound.length + 0.1f); /// Destruir después del sonido
    }
  }

  /// Corrutina que anima la aparición de la Ender Pearl.
  /// <param name="pearl"> El GameObject de la Ender Pearl. </param>
  IEnumerator AnimateSpawn(GameObject pearl) {
    /// Posicionar y activar las partículas
    currentParticleSystem.transform.position = pearl.transform.position;
    currentParticleSystem.SetActive(true);

    float elapsedTime = 0f;

    while (elapsedTime < spawnDuration) {
      float t = elapsedTime / spawnDuration;
      pearl.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, t);
      elapsedTime += Time.deltaTime;
      yield return null;
    }

    /// Asegurar el tamaño final correcto
    pearl.transform.localScale = originalScale;

    /// Activar físicas y agarre
    Rigidbody rb = pearl.GetComponent<Rigidbody>();
    XRGrabInteractable grabInteractable = pearl.GetComponent<XRGrabInteractable>();
    if (rb != null) rb.isKinematic = false;
    if (grabInteractable != null) grabInteractable.enabled = true;

    /// Cambiar el material para indicar que es interactuable
    if (interactableMaterial != null) {
      Renderer renderer = pearl.GetComponent<Renderer>();
      if (renderer != null) renderer.material = interactableMaterial;
    }

    /// Desactivar partículas al terminar
    currentParticleSystem.SetActive(false);
  }
}
