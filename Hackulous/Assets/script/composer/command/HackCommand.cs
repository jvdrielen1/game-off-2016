using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HackCommand : CommandExecutor {

	private Composer composer;
	private HackingEvent current;

	public override bool Execute (string command, string[] args, Composer composer) {
		this.composer = composer;
		composer.setTypePermission(false);

		GameManager gm = GameManager.Instance ();
		HackManager hm = HackManager.Instance ();
		Player player = gm.getPlayer ();
		List<Hack> hacks = hm.getHacksForPlayer(player);

		if (args.Length == 2) {
			string id = args [1];
			try {
				int intId = Int32.Parse(id);
				foreach (Hack hack in hacks){
					if (hack.getId() == intId){
						if (hack.canHack(player)){
							current = new HackingEvent(composer, hack);
							composer.StartEvent(current);
							return true;
						} else {
							print("/-------------------------\\");
							print("|----- UPGRADE NEEDED ----|");
							print("|- BUY IT BEFORE HACKING -|");
							print("\\-------------------------/");
							print("");
							composer.setTypePermission(true);
							return true;
						}
					}
				}
				print("Hack not found. Check available jobs.");
			} catch (Exception e){
				Debug.Log (e.StackTrace);
				return true;
			}
		}

		if (current == null) {
			print ("<b>Available hacks:</b>");
			if (hacks.Count == 0) {
				print ("There are no hacking jobs available.. Try upgrading your hardware!");
				return true;
			}

			print ("To start hack, use command: hack <id>");

			foreach (Hack hack in hacks){
				print ("-------------------");
				print ("Hacking job: <i>" + hack.getId() + "</i>");
				print ("Name: <i>" + hack.getName() + "</i>");
				print ("  - Description:<i> ");
				print ("       " + hack.getDescription() + "</i>");
				print ("  - Reward:<i> " + hack.getReward() + "$</i>");
			}

			print ("");
			composer.setTypePermission (true);
			return true;
		}
		return true;
	}

	public override string Usage () {
		return "hack <id>";
	}

	public void clearCurrent(){
		this.current = null;
	}

	public class HackingEvent : ComposerEvent {

		private Composer composer;
		private Hack hack;

		int times = 0;

		public HackingEvent(Composer composer, Hack hack){
			this.composer = composer;
			this.hack = hack;
		}

		public override IEnumerator Run () {
			composer.setCurrentText (composer.getCurrentText () + "\nHacking (" + hack.getName() + "): [");
			while (times < 20) {
				times++;
				composer.setCurrentText (composer.getCurrentText () + "-");

				if (times == 20) {
					composer.setCurrentText (composer.getCurrentText () + "]\n");
					composer.setTypePermission (true);
					Player player = GameManager.Instance ().getPlayer ();
					/*player.setBalance (player.getBalance() + hack.getReward());
					player.completeHack (hack.getId ());
					player.addXp (50);
					composer.CalculateLines ();
					SaveGame.Save ();*/
					GameManager.Instance ().saveComposerState (composer.getCurrentText());
					yield break;
				}

				yield return new WaitForSeconds (0.1f);
			}
		}

	}

}
