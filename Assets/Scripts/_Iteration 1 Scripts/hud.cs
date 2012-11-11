using UnityEngine;
using System.Collections;

//Last modified by: Brendan, Thursday 11/8 @ 7 PM

public class hud : MonoBehaviour {
	
	public GameManagement mgmt;
	public GameObject ship1;
	public GameObject ship2;
	public GameObject ship3;
	public Ship ship1Script;
	public Ship ship2Script;
	public Ship ship3Script;
	protected int maxHealth;
	protected int cur1Health;
	protected int cur2Health;
	protected int cur3Health;
	protected int multCons = 20;//constant multiplier for display purposes
	//int l =100; 
	//int h = 20;
	// Use this for initialization
	void Start () {
		mgmt = GetComponent<GameManagement>();
		ship1 = mgmt.GetShip(1);
		ship2 = mgmt.GetShip(2);
		ship3 = mgmt.GetShip(3);
		ship1Script = ship1.GetComponent<Ship>();
		ship2Script = ship2.GetComponent<Ship>();
		ship3Script = ship3.GetComponent<Ship>();
		
		cur1Health = ship1Script.GetHealth();
		cur2Health = ship2Script.GetHealth();
		cur3Health = ship3Script.GetHealth();
		maxHealth = 6;
	}
	
	// Update is called once per frame
	void Update () {//get the current value for health
		
		cur1Health = ship1Script.GetHealth();
		cur2Health = ship2Script.GetHealth();
		cur3Health = ship3Script.GetHealth();
	}
	
	void OnGUI(){
		//Debug.Log("onGUI");
		GUI.Box(new Rect(10,10,(cur1Health * multCons), 10), "HP:" + cur1Health + "/"	+ maxHealth);
		GUI.Box(new Rect(150,10,(cur2Health * multCons), 10), "HP:" + cur2Health + "/"	+ maxHealth);
		GUI.Box(new Rect(290,10,(cur3Health * multCons), 10), "HP:" + cur3Health + "/"	+ maxHealth);
	}
}
