using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script controla todos los portales en la escena.
// Consulta el ManagerTransition para obtener el estado de los niveles completados (completedLevels).
// Si un nivel está completado, desactiva el portal correspondiente.
public class portalManager : MonoBehaviour
{
    // Array que almacena los portales en la escena.
    public GameObject[] portals;

    // Array que almacena el estado de los niveles completados (true si completado, false si no).
    public bool[] completedLevels = new bool[4];

    void Start()
    {
        // Busca el objeto TransitionManager en la escena.
        GameObject element = GameObject.Find("TransitionManager");
        if (element != null)
        {
            // Intenta obtener el componente ManagerTransition del objeto encontrado.
            ManagerTransition managerTransition = element.GetComponent<ManagerTransition>();
            if (managerTransition != null)
            {
                // Si se encuentra el script ManagerTransition, copia los niveles completados.
                completedLevels = managerTransition.completedLevels;
            }
            else
            {
                // Muestra un error si no se encuentra el script ManagerTransition en el objeto.
                Debug.LogError("No se encontró el script ManagerTransition en el objeto ManagerTransition.");
            }
        }
        else
        {
            // Muestra un error si no se encuentra el objeto TransitionManager en la escena.
            Debug.LogError("No se encontró el objeto ManagerTransition en la escena.");
        }

        // Desactiva los portales correspondientes si los niveles ya están completados.
        for (int i = 0; i < completedLevels.Length; i++)
        {
            if (completedLevels[i])
            {
                // Si el nivel está completado, desactiva el portal correspondiente.
                portals[i].SetActive(false);
                Debug.Log("Portal " + i + " desactivado");
            }
            else
            {
                // Si el nivel no está completado, deja el portal activado.
                Debug.Log("Portal " + i + " activado");
            }
        }
    }

    // Método para comprobar y actualizar el estado de los portales según los niveles completados.
    public void checkPortal(bool[] completedLevels)
    {
        // Recorre el array de niveles completados y desactiva los portales correspondientes.
        for (int i = 0; i < completedLevels.Length; i++)
        {
            if (completedLevels[i])
            {
                // Desactiva el portal si el nivel está completado.
                portals[i].SetActive(false);
            }
        }
    }
}
