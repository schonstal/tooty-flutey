using UnityEngine;
using System.Collections;

public class RecorderCalibrator : MonoBehaviour {
  GameObject xRotator;
  GameObject zRotator;
  RecorderInput xRecorder;
  RecorderInput zRecorder;

  void Start() {
    xRotator = GameObject.Find("X Rotator");
    zRotator = GameObject.Find("Z Rotator");
    xRecorder = xRotator.GetComponent<RecorderInput>();
    zRecorder = zRotator.GetComponent<RecorderInput>();
  }

  void Update() {
    if (!xRecorder.IsInitialized) {
      InitializeRecorder(xRecorder);
    } else if (!zRecorder.IsInitialized) {
      InitializeRecorder(zRecorder);
    }
  }

  void InitializeRecorder(RecorderInput recorder) {
    for (int i = 0; i < Microphone.devices.Length; i++) {
      if (Input.GetKeyDown(i.ToString())) {
        recorder.initialize(Microphone.devices[i]);
      }
    }
  }

  void OnGUI() {
    if (!xRecorder.IsInitialized) {
      RenderDevices("Select X Device");
    } else if (!zRecorder.IsInitialized && xRecorder.IsCalibrated) {
      RenderDevices("Select Z Device");
    }
  }

  void RenderDevices(string prompt) {
    GUI.Label(new Rect(0,0, Screen.width, Screen.height), prompt);
    for (int i = 0; i < Microphone.devices.Length; i++) {
      GUI.Label(
        new Rect(0, 20 * (i+1), Screen.width, Screen.height),
        string.Format("[{0}] {1}", i, Microphone.devices[i])
      );
    }
  }
}
