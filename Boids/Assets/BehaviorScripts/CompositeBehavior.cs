﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FilteredFlockBehavior
{
    public FlockBehavior[] behaviors;
    public float[] weights;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (weights.Length != behaviors.Length)
        {
            Debug.Log("Data mismatch in " + name, this);
            return Vector2.zero;
        }

        //Setup movement
        Vector2 move = Vector2.zero;

        //Iterate through the behaviors
        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector2 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];

            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }

        return move;
    }
}
