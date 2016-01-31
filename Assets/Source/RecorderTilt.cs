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
}
