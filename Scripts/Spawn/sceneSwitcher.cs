using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private InputActionReference switchSceneAction; // Acción para cambiar de escena
    [SerializeField] private string nextSceneName; // Nombre de la próxima escena
    [SerializeField] private TMP_Text instructionText; // Texto de instrucciones (puede ser normal Text)

    private bool sceneChanging = false;

    private void Awake()
    {
        // Habilitar la acción de entrada
        if (switchSceneAction != null)
        {
            switchSceneAction.action.Enable();
            switchSceneAction.action.performed += OnSwitchScene;
        }
    }

    private void OnDestroy()
    {
        // Deshabilitar la acción para evitar problemas
        if (switchSceneAction != null)
        {
            switchSceneAction.action.performed -= OnSwitchScene;
            switchSceneAction.action.Disable();
        }
    }

    private void Start()
    {
        // Mostrar el texto inicial si está configurado
        if (instructionText != null)
        {
            instructionText.text = "Presiona la X para continuar.";
            instructionText.gameObject.SetActive(true);
        }
    }

    private void OnSwitchScene(InputAction.CallbackContext context)
    {
        if (!sceneChanging && !string.IsNullOrEmpty(nextSceneName))
        {
            sceneChanging = true;

            // Cambiar directamente de escena
            if (instructionText != null)
            {
                instructionText.text = "Accediendo al mundo de las hadas...";
            }
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
