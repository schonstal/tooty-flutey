using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
  public Transform[] levels;

  int levelIndex = 0;
  Transform currentLevel;

  void Start() {
    startLevel();
  }

  void Update() {
    if (Input.GetKeyDown("space")) nextLevel();
  }

  public void nextLevel() {
    if (levelIndex < levels.Length - 1) levelIndex++;
    startLevel();
  }

  public void startLevel() {
    if (currentLevel != null) GameObject.Destroy(currentLevel.gameObject);
    currentLevel = Instantiate(levels[levelIndex]) as Transform;
  }
}
