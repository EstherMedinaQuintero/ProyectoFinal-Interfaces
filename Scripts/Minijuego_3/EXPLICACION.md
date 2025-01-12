# Minijuego Teleport Parkour

Este proyecto implementa un minijuego interactivo  "Teleport Parkour" donde el jugador debe lanzar perlas de teletransportación para moverse a través del escenario y alcanzar puntos de control. Este documento explica el funcionamiento del juego y describe el propósito de cada script.

---

## Índice

1. [Funcionalidad del juego](#funcionalidad-del-juego)
2. [Estructura de scripts](#estructura-de-scripts)
    - [Gestión del juego](#gestión-del-juego)
    - [Teletransportación](#teletransportación)
    - [Interactividad y tutorial](#interactividad-y-tutorial)
    - [Antigua jugabilidad](#antigua_jugabilidad)
3. [Flujo del juego](#flujo-del-juego)
4. [Cómo jugar](#cómo-jugar)

---

## Funcionalidad del juego <div id="funcionalidad-del-juego"/>

1. **Tutorial interactivo**:
   - El jugador recibe instrucciones paso a paso para aprender a jugar. Esto incluye:
     - Crear perla de teletransportación.
     - Teletransportarse.
     - Indicación de meta.

2. **Sistema de teletransportación**:
   - El jugador se mueve lanzando perlas que al impactar con una superficie lo teletransportan a ese punto.
   - Hay distintos tipos de checkpoints.

3. **Sistema de progreso**:
   - El juego realiza un seguimiento de los checkpoints alcanzados y finaliza el minijuego cuando el jugador llega a la meta.

---

## Estructura de scripts <div id="estructura-de-scripts"/>

### 1. Gestión del juego <div id="gestión-del-juego"/>

- **`CheckPointManager.cs`**:
  - Gestiona el sistema de checkpoints.
  - Registra el progreso del jugador y activa los puntos de control alcanzados.

- **`DeactivateParticleAndWin.cs`**:
  - Se activa cuando el jugador alcanza el último punto del nivel.
  - Desactiva efectos visuales y muestra un mensaje de victoria.

### 2. Teletransportación <div id="teletransportación"/>

- **`PearlTeleporter.cs`**:
  - Controla la lógica de teletransportación de la perla.
  - Detecta el punto de impacto y mueve al jugador a esa posición.
  - Detecta la colisión con algo que no sea el juego y la destruye

- **`EnderPearlVR.cs`**:
  - Implementa la interacción en VR para lanzar la perla.
  - Gestiona la fuerza del lanzamiento de la perla en el escenario.
  - Implementa la aparición de la perla

- **`CheckpointTeleporter.cs`**:
  - Permite la activación de checkpoints al teletransportarse a ciertas ubicaciones.
  - Se encarga de validar que el jugador sigue el camino correcto.
  - Activa el movimiento con el mando de una zona

- **`temporalcheckpointTeleporter.cs`**:
  - Funciona como un checkpoint temporal.
  - Permite al jugador moverse con la teletransportación del mando en una zona.

### 3. Interactividad y tutorial <div id="interactividad-y-tutorial"/>

- **`StartGame.cs`**:
  - Gestiona la secuencia inicial.
  - Explica al jugador cómo usar la teletransportación y progresar en el nivel.
### 4. Antigua jugabilidad <div id="antigua_jugabilidad"/>
 Anteriormente, la mecánica de este nivel era impulsarse con materiales metálicos y atraerlos (basado en los poderes de los libros de Mistborn). Con esta mecánica se iba a poder hacer el plataformeo. Se descartó esta mecánica porque podría causar mareo al impulsar al jugador
- **`Alomancia.cs`**:
  - Implementa la atracción de metales y el empuje, dependiendo de los pesos del objeto y el jugador, tendría un efecto u otro
- **`DestacarMetales.cs`**:
  - Para sabe con qué se podía interactuar este script cambiaba el material de los objetos metálicos de un área
---

## Flujo del juego <div id="flujo-del-juego"/>

1. **Inicio**:
   - El tutorial introduce al jugador a la mecánica de teletransportación con instrucciones claras y guía auditiva.
   
2. **Uso de la perla de teletransportación**:
   - El jugador lanza la perla hacia una superficie.
   - Al impactar, el jugador es teletransportado a ese punto.
   
3. **Progreso por los checkpoints**:
   - Cada checkpoint alcanzado es registrado.
   - El jugador reaparece en el último checkpoint si falla un salto.

4. **Finalización del nivel**:
   - Al llegar al último punto, `DeactivateParticleAndWin` marca el final del minijuego.

---

## Cómo jugar <div id="cómo-jugar"/>

1. Inicia el juego y sigue las instrucciones del tutorial.
2. Usa el controlador para:
   - Lanzar una perla de teletransportación.
   - Teletransportarte a plataformas y checkpoints.
3. Llega al último checkpoint para completar el minijuego.
