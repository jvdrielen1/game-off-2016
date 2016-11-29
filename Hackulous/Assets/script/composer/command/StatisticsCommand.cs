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
		print ("  - Balance: " + player.getBalance() + "$");
		print ("  -------");
		print ("  - Hacks type: " + player.getHackType());
		print ("  - Hacks executed: 5");
		return true;
	}

	public override string Usage () {
		return "stats";
	}

}
