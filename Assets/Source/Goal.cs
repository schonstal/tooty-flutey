using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
  void Start() {
  }

  void Update() {
    transform.Rotate(new Vector3(1.5f, 1, 0.5f));
  }
}
