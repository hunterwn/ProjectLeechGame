using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class PlayerState : MonoBehaviour
    {
        public bool CheckRunInput()
        {
            if(Input.GetKey("left shift"))
            {
                return true;
            } else {
                return false;
            }
        }

        public int GetDirectionHeld()
        {
            if(Input.GetKey("a"))
            {
                return -1;
            } else if (Input.GetKey("d")){
                return 1;
            } else {
                return 0;
            }
        }

        public bool CheckAnimationFinished()
        {
            Animator animator = GetComponent<Animator>();
            return (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0));
        }

        public bool CheckAnimationTransition()
        {
            Animator animator = GetComponent<Animator>();
            AnimatorTransitionInfo currentTransition = animator.GetAnimatorTransitionInfo(0);
            return (currentTransition.duration > 0);
        }

        public void RayCastGround()
        {
            PlayerController player = GetComponent<PlayerController>();
            CapsuleCollider collider = GetComponent<CapsuleCollider>();
            float offset = 0.1f;
            Debug.DrawRay(collider.bounds.center, Vector3.down * (collider.bounds.extents.y + offset));
            if(Physics.Raycast(collider.bounds.center, Vector3.down, collider.bounds.extents.y + offset))
            {
                player.airstate = false;
            } else {
                player.airstate = true;
            }
        }

        public void ReverseFacingDirection()
        {
            Animator animator = GetComponent<Animator>();
            int facing_dir = animator.GetInteger("facing_direction");
            facing_dir *= -1;
            animator.SetInteger("facing_direction", facing_dir);
        }

        public void ApplyHorizontalFriction(float friction)
        {
            PlayerController player = GetComponent<PlayerController>();
            int movement_dir = (player.current_speed_h > 0) ? 1 : -1;
            if(Mathf.Abs(player.current_speed_h) > 0)
            {
                float newSpeed = player.current_speed_h - friction * Time.deltaTime * movement_dir;
                
                if(newSpeed > 0 && movement_dir > 0 || newSpeed < 0 && movement_dir < 0)
                {
                    player.current_speed_h = newSpeed;
                }
            }
        }

        public void ApplyAerialDrift(float drift)
        {
            PlayerController player = GetComponent<PlayerController>();
            Animator animator = GetComponent<Animator>();
            int inputDir = GetDirectionHeld();
            if(inputDir != 0)
            {
                float newSpeed = player.current_speed_h + drift * Time.deltaTime * inputDir;
                player.current_speed_h = newSpeed;
                if(Mathf.Abs(newSpeed) < player.max_airdriftspeed)
                {
                    player.current_speed_h = newSpeed;
                } else if (Mathf.Abs(newSpeed) > player.max_airdriftspeed)
                {
                    if(newSpeed > 0)
                    {
                        player.current_speed_h = player.max_airdriftspeed;
                    } else {
                        player.current_speed_h = -1*player.max_airdriftspeed;
                    }
                }
            }
        }

        public void ApplyGravity(float gravity)
        {
            PlayerController player = GetComponent<PlayerController>();
            player.current_speed_v -= gravity * Time.deltaTime; 
        }

        public void EnterIdle()
        {
            this.enabled = false;
            GetComponent<Idle>().enabled = true;
        }
        public void EnterFall()
        {
            print("enter fall");
            this.enabled = false;
            GetComponent<Fall>().enabled = true;
        }
        public void EnterWalk()
        {
            this.enabled = false;
            GetComponent<Walk>().enabled = true;
        }
        public void EnterRun()
        {
            this.enabled = false;
            GetComponent<Run>().enabled = true;
        }
        public void EnterRunBrake()
        {
            this.enabled = false;
            GetComponent<RunBrake>().enabled = true;
        }
        public void EnterLanding()
        {
            print("enter landing");
            this.enabled = false;
            GetComponent<Landing>().enabled = true;
        }
    }
}