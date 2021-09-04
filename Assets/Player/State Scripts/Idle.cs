using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Idle : PlayerState
    {
        Animator animator;
        PlayerController player;
        CharacterController controller;
        void OnEnable() {
            this.animid = "idle";
            controller = GetComponent<CharacterController>();
            player = GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
            animator.SetBool(this.animid, true);
        }
        void Update() {
            PhysicsHandler();
            CollisionHandler();
            InputHandler();
        }

        void PhysicsHandler() {
            player.current_speed_h = 0;
        }

        void CollisionHandler() {
            if(!controller.isGrounded)
            {
                EnterFall();
            }
        }

        void InputHandler() {
            int inputDir = GetDirectionHeld();
            int facing_direction = animator.GetInteger("facing_direction");

            if(CheckJumpInput())
            {
                EnterJumpSquat();
                return;
            }

            if(inputDir == facing_direction * -1) 
            {
                ReverseFacingDirection();
            } else if(inputDir == facing_direction) 
            {
                if(CheckRunInput())
                {
                    EnterRun();
                } else {
                    EnterWalk();
                }
            }
        }
    }
}