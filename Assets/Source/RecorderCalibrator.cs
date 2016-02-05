using UnityEngine;
using System.Collections;

public class RecorderCalibrator : MonoBehaviour {
  public Font font;

  GameObject xRotator;
  GameObject zRotator;
  RecorderInput xRecorder;
  RecorderInput zRecorder;

  GUIStyle style;

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
      RenderDevices("SELECT X DEVICE:");
    } else if (!xRecorder.IsCalibrated) {
      CalibrateNotes(xRecorder);
    } else if (!zRecorder.IsInitialized) {
      RenderDevices("SELECT Z DEVICE:");
    } else if (!zRecorder.IsCalibrated) {
      CalibrateNotes(zRecorder);
    }
  }

  void CalibrateNotes(RecorderInput recorder) {
    style = new GUIStyle(GUI.skin.GetStyle("label"));
    style.fontSize = 100;
    style.font = font;

    style.normal.textColor = Color.white;

    GUI.Label(
      new Rect(15, 0, Screen.width, Screen.height),
      string.Format("HOLD NOTE {0}", recorder.HasHighNote ? "TWO" : "ONE"),
      style
    );

    GUI.Label(new Rect(0, 100, Screen.width, Screen.height), "[SPACE] LOCK-IN", style);
  }

  void RenderDevices(string prompt) {
    style = new GUIStyle(GUI.skin.GetStyle("label"));
    style.fontSize = 100;
    style.font = font;

    style.normal.textColor = Color.white;

    GUI.Label(new Rect(15, 0, Screen.width, Screen.height), prompt, style);
    for (int i = 0; i < Microphone.devices.Length; i++) {
      GUI.Label(
        new Rect(0, 100 * (i+1), Screen.width, Screen.height),
        string.Format("[{0}] {1}", i, Microphone.devices[i].ToUpper()),
        style
      );
    }
  }
}
