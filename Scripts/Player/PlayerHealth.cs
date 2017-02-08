using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
	public Font font;
    public int currentHealth;
    public Slider healthSlider;
    public Image m_FillImage; 
    public AudioClip deathClip;
	public AudioClip heartClip;
    public float flashSpeed = 2.5f;
	public GameObject other; //walker
	public GameObject other1; //walkerking
    //public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	public Color m_FullHealthColor = Color.green;  
	public Color m_ZeroHealthColor = Color.red; 
	//public int scoreValue = 0; // The amount added to the player's score when the enemy dies.

	//public Texture2D blood_heart;

	//public Camera camera;

	EnemyHealth Enemyhealth;
	//private string name = "Score: ";
	GameObject Ninja;
	float NinjaHeight;

    Animator anim;
    GvrAudioSource playerAudio;
	GvrAudioSource heartAudio;
    //PlayerMovement playerMovement;
    //PlayerShooting playerShooting;
    bool isDead;
    bool damaged;

    void Awake ()
    {
		/*
		Ninja = GameObject.FindGameObjectWithTag ("Player");

		CapsuleCollider collider = Ninja.GetComponent<CapsuleCollider> ();

		float size_y = collider.bounds.size.y;

		float scal_y = transform.localScale.y;

		NinjaHeight = (size_y * scal_y);
		*/
		anim = GetComponentInChildren <Animator> ();

		playerAudio = GetComponent <GvrAudioSource> ();

		heartAudio = GetComponent <GvrAudioSource> ();
		
		Enemyhealth = GetComponent <EnemyHealth> ();

        //playerMovement = GetComponent <PlayerMovement> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }

	private void OnEnable()
	{
		currentHealth = startingHealth;
		isDead = false;

		SetHealthUI();
	}

	/*
	void OnGUI()
	{
		Vector3 worldPosition = new Vector3 (transform.position.x, transform.position.y + NinjaHeight, transform.position.z);

		Vector2 position = camera.WorldToScreenPoint (worldPosition);

		position = new Vector2 (position.x, Screen.height - position.y);

		Vector2 bloodSize = GUI.skin.label.CalcSize (new GUIContent (blood_heart));

		Vector2 nameSize = GUI.skin.label.CalcSize (new GUIContent (name));

		GUI.skin.font = font;

		GUIStyle myStyle = new GUIStyle (GUI.skin.GetStyle (name));

		myStyle.fontSize = 50;

		myStyle.normal.textColor = Color.white;

		GUI.Label(new Rect(position.x - (nameSize.x/2), position.y - nameSize.y - bloodSize.y, nameSize.x, nameSize.y), name + scoreValue, myStyle);

	}
	*/
    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

		SetHealthUI ();

		if (currentHealth <= 30) 
		{
			heartAudio.clip = heartClip;
			heartAudio.Play ();
			heartAudio.loop = true;
			heartAudio.volume = 0.8f; 
		}
        
		if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }

	private void SetHealthUI()
	{
		// Adjust the value and colour of the slider.
		healthSlider.value = currentHealth;

		m_FillImage.color = Color.Lerp (m_ZeroHealthColor, m_FullHealthColor, currentHealth / startingHealth);
	}

    void Death ()
    {
        isDead = true;

		Debug.Log("Player death");

        anim.SetTrigger ("Ninja_Death");

        playerAudio.clip = deathClip;
        playerAudio.Play ();
		playerAudio.loop = false;

		//StopAnimator ();
    }

    public void RestartLevel ()
    {
        SceneManager.LoadScene (1);
    }

	public void SceneSwitch ()
	{
		//Destroy (other);
		//Destroy (other1);
		SceneManager.LoadScene (1);
	}
}
