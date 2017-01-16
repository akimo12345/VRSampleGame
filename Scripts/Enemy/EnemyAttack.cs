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

    [Tooltip("Each time the PickAndPlaySound method is called, one of the sounds in this array " +
 "will be randomly selected and played through the Listener.")]
    public AudioClip[] Clips;
    private GvrAudioSource audiosource;
    public bool PreventRepeats = true;
    private int prevID = -1;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
        audiosource = gameObject.GetComponent<GvrAudioSource>();
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
            PickAndPlaySound();
            Animator charanim = GameObject.Find ("Ninja_Rig").GetComponent<Animator>();
			charanim.SetTrigger ("Knockdown");          
			anim.SetBool("WalkerMove",true);
			yield return new WaitForSeconds(0f);
        }
    }

    public void PickAndPlaySound()
    {
        if (Clips.Length <= 0)
        {
            return;
        }
        int id = Random.Range(0, Clips.Length - 1);
        if (Clips.Length > 1 && PreventRepeats)
        {
            // If we're preventing repeats, Shift the resulting ID up or down one space to prevent the
            // same sound from being selected twice in a row.
            if (id == prevID)
            {
                if (id + 1 < Clips.Length)
                {
                    id = id++;
                }
                else if (id - 1 >= 0)
                {
                    id = id--;
                }
            }
            prevID = id;
        }
        audiosource.PlayOneShot(Clips[id]);
    }
}
