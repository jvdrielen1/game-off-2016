﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class CommandExecutor {

	abstract public bool Execute(string command, string[] args);

	public void print(string msg){
		GameObject composer = GameObject.Find("Composer");
		Composer textWriter = composer.GetComponent<Composer> ();
		textWriter.PrintLn(msg);
	}

}