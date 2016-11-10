using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Composer : MonoBehaviour {

	public float textSpeed = 0.01f;

	private string text = "> Loading components..\n> Initializing server side applications\n> Connecting to database.\n> ...\n> Connected";
	private string currentText = "";

	private int currentChar = 0;

	private float nextChar = 0.0f;

	void Start () {
		GetComponent<Text> ().text = "";
	}

	void Update () {
		if (text != "" && (nextChar <= Time.time)) {
			
			int fullLength = text.Length;
			int currentLength = currentText.Length;

			if (currentLength < fullLength)
				this.currentText += text.ToCharArray () [currentLength];

			currentLength++;

			GetComponent<Text> ().text = this.currentText + (Mathf.Round(Time.time) % 2 == 0 ? " ▇" : "");

			nextChar = Time.time + textSpeed;
		}
	}

	public void fillText(string text){
		this.text = text;
	}

}
