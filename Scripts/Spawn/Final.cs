using UnityEngine;

public class Final : MonoBehaviour
{
    [SerializeField] private AudioSource finalSound;
    private ManagerTransition managerTransition;
    private bool hasPlayedSound = false;
    private bool hasIncreasedLight = false;
    
    // Lista de luces a modificar
    [SerializeField] private Light[] lights;
    
    private void Start()
    {
        GameObject transitionManagerObject = GameObject.Find("TransitionManager");
        if (transitionManagerObject != null)
        {
            managerTransition = transitionManagerObject.GetComponent<ManagerTransition>();
        }

        if (managerTransition == null)
        {
            Debug.LogError("No se encontró el objeto 'TransitionManager' o el componente 'ManagerTransition'.");
        }
    }

    private void Update()
    {
        if (managerTransition != null && !hasPlayedSound && AreAllLevelsCompleted())
        {
            PlayFinalSound();
            hasPlayedSound = true;
            Invoke("IncreaseLightIntensity", finalSound.clip.length); // Aumenta la luz al final del audio
        }
    }

    private bool AreAllLevelsCompleted()
    {
        int levelsCompleted = 0;
        foreach (bool levelCompleted in managerTransition.completedLevels)
        {
            if (levelCompleted)
            {
                levelsCompleted++;
            }
        }
        if (levelsCompleted == 3)
        {
            return true;
        }
        return false;
    }

    private void PlayFinalSound()
    {
        if (finalSound != null)
        {
            finalSound.Play();
            Debug.Log("¡Todos los niveles completados! Sonido final reproducido.");
        }
        else
        {
            Debug.LogWarning("El AudioSource no está asignado.");
        }
    }

    private void IncreaseLightIntensity()
    {
        if (hasIncreasedLight) return;

        foreach (Light light in lights)
        {
            if (light != null)
            {
                light.intensity *= 4; // Duplica la intensidad de la luz
            }
        }
        hasIncreasedLight = true;
        Debug.Log("Intensidad de las luces aumentada.");
    }
}
