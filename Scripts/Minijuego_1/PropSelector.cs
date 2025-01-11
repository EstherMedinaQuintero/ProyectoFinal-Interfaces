using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// Clase que selecciona un número específico de props de una lista de objetos
/// y les asigna comportamiento adicional para pistas y destrucción.
public class PropSelector: MonoBehaviour {
  /// Número de props que deben ser seleccionados aleatoriamente.
  [SerializeField] private int numberOfPropsToSelect = 5;

  /// Clip de audio que se usará como pista acústica para los props seleccionados.
  [SerializeField] private AudioClip propHintAudioClip;

  /// Lista de props seleccionados aleatoriamente.
  private List<GameObject> selectedProps = new List<GameObject>();

  /// Referencia al GameManager para actualizar el estado del juego.
  private GameManager gameManager;

  /// Referencia al texto de la UI que muestra información sobre los props seleccionados.
  public TMPro.TextMeshPro propsText;

  /// Inicializa el proceso de selección de props y configuración.
  void Start() {
    /// Busca el GameManager en la escena
    gameManager = FindObjectOfType<GameManager>();
    if (gameManager == null) {
      Debug.LogError("No se encontró el GameManager en la escena.");
      return;
    }

    /// Encuentra el contenedor de todos los props
    Transform propParent = GameObject.Find("PropHuntObjects").transform;
    if (propParent == null) {
      Debug.LogError("PropHuntObjects no se encontró en la escena.");
      return;
    }

    /// Obtiene todos los hijos del contenedor de props
    List<Transform> allProps = new List<Transform>(propParent.GetComponentsInChildren<Transform>());
    allProps.Remove(propParent); /// Elimina el contenedor principal de la lista

    /// Ajusta el número de props a seleccionar si no hay suficientes disponibles
    if (allProps.Count < numberOfPropsToSelect) {
      Debug.LogWarning($"Hay menos objetos ({allProps.Count}) que el número de props requeridos ({numberOfPropsToSelect}). Seleccionando todos los disponibles.");
      numberOfPropsToSelect = allProps.Count;
    }

    /// Selecciona props aleatoriamente
    while (selectedProps.Count < numberOfPropsToSelect && allProps.Count > 0) {
      int randomIndex = Random.Range(0, allProps.Count);
      GameObject selectedProp = allProps[randomIndex].gameObject;

      selectedProps.Add(selectedProp);
      allProps.RemoveAt(randomIndex);

      Debug.Log($"Prop seleccionado: {selectedProp.name}");
    }

    // Configura cada prop seleccionado con comportamiento y pistas
    foreach (var prop in selectedProps) {
      /// Añade el componente PropBehavior al prop
      PropBehavior propBehavior = prop.AddComponent<PropBehavior>();

      /// Configura el audio para las pistas acústicas
      AudioSource audioSource = prop.AddComponent<AudioSource>();
      audioSource.clip = propHintAudioClip;
      audioSource.playOnAwake = false;
      audioSource.loop = false;
      audioSource.volume = 0.5f; /// Ajusta el volumen
      audioSource.spatialBlend = 1.0f; /// Sonido 3D

      /// Pasa el AudioSource y el AudioClip al PropBehavior
      propBehavior.SetAudioSource(audioSource);
      propBehavior.SetHintSound(propHintAudioClip);
    }

    Debug.Log($"Se han seleccionado {selectedProps.Count} props válidos.");

    /// Informa al GameManager cuántos props hay
    gameManager.SetTotalProps(selectedProps.Count);
  }
}
