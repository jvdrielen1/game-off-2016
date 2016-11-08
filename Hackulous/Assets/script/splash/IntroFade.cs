using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroFade : MonoBehaviour {

	private Rect centerRect;
	private Rect centerBottomRect;

	public Texture2D image;
	public GUIStyle style;

	public string text;
	public float typeRate;

	private string currentText = "";
	private float lastType = 0;

	private float imageAlpha = 0.0f;
	private Image img;

	// Use this for initialization
	void Start () {
		centerRect = new Rect (new Vector2 (0, 0), new Vector2 (Screen.width, Screen.height));
		centerBottomRect = new Rect (new Vector2 (0, 0), new Vector2 (Screen.width, Screen.height));
	}

	void Update(){
		if (lastType < Time.time) {
			lastType += typeRate;

			int lFull = text.Length;
			int lCurrent = currentText.Length;

			if (lCurrent < lFull)
				currentText += text.ToCharArray()[lCurrent];
		}

		imageAlpha += 0.01f;
	}
	
	void OnGUI(){
		Color color = GUI.color;
		color.a = imageAlpha;
		GUI.color = color;
		GUI.DrawTexture (centerRect, image, ScaleMode.StretchToFill);

		color.a = 1.0f;
		GUI.color = color;

		GUI.Label (centerBottomRect, currentText, style);
	}
}
