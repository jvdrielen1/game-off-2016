using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

	private Rect rect;
	private float alpha = 0.0f;
	private float end = 0.0f;

	public float fadeSpeed = 0.1f;
	public Texture2D mat;

	void Start(){
		rect = new Rect (new Vector2(0, 0), new Vector2(Screen.width, Screen.height));
	}

	public void openScene(string scene){
		try {
			fade();
			SceneManager.LoadScene (scene);
		} catch (System.Exception e){
			Debug.Log ("Scene \"" + scene + "\" not found.");
		}
	}

	void Update(){
		if (end == -1f)
			return;
		
		if (end == 0.0f && alpha > 0.0f) {
			alpha -= fadeSpeed;
		} else if (end == 1.0f && alpha < 1.0f) {
			alpha += fadeSpeed;
		}
	}

	void OnGUI(){
		if (mat != null) {
			Color color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);

			GUI.color = color;
			GUI.DrawTexture (rect, mat);
		}
	}

	void OnLevelWasLoaded(){
		alpha = 1.0f;
		end = 0.0f;
	}

	IEnumerator fade(){
		alpha = 0.0f;
		end = 1.0f;
		yield return new WaitForSeconds (fadeSpeed);
	}

}
