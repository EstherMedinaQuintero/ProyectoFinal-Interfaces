
# Home de Juegos VR

Este proyecto gestiona la transición entre escenas y la interacción con diferentes juegos en un entorno de realidad virtual. Además, rastrea el progreso del jugador, mostrando visualmente qué niveles han sido completados mediante figuras encima de los portales correspondientes.

---

## Índice

1. [Funcionalidad del juego](#funcionalidad-del-juego)  
2. [Estructura de scripts](#estructura-de-scripts)  
   1. [Gestión del juego](#gestión-del-juego)  
   2. [Interactividad y Tutorial](#interactividad-y-tutorial)  
3. [Flujo del juego](#flujo-del-juego)  
4. [Cómo jugar](#cómo-jugar)  

---

## Funcionalidad del juego <div id="funcionalidad-del-juego"/>

### Características principales:

1. **Tutorial interactivo**  
   - Introduce al jugador a las mecánicas principales, como el teletransporte, mediante instrucciones paso a paso y guía auditiva.

2. **Sistema de progreso**  
   - Los niveles completados son marcados con una figura visual sobre el portal correspondiente.  
   - Los portales completados se cierran automáticamente, guiando al jugador hacia el siguiente desafío.

---

## Estructura de scripts <div id="estructura-de-scripts"/>

### 1. Gestión del juego <div id="gestión-del-juego"/>

- **`ManagerTransition.cs`**  
  - Gestiona las transiciones entre escenas con animaciones de **fade in** y **fade out**.  
  - Almacena el progreso del jugador, activando o desactivando portales según el estado del nivel.

- **`ManagerRewards.cs`**  
  - Controla las recompensas al finalizar un nivel.  
  - Verifica el progreso accediendo al estado de los niveles desde `ManagerTransition`.  
  - Maneja las animaciones relacionadas con las recompensas.

- **`PortalManager.cs`**  
  - Activa y desactiva los portales en función del progreso del jugador.  

### 2. Interactividad y Tutorial <div id="interactividad-y-tutorial"/>

- **`ManagerActivateAudio.cs`**  
  - Controla la reproducción de la historia del juego al acceder a los niveles.  
  - Interactúa con `ManagerTransition` para verificar si el jugador ha escuchado la historia.

---

## Flujo del juego <div id="flujo-del-juego"/>

1. **Inicio del tutorial**  
   - El jugador aprende a teletransportarse mediante instrucciones claras y soporte auditivo.

2. **Desarrollo de la historia**  
   - Al entrar en un nivel, se activa la narración para sumergir al jugador en la historia.

3. **Progreso visual**  
   - Al completar un nivel, se otorga una recompensa, y el portal correspondiente se desactiva para indicar el avance.

4. **Finalización del nivel**  
   - El progreso del jugador queda registrado, desbloqueando nuevos desafíos.

---

## Cómo jugar <div id="cómo-jugar"/>

1. **Moverse**  
   - Usa el gatillo para teletransportarte entre ubicaciones.  

2. **Explorar la historia**  
   - Escucha la narrativa del juego al entrar en cada nivel.  

3. **Seleccionar niveles**  
   - Elige un portal para acceder a un nivel disponible.  

4. **Completar niveles**  
   - Finaliza el nivel y recibe una recompensa visual.  

---
