using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase que en un pedestal, gestiona la activación de un cristal específico y su efecto mágico
public class WildCrystal : MonoBehaviour
{
    // Objeto que representa el cristal que debe activarse
    public GameObject crystal;

    // Objeto que gestiona los cristales en la escena
    public GameObject manager;

    // Indica si el cristal está listo para activarse
    public bool crystalReady = false;

    // Objeto que representa el efecto de explosión mágica cuando el cristal se activa
    public GameObject magicExplosion;

    void Start()
    {
        // Desactiva el efecto de explosión mágica al inicio
        magicExplosion.SetActive(false);
    }

    // Método que se ejecuta cuando otro objeto entra en el área de colisión
    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en la zona es el cristal
        if (other.gameObject == crystal)
        {
            // Marca el cristal como listo para activarse
            crystalReady = true;

            // Activa el efecto de explosión mágica
            magicExplosion.SetActive(true);

            // Llama al método para activar el cristal en el manager de cristales
            manager.GetComponent<crystalManager>().ActivateCrystal(crystal);
        }
    }
}
