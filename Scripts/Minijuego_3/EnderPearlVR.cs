using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class EnderPearlVR : MonoBehaviour {
    public GameObject pearlPrefab; // Prefab de la pelota
    public Transform handTransform; // Lugar donde aparecerá la pelota
    public InputActionReference accionActivar;
    public AudioClip spawnSound; // Sonido al aparecer la pelota
    public AudioClip teleportSound; // Sonido al teletransportarse
    public float spawnDuration = 0.5f;
    public float soundVolume = 0.5f; // Volumen base del sonido
    public float pitchVariation = 0.2f; // Variación del tono del sonido
    public Material interactableMaterial; // Nuevo material cuando se pueda agarrar
    public GameObject particleSystemPrefab; // Prefab del sistema de partículas
    public GameObject arrivalParticleSystemPrefab; // Prefab del sistema de partículas de llegada

    private GameObject currentPearl;
    private GameObject currentParticleSystem;
    private Vector3 originalScale = new Vector3(0.15147f, 0.15147f, 0.15147f);

    void Awake() {
        if (particleSystemPrefab != null) {
            currentParticleSystem = Instantiate(particleSystemPrefab); // Instanciar el sistema de partículas
            currentParticleSystem.SetActive(false); // Desactivarlo por defecto
        }

        if (accionActivar != null) {
            accionActivar.action.Enable();
            accionActivar.action.performed += ctx => SpawnPearl();
        }
    }

    void SpawnPearl() {
        if (currentPearl == null && pearlPrefab != null) {
            // Instanciar la perla
            currentPearl = Instantiate(pearlPrefab, handTransform.position, handTransform.rotation);
            Rigidbody rb = currentPearl.GetComponent<Rigidbody>();
            XRGrabInteractable grabInteractable = currentPearl.GetComponent<XRGrabInteractable>();

            // Desactivar físicas y agarre antes de la animación
            if (rb != null) rb.isKinematic = true;
            if (grabInteractable != null) grabInteractable.enabled = false;

            currentPearl.transform.localScale = Vector3.zero;
            StartCoroutine(AnimateSpawn(currentPearl));

            // Reproducir sonido con variación aleatoria
            PlayRandomizedSound();

            currentPearl.AddComponent<PearlTeleport>();
            PearlTeleport pearlTeleport = currentPearl.GetComponent<PearlTeleport>();
            pearlTeleport.teleportSound = teleportSound;
            pearlTeleport.soundVolume = soundVolume;
            pearlTeleport.pitchVariation = pitchVariation;
            pearlTeleport.particleSystemPrefab = arrivalParticleSystemPrefab;
        }
    }

    void PlayRandomizedSound() {
        if (spawnSound != null) {
            GameObject soundObject = new GameObject("TempAudio");
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.clip = spawnSound;
            
            // Aplicar variación aleatoria al volumen y pitch
            audioSource.volume = soundVolume * Random.Range(0.9f, 1.1f); // Variar volumen
            audioSource.pitch = 1.0f + Random.Range(-pitchVariation, pitchVariation); // Variar tono

            audioSource.Play();
            Destroy(soundObject, spawnSound.length + 0.1f); // Destruir el objeto cuando termine el sonido
        }
    }

    IEnumerator AnimateSpawn(GameObject pearl) {
        // Posicionar el sistema de partículas en la posición de la perla
        currentParticleSystem.transform.position = pearl.transform.position;
        currentParticleSystem.SetActive(true); // Activar el sistema de partículas

        float elapsedTime = 0f;

        while (elapsedTime < spawnDuration) {
            float t = elapsedTime / spawnDuration;
            pearl.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurar tamaño final correcto
        pearl.transform.localScale = originalScale;

        // Activar físicas y agarre después de la animación
        Rigidbody rb = pearl.GetComponent<Rigidbody>();
        XRGrabInteractable grabInteractable = pearl.GetComponent<XRGrabInteractable>();

        if (rb != null) rb.isKinematic = false;
        if (grabInteractable != null) grabInteractable.enabled = true;

        // Cambiar material cuando se pueda agarrar
        if (interactableMaterial != null) {
            Renderer renderer = pearl.GetComponent<Renderer>();
            if (renderer != null) {
                renderer.material = interactableMaterial;
            }
        }

        // Desactivar las partículas después de la animación
        currentParticleSystem.SetActive(false);
    }
}
