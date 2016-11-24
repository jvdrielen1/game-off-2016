using UnityEngine;
using System.Collections;

public class SpaceAction : KeyAction {

	public override bool Execute (Composer composer) {
		composer.setCurrentText(composer.getCurrentText() + " ");
		return true;
	}

}
