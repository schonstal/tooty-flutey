using UnityEngine;
using System.Collections;

public class RecorderInput : MonoBehaviour {
  static int SAMPLES = 512;
  float[] spectrum = new float[SAMPLES];
  GameObject[] cubes = new GameObject[SAMPLES];
  AudioSource audioSource;

  void Start() {
    audioSource = GetComponent<AudioSource>();
    audioSource.clip = Microphone.Start("Princess", true, 1, 44100);
    audioSource.loop = true;
    while(!(Microphone.GetPosition("Princess") > 0)) {}
    audioSource.Play();

    for(int i = 0; i < spectrum.Length-1; i++) {
      cubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
      cubes[i].transform.position = new Vector3(i, 1f, 0);
    }
  }

  void Update() {
    audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
    for(int i = 0; i < spectrum.Length-1; i++) {
      cubes[i].transform.localScale = new Vector3(1f, spectrum[i] * 100, 1);
    }
  }
}
