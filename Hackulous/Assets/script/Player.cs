using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Player {

	private string name;
	private double money;
	private int level = 0;

	private HackType type = HackType.WHITEHAT;

	private int hacksDone = 0;
	private int hacksSuccesfull = 0;
	private int hacksFailed = 0;

	private Dictionary<string, int> upgradeMap = new Dictionary<string, int>();

	public bool isFirstTime = true;

	public Player(string name, double money){
		this.name = name;
		this.money = money;
	}

	public void setName(string name){
		this.name = name;
	}

	public string getName(){
		return this.name;
	}

	public void setBalance(double money){
		this.money = money;
	}

	public double getBalance(){
		return this.money;
	}

	public void setHackType(HackType type){
		this.type = type;
	}

	public HackType getHackType(){
		return this.type;
	}

	public int getLevel(string hardware){
		if (upgradeMap.ContainsKey (hardware)) {
			return upgradeMap [hardware];
		} else {
			upgradeMap.Add (hardware, 1);
			return 1;
		}
	}

	public int levelUp(string hardware){
		if (!upgradeMap.ContainsKey (hardware)) {
			upgradeMap.Add (hardware, 1);
		} else {
			upgradeMap [hardware] = upgradeMap [hardware] + 1;
		}

		return upgradeMap [hardware];
	}

}
