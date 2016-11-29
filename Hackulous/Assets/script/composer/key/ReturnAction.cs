using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ReturnAction : KeyAction {
	
	public override bool Execute(Composer composer){
		string currentText = composer.getCurrentText();

		string[] sentences = currentText.Split(new string[]{"\n"}, StringSplitOptions.None);
		string command = sentences [sentences.Length-1];

		if (command != "") {
			composer.ExecuteCommand (command);
		}

		if (!composer.hasTypePermission())
			return true;

		composer.setCurrentText (composer.getCurrentText() + "\n");
		composer.CalculateLines();
		return true;
	}

}
