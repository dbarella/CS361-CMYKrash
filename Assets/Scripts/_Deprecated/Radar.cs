using UnityEngine;
using System.Collections;

//The collider attached to this object, known as the Hull, will call the die method on it's parent on collision with the player

public class Radar : Kamikaze {
	// Use this for initialization
	void Start () {
//		Debug.Log (transform.parent.name);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			Kamikaze kami = transform.parent.GetComponent<Kamikaze>();
			kami.Die();
		}
		if(col.tag == "Ammo"){
			Kamikaze kami = transform.parent.GetComponent<Kamikaze>();
			float temp = col.gameObject.GetComponent<Ammo>().GetDamage();
			kami.TakeDamage(temp);
		}
	}
}
