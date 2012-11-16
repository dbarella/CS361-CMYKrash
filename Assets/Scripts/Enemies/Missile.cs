using UnityEngine;
using System.Collections;

public class Missile : Spawnable {
	public bool isRandom;
	public float laneChangeTime;
	
	private float _timer;
	
	void Start () {
		base.damage = 1;
		base.health = 2;
		base.speedMult = 50;
	}
	
	void FixedUpdate () {
		Move();
		_timer -= Time.deltaTime;
		
		if(laneChanging) ChangeLane(direction);
		else if(_timer <= 0f && isRandom) {
			_timer = laneChangeTime; //Reset the timer
			Debug.Log("Reset Timer");
			ChangeLane( Random.Range(-1,2) ); //Generate a random into to change lanes
		}
	}
}
