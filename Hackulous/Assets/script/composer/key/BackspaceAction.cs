using UnityEngine;
using System.Collections;

public class BackspaceAction : KeyAction {

	public override bool Execute (Composer composer) {
		composer.setCurrentText(composer.getCurrentText().Substring (0, composer.getCurrentText().Length - 1));
		return true;
	}

}
