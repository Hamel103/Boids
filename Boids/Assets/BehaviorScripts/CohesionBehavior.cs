using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //If no neighbors, no adjustment needed
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        //Add all of the neighbor's points together and average them
        Vector2 cohesionMove = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= context.Count;

        //Create offset from the agent's position
        cohesionMove -= (Vector2)agent.transform.position;
        return cohesionMove;
    }
}
