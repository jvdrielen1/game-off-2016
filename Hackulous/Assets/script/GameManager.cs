using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager {

	private static GameManager instance;

	private Player player;

	private string saveState = "";

	public static GameManager Instance(){
		if (instance == null) {
			instance = new GameManager ();
			SaveGame.Load();
		}

		return instance;
	}

	public void saveComposerState(string text){
		saveState = text;
	}

	public string getComposerSaveState(){
		return saveState;
	}

	public bool hasComposerSaveState(){
		return saveState != "";
	}

	public void removeSaveState(){
		saveState = "";
	}

	public void setPlayer(Player player){
		this.player = player;
	}

	public Player getPlayer(){
		return this.player;
	}

}
