using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Entry : PlayerState
    {
        public string animid = "entry";
        Animator animator;
        PlayerController player;
        void OnEnable() {
            //animator.SetBool(this.animid, true);
            EnterIdle();
        }
        void OnDisable() {
            //animator.SetBool(this.animid, false);
        }
        void Update() {
            PhysicsHandler();
            CollisionHandler();
            InputHandler();
        }

        void PhysicsHandler() {

        }

        void CollisionHandler() {

        }

        void InputHandler() {

        }
    }
}