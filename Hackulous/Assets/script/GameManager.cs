using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager {

	private static GameManager instance;

	private Dictionary<string, int> upgradeMap = new Dictionary<string, int>();

	public static GameManager Instance(){
		if (instance == null) {
			instance = new GameManager ();
		}

		return instance;
	}

	public int GetLevel(string hardware){
		if (upgradeMap.ContainsKey (hardware)) {
			return (upgradeMap [hardware] == 0 ? 1 : upgradeMap [hardware]);
		} else {
			upgradeMap.Add (hardware, 1);
			return 1;
		}
	}

	public int LevelUp(string hardware){
		if (!upgradeMap.ContainsKey (hardware)) {
			upgradeMap.Add (hardware, 1);
		} else {
			upgradeMap [hardware] = upgradeMap [hardware] + 1;
		}

		return upgradeMap [hardware];
	}

}
