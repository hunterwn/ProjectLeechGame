using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Jump : PlayerState
    {
        Animator animator;
        PlayerController player;
        CharacterController controller;
        private bool jumpVelocityApplied;
        void OnEnable() {
            this.animid = "jump";
            controller = GetComponent<CharacterController>();
            player = GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
            animator.SetBool(this.animid, true);
            jumpVelocityApplied = false;
        }
        void Update() {
            PhysicsHandler();
            CollisionHandler();
            InputHandler();
        }

        void PhysicsHandler() {
            if(!jumpVelocityApplied)
            {
                jumpVelocityApplied = true;
                player.current_speed_v = player.jump_initial_velocity;
            } else {
                ApplyGravity(player.gravity);
                ApplyHorizontalFriction(player.air_friction);
                ApplyAerialDrift(player.aerial_drift);
            }
        }

        void CollisionHandler() {
            if(jumpVelocityApplied && player.current_speed_v < 0 && controller.isGrounded)
            {
                player.current_speed_v = 0;
                EnterLanding();
            }
        }

        void InputHandler() {
            int inputDir = GetDirectionHeld();
            int facing_dir = animator.GetInteger("facing_direction");

            if(!CheckAnimationTransition() && inputDir == facing_dir * -1)
            {
                ReverseFacingDirection();
            }

            if(CheckAnimationFinished())
            {
                EnterFall();
            }
        }
    }
}