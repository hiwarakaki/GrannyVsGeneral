using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class ThrowItem : MonoBehaviour
{
    private bool aldATK;
    public int Damage;
    public GameObject granny;
    public GameObject general;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!aldATK)
        {
            if (collision.gameObject.CompareTag("Player")) 
                aldATK = true;
                ChargeThrow player = collision.gameObject.GetComponent<ChargeThrow>();
                SkeletonAnimation skeletonAnimation = collision.gameObject.GetComponent<SkeletonAnimation>();

                if (player != null)
                {
                    Vector2 hitPoint = collision.ClosestPoint(transform.position); 
                    float characterCenterY = collision.bounds.center.y +0.6f;
                Debug.DrawLine(hitPoint, collision.bounds.center + new Vector3(0,0.6f,0), Color.red, 1f);
                if (skeletonAnimation != null)
                    {
                    Debug.Log("Hit Point: " + hitPoint.y + " | Character Center: " + characterCenterY);
                    if (hitPoint.y >= characterCenterY)
                        {
                            skeletonAnimation.AnimationState.SetAnimation(0, "Sleep UnFriendly", false); 
                            skeletonAnimation.AnimationState.AddAnimation(0, "Idle Friendly 1", true, 0); 
                            player.TakeDamage(Damage + 5);
                        }
                        else if (hitPoint.y < characterCenterY) 
                        {
                            skeletonAnimation.AnimationState.SetAnimation(0, "Moody Friendly", false);
                            skeletonAnimation.AnimationState.AddAnimation(0, "Idle Friendly 1", true, 0);
                            player.TakeDamage(Damage);
                        }
                        if(player.PlayerHealth <= 0)
                        {
                            skeletonAnimation.AnimationState.SetAnimation(0, "Moody UnFriendly", true);
                        }
                    }
                }

                Destroy(gameObject);
            }
            else if (collision.gameObject.CompareTag("floor"))  
            {
                SkeletonAnimation skeletonAnimation;
                if (Gamemanager.instance.currentTurn == 0)
                {
                    skeletonAnimation = granny.GetComponent<SkeletonAnimation>(); 
                }
                else
                {
                    skeletonAnimation = general.GetComponent<SkeletonAnimation>();
                }
                aldATK = true;
                skeletonAnimation.AnimationState.SetAnimation(0, "Sleep Friendly", false); 
                skeletonAnimation.AnimationState.AddAnimation(0, "Idle Friendly 1", true, 0); 
                Destroy(gameObject);
            }
        }
    }
