using UnityEngine;
using System.Collections;

public class PowerupLaser : PowerUp {
	new public void onActivate(){
		Vector3 pos = s.transform.position;	//Position the power up is being activated in
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Enemy"))
		{ 
			if (o.transform.position.y == pos.y)
				Destroy(o);
		}
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Planet")){
			if(o.transform.position.y == pos.y)
				Destroy(o);
		}	
	}
}
