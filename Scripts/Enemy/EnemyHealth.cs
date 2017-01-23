using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 100; // The amount of health the enemy starts the game with.
	public int currentHealth; // The current health the enemy has.
	//public int score;
	public int scoreValue = 10;  //for normal walker
	public float sinkSpeed = 2.5f;  // The speed at which the enemy sinks through the floor when dead.
	public AudioClip deathClip; // The sound to play when the enemy dies.
	public AudioClip hurtClip;

	Animator anim; // Reference to the animator.
	GvrAudioSource enemyAudio; // Reference to the audio source.
	GvrAudioSource enemyAudio1; //Reference to audio source.
	ParticleSystem BloodSplatter; // Reference to the particle system that plays when the enemy is damaged.
	CapsuleCollider capsuleCollider; // Reference to the capsule collider.
	PlayerHealth playerhealth;

	bool isDead; // Whether the enemy is dead.
	bool isSinking; // Whether the enemy has started sinking through the floor.

    void Awake ()
    {
		//score = 0;
        anim = GetComponent <Animator> ();
		enemyAudio = GetComponent <GvrAudioSource> ();
		enemyAudio1 = GetComponent <GvrAudioSource> ();
		BloodSplatter = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
		playerhealth = GameObject.Find("Ninja").GetComponent<PlayerHealth> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
			StartSinking ();
        }
    }


	public void TakeDamage (int amount)
    {
        if(isDead)
            return;

		if (playerhealth.currentHealth <= 0) 
		{
			currentHealth = startingHealth;
		} 
		else 
		{
			currentHealth -= amount;
			enemyAudio1.clip = hurtClip;
			enemyAudio1.Play ();
			enemyAudio1.loop = false;
			if(!BloodSplatter.isPlaying)
				BloodSplatter.Play();
		}
			
		//BloodSplatter.transform.position = hitPoint;

        if(currentHealth <= 0f)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;
        anim.SetTrigger ("WalkerDead");
		//Debug.Log ("WalkerDead");
        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
		enemyAudio.loop = false;
		ScoreManager.score += scoreValue;
		//playerhealth.scoreValue += 10;
		isSinking = true;
    }


    public void StartSinking ()
    {
        GetComponent <NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;           
        Destroy (gameObject, 2f);
    }
}
