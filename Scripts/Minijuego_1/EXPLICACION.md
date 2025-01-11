# Minijuego Prop Hunt

Este proyecto implementa un minijuego interactivo de tipo "Prop Hunt" donde el jugador debe buscar y disparar a objetos escondidos en un escenario. Este documento explica el funcionamiento del juego y describe el propósito de cada script.

---

## Índice

1. [Funcionalidad del juego](#funcionalidad-del-juego)
2. [Estructura de scripts](#estructura-de-scripts)
    - [Gestión del juego](#gestión-del-juego)
    - [Armas](#armas)
    - [Proyectiles](#proyectiles)
    - [Props](#props)
    - [Interactividad y tutorial](#interactividad-y-tutorial)
    - [Interfaces](#interfaces)
3. [Flujo del juego](#flujo-del-juego)
4. [Cómo jugar](#cómo-jugar)

---

## Funcionalidad del juego <div id="funcionalidad-del-juego"/>

1. **Tutorial interactivo**:
   - El jugador recibe instrucciones paso a paso para aprender a jugar. Esto incluye:
     - Coger un arma.
     - Disparar.
     - Buscar y destruir objetos escondidos (props) en el escenario.

2. **Búsqueda de props**:
   - Los jugadores deben encontrar y destruir 5 props seleccionados aleatoriamente. Los props ofrecen pistas visuales y acústicas para facilitar su localización.

3. **Sistema de progreso**:
   - El juego realiza un seguimiento de los props destruidos y finaliza el minijuego cuando todos los objetivos han sido eliminados.

---

## Estructura de scripts <div id="estructura-de-scripts"/>

### 1. Gestión del juego <div id="gestión-del-juego"/>

- **`GameManager.cs`**:
  - Maneja el estado del juego.
  - Registra los props destruidos y notifica cuando todos han sido encontrados.

- **`SpawnerManager.cs`**:
  - Detecta cuándo el minijuego ha sido completado utilizando el evento `OnGameCompleted` del `GameManager`.

### 2. Armas <div id="armas"/>

- **`Weapon.cs`**:
  - Clase base para todas las armas. Define la lógica general como disparar, aplicar retroceso y gestionar eventos de interacción XR.

- **`Pistol.cs`**:
  - Extiende la funcionalidad de `Weapon` para una pistola. Crea y lanza un proyectil al disparar.

- **`Rifle.cs`**:
  - Extiende `Weapon` para implementar disparos continuos, cambiando el color del láser mientras dispara.

### 3. Proyectiles <div id="proyectiles"/>

- **`Projectile.cs`**:
  - Clase base para proyectiles. Define métodos para inicializar y lanzar proyectiles.

- **`PhysicsProjectile.cs`**:
  - Proyectil basado en física que utiliza un `Rigidbody` para moverse.

- **`RaycastProjectile.cs`**:
  - Proyectil que utiliza un raycast para detectar colisiones instantáneamente.

- **`PhisicsDamage.cs`**:
  - Implementa la interfaz `ITakeDamage` y aplica una fuerza física a los objetos al recibir daño.

### 4. Props <div id="props"/>

- **`PropBehavior.cs`**:
  - Define el comportamiento de los props.
  - Registra el daño recibido y utiliza pistas visuales y acústicas para ayudar al jugador a encontrarlos.

- **`PropSelector.cs`**:
  - Selecciona aleatoriamente un número de props del escenario y los configura con comportamientos adicionales.

### 5. Interactividad y tutorial <div id="interactividad-y-tutorial"/>

- **`TutorialBehavior.cs`**:
  - Gestiona la secuencia del tutorial. Incluye instrucciones en pantalla y reproducción de clips de audio para guiar al jugador.

### 6. Interfaces <div id="interfaces"/>

- **`ITakeDamage.cs`**:
  - Define una interfaz para objetos que pueden recibir daño. Es implementada por props y proyectiles.

---

## Flujo del juego <div id="flujo-del-juego"/>

1. **Inicio**:
   - El tutorial introduce al jugador al minijuego con instrucciones claras y guía auditiva.
   
2. **Selección de props**:
   - El `PropSelector` selecciona 5 props aleatoriamente del escenario.
   - Cada prop es configurado con pistas visuales y acústicas mediante `PropBehavior`.

3. **Interacción del jugador**:
   - El jugador usa un arma (pistola o rifle) para encontrar y destruir los props seleccionados.

4. **Progreso y finalización**:
   - El `GameManager` realiza un seguimiento del número de props destruidos.
   - Cuando todos los props son destruidos, se activa el evento `OnGameCompleted` para avanzar a la siguiente fase.

---

## Cómo jugar <div id="cómo-jugar"/>

1. Inicia el juego y sigue las instrucciones del tutorial.
2. Usa el controlador para:
   - Coger un arma.
   - Disparar a los props siguiendo las pistas.
3. Encuentra y destruye los 5 props seleccionados para completar el minijuego.