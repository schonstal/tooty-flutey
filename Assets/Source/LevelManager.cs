using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
  public Font font;
  public Transform[] levels;

  int levelIndex = 0;
  Transform currentLevel;

  bool finished = false;

  GUIStyle style;

  void Start() {
    startLevel();
  }

  void Update() {
    if (Input.GetKeyDown("q")) nextLevel();
  }

  public void nextLevel() {
    if (levelIndex < levels.Length - 1)  {
      levelIndex++;
      startLevel();
    } else {
      finished = true;
      destroyLevel();
    }
  }

  public void startLevel() {
    if (currentLevel != null) destroyLevel();
    currentLevel = Instantiate(levels[levelIndex]) as Transform;
  }

  void destroyLevel() {
    GameObject.Destroy(currentLevel.gameObject);
  }

  void OnGUI() {
    if (!finished) return;

    style = new GUIStyle(GUI.skin.GetStyle("label"));
    style.fontSize = 100;
    style.font = font;
    style.alignment = TextAnchor.UpperCenter;

    style.normal.textColor = Color.white;

    GUI.Label(
      new Rect(15, Screen.height/2 - 50, Screen.width, Screen.height),
      "FINISH",
      style
    );
  }
}
