using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class WalkerMovement : MonoBehaviour {

	Transform player;
	PlayerHealth playerHealth;
	NavMeshAgent nav;
	EnemyHealth enemyHealth;

    public UnityEvent OnRoar;
    int roarFrame, frameCount;

    // Use this for initialization
    void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent<NavMeshAgent>();

        frameCount = 0;
        roarFrame = getRoarFrame();
    }
	
	// Update is called once per frame
	void Update () {


        if (frameCount++ > roarFrame)
        {
            frameCount = 0;
            roarFrame = getRoarFrame();
            OnRoar.Invoke();
        }

        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) 
		{
			nav.SetDestination (player.position);
		} 
		else 
		{
			nav.enabled = false;
		}

    }

    int getRoarFrame()
    {
        // roar after 200~ 400 frames
        return Random.Range(200, 400);
    }
}
