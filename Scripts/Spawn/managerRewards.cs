using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerRewards : MonoBehaviour
{
    // Array que contiene los objetos de recompensa (presumiblemente, objetos que se activarán como recompensas al completar niveles)
    public GameObject[] rewards;

    void Update()
    {
        // Este método se ejecuta cada frame. En este caso, se usa para aplicar una rotación lenta a cada objeto de recompensa.
        
        for (int i = 0; i < rewards.Length; i++)
        {
            // Hace girar cada objeto de recompensa lentamente alrededor del eje Y (rotación en el plano horizontal).
            rewards[i].transform.Rotate(0, 0.1f, 0);
        }
    }

    // Método para comprobar las recompensas basadas en los niveles completados
    public void checkReward(bool[] completedLevels, string sceneName)
    {
        // Recorre todos los niveles completados y activa las recompensas correspondientes.
        for (int i = 0; i < completedLevels.Length; i++)
        {
            if (completedLevels[i])
            {
                // Si el nivel está marcado como completado, activa la recompensa asociada.
                rewards[i].SetActive(true);
            }
        }
    }
}