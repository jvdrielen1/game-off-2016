using UnityEngine;
using System.Collections;

public class StatisticsCommand : CommandExecutor {

	public override bool Execute (string command, string[] args, Composer composer) {
		if (args.Length > 1) {
			return false;
		}

		Player player = GameManager.Instance ().getPlayer();

		print ("> Hackulous Statistics");
		print ("  - Player Name: " + player.getName());
		print ("  - Level: " + player.getPlayerLevel());
		print ("  - Level progress: " + player.getXpPercentage() + "%");
		print ("  - XP: " + player.getXp());
		print ("  - XP needed: " + player.getXpNextLevel());
		print ("  - Balance: " + player.getBalance() + "$");
		print ("  -------");
		print ("  - Hacks type: " + player.getHackType());
		print ("  - Hacks executed: " + player.getCompletedHacks().Count);
		return true;
	}

	public override string Usage () {
		return "stats";
	}

}
