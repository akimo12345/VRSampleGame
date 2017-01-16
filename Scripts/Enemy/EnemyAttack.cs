using UnityEngine;
using System.Collections;
using GVR.Samples.NinjaTraining;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 1.5f;
    public int attackDamage = 25;

	Animator anim; // Reference to the animator component.
	GameObject player; // Reference to the player GameObject.
	PlayerHealth playerHealth; // Reference to the player's health.
    EnemyHealth enemyHealth;

	bool playerInRange; // Whether player is within the trigger collider and can be attacked.
	float timer; // Timer for counting up to the next attack.

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
	{
		//Debug.Log ("playerInRange=" + playerInRange);
		if(other.gameObject == player && anim.GetCurrentAnimatorStateInfo(0).IsName("walk"))
        {
            playerInRange = true;
			//Debug.Log ("playerInRange=" + playerInRange);
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
		anim.SetBool("WalkerMove",true);
    }


    void Update ()
    {
        timer += Time.deltaTime;
		//Debug.Log ("timer=" + timer);
        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {	
			//Debug.Log ("Enemy Attack()");
			//anim.SetTrigger ("WalkerAttack");
			StartCoroutine("Attack");
			playerHealth.TakeDamage (attackDamage);	
        }

        if(playerHealth.currentHealth <= 0)
        {
			//Debug.Log ("NinjaDead()");
            anim.SetTrigger ("NinjaDead");
			anim.SetBool("WalkerMove",false);
			anim.SetBool("WalkerAttack",false);
        }
    }


	IEnumerator Attack()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {	
			anim.SetBool("WalkerAttack",true);
			Animator charanim = GameObject.Find ("Ninja_Rig").GetComponent<Animator>();
			charanim.SetTrigger ("Knockdown");          
			anim.SetBool("WalkerMove",true);
			yield return new WaitForSeconds(0f);
        }
    }
}
