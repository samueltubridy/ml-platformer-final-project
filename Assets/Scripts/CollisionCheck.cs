using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public LayerMask blockLayer;
    public Player player;
    public Rigidbody2D rb;

    void Start() {
        player = transform.parent.GetComponent<Player>();
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.velocity.x == 0 && ((1 << collision.gameObject.layer) & blockLayer.value) != 0)
        {
            Debug.Log("You crashed into a block and tragically passed away");
            player.EndEpisode();
        }
    }
}