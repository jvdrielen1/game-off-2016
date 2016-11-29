using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Composer : MonoBehaviour {

	public float textSpeed = 0.01f;

	private bool canType = false;

	private string text = "> Loading components..\n> Initializing server side applications\n> Connecting to database.\n> ...\n> Connected\n\n";
	private string currentText = "";

	private int currentChar = 0;

	private float nextChar = 0.0f;

	private Dictionary<string, CommandExecutor> commandExec = new Dictionary<string, CommandExecutor> ();
	private Dictionary<string, KeyAction> keyActions = new Dictionary<string, KeyAction> ();

	void Start () {
		GetComponent<Text> ().text = "";

		// Register commands
		registerCommand ("help", new HelpCommand());
		registerCommand ("hack", new HackCommand());
		registerCommand ("stats", new StatisticsCommand());

		// Register key actions
		registerKeyAction ("backspace", new BackspaceAction());
		registerKeyAction ("return", new ReturnAction());
		registerKeyAction ("enter", new ReturnAction());
		registerKeyAction ("space", new SpaceAction());
	}

	void Update () {
		if (text != "" && (nextChar <= Time.time)) {
			
			int fullLength = text.Length;
			int currentLength = currentText.Length;

			if (currentLength < fullLength)
				this.currentText += text.ToCharArray () [currentLength];

			currentLength++;

			GetComponent<Text> ().text = this.currentText + (Mathf.Round (Time.time) % 2 == 0 ? "▇" : "");

			nextChar = Time.time + textSpeed;

			if (currentLength == fullLength) {
				canType = true;
			}
		}

		if (canType)
			detectPressedKeyOrButton ();
	}

	public void fillText(string text){
		this.text = text;
	}

	public void registerCommand(string command, CommandExecutor exec){
		commandExec[command] = exec;
	}

	public void registerKeyAction(string key, KeyAction exec){
		keyActions[key] = exec;
	}

	public Dictionary<string, CommandExecutor> getCommands(){
		return this.commandExec;
	}

	public void detectPressedKeyOrButton() {
		foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyUp (kcode)) {
				Type (kcode.ToString());
				GetComponent<Text> ().text = this.currentText + (Mathf.Round(Time.time) % 2 == 0 ? "▇" : "");
			}
		}
	}

	public void StartEvent(ComposerEvent e){
		StartCoroutine (e.Run());
	}

	

	void Type(String s){
		s = s.ToLower ();

		// Todo alpha/keypad
		if (s.StartsWith("alpha")){
			s = s.Replace ("alpha", "");
		} else if (s.StartsWith("keypad")){
			s = s.Replace ("keypad", "");
		}

		Debug.Log (s);

		if (keyActions.ContainsKey (s)) {
			if (keyActions [s].Execute(this)) {
				return;
			}
		}

		if (s.Length > 1) {
			return;
		}

		currentText += s;
	}

	public void ExecuteCommand(string cmd){
		string[] args = cmd.Split(new string[]{" "}, StringSplitOptions.None);
		if (commandExec.ContainsKey (args[0])) {
			if (!commandExec [args [0]].Execute (args [0], args, this)) {
				currentText += "\n> Command usage: " + commandExec [args [0]].Usage ();
			};
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
	}

	public String getCurrentText(){
		return this.currentText;
	}

	public void setCurrentText(string text){
		this.currentText = text;
		GetComponent<Text> ().text = this.currentText + (Mathf.Round (Time.time) % 2 == 0 ? "▇" : "");
	}

	public bool hasTypePermission(){
		return this.canType;
	}

	public void setTypePermission(bool b){
		this.canType = b;
	}

}
