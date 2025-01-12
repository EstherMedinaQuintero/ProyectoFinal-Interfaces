using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerTransition : MonoBehaviour
{
    // Referencia al animador responsable de las transiciones de fade in/out
    [SerializeField] private Animator fadeAnimator;

    // Array para rastrear los niveles completados (4 niveles en este caso)
    public bool[] completedLevels = new bool[4];

    // Indica si el jugador está jugando por primera vez
    public bool FirstTime = true;

    // Instancia estática para implementar el patrón Singleton
    private static ManagerTransition instance;

    // Referencia al jugador y su posición
    public GameObject player;
    private Vector3 playerPosition;

    void Awake()
    {
        // Implementación del patrón Singleton
        if (instance != null && instance != this)
        {
            // Si ya existe una instancia de ManagerTransition, destruye esta para evitar duplicados
            Destroy(this.gameObject);
            return;
        }

        // Asigna esta instancia como la única
        instance = this;

        // Marca este objeto para que no sea destruido al cambiar de escena
        DontDestroyOnLoad(gameObject);

        // Inicializa el estado de los niveles como no completados
        for (int i = 0; i < completedLevels.Length; i++)
        {
            completedLevels[i] = false;
        }
    }

    // Inicia la transición de escena con fade in/out y carga de escena
    public void StartSceneTransition(string sceneToLoad, int levelIndex)
    {
        // Marca que ya no es la primera vez que se juega
        FirstTime = false;

        // Lanza la corrutina para manejar la transición de escena
        StartCoroutine(TransitionCoroutine(sceneToLoad, levelIndex));
    }

    private IEnumerator TransitionCoroutine(string sceneToLoad, int levelIndex)
    {
        // Reproduce la animación de fade out
        fadeAnimator.Play("fadeout");

        // Espera a que termine la animación de fade out (1 segundo)
        yield return new WaitForSeconds(1.0f);

        // Carga la nueva escena
        SceneManager.LoadScene(sceneToLoad);

        // Si se carga la escena "home", realiza acciones específicas
        if (sceneToLoad == "home")
        {
            // Espera un frame para asegurar que la nueva escena se haya cargado
            yield return new WaitForSeconds(1.0f);

            // Busca y actualiza el estado de recompensas
            GameObject rewardController = GameObject.Find("ControllerReward");
            if (rewardController != null)
            {
                managerRewards rewardManager = rewardController.GetComponent<managerRewards>();
                if (rewardManager != null)
                {
                    completedLevels[levelIndex] = true;
                    rewardManager.checkReward(completedLevels, sceneToLoad);
                }
                else
                {
                    Debug.LogError("No se encontró el script managerRewards en el objeto ControllerReward.");
                }
            }
            else
            {
                Debug.LogError("No se encontró el objeto ControllerReward en la escena.");
            }

            // Busca y actualiza el estado de portales
            GameObject portalController = GameObject.Find("PortalManager");
            if (portalController != null)
            {
                portalManager portalManager = portalController.GetComponent<portalManager>();
                if (portalManager != null)
                {
                    completedLevels[levelIndex] = true;
                    portalManager.checkPortal(completedLevels);
                }
                else
                {
                    Debug.LogError("No se encontró el script portalManager en el objeto ControllerPortal.");
                }
            }
            else
            {
                Debug.LogError("No se encontró el objeto ControllerPortal en la escena.");
            }
        }

        // Espera otro segundo para estabilizar la transición
        yield return new WaitForSeconds(1.0f);

        // Reproduce la animación de fade in
        fadeAnimator.Play("fadeIn");

        // Espera a que termine la animación de fade in (1 segundo)
        yield return new WaitForSeconds(1.0f);
    }

    // Marca un nivel como completado
    public void SetLevelCompleted(int levelIndex)
    {
        completedLevels[levelIndex] = true;
    }

    // Guarda la posición actual del jugador
    public bool SavePosition()
    {
        playerPosition = player.transform.position;
        return true;
    }

    // Carga la posición previamente guardada del jugador
    public Vector3 LoadPosition()
    {
        return playerPosition;
    }
}
