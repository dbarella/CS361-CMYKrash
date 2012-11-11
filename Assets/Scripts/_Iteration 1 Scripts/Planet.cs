using UnityEngine;
using System.Collections;

//Last modified by: Brendan, Thursday 11/8 @ 7 PM

public class Planet : MonoBehaviour {
	//Reference to the game management
	protected GameManagement mgmt;
	public float speedMult=1;
	public int damageDealt = 3;
	//public int health = 2;

	// Use this for initialization
	void Start () {
		//Source the Game Management
		mgmt = Camera.main.GetComponent<GameManagement>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(new Vector3(-1*speedMult,0,0));
	}
	
	/*public void takeDamage(int i) {
		health -= i;
		if(health <= 0) {
			Destroy(gameObject);
		}	
	}
	*/
	void OnTriggerEnter(Collider col) {
		if(col.gameObject.tag == "Player"){
			//Debug.Log ("Are we there yet?");
			int shipNo = col.gameObject.GetComponent<Ship>().getNo();
			mgmt.PlayerHit(damageDealt, shipNo);
			Debug.Log(gameObject);
			Destroy(gameObject);
		}
	}
}
