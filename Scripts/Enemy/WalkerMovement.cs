using UnityEngine;
using System.Collections;

public class WalkerMovement : MonoBehaviour {

	Transform player;
	PlayerHealth playerHealth;
	NavMeshAgent nav;
	EnemyHealth enemyHealth;

    public int minRoarFrame = 150;
    public int maxRoarFrame = 600;

    int frameCount, roarFrame;

    [Tooltip("Each time the PickAndPlaySound method is called, one of the sounds in this array " +
         "will be randomly selected and played through the Listener.")]
    public AudioClip[] Clips;
    private GvrAudioSource audiosource;
    public bool PreventRepeats = true;

    private int prevID = -1;

    // Use this for initialization
    void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent<NavMeshAgent>();

        audiosource = gameObject.GetComponent<GvrAudioSource>();

        frameCount = 0;
        roarFrame = getRoarFrame();
    }
	
	// Update is called once per frame
	void Update () {
		if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) 
		{
			nav.SetDestination (player.position);
            walkerRoar();
        } 
		else 
		{
			nav.enabled = false;
		}
	}

    void walkerRoar()
    {
        if (frameCount++ > roarFrame)
        {
            PickAndPlaySound();
            frameCount = 0;
            roarFrame = getRoarFrame();
        }
    }

    int getRoarFrame()
    {
        // roar after 200~ 400 frames
        return Random.Range(minRoarFrame, maxRoarFrame);
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
