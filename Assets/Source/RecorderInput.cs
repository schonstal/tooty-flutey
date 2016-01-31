using UnityEngine;
using System.Collections;
using System.Linq;

public class RecorderInput : MonoBehaviour {
  public static int SAMPLES = 8192;
  public static int SAMPLE_RATE = 44100;

  public float highNote = 1661.22f;
  public float lowNote = 1244.51f;
  public float tolerance = 100f;

  float maxFrequency = 0.0f;

  public bool IsInitialized {
    get {
      return initialized;
    }
  }

  bool initialized = false;
  string deviceName = "Princess";

  float[] spectrum = new float[SAMPLES];
  AudioSource audioSource;
  float frequency = 0.0f;

  void Start() {
  }

  void Update() {
    if (!initialized) return;
    frequency = GetFrequency();
  }

  float GetFrequency() {
    float frequency = 0.0f;
    audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
    int index = 0;
    maxFrequency = 0.0f;
    for (int i = 1; i < SAMPLES; i++) {
      if (maxFrequency < spectrum[i-1]) {
        maxFrequency = spectrum[i-1];
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

  public void initialize(string device) {
    initialized = true;
    deviceName = device;
    var iteration = 0;
    if(!Microphone.devices.Contains(deviceName)) return;
    audioSource = GetComponent<AudioSource>();
    audioSource.clip = Microphone.Start(deviceName, true, 1, SAMPLE_RATE);
    audioSource.loop = true;
    while(!(Microphone.GetPosition(deviceName) > 0)) { iteration++; if(iteration > 10000) break;}
    audioSource.Play();
  }
}
