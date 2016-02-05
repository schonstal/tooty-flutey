using UnityEngine;
using System.Collections;

public class RecorderTilt : MonoBehaviour {
  GameObject xRotator;
  GameObject zRotator;

  RecorderInput xRecorder;
  RecorderInput zRecorder;

  Vector3 rotationTarget = new Vector3(0, 0, 0);

  bool debugMode = false;

  float startTime = 0f;

  void Start() {
    xRotator = GameObject.Find("X Rotator");
    zRotator = GameObject.Find("Z Rotator");
    xRecorder = xRotator.GetComponent<RecorderInput>();
    zRecorder = zRotator.GetComponent<RecorderInput>();
  }

  void Update() {
    startTime += Time.deltaTime;

    if (Input.GetKeyDown("d")) debugMode = !debugMode;
    if(!debugMode) {
      if (!(xRecorder.IsCalibrated && zRecorder.IsCalibrated)) return;
    }

    if(startTime < 1.75) return;

    var xRotation = 0;
    if (xRecorder.lowTriggered() || Input.GetKey("up")) {
      xRotation = 20;
    } else if (xRecorder.highTriggered() || Input.GetKey("down")) {
      xRotation = -20;
    } else {
      xRotation = 0;
    }

    var zRotation = 0;
    if (zRecorder.lowTriggered() || Input.GetKey("left")) {
      zRotation = 20;
    } else if (zRecorder.highTriggered() || Input.GetKey("right")) {
      zRotation = -20;
    } else {
      zRotation = 0;
    }

    rotationTarget = new Vector3(xRotation, 0, zRotation);

    Quaternion target = Quaternion.Euler(xRotation, 0, zRotation);
    transform.rotation = Quaternion.Lerp(transform.rotation, target, 0.1f);
  }
}
