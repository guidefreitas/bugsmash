using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class GameManager : MonoBehaviour {

	[Range(0,100)]
	public float PlayerHealth = 100;
    public bool gameStarted = false;

	public Transform[] EnemySpawnPoints;

	public GameObject EnemyPrefab;

	public GameObject CurrentTower;

	private List<GameObject> Enemies;
	// Use this for initialization
	void Start () {
		this.Enemies = new List<GameObject>();
	}
	
	void SpawnEnemies(){
		if(this.Enemies.Count < 3){
			var spawnPointIndex = Random.Range(0, EnemySpawnPoints.Length - 1);
			var spawnPoint = EnemySpawnPoints[spawnPointIndex];
			var enemy = (GameObject)Instantiate(EnemyPrefab, spawnPoint.position, Quaternion.identity);
			this.Enemies.Add(enemy);
			var enemyController = enemy.GetComponent<EnemyController>();
			enemyController.Speed = Random.Range(0.2f, 10.0f);
			enemyController.SetTargetDestination(CurrentTower.transform);
			
		}
	}
	void RemoveDeadEnemies(){
		for(int i=0;i<this.Enemies.Count;i++){
			var enemy = this.Enemies[i];
			var enemyController = enemy.GetComponent<EnemyController>();
			if(enemyController.Health <= 0){
				enemyController.Die();
				this.Enemies.RemoveAt(i);
			}
		}
	}

	void VerifyPlayerHealth(){

	}

	public void PlayerTakeDamage(float amount){
		this.PlayerHealth -= amount;
	}

	
	// Update is called once per frame
	void Update () {
		if(gameStarted){
			RemoveDeadEnemies();
			SpawnEnemies();
			VerifyPlayerHealth();
		}
	}
}
