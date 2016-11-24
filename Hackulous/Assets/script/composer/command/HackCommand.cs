using UnityEngine;
using System.Collections;

public class HackCommand : CommandExecutor {

	private Composer composer;

	public override bool Execute (string command, string[] args, Composer composer) {
		this.composer = composer;
		composer.setTypePermission(false);
		composer.StartEvent (new HackingEvent(composer));
		return true;
	}

	public override string Usage () {
		return "hack <website> <type> <level>";
	}

	public class HackingEvent : ComposerEvent {

		private Composer composer;

		int times = 0;

		public HackingEvent(Composer composer){
			this.composer = composer;
		}

		public override IEnumerator Run () {
			times++;
			composer.setCurrentText (composer.getCurrentText() + ".");

			if (times == 10) {
				composer.setTypePermission (true);
				yield break;
			}

			yield return new WaitForSeconds(0.1f);
		}

	}

}
