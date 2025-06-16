using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Player : Agent
{
    public GameManager gameManager;
    public float moveSpeed = 6f;
    public float jumpVelocity = 12f;
    // private bool buffer;

    public Collider2D sideCollider;
    public Vector2 bottomBoxSize = new Vector2(1f, 0.05f);
    public Vector2 bottomBoxOffset = new Vector2(0, -0.5f);
    public LayerMask blockLayer;
    public LayerMask spikeLayer;
    public Rigidbody2D rb;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        // buffer = false;
    }

    public override void OnActionReceived(ActionBuffers actionBuffers) {
        float jump = actionBuffers.DiscreteActions[0];

        if (jump == 1)
        {
            Jump();
            AddReward(-0.005f);
        }

        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        if (Physics2D.OverlapBox((Vector2)transform.position, new Vector2(1.05f, 1.05f), 0f, spikeLayer) != null)
            EndEpisode();

        if (MaxStep > 0) AddReward(10f / MaxStep);
    }

    public override void Heuristic(in ActionBuffers actionsOut) {
        int jump = 0;
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            jump = 1;
        }

        actionsOut.DiscreteActions.Array[0] = jump;
    }

    public override void OnEpisodeBegin() {
        transform.position = new Vector3(0f, 0f, 0f);
        rb.velocity = new Vector2(moveSpeed, 0f);
    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation((Vector2)(transform.position));

        Vector2Int roundedPos = new Vector2Int((int)Mathf.Round(transform.position.x), (int)Mathf.Round(transform.position.y));
        int[,] objectSurroundings = new int[5, 7];
        for (int i = 0; i < gameManager.objectPlaces.Count; i++) {
            int dx = (int)gameManager.objectPlaces[i].x - roundedPos.x;
            int dy = (int)gameManager.objectPlaces[i].y - roundedPos.y;
        
            if (dx >= 0 && dx < 5 && dy >= -3 && dy <= 3) {
                int arrayX = dx;
                int arrayY = 3 - dy;

                objectSurroundings[arrayX, arrayY] = gameManager.objectTypes[i];
            }
        }
        
        for (int i = 0; i < 5; i++) {
            for (int j = 0; j < 7; j++) {
                sensor.AddObservation((float)objectSurroundings[i, j]);
            }
        }

        Vector2[] rayDirections = new Vector2[] {Vector2.right, (Vector2.right + Vector2.up).normalized, (Vector2.right + Vector2.down).normalized};
        foreach (Vector2 dir in rayDirections) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 10f);
            float distance = hit.collider ? hit.distance : 10f;
            sensor.AddObservation(distance / 10f);
        }
    }

    void Jump()
    {
        if (Physics2D.OverlapBox((Vector2)transform.position + bottomBoxOffset, bottomBoxSize, 0f, blockLayer) != null)
        {
            Debug.Log("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            // buffer = false;
        }
    }

    void Update()
    {
        // rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        // if (Physics2D.OverlapBox((Vector2)transform.position, new Vector2(1f, 1f), 0f, spikeLayer) != null)
        //     EndEpisode();
        if (Input.GetKeyDown(KeyCode.R))
            EndEpisode();
    }
}