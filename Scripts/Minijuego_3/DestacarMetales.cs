using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class DestacarMetales : MonoBehaviour {

  public float radioDeteccion = 3f;
  public Transform pechoJugador;
  public LayerMask capaMetal;
  public Material materialDestacado;
  public Material colorLinea;
  public KeyCode teclaActivacion = KeyCode.F;
  public float grosorMax = 0.01f;
  public float grosorMin = 0.005f;
  public Alomancia alomancia;

  private Dictionary<Renderer, Material> objetosOriginales = new Dictionary<Renderer, Material>();
  private List<GameObject> objetosMetales = new List<GameObject>();
  private List<LineRenderer> lineas = new List<LineRenderer>();
  public InputActionReference accionDestacarMetales;

  void Awake() {
    accionDestacarMetales.action.Enable();
    accionDestacarMetales.action.performed += ctx => DestacarYResaltarMetales();
    accionDestacarMetales.action.canceled += ctx => RestaurarMateriales();
  }

  void Update() {
    for (int i = 0; i < objetosMetales.Count; i++) {
      if (lineas[i] != null && objetosMetales[i] != null) {
        float distancia = Vector3.Distance(pechoJugador.position, objetosMetales[i].transform.position);
        float grosor = Mathf.Lerp(grosorMax, grosorMin, distancia / radioDeteccion);
        lineas[i].startWidth = grosor;
        lineas[i].endWidth = grosor;
        lineas[i].SetPosition(0, pechoJugador.position);
        lineas[i].SetPosition(1, objetosMetales[i].transform.position);
      }
    }
  }



  void DestacarYResaltarMetales() {
    alomancia.estoyDestacandoMetales = true;
    // Detectar los objetos cercanos
    Collider[] objetosCercanos = Physics.OverlapSphere(transform.position, radioDeteccion, capaMetal);

    // Lista para almacenar los objetos que ya están dentro del rango
    List<GameObject> objetosDentroDelRango = new List<GameObject>();

    foreach (Collider col in objetosCercanos) {
      if (col.CompareTag("Metal")) {
        float distancia = Vector3.Distance(pechoJugador.position, col.transform.position);
        
        // Si el objeto está dentro del rango
        if (distancia <= radioDeteccion) {
          objetosDentroDelRango.Add(col.gameObject);

          // Si el objeto no está en la lista de objetos metálicos, se agrega
          if (!objetosMetales.Contains(col.gameObject)) {
            objetosMetales.Add(col.gameObject);
            Renderer rend = col.GetComponent<Renderer>();
            if (rend != null) {
              // Verificar si el objeto ya existe en el diccionario
              if (!objetosOriginales.ContainsKey(rend)) {
                objetosOriginales.Add(rend, rend.material); // Agregar el material original si no existe
              }
              rend.material = materialDestacado; // Cambiar al material azul
            }

            // Crear una nueva línea para el objeto
            LineRenderer linea = col.gameObject.AddComponent<LineRenderer>();
            linea.material = colorLinea;
            linea.startColor = Color.blue;
            linea.endColor = Color.blue;
            linea.startWidth = 0.01f;
            linea.endWidth = 0.01f;
            linea.positionCount = 2;
            linea.useWorldSpace = true;
            lineas.Add(linea);
          }
        }
      }
    }

    // Restaurar materiales y eliminar las líneas de los objetos que ya no están dentro del rango
    for (int i = objetosMetales.Count - 1; i >= 0; i--) {
      if (!objetosDentroDelRango.Contains(objetosMetales[i])) {
        // Restaurar el material original del objeto
        Renderer rend = objetosMetales[i].GetComponent<Renderer>();
        if (rend != null && objetosOriginales.ContainsKey(rend)) {
          rend.material = objetosOriginales[rend];
        }

        // Eliminar la línea asociada
        LineRenderer linea = objetosMetales[i].GetComponent<LineRenderer>();
        if (linea != null) {
          lineas.Remove(linea);
          Destroy(linea);
        }

        // Eliminar del listado
        objetosMetales.RemoveAt(i);
      }
    }
  }

  void RestaurarMateriales() {
    alomancia.estoyDestacandoMetales = false;
    // Restaurar los materiales originales
    foreach (var entry in objetosOriginales) {
      if (entry.Key != null) {
        entry.Key.material = entry.Value;
      }
    }

    // Limpiar las líneas y objetos metálicos
    LimpiarLineas();
    objetosMetales.Clear();
    objetosOriginales.Clear();
  }

  void LimpiarLineas() {
    // Eliminar las líneas existentes
    foreach (LineRenderer linea in lineas) {
      if (linea != null) {
        Destroy(linea);
      }
    }
    lineas.Clear();
  }

}
