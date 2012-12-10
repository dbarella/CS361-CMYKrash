using UnityEngine;
using System.Collections;

public class Laser : Ammo {
	public GameObject fizzle;
	public GameManagement mgmt;
	private float x;
	private float width;

	void Start(){
		//GM
		mgmt = Camera.main.GetComponent<GameManagement>();
		width = mgmt.GetScreenWidth();
		//okay
		x = (transform.position.x+(width/3));
		//On startup, immediately shift forward so the edge will be on the ship, rather than the laser being half forward and half backwards.
		//transform.Translate(new Vector3(-x-x/5, 0, 0));
		transform.Translate ((gameObject.collider as CapsuleCollider).height/2*transform.localScale.y*Vector3.right);
		transform.Rotate(90,90,0);
//		Debug.Log("Current position" + transform.position);
	}

	void LateUpdate(){
		//OnTriggerEnter is guarenteed to be called before this, so just die.
		StartCoroutine(WaitandDie());
	}
	
	IEnumerator WaitandDie(){
		Instantiate(fizzle,transform.position,Quaternion.identity);
		yield return new WaitForSeconds(0.2f);
		Die ();
	}

	void OnTriggerEnter(Collider col){
		//Debug.Log ("Laser collided with "+col.gameObject.tag);
	}

}
