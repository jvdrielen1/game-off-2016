using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpgradeManager : MonoBehaviour {

	public GameObject cpuText, ramText, coolingText, balanceText;
	private int firstCost = 650;

	void Start(){
		UpdateText ();
	}

	public void Upgrade(string type){
		Player player = GameManager.Instance ().getPlayer ();
		if (player.getBalance () >= getUpgradeCost (type)) {
			player.setBalance (player.getBalance () - getUpgradeCost (type));
			player.levelUp (type);
			SaveGame.Save ();
		}
		UpdateText ();
	}

	public void UpdateText(){
		Player player = GameManager.Instance ().getPlayer ();
		cpuText.GetComponent<Text> ().text = "CPU Level: " + player.getLevel ("cpu") + "\nCost: " + getUpgradeCost("cpu");
		ramText.GetComponent<Text> ().text = "RAM Level: " + player.getLevel ("ram") + "\nCost: " + getUpgradeCost("ram");
		coolingText.GetComponent<Text> ().text = "Cooling Level: " + player.getLevel ("cooling") + "\nCost: " + getUpgradeCost("cooling");
		balanceText.GetComponent<Text> ().text = "Money: $" + player.getBalance();
	} 

	public int getUpgradeCost(string type){
		Player player = GameManager.Instance ().getPlayer ();
		int level = player.getLevel (type);
		return firstCost * ((int)(1.6 * level));
	}

}
