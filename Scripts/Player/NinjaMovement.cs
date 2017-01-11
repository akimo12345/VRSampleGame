using UnityEngine;
using System.Collections;
using System;
using GVR.GUI;

namespace GVR.Samples.NinjaTraining{
	public class NinjaMovement : MonoBehaviour {
		
#if UNITY_EDITOR
		public void OnValidate() {

			if (InputProvider != null) {
				if (!(InputProvider is INinjaPlayerInputProvider)) {
					Debug.LogError("Input must implement IPlayerInputProvider");
					InputProvider = null;
				}
			}

			if (CharacterAnimationController != null) {
				if (!(CharacterAnimationController is INinjaCharacterAnimatorController)) {
					Debug.LogError("Input must implement ICharacterAnimatorController");
					CharacterAnimationController = null;
				}
			}
		}

#endif

		[Header("Movement")]
		[Tooltip("The max speed that the character will move.")]
		public float MovementSpeed = 4.0f;

		[Tooltip("The input provider that will be polled for a direction vector each frame. " +
			"This must implement IPlayerInputProvider.")]
		public MonoBehaviour InputProvider;

		[Tooltip("The animation controller to drive animation and movement on this character. " +
			"If this is not null, input will be sent to this animation controller to drive an " +
			"animation. Root motion of the animation will be processed as world movement. " +
			"If this is null, input will directly move the player.")]
		public MonoBehaviour CharacterAnimationController;

		private INinjaPlayerInputProvider inputProvider;
		private INinjaCharacterAnimatorController characterAnimationController;

		Rigidbody NinjaRigidbody;
		PlayerHealth playerHealth;

		// Use this for initialization
		void Start () {
			inputProvider = InputProvider as INinjaPlayerInputProvider;
			characterAnimationController = CharacterAnimationController as INinjaCharacterAnimatorController;
			NinjaRigidbody = GetComponent<Rigidbody> ();
			playerHealth = GetComponent<PlayerHealth> ();
		}
		
		// Update is called once per frame
		void Update () {

			if(playerHealth.currentHealth > 0)
				ProcessMovementRequests();
		}

		private void ProcessMovementRequests() {
			if (inputProvider.IsReady()) {
				Vector3 direction = inputProvider.GetMovementVector();
				if (direction.sqrMagnitude > 0.001f) {
					if (characterAnimationController != null) {
						characterAnimationController.ProcessInput(direction, MovementSpeed, Time.deltaTime);
					} else {
						Vector3 idealMove = direction * MovementSpeed * Time.deltaTime;
						transform.LookAt(transform.position + idealMove);
						NinjaRigidbody.MovePosition (transform.position + idealMove);
					}
				} else {
					if (characterAnimationController != null) {
						characterAnimationController.ProcessInput(Vector3.zero, MovementSpeed, Time.deltaTime);
					}
				}
			}
		}
	}
}