using UnityEngine;
using System.Collections;

public class WalkerMovement : MonoBehaviour {

	Transform player;
	PlayerHealth playerHealth;
	NavMeshAgent nav;
	EnemyHealth enemyHealth;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) 
		{
			nav.SetDestination (player.position);
		} 
		else 
		{
			nav.enabled = false;
		}
	}
}
