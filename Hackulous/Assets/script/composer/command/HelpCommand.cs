using UnityEngine;
using System.Collections;

public class HelpCommand : CommandExecutor {

	public override bool Execute (string command, string[] args, Composer composer) {
		if (args.Length > 1) {
			return false;
		}

		print ("> Hackulous Help");
		print ("- Commands:");
		print ("   help : Display all commands");
		print ("   hack : Start hacking");
		return true;
	}

	public override string Usage () {
		return "help";
	}

}
