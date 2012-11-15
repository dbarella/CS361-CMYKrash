using UnityEngine;
using System.Collections;

public abstract class _Enemy : MonoBehaviour {
	
	public int health;
	public int speed;
	protected GameManagement mgmt;
	private float gameSpeed;

	// Use this for initialization
	void Start () {
		//Source the Game Management
		mgmt = Camera.main.GetComponent<GameManagement>();
		gameSpeed = mgmt.GetGameSpeed();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void TakeDamage(int damage){
		health-=damage;
		if(damage<=0){
			Die();
		}
	}
		
	private void Move(){
		transform.Translate(new Vector3(-1*gameSpeed*speed,0,0));
	}
	
	private void Die(){
		Destroy(gameObject);
	}
	
	void OnTriggerEnter(Collider col){
		
	}
}
