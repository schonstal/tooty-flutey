using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
  LevelManager levelManager;

  void Start() {
    levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
  }

  void Update() {
    transform.Rotate(new Vector3(1.5f, 1, 0.5f));
  }

  void OnCollisionEnter(Collision collision) {
    if (collision.collider.isTrigger) levelManager.nextLevel();
  }
}
