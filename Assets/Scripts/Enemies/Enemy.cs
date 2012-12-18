using UnityEngine;
using System.Collections;

public abstract class Enemy : Spawnable {
	
	public GameObject explosionPrefab;
	protected AudioSource deathBlip;
	public AudioClip deathClip;
	public float deathClipVolume;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnTriggerEnter(Collider col){
		float temp;
		if(col.tag == "Player"){
			Die();
		}
		if(col.tag == "Ammo"){
			temp = col.gameObject.GetComponent<Ammo>().GetDamage();
			TakeDamage(temp);
//			Debug.Log(gameObject.name+" took damage. Health = "+health);
			if(health<=0){
				Die();
			}
		}
	}
	new public void Die() {
		//First, send the score value
		SendScore();
		//Animate Death Function
		
//		explosionPrefab = Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject;
		if(explosionPrefab != null){
		explosionPrefab = Instantiate(explosionPrefab,transform.position,Quaternion.identity) as GameObject;
		StartCoroutine(WaitandDestroy(explosionPrefab,4));}
		else{
			Debug.Log (gameObject.name+" Does not have an explosion");}
		if(deathClip != null){
			audio.PlayOneShot(deathClip, deathClipVolume);
			Debug.Log (gameObject.name + " has played deathClip");
		}
		else{
			Debug.Log (gameObject.name+" does not have an audio clip/");
		}
		Destroy(gameObject);
	}
	
	IEnumerator WaitandDestroy(GameObject o, int n){
		yield return new WaitForSeconds(n);
		Destroy(o);
	}
	
}
