using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveGame {

	public static void Save(){
		Player player = GameManager.Instance ().getPlayer ();
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerData.gd");
		bf.Serialize (file, player);
		file.Close ();
	}

	public static void Load(){
		Debug.Log (Application.persistentDataPath + "/playerData.gd");
		if (File.Exists (Application.persistentDataPath + "/playerData.gd")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerData.gd", FileMode.Open);
			GameManager.Instance ().setPlayer ((Player)bf.Deserialize (file));
			file.Close ();
		} else {
			GameManager.Instance ().setPlayer (new Player("Unknown", 1000));
		}
	}

}
