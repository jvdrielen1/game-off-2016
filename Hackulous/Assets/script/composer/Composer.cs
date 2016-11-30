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

	private GameObject continueButton;

	private HackCommand hackCommand;

	void Start () {
		GameManager gm = GameManager.Instance ();
		HackManager hm = HackManager.Instance ();
		Player player = gm.getPlayer ();

		if (player.isFirstTime) {
			continueButton = GameObject.Find ("ContinueButton");
			continueButton.SetActive(false);
		}

		GetComponent<Text> ().text = "";

		if (player.isFirstTime) {
			setTypePermission (false);
			text = "> Booting up Hackulous OS V.II\n> Boot completed!\n> Validating Hackulous server details and requiring IP addresses.\n> Completed, welcome to Hackulous \"UNKNOWN\"\n\n???: Since you're a starting hacker, let me explain the basics.\n???: Hackulous is an advanced hacking operating system used by all sorts of hackers around the world!\n???: Starting today you're able to start your own hacking emperium.\n???: Since you were highly recommended on dark forums I will give you a start budget of $1000 to buy basic hardware.\n???: Access the terminal to start hacking business, websites, file servers, databases and entire networks.\n???: ....\n???: Just a tip, open the terminal and try to use the command \"help\" to get started.\n\n???: Goodluck.\n\n> Encrypted online chat terminated.";
		} else {
			// Register commands
			registerCommand ("help", new HelpCommand ());
			registerCommand ("hack", hackCommand = new HackCommand ());
			registerCommand ("stats", new StatisticsCommand ());
		}

		// Register key actions
		registerKeyAction ("backspace", new BackspaceAction());
		registerKeyAction ("return", new ReturnAction());
		registerKeyAction ("enter", new ReturnAction());
		registerKeyAction ("space", new SpaceAction());

		if (gm.hasComposerSaveState ()) {
			setCurrentText(gm.getComposerSaveState());
			Hack hack = hm.getCurrentHack();
			if (hack != null) {
				if (hm.latestWasSuccesfull ()) {
					player.addXp (50);
					player.setBalance (player.getBalance() + hack.getReward());
					player.completeHack (hack.getId ());
					PrintLn ("> Hack completed.");
					PrintLn ("> Job title: " + hack.getName ());
					PrintLn ("> Reward: $" + hack.getReward ());
					PrintLn ("> XP gained: " + 50);
					PrintLn ("> XP needed for level up: " + (player.getXpNextLevel () - player.getXp ()));
					PrintLn ("");
				} else {
					player.addXp (25);
					PrintLn ("> Hack failed.");
					PrintLn ("> Job title: " + hack.getName ());
					PrintLn ("> Reward: $0");
					PrintLn ("> XP gained: 25");
					PrintLn ("> XP needed for level up: " + (player.getXpNextLevel () - player.getXp ()));
					PrintLn ("");
				}
			}
			canType = true;
			gm.removeSaveState ();
			hackCommand.clearCurrent ();
		}
	}

	void Update () {
		Player player = GameManager.Instance ().getPlayer ();
		if (text != "" && (nextChar <= Time.time)) {
			
			int fullLength = text.Length;
			int currentLength = currentText.Length;

			if (currentLength < fullLength)
				this.currentText += text.ToCharArray () [currentLength];

			currentLength++;

			GetComponent<Text> ().text = this.currentText + (Mathf.Round (Time.time) % 2 == 0 ? "▇" : "");

			nextChar = Time.time + textSpeed;

			if (currentLength == fullLength) {
				if (player.isFirstTime) {
					continueButton.SetActive(true);
				} else {
					canType = true;
				}
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
		CalculateLines ();
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
