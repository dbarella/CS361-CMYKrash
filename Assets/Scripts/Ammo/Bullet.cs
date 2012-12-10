using UnityEngine;
using System.Collections;

public class Bullet : Ammo {
	public GameObject partycles;
	void Start(){
		
	}
	
	void OnTriggerEnter(Collider o){
			if(o.gameObject.CompareTag("Enemy")) {
//				Debug.Log(partycles.name);
				Instantiate(partycles,new Vector3(transform.position.x,transform.position.y,0),Quaternion.identity);
				Die();
			}
		
	}
}
