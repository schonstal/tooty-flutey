using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
  GameObject xRotator;
  GameObject zRotator;

  RecorderInput xRecorder;
  RecorderInput zRecorder;

  GameObject spawner;

  Vector3 originalPosition;

  void Start() {
    xRotator = GameObject.Find("X Rotator");
    zRotator = GameObject.Find("Z Rotator");
    xRecorder = xRotator.GetComponent<RecorderInput>();
    zRecorder = zRotator.GetComponent<RecorderInput>();

    spawner = GameObject.Find("Spawner");
  }

	void Awake() {
    originalPosition = new Vector3(
      transform.position.x,
      transform.position.y,
      transform.position.z
    );
	}

	void Update() {
    if (transform.position.y < -10) {
      transform.position = new Vector3(
        spawner.transform.position.x,
        spawner.transform.position.y,
        spawner.transform.position.z
      );

      GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
    } 
	}

  void OnCollisionEnter(Collision collision) {
  }
}
