using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HelpCommand : CommandExecutor {

	public override bool Execute (string command, string[] args, Composer composer) {
		if (args.Length > 1) {
			return false;
		}

		print ("> Hackulous Help");
		print ("- Commands:");
		foreach (KeyValuePair<string, CommandExecutor> entry in composer.getCommands()){		
			print ("   " + entry.Key + " : " + entry.Value.Usage());
		}
		return true;
	}

	public override string Usage () {
		return "help";
	}

}
