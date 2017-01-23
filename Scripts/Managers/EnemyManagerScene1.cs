using UnityEngine;
using System.Collections;

public class EnemyManagerScene1 : MonoBehaviour {

	public GameObject enemy;

	private bool waveActive = false;

	public Transform[] spawnPoints;
	public int enemyAmount = 20;

	private int waveLevel = 0;
	private float diffucultyMultiplier = 1.0f;
	private float intermissionLength = 5f;
	private int enemyCount = 0;
	private ArrayList enemies;
	private bool allEnemiesSpawned = false;

	private float spawnIntervall = 3;

	//Animator anim;

	//private GUIScript gui;

	public enum GameState {
		preStart,
		activeWave,
		intermission
	}

	GameState state = GameState.preStart;

	void Awake(){
		
		enemies = new ArrayList();

		//anim = GetComponent<Animator>();
		//gui = Camera.main.GetComponentInChildren<GUIScript>();
	}

	void Update () {
		switch(state){

		case GameState.preStart:
			//if(gui.startWave){
				setNextWave();
				startNewWave();
				//gui.startWave = false;
			//}else {

			//}
			break;

		case GameState.activeWave:
			if(enemyCount == 0 && waveActive && allEnemiesSpawned){
				finishWave();
			}
			break;

		case GameState.intermission:
			break;
		}
	}

	void LateUpdate(){
		for(int i = 0; i < enemies.Count; i++){
			if((GameObject)(enemies[i]) == null){
				enemies.Remove(enemies[i]);
			}
		}
		enemyCount = enemies.Count;
	}

	void setNextWave(){
		diffucultyMultiplier = (diffucultyMultiplier * waveLevel) / 2;
	}

	void startNewWave(){
		state = GameState.activeWave;

		StartCoroutine(StartMission(1.5f));

		waveLevel++;

	}

	IEnumerator InterMission(float seconds){
		yield return new WaitForSeconds(seconds);
		setNextWave();
		startNewWave();
	}

	IEnumerator EnemySpawnerRoutine(float spawnIntervall, int enemyAmount){
		for(int i = 0; i < enemyAmount; i++){
			spawnNewEnemy();
			yield return new WaitForSeconds(spawnIntervall);
		}
		allEnemiesSpawned = true;
	}

	void finishWave(){
		//anim.SetTrigger ("StageComplete");
		StartCoroutine("InterMission",intermissionLength);
		state = GameState.intermission;
		waveActive = false;
	}

	void spawnNewEnemy(){
		int i = Random.Range(0,2);
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		GameObject e = (GameObject) Instantiate (enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
		enemyCount++;
		enemies.Add(e);
	}

	IEnumerator StartMission(float seconds){
		yield return new WaitForSeconds(seconds);

		allEnemiesSpawned = false;

		StartCoroutine(EnemySpawnerRoutine(spawnIntervall,enemyAmount));

		waveActive = true;
	}
}
