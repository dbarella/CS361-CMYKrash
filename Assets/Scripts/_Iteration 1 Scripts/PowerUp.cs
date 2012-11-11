using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
	protected Ship s;
	public float speedMult=1;
	public void onActivate(){
		Debug.Log ("Empty powerup used");
	}
	
	void Update(){
		transform.Translate(new Vector3(-1*speedMult,0,0));
	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag =="Player"){
			s = other.gameObject.GetComponent<Ship>();
			s.AddPowerUp(this);		
		}
	}
}
