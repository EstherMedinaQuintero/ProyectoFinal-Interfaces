using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Alomancia : MonoBehaviour {
    public float maxDistancia = 10f;  // Distancia máxima para aplicar la fuerza
    public float fuerzaEmpuje = 10f;  // Fuerza de empuje
    public Rigidbody jugadorRb;  // Referencia al Rigidbody del jugador
    public LayerMask capaMetal;  // Capa de los metales
    
    public bool estoyDestacandoMetales = false;  // Variable para saber si estamos destacando metales
    private bool empujando = false;
    private bool atrayendo = false;

    public InputActionReference accionEmpujar;
    public InputActionReference accionAtraer;
    public float distanciaMinima = 1f;

    void Awake() {
      accionAtraer.action.Enable();
      accionEmpujar.action.Enable();
      accionAtraer.action.performed += ctx => atrayendo = true;
      accionAtraer.action.canceled += ctx => atrayendo = false;
      accionEmpujar.action.performed += ctx => empujando = true;
      accionEmpujar.action.canceled += ctx => empujando = false;

    }

    void FixedUpdate() {
        if (empujando) {
            AplicarFuerza(transform.forward, false);
        } else if (atrayendo) {
            AplicarFuerza(transform.forward, true);
        }
    }

    void AplicarFuerza(Vector3 direccion, bool atrayendo = false) {
        Debug.Log("Aplicando fuerza");
        if (!estoyDestacandoMetales) {
            return; // Si no estamos destacando metales, no hacemos nada
        }

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 1f ,direccion, out hit, maxDistancia, capaMetal)) {
            Debug.Log("Objeto metálico detectado");
            GameObject objetoMetalico = hit.collider.gameObject;

            if (objetoMetalico.CompareTag("Metal")) { // Solo afecta objetos con la tag "Metal"
                Rigidbody rb = objetoMetalico.GetComponent<Rigidbody>();
                if (rb != null) { // Si el objeto tiene un Rigidbody, aplicamos la mecánica de fuerza
                    // Calcular la dirección hacia el objeto metálico desde el jugador
                    Vector3 direccionHaciaObjeto = (objetoMetalico.transform.position - transform.position).normalized;

                    float masaObjeto = rb.mass;
                    float masaJugador = jugadorRb.mass;
                    Vector3 fuerza = direccionHaciaObjeto * fuerzaEmpuje;  // La fuerza se aplica en la dirección hacia el objeto

                    // Verificamos la distancia del objeto para aplicar el rango máximo
                    float distancia = Vector3.Distance(transform.position, objetoMetalico.transform.position);
                    if (distancia > maxDistancia) {
                        return; // Si está fuera del rango, no hacemos nada
                    } else if (distancia < distanciaMinima) {
                        return; // Si está muy cerca, no hacemos nada
                    }
                    if (!rb.isKinematic) { // Si el objeto NO está anclado, puede moverse
                        if (masaObjeto < masaJugador) {
                            if (atrayendo) {
                                Debug.Log("Atrayendo");
                                rb.AddForce(-fuerza * (masaJugador / masaObjeto), ForceMode.Force);
                            } else {
                                Debug.Log("Empujando");
                                rb.AddForce(fuerza * (masaJugador / masaObjeto), ForceMode.Force);
                            }
                        } else if (masaObjeto > masaJugador) {
                            if (atrayendo) {
                                Debug.Log("Atrayendo");
                                jugadorRb.AddForce(-fuerza * (masaObjeto / masaJugador), ForceMode.Force);
                            } else {
                                Debug.Log("Aplicando: " + fuerza * (masaObjeto / masaJugador));
                                jugadorRb.AddForce(fuerza * (masaObjeto / masaJugador), ForceMode.Force);
                            }
                        }
                    } else { // Si el objeto es estático (kinemático), el jugador siempre es empujado
                        jugadorRb.AddForce(fuerza, ForceMode.Force);
                    }

                    // Detectar si el empuje es hacia el suelo y si el objeto está en el suelo
                    if (empujando && direccion.y < -0.3f) { // Si la dirección del empuje es hacia abajo
                    Debug.Log("Dirección hacia abajo");
                        RaycastHit sueloHit;
                        if (Physics.Raycast(objetoMetalico.transform.position, Vector3.down, out sueloHit, 1f)) {
                            if (sueloHit.collider != null) {  // Si hay algo debajo del objeto
                                // Aplicamos el empuje al jugador en dirección opuesta al objeto
                                jugadorRb.AddForce(-fuerza * fuerzaEmpuje, ForceMode.Force);
                                Debug.Log("Empuje hacia el jugador en dirección contraria al suelo");
                            }
                        }
                    }
                }
            }
        }
    }
}
