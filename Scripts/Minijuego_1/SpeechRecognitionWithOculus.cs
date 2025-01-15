using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.IO; /// Para usar MemoryStream

namespace HuggingFace.API.Examples {
  /// Clase que gestiona el reconocimiento de voz en un entorno VR con Oculus,
  /// activando un portal al decir una palabra específica ("portal").
  public class SpeechRecognitionWithOculus: MonoBehaviour {
    /// Referencia al portal que se activará al reconocer la palabra correcta.
    [SerializeField] private GameObject portal;

    /// Palabra que activa el portal al ser reconocida.
    [SerializeField] private string activationWord = "portal";

    private AudioClip clip; /// Clip de audio que almacena la grabación del micrófono.
    private byte[] bytes; /// Almacena los datos codificados como WAV.
    private bool recording; /// Indica si el micrófono está grabando.

    /// Acciones para iniciar y detener la grabación desde el controlador de Oculus.
    public InputActionReference startRecordingAction;
    public InputActionReference stopRecordingAction;

    /// Mensajes de error o éxito, y referencia al texto para mostrar lo que se reconoció.
    public GameObject errorObject;
    [SerializeField] private TMP_Text recognizedText;

    private void Awake() {
      /// Habilita las acciones de entrada para grabar.
      startRecordingAction.action.Enable();
      stopRecordingAction.action.Enable();

      /// Asocia las acciones de entrada con sus métodos correspondientes.
      startRecordingAction.action.performed += ctx => StartRecording();
      stopRecordingAction.action.performed += ctx => StopRecording();
    }

    private void Update() {
      /// Si está grabando, detiene automáticamente al alcanzar el límite de muestras del clip.
      if (recording && Microphone.GetPosition(null) >= clip.samples) {
        StopRecording();
      }
    }

    /// Inicia la grabación de audio usando el micrófono.
    private void StartRecording() {
      Debug.Log("Has presionado el botón de grabación.");
      recognizedText.text = "¿Quieres hablarnos, mortal?";
      if (recording) return; /// No permite iniciar otra grabación si ya está grabando.

      Debug.Log("Recording...");
      /// Inicia la grabación del micrófono, con duración máxima de 10 segundos y frecuencia de 44.1 kHz.
      clip = Microphone.Start(null, false, 10, 44100);
      recording = true;
    }

    /// Detiene la grabación de audio y procesa los datos para enviarlos a la API.
    private void StopRecording() {
      Debug.Log("Has presionado el botón de detener grabación.");
      if (!recording) return; /// No hace nada si no se estaba grabando.

      var position = Microphone.GetPosition(null); /// Obtiene la posición actual del micrófono.
      Microphone.End(null); /// Detiene el micrófono.
      if (position <= 0) { /// Valida si el micrófono capturó algún dato.
        Debug.LogWarning("Posición del micrófono no válida: " + position);
        recognizedText.text = "Tu voz no está funcionando correctamente, mortal.";
        return;
      }

      /// Extrae las muestras de audio del clip.
      var samples = new float[position * clip.channels];
      recognizedText.text = "Antes de getData";
      clip.GetData(samples, 0);
      recognizedText.text = "Después de getData";

      /// Convierte las muestras en formato WAV.
      bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
      recognizedText.text = "Después de encode";

      recording = false; /// Marca que la grabación ha terminado.
      SendRecording(); /// Envía la grabación a la API.
    }

    /// Envía la grabación a la API de Hugging Face para realizar el reconocimiento de voz.
    private void SendRecording() {
        Debug.Log("Sending...");
        recognizedText.text = "Gracias por intentar hablar con nosotros, mortal. Ahora escucharemos lo que has dicho.";

        /// Llama a la API para el reconocimiento de voz.
        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response => {
          Debug.Log("Dijiste: " + response);
          errorObject.SetActive(false);

          /// Activa el portal si se detecta la palabra correcta.
          if (response != null && response.ToLower().Contains(activationWord.ToLower())) {
            recognizedText.text = "Hemos escuchado algo: " + response;
            Debug.Log("¡Palabra correcta! El portal se ha activado.");
            ActivatePortal();
          } else {
            recognizedText.text = "No has dicho la palabra correcta, mortal.";
            Debug.Log("Palabra incorrecta. Intenta de nuevo.");
          }

        }, error => {
          /// Manejo de errores en caso de fallo en la API.
          recognizedText.text = "No te podemos escuchar ahora: " + error;
          errorObject.SetActive(true);
        });

        /// Mensaje por defecto si no hay respuesta de la API.
        if (recognizedText.text == "Gracias por intentar hablar con nosotros, mortal. Ahora escucharemos lo que has dicho.") {
          recognizedText.text = "Nuestra magia para escuchar no ha funcionado.";
        }
    }

    /// Activa el portal si la palabra correcta ha sido reconocida.
    private void ActivatePortal() {
      if (portal != null) {
        portal.SetActive(true); /// Activa el GameObject del portal.
      }
    }

    /// Convierte las muestras de audio en formato WAV.
    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels) {
      using (var memoryStream = new MemoryStream(44 + samples.Length * 2)) {
        using (var writer = new BinaryWriter(memoryStream)) {
          /// Cabecera del archivo WAV.
          writer.Write("RIFF".ToCharArray());
          writer.Write(36 + samples.Length * 2);
          writer.Write("WAVE".ToCharArray());
          writer.Write("fmt ".ToCharArray());
          writer.Write(16);
          writer.Write((ushort)1); 
          writer.Write((ushort)channels); 
          writer.Write(frequency); 
          writer.Write(frequency * channels * 2);
          writer.Write((ushort)(channels * 2));
          writer.Write((ushort)16);
          writer.Write("data".ToCharArray());
          writer.Write(samples.Length * 2);

          /// Datos de las muestras.
          foreach (var sample in samples) {
            writer.Write((short)(sample * short.MaxValue));
          }
        }
        return memoryStream.ToArray(); /// Devuelve el archivo WAV como byte array.
      }
    }
  }
}
