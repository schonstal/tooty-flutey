using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
  GameObject xRotator;
  GameObject zRotator;

  RecorderInput xRecorder;
  RecorderInput zRecorder;

  GameObject spawner;

  Vector3 originalPosition;

  LevelManager levelManager;

  Rigidbody rigidbody;

  void Awake() {
    xRotator = GameObject.Find("X Rotator");
    zRotator = GameObject.Find("Z Rotator");
    xRecorder = xRotator.GetComponent<RecorderInput>();
    zRecorder = zRotator.GetComponent<RecorderInput>();

    spawner = new GameObject();
    spawner.transform.position = transform.position;
    spawner.transform.parent = transform.parent;

    levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

    rigidbody = GetComponent<Rigidbody>();
	}

	void Update() {
    if (transform.position.y < -10) {
      transform.position = new Vector3(
        spawner.transform.position.x,
        spawner.transform.position.y,
        spawner.transform.position.z
      );

      rigidbody.velocity = Vector3.zero;
      rigidbody.angularVelocity = Vector3.zero;
    } 
	}

  void OnCollisionEnter(Collision collision) {
  }
}
