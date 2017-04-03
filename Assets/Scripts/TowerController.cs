using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

	private GameManager gameManager;
	void Awake () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}

	void OnTriggerEnter(Collider other){
		Debug.Log("Tower: " + other.gameObject.tag);
		if(other.gameObject.tag.Equals("Enemy")){
			gameManager.PlayerTakeDamage(10);
			var enemyController = other.gameObject.GetComponent<EnemyController>();
			enemyController.TakeDamage(enemyController.Health);
		};
		
	}
}
