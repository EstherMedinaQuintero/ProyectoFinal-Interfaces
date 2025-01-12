using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerActivateaudio : MonoBehaviour
{

    public GameObject audioToPlay;
    // Start is called before the first frame update
    // Variable to store explication one first
    public bool explicationOne;

    // Revisa si el objeto TransitionManager está en la escena y si el script ManagerTransition está en el objeto TransitionManager
    // verifica si es la primera vez que se activa el audio
    void Start()
    {
        GameObject element = GameObject.Find("TransitionManager");
        if (element != null)
        {
            ManagerTransition managerTransition = element.GetComponent<ManagerTransition>();
            if (managerTransition != null)
            {
                explicationOne = managerTransition.FirstTime;
            }
            else
            {
                Debug.LogError("No se encontró el script ManagerTransition en el objeto ManagerTransition.");
            }
        }
        else
        {
            Debug.LogError("No se encontró el objeto ManagerTransition en la escena.");
        }
    }

    // cuando el jugador entra en contacto con el objeto, se activa el audio
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (explicationOne == true)
            {
                PlayAudio();
                explicationOne = false;
            }
        }
    }
    public void PlayAudio()
    {
        // acceder al componente AudioSource del objeto audioToPlay
        AudioSource audioSource = audioToPlay.GetComponent<AudioSource>();
        // reproducir el audio
        audioSource.Play();
    }

}
