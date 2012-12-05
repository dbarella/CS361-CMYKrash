using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Last modified by: Brendan, Thursday 11/8 @ 7 PM

public class Spawner : MonoBehaviour {

	//These variables hold information about what the spawner will spawn
	public GameObject[] objArr;
	public bool isRandomSpawner;
	int[] tickArray;	//The list to iterate through when not randomized
	private int ticker = 0;	//The interal value determining where in the ticker this is.
	float[] probabilities;
	//These variables are used only when random spawning is in effect
	public float randMinTime;
	public float randMaxTime;
	public float spawnTime;
	private float timer;

	/*
	*Currently, there's no instantiation that needs to take place with this.
	*/
	void Start(){

//		Debug.Log ("camera: "+Camera.main.GetComponent<GameManagement>());
		objArr = Camera.main.GetComponent<GameManagement>().GetObjectArray();
		timer = spawnTime;
		//randMinTime = spawnTime;
		//randMaxTime = spawnTime + 1;
	}
	/*Handles the spawning of enemies according to whatever paradigm we need
	 *Right now we pretty much only have the random spawning, so that's what it does
	 */
	void Update(){
		//If using random spawning and spawn timer is 0, spawn something
		if(timer <= 0 && isRandomSpawner){
			float randomObj = Random.value;
			int index = 0;

			while(randomObj >= probabilities[index]){

				randomObj -= probabilities[index];
				index++;

			}
			index--;
			if(index != 0){			
				SpawnObject (objArr[index]);
			}
			//SpawnObject(prefabList[Random.Range(0, prefabList.Count -1)]);
			timer = Random.Range(randMinTime, randMaxTime);
		}
		//If we're not a random spawner, iterate throught the ticker.
		if(!isRandomSpawner && ticker >= tickArray.Length){
			Camera.main.GetComponent<GameManagement>().LevelEnded();
		}
		else if(!isRandomSpawner && timer <=0){
//			Debug.Log ("spawner: "+tickArray[ticker]+" "+ticker);
			if(tickArray[ticker] != 0)SpawnObject(objArr[(tickArray[ticker])]);
			timer = spawnTime;
			ticker++;
		}
		
		//Dercrement timer if > 0
		if(timer > 0){
			timer -= Time.deltaTime;
		}
	}
	/*Spawns the given object at the spawner's location, presumably to be sent down the lane.
	 * There might be more logic here later, so I made it another method, but it's one line as is.
	 */
	GameObject SpawnObject(GameObject o){
	//Instantiate the chosen object at the spawner's position and direction.
		return (GameObject)Instantiate(o, transform.position, transform.rotation);
	}
	public void SetRandom(bool isRandom) {
		this.isRandomSpawner = isRandom;
	}
	public void SetTickArray(int[] arr){
		tickArray = arr;
	}
	public void SetProbabilityArray(float[] f){
		probabilities = f;
	}
	public int[] GetTickArray(){
		return tickArray;
	}
	public void SetObjArray(GameObject[] array){
		objArr = array;
	}
}
