using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager {

	private static GameManager instance;

	private Player player;
	private List<Hack> hacks = new List<Hack> ();

	public static GameManager Instance(){
		if (instance == null) {
			instance = new GameManager ();

			instance.addHack (new Hack(9714, "Operation Whitehouse", "Hack into the servers inside the White House and release critical information", 0, HackType.BLACKHAT, 2, 0, 1250));
			instance.addHack (new Hack(6813, "Boycot Google", "DDoS Google.com", 0, HackType.BLACKHAT, 2, 1, 5700));

			SaveGame.Load();
		}

		return instance;
	}

	public void setPlayer(Player player){
		this.player = player;
	}

	public Player getPlayer(){
		return this.player;
	}

	public void addHack(Hack hack){
		hacks.Add (hack);
	}

	public List<Hack> getHacks(){
		return this.hacks;
	}

}
