using UnityEngine;
using System.Collections;
using System.Linq;

public class RecorderInput : MonoBehaviour {
  public static int SAMPLES = 8192;
  public static int SAMPLE_RATE = 44100;

  public string deviceName = "Princess";
  public float highNote = 1661.22f;
  public float lowNote = 1244.51f;
  public float tolerance = 100f;

  float[] spectrum = new float[SAMPLES];
  AudioSource audioSource;
  float frequency = 0.0f;

  void Start() {
    var iteration = 0;
    if(!Microphone.devices.Contains(deviceName)) return;
    audioSource = GetComponent<AudioSource>();
    audioSource.clip = Microphone.Start(deviceName, true, 1, SAMPLE_RATE);
    audioSource.loop = true;
    while(!(Microphone.GetPosition(deviceName) > 0)) { iteration++; if(iteration > 10000) return;}
    audioSource.Play();
  }

  void Update() {
    frequency = GetFrequency();
  }

  float GetFrequency() {
    float frequency = 0.0f;
    audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
    float max = 0.0f;
    int index = 0;
    for (int i = 1; i < SAMPLES; i++) {
      if (max < spectrum[i-1]) {
        max = spectrum[i-1];
        index = i;
      }
    }
    frequency = index * SAMPLE_RATE / SAMPLES;
    return frequency;
  }

  bool noteTriggered(float note) {
    return frequency > note - tolerance && frequency < note + tolerance;
  }

  public bool lowTriggered() {
    return noteTriggered(highNote);
  }

  public bool highTriggered() {
    return noteTriggered(lowNote);
  }

  void OnGUI() {
    GUI.Label(new Rect(0,0, Screen.width, Screen.height), "Frequency: " + frequency + "Hz");
    GUI.Label(new Rect(0,20, Screen.width, Screen.height), "High: " + highTriggered());
    GUI.Label(new Rect(0,40, Screen.width, Screen.height), "Low: " + lowTriggered());
  }
}
