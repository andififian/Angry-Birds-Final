using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBird : Bird
{
    [SerializeField]
    public float blastRadius;
    public float blastForce;
    public LayerMask LayerToHit;
    private CircleCollider2D _blastCollider;
    
    void Explode(){
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position,blastRadius, LayerToHit);
        foreach(Collider2D obj in objects){
            Vector2 direction = obj.transform.position - transform.position;

            obj.GetComponent<Rigidbody2D>().AddForce(direction * blastForce);
        }
    }

    public override void OnTap(){
        Explode();
    }


}
