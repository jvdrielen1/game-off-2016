using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Composer : MonoBehaviour {

	public float textSpeed = 0.01f;

	private string text = "> Loading components..\n> Initializing server side applications\n> Connecting to database.\n> ...\n> Connected\n\n";
	private string currentText = "";

	private int currentChar = 0;

	private float nextChar = 0.0f;

	private Dictionary<string, CommandExecutor> executor = new Dictionary<string, CommandExecutor> ();

	void Start () {
		GetComponent<Text> ().text = "";

		registerCommand ("help", new HelpCommand());
	}

	void Update () {
		if (text != "" && (nextChar <= Time.time)) {
			
			int fullLength = text.Length;
			int currentLength = currentText.Length;

			if (currentLength < fullLength)
				this.currentText += text.ToCharArray () [currentLength];

			currentLength++;

			GetComponent<Text> ().text = this.currentText + (Mathf.Round(Time.time) % 2 == 0 ? "▇" : "");

			nextChar = Time.time + textSpeed;
		}

		detectPressedKeyOrButton ();
	}

	public void fillText(string text){
		this.text = text;
	}

	public void registerCommand(string command, CommandExecutor exec){
		executor[command] = exec;
	}

	List<string> filter = new List<string> (){
		"mouse0","mouse1", "leftshift", "rightshift"
	};

	public void detectPressedKeyOrButton() {
		foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyUp (kcode)) {
				//Debug.Log ("KeyCode down: " + kcode);
				Type (kcode.ToString());
				GetComponent<Text> ().text = this.currentText + (Mathf.Round(Time.time) % 2 == 0 ? "▇" : "");
			}
		}
	}

	void Type(String s){
		s = s.ToLower ();

		if (s == "return") {
			string[] sentences = currentText.Split(new string[]{"\n"}, StringSplitOptions.None);
			string command = sentences [sentences.Length-1];

			if (command != "") {
				ExecuteCommand (command);
			}

			currentText += "\n";
			CalculateLines ();
			return;
		}

		if (s == "space") {
			currentText += " ";
			return;
		}

		if (s == "backspace") {
			currentText = currentText.Substring (0, currentText.Length - 1);
			return;
		}

		if (filter.Contains (s)) {
			return;
		}

		currentText += s;
	}

	void ExecuteCommand(string cmd){
		string[] args = cmd.Split(new string[]{" "}, StringSplitOptions.None);
		if (executor.ContainsKey (args[0])) {
			executor [args [0]].Execute (args[0], args);
		} else {
			currentText += "\n> Command not found";
		}
	}

	public void PrintLn(string msg){
		currentText += "\n" + msg;
		GetComponent<Text> ().text = this.currentText + (Mathf.Round(Time.time) % 2 == 0 ? "▇" : "");
	}

	public void CalculateLines(){
		// Max 26 lines
		string[] sentences = currentText.Split(new string[]{"\n"}, StringSplitOptions.None);
		List<string> sentenceList = new List<string> (sentences);

		bool needUpdate = false;

		while (sentenceList.Count > 26){
			sentenceList.Remove(sentenceList[0]);
			needUpdate = true;
		}

		if (needUpdate) {
			currentText = "";
			foreach (string sentence in sentenceList) {
				currentText += sentence + (sentenceList[sentenceList.Count-1] == sentence ? "" : "\n");
			}
			GetComponent<Text> ().text = this.currentText + (Mathf.Round (Time.time) % 2 == 0 ? "▇" : "");
		}

		Debug.Log (sentenceList.Count);
	}

	public String getCurrentText(){
		return this.currentText;
	}

	public void setCurrentText(string text){
		this.currentText = text;
		GetComponent<Text> ().text = this.currentText + (Mathf.Round (Time.time) % 2 == 0 ? "▇" : "");
	}

}
