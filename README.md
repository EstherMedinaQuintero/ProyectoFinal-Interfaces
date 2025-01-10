# ProyectoFinal-Interfaces

## **Introducción**

Este proyecto corresponde al prototipo final de un juego de RV desarrollado como parte de la asignatura. El juego integra técnicas abordadas en las diferentes prácticas realizadas durante el módulo, así como recomendaciones de diseño para aplicaciones de RV y el uso de interfaces multimodales. A continuación, se detalla toda la información relevante para el uso, desarrollo y evaluación del proyecto.

---

## **Cuestiones importantes para el uso**

TODO: Son ejemplos

1. **Requisitos del sistema:**
   - Dispositivo compatible con RV (Oculus Quest 2, HTC Vive, etc.).
   - PC con Windows 10 o superior y GPU con soporte para gráficos de alta calidad.
   - Unity versión `2021.3.x LTS`.
2. **Instalación:**
   - Descargue el archivo `apk` incluido en esta entrega y transfiéralo al dispositivo RV.
   - Ejecute el juego desde el visor tras instalar el archivo `apk`.
3. **Controles:**
   - Movimiento: Joystick izquierdo.
   - Interacción: Botón de acción (Trigger derecho).
   - Menú: Botón secundario (Botón B).

---

## **Hitos de programación logrados**
### **Relación con los contenidos impartidos**

1. **Control de movimiento basado en RV:**
   - Implementación de locomoción suave y teletransporte. Esta técnica fue trabajada en las prácticas de control de movimiento en entornos RV.
2. **Interacción con objetos:**
   - Uso de raycasting para seleccionar e interactuar con objetos del escenario. Relacionado con la práctica de sistemas de interacción.
3. **Integración de interfaces multimodales:**
   - Uso de entrada por voz para activar comandos en el juego, basada en los ejercicios realizados con sensores y micrófonos.
4. **Diseño del entorno:**
   - Creación de un entorno inmersivo que sigue las recomendaciones de diseño para minimizar el mareo en aplicaciones RV, aplicadas en la práctica de diseño de entornos.

---

## **Aspectos destacados de la aplicación**
- **Inmersión total:**
  El diseño del entorno aprovecha técnicas de iluminación y texturizado realista para aumentar la sensación de presencia.
- **Interactividad multimodal:**
  Combinación de entrada por controladores y comandos de voz para una experiencia intuitiva.
- **Optimización:**
  Uso de técnicas de LOD (Level of Detail) para garantizar un rendimiento fluido incluso en hardware de gama media.

---

## **Integración de sensores**
Se incluye la funcionalidad de **reconocimiento de voz**, basada en las prácticas de interfaces multimodales. El sistema permite al usuario activar habilidades especiales mediante comandos hablados.

---

## **Visualización**
![Gameplay GIF](./gameplay.gif)
_Un breve ejemplo de ejecución del juego._

---

## **Acuerdos del grupo**
**Reparto de tareas:**
1. **Diseño del escenario:**
   - Responsable: Juan Pérez.
   - Desarrollado en equipo durante las primeras sesiones.
2. **Programación de interacciones:**
   - Responsable: María López.
   - Incluye raycasting y detección de colisiones.
3. **Integración de sensores:**
   - Responsable: Carlos García.
   - Desarrollo del módulo de reconocimiento de voz.
4. **Documentación:**
   - Responsable: Ana Sánchez.
   - Creación de este documento y preparación del repositorio.

**Tareas grupales:**
- Pruebas de usuario y ajuste de jugabilidad.
- Revisión del código para garantizar calidad y cumplimiento de estándares.

---

## **Archivos incluidos**
- **Repositorio GitHub:** Enlace: [GitHub Repository](https://github.com/equipo/rv-game-prototype).
- **Paquete Unity:** Archivo comprimido con el proyecto completo (`UnityPackage.zip`).
- **Repositorio comprimido:** Archivo ZIP que incluye los scripts y el `README.md`.
- **APK:** Archivo ejecutable (`rv-game.apk`).
- **Acta del grupo:** Documento PDF con los acuerdos del equipo.

---

### **Contacto**
Para cualquier duda o sugerencia, contactar con el equipo en `equipo.rv@gmail.com`.
