using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HackManager {

	private static HackManager instance;

	private List<Hack> hacks = new List<Hack> ();

	public static HackManager Instance(){
		if (instance == null) {
			instance = new HackManager ();

			instance.addHack (new Hack(9714, "Operation Whitehouse", "Hack into the servers inside the White House and release critical information", 0, HackType.BLACKHAT, 2, 0, 1250));
			instance.addHack (new Hack(6813, "Boycot Google", "DDoS Google.com", 0, HackType.BLACKHAT, 2, 1, 5700));
			instance.addHack (new Hack(4576, "Hack Pieter", "Hihi fuck pieter", 1, HackType.WHITEHAT, 0, 0, 100));
		}

		return instance;
	}

	public void addHack(Hack hack){
		hacks.Add (hack);
	}

	public List<Hack> getHacks(){
		return this.hacks;
	}

	public List<Hack> getHacksForPlayer(Player player){
		List<Hack> availableHacks = new List<Hack> ();

		foreach (Hack hack in hacks) {
			if (hack.canHack (player) && hack.getMinLevel() <= player.getPlayerLevel ()) {
				availableHacks.Add (hack);
			}
		}

		return availableHacks;
	}

	public Hack getHack(int id){
		foreach (Hack hack in hacks) {
			if (hack.getId() == id) {
				return hack;
			}
		}
		return null;
	}

}
