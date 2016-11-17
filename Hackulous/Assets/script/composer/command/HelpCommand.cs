using UnityEngine;
using System.Collections;

public class HelpCommand : CommandExecutor {

	public override bool Execute (string command, string[] args) {
		print ("> Hackulous Help");
		print ("- Commands:");
		print ("   help : You just did this?");
		print ("   hack : Start hacking");
		return true;
	}

}
