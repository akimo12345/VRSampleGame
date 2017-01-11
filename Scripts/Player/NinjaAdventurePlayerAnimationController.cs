// Copyright 2016 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using GVR.GUI;

using UnityEngine;

namespace GVR.Samples.NinjaTraining {
  public class NinjaAdventurePlayerAnimationController: MonoBehaviour, INinjaCharacterAnimatorController {
    public string ForwardMovementParamName = "Rate";
    private int forwardMovementParamID;

    public Animator Animator;
    public bool InstantRotate;
    public float RotateSpeed = 8.0f;

	Rigidbody NinjaRigidbody;

    void Start() {
      forwardMovementParamID = Animator.StringToHash(ForwardMovementParamName);
	  NinjaRigidbody = GetComponent<Rigidbody> ();
    }
			
    public Vector3 GetAnimationDelta() {
      return Animator.deltaPosition;
    }

    public void ProcessInput(Vector3 direction, float maxSpeed, float dt) {
      Vector3 idealMove = direction * maxSpeed * Time.deltaTime;

	  NinjaRigidbody.MovePosition (transform.position + idealMove);

      if (InstantRotate) {
        transform.LookAt(transform.position + idealMove);
		NinjaRigidbody.MovePosition (transform.position + idealMove);
      } else {
        transform.rotation = Quaternion.LookRotation(
            Vector3.RotateTowards(transform.forward, idealMove, RotateSpeed * Time.deltaTime, 0.0f));
		NinjaRigidbody.MoveRotation (transform.rotation);
      }				

      if (direction.sqrMagnitude > 0.001f) {
        Animator.SetFloat(forwardMovementParamID, direction.magnitude * maxSpeed);
      } else {
        Animator.SetFloat(forwardMovementParamID, 0.0f);
      }
    }
  }
}
