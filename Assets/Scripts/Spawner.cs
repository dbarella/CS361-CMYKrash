using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Last modified by: Brendan, Thursday 11/8 @ 7 PM

public class Spawner : MonoBehaviour {
	//public List<GameObject> prefabList;	//List of all prefabs, for random spawning. I'm pretty sure this can be filled via inspector

	//These specific prefabs are unused right now, as everything is randomized.
	public GameObject[] objects;
	public bool isRandomSpawner;

	//These variables are used only when random spawning is in effect
	public float randMinTime;
	public float randMaxTime;
	private float spawnTimer;

	/*
	*Currently, there's no instantiation that needs to take place with this.
	*/
	void Start(){
	}
	/*Handles the spawning of enemies according to whatever paradigm we need
	 *Right now we pretty much only have the random spawning, so that's what it does
	 */
	void Update(){
		//If using random spawning and spawn timer is 0, spawn something
		if(spawnTimer <=0 && isRandomSpawner){
			int randomObj = Random.Range(0, objects.Length);
			SpawnObject (objects[randomObj]);
			
			//SpawnObject(prefabList[Random.Range(0, prefabList.Count -1)]);
			spawnTimer = Random.Range(randMinTime, randMaxTime);
		}
		if(!isRandomSpawner && spawnTimer <=0){

			int randomObj = Random.Range(0, objects.Length);
			SpawnObject(objects[randomObj]);
			spawnTimer = 5;
		}

		//Dercrement timer if > 0
		if(spawnTimer > 0){
			spawnTimer -= Time.deltaTime;
		}
	}
	/*Spawns the given object at the spawner's location, presumably to be sent down the lane.
	 * There might be more logic here later, so I made it another method, but it's one line as is.
	 */
	GameObject SpawnObject(GameObject o){
	//Instantiate the chosen object at the spawner's position and direction.
	return (GameObject)Instantiate(o, transform.position, transform.rotation);
	}

}
