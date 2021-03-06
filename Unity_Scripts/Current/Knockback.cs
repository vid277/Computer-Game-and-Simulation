/*

@Author Vidyoot Senthilvenkatesh
@Version 2/21/2022

*/using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float pushforcething;
    public float time;
    public float damage;

    /**
     * Destroys breakable objects when it is hit
     * @param other This is hitbox of the object.
     **/
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("hitboxes")){
            other.GetComponent<pot>().destroy();
        }
            /**
             * Defines the knockback mechanic for players and enemies. When entities are hit, they are moved back a distance based on the entity that hit them.
            **/
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player")){
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            Vector2 difference = hit.transform.position - transform.position;
            difference = difference.normalized * pushforcething;
            hit.AddForce(difference, ForceMode2D.Impulse);
            if(hit != null){
        
                if (other.gameObject.CompareTag("Enemy") && other.isTrigger){
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().knock(hit, time, damage);
                }
                if(other.CompareTag("Player") && !other.gameObject.CompareTag("Enemy")){
                    if(other.GetComponent<Player_move>().currentState != PlayerState.stagger){
                        hit.GetComponent<Player_move>().currentState =  PlayerState.stagger;
                        other.GetComponent<Player_move>().knock(time, damage);
                    }
                }
            }
        }
    }
}
