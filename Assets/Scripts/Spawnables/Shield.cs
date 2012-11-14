using UnityEngine;
using System.Collections;

//Last edited by Brendan at 4AM, 11/12/12

//Until I see the Spawnable class and know what I'm doing, consider this a WIP.

public class Shield : Spawnable { //afaik this is how you extend classes in C#.

	// Use this for initialization
	void Start () {
		speedMult = 1;//gets same speed as planet
		health = 1;//gave it 1 health so i guess it doesnt destroy itself?
		damage = 0;//it does 0 damage
	}
	
	// Update is called once per frame
	void Update () {//all update needs to do is move the shield to the left.
		Move();
	}
	
	void OnTriggerEnter(Collider other) {//collider to give powerup!
		if(other.gameObject.CompareTag("Ship")) {//if collides with a ship
			//other.gameObject.getItem(this.gameObject.tag);//calls it's getItem, passing in Shield
			this.Die();//and destroys itself (am i doing it right?)
		} 	
	}
	
	
}
