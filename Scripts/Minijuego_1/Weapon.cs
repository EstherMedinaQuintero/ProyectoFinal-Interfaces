using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// Clase base para armas, compatible con el sistema de interacción XR.
/// Gestiona disparos, retroceso y eventos de interacción con el jugador.
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]

public class Weapon: MonoBehaviour {
  /// Fuerza de disparo aplicada al proyectil.
  [SerializeField] protected float shootingForce;

  /// Posición desde donde se genera el proyectil.
  [SerializeField] protected Transform bulletSpawn;

  /// Fuerza de retroceso aplicada al arma cuando dispara.
  [SerializeField] protected float recoilForce;

  /// Daño que inflige el arma.
  [SerializeField] protected float damage;

  /// Componente opcional para manejar eventos tutoriales relacionados con el arma.
  public TutorialBehavior tutorial;

  /// Bandera para rastrear si el arma ha sido recogida.
  private bool hasPickedUpWeapon = false;

  /// Bandera para rastrear si el arma ha disparado al menos una vez.
  private bool hasShot = false;

  /// Referencia al componente Rigidbody del arma.
  private Rigidbody rigidbody;

  /// Referencia al componente XRGrabInteractable para manejar interacciones XR.
  private XRGrabInteractable interactableWeapon;

  /// Inicializa las referencias y configura los eventos de interacción del arma.
  protected virtual void Awake() {
    rigidbody = GetComponent<Rigidbody>();
    interactableWeapon = GetComponent<XRGrabInteractable>();
    SetUpInteractableWeaponEvents();
  }

  /// Configura los eventos del sistema XRGrabInteractable.
  /// Maneja recogida, suelta, inicio de disparo y detención de disparo.
  private void SetUpInteractableWeaponEvents() {
    interactableWeapon.onSelectEntered.AddListener(PickUpWeapon);
    interactableWeapon.onSelectExited.AddListener(DropWeapon);
    interactableWeapon.onActivate.AddListener(StartShooting);
    interactableWeapon.onDeactivate.AddListener(StopShooting);
  }

  /// Llamado cuando el arma es recogida.
  /// Informa al tutorial si el arma ha sido recogida por primera vez.
  private void PickUpWeapon(XRBaseInteractor interactor) {
    if (tutorial != null && !hasPickedUpWeapon) {
      tutorial.WeaponPickedUp();
      hasPickedUpWeapon = true;
    }
  }

  /// Llamado cuando el arma es soltada.
  /// Puede ser sobrescrito para manejar lógica adicional.
  private void DropWeapon(XRBaseInteractor interactor) {
    /// Lógica para cuando se suelta el arma (vacío intencionalmente).
  }

  /// Llamado cuando se inicia el disparo.
  /// Puede ser sobrescrito por clases derivadas para agregar funcionalidad.
  protected virtual void StartShooting(XRBaseInteractor interactor) {
    /// Lógica para iniciar el disparo (vacío intencionalmente).
  }

  /// Llamado cuando se detiene el disparo.
  /// Puede ser sobrescrito por clases derivadas.
  protected virtual void StopShooting(XRBaseInteractor interactor) {
    /// Lógica para detener el disparo (vacío intencionalmente).
  }

  /// Lógica para realizar un disparo, incluyendo retroceso y notificación de tutorial.
  /// Puede ser sobrescrito para agregar comportamiento específico.
  protected virtual void Shoot() {
    ApplyRecoil();
    if (tutorial != null && !hasShot) {
      Debug.Log("Tutorial exists for shooting");
      tutorial.PlayerShot();
      hasShot = true;
    }
  }

  /// Aplica una fuerza de retroceso al arma cuando dispara.
  private void ApplyRecoil() {
    rigidbody.AddRelativeForce(Vector3.back * recoilForce, ForceMode.Impulse);
  }

  /// Devuelve la fuerza de disparo del arma.
  /// <returns> La fuerza de disparo. </returns>
  public float GetShootingForce() {
    return shootingForce;
  }

  /// Devuelve el daño que inflige el arma.
  /// <returns> El valor del daño. </returns>
  public float GetDamage() {
    return damage;
  }
}
