using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	public int damagePerSlice = 50;
	public float timeBetweenAttacks = 0.5f;

	private Rigidbody _body;

	float timer;

	PlayerHealth playerHealth; // Reference to the player's health.
	//ParticleSystem BloodSplatter; // Reference to the particle system that plays when the enemy is damaged.
	bool EnemyInRange;

    void Awake ()
    {
		playerHealth = GetComponentInParent<PlayerHealth> ();
		//BloodSplatter = GetComponentInChildren <ParticleSystem> ();
		_body = GetComponent<Rigidbody>();
    }

	void OnTriggerEnter (Collider other)
	{
		
		if ((other.transform.tag == "Walker" || other.transform.tag == "WalkerKing") && timer >= timeBetweenAttacks && playerHealth.currentHealth > 0)  
		{
			timer = 0f;

			Rigidbody targetRigibody = other.GetComponent<Rigidbody> ();
			
			EnemyHealth enemyhealth = targetRigibody.GetComponent<EnemyHealth> ();

			if (enemyhealth.currentHealth > 0) 
			{
				//Debug.Log ("enemy hurt");
				//if(!BloodSplatter.isPlaying)
				//	BloodSplatter.Play ();
				enemyhealth.TakeDamage (damagePerSlice);
			}
		}

	}	

	void OnTriggerExit (Collider other)
	{
		Debug.Log ("exit");
	}

	void Update ()
	{
		timer += Time.deltaTime;
	}
		
	public Vector3 Velocity {
		get { return _body.velocity; }
	}

}
