using UnityEngine;
using System.Collections;

public class RecorderTilt : MonoBehaviour {
  public GameObject xRotator;
  public GameObject zRotator;

  RecorderInput xRecorder;
  RecorderInput zRecorder;

  void Start() {
    xRecorder = xRotator.GetComponent<RecorderInput>();
    zRecorder = zRotator.GetComponent<RecorderInput>();
  }

  void Update() {
    if (!xRecorder.IsInitialized) {
      for (int i = 0; i < Microphone.devices.Length - 1; i++) {
        if (Input.GetKeyDown(i.ToString())) {
          xRecorder.initialize(Microphone.devices[i]);
        }
      }
      return;
    } else if (!zRecorder.IsInitialized) {
      for (int i = 0; i < Microphone.devices.Length - 1; i++) {
        if (Input.GetKeyDown(i.ToString())) {
          zRecorder.initialize(Microphone.devices[i]);
        }
      }
      return;
    }

    var xRotation = 0;
    if (xRecorder.lowTriggered()) {
      xRotation = 10;
    } else if (xRecorder.highTriggered()) {
      xRotation = -10;
    }

    var zRotation = 0;
    if (zRecorder.lowTriggered()) {
      zRotation = 10;
    } else if (zRecorder.highTriggered()) {
      zRotation = -10;
    }
    
    transform.eulerAngles = new Vector3(xRotation, 0, zRotation);
  }

  void OnGUI() {
    if (!xRecorder.IsInitialized) {
      GUI.Label(new Rect(0,0, Screen.width, Screen.height), "Select X Device (press key)");
      for (int i = 0; i < Microphone.devices.Length - 1; i++) {
        GUI.Label(new Rect(0,20 * (i+1), Screen.width, Screen.height), string.Format("[{0}] {1}", i, Microphone.devices[i]));
      }
      return;
    } else if (!zRecorder.IsInitialized) {
      GUI.Label(new Rect(0,0, Screen.width, Screen.height), "Select Z Device (press key)");
      for (int i = 0; i < Microphone.devices.Length - 1; i++) {
        GUI.Label(new Rect(0,20 * (i+1), Screen.width, Screen.height), string.Format("[{0}] {1}", i, Microphone.devices[i]));
      }
      return;
    }
  }
}
