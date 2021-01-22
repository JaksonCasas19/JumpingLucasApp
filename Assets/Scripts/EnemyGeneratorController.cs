﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour {
	public GameObject enemyPretfab;
	public float generatorTimer = 1.75f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void CreateEnemy(){
		Instantiate (enemyPretfab, transform.position, Quaternion.identity);
	}
	public void StartGenerator(){
		InvokeRepeating ("CreateEnemy", 0f, generatorTimer);
	}
	public void CancelGenerator(bool clean = false){
		CancelInvoke ("CreateEnemy");
		if (clean) {
			Object[] allEnemies = GameObject.FindGameObjectsWithTag ("Enemy");
			foreach(GameObject enemy in allEnemies){
				Destroy (enemy);
			}
		}
	}
}
