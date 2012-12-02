using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	/*SCRIPT REFERENCES*/
	protected GameManagement mgmt;
	public Ship cShip;
	public Ship mShip;
	public Ship yShip;
	
	/*VARIABLES*/
	protected float cHealth;
	protected float mHealth;
	protected float yHealth;
	protected float maxHealth;
	protected float multCons = 20.0f;

	//Fix this code to be attached to each ship individually
	/*void Start () {
		mgmt = Camera.main.GetComponent<GameManagement>();
		cShip = mgmt.ships[0].GetComponent<Ship>();
		mShip = mgmt.ships[1].GetComponent<Ship>();
		yShip = mgmt.ships[2].GetComponent<Ship>();
		cHealth = cShip.GetHealth();
		mHealth = mShip.GetHealth();
		yHealth = yShip.GetHealth();
		maxHealth = 7;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		cHealth = cShip.GetHealth();
		if(cHealth < 0) cHealth = 0;
		mHealth = mShip.GetHealth();
		if(mHealth < 0) mHealth = 0;
		yHealth = yShip.GetHealth();
		if(yHealth < 0) yHealth = 0;
	}
	
	void OnGUI () {
		GUI.Box(new Rect(10,10,(cHealth * multCons), 10), "HP:" + cHealth + "/"	+ maxHealth);
		GUI.Box(new Rect(170,10,(mHealth * multCons), 10), "HP:" + mHealth + "/"	+ maxHealth);
		GUI.Box(new Rect(330,10,(yHealth * multCons), 10), "HP:" + yHealth + "/"	+ maxHealth);

	}*/
}
