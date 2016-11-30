using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Player {

	private string name;
	private double money;
	private int level = 1;

	private int xp = 0;
	private int xpNextLevel = 300;

	private HackType type = HackType.WHITEHAT;

	private int hacksDone = 0;
	private int hacksSuccesfull = 0;
	private int hacksFailed = 0;

	private Dictionary<string, int> upgradeMap = new Dictionary<string, int>();

	private List<int> completedHacks = new List<int> ();

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

	public void completeHack(int id){
		completedHacks.Add (id);
	}

	public List<int> getCompletedHacks(){
		return this.completedHacks;
	}

	public int getPlayerLevel(){
		return this.level;
	}

	public void playerLevelUp(){
		this.level++;
		SaveGame.Save();
	}

	public int getXp(){
		return this.xp;
	}

	public int getXpNextLevel(){
		return this.xpNextLevel;
	}

	public void addXp(int xp){
		this.xp += xp;
		if (this.xp >= this.xpNextLevel) {
			this.xp -= this.xpNextLevel;

			if (this.xp < 0) {
				this.xp = 0;
			}

			this.xpNextLevel = (int)((double) this.xpNextLevel * 1.5);

			playerLevelUp();
		}
	}

	public int getXpPercentage(){
		Debug.Log (this.xp);
		if (this.xp == 0)
			return 0;

		Debug.Log (this.xpNextLevel);
		decimal p = ((decimal)this.xp / (decimal)this.xpNextLevel) * 100;
		return (int)p;
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
