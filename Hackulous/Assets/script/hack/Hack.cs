using UnityEngine;
using System.Collections;

public class Hack {

	private int id;
	private string name;
	private string description;

	private int minLevel;
	private HackType type;
	private int minCpuLevel;
	private int minRamLevel;
	private double reward;

	public Hack (int id, string name, string description, int minLevel, HackType type, int minCpuLevel, int minRamLevel, double reward)
	{
		this.id = id;
		this.name = name;
		this.description = description;
		this.minLevel = minLevel;
		this.type = type;
		this.minCpuLevel = minCpuLevel;
		this.minRamLevel = minRamLevel;
		this.reward = reward;
	}

	public int getId(){
		return this.id;
	}

	public string getName(){
		return this.name;
	}

	public void setName(string name){
		this.name = name;
	}

	public string getDescription(){
		return this.description;
	}

	public HackType getType(){
		return this.type;
	}

	public double getReward(){
		return this.reward;
	}

	public bool canHack(Player player){
		return (player.getLevel ("cpu") >= minCpuLevel) && (player.getLevel ("ram") >= minRamLevel);
	}

}
