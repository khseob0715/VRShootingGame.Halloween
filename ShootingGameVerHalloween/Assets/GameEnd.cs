﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour {

	private GameObject MenuUI;
	private GameObject Time_score;

	private GameObject Score_Text;

	public static bool check = false;
	private TextMesh temp;
	// Use this for initialization
	void Start () {
		MenuUI = GameObject.FindGameObjectWithTag ("Menu");
		Time_score = GameObject.FindGameObjectWithTag ("Time_score");
		MenuUI.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (check) {	
			MenuUI.SetActive (true);
			temp = GameObject.FindGameObjectWithTag ("Result_Score").GetComponent<TextMesh> ();
			temp.text = "Clear Time : " + LaserGun.GameTime + "s";
			Time_score.SetActive (false);	
		}
	}
}
