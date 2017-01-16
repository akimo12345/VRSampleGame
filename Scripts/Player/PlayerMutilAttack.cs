using UnityEngine;
using System.Collections;

public class PlayerMutilAttack : MonoBehaviour {

	public LayerMask m_EnemyMask;
	public int damagePerSlice = 25;
	public float timeBetweenAttacks = 0.5f;
	public float m_SliceRidus = 0.0f;

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
		//Debug.Log ("triggerEnter");
		Collider[] colliders = Physics.OverlapSphere (transform.position, m_SliceRidus, m_EnemyMask);

		for (int i =0; i < colliders.Length; i++)
		{
			//Debug.Log ("i=" + i);
			Rigidbody targetRigibody = colliders[i].GetComponent<Rigidbody> ();
			if (!targetRigibody)
				continue;

			EnemyHealth targetHealth = targetRigibody.GetComponent<EnemyHealth> ();

			if (!targetHealth)
				continue;
			
			if (targetHealth.currentHealth > 0) 
			{
				targetHealth.TakeDamage (damagePerSlice);
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
