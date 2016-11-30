using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HackingDatabase : MonoBehaviour {

	public GameObject idColumn, nameColumn, typeColumn, rewardColumn;

	void Start () {
		Player player = GameManager.Instance ().getPlayer ();
		List<int> completed = player.getCompletedHacks ();

		foreach (int i in completed) {
			Hack hack = HackManager.Instance ().getHack(i);
			if (hack != null) {
				idColumn.GetComponent<Text> ().text += "\n| " + hack.getId ();
				nameColumn.GetComponent<Text> ().text += "\n| " + hack.getName ();
				typeColumn.GetComponent<Text> ().text += "\n| " + hack.getType();
				rewardColumn.GetComponent<Text> ().text += "\n| $" + hack.getReward ();
			}
		}

	}

}
