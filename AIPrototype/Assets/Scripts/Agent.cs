using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;

public abstract class Agent : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected AgentState currentAgentState = AgentState.Waiting;
    protected BehaviourTree tree;
    protected NodeStatus status = NodeStatus.RUNNING;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (tree != null && status == NodeStatus.RUNNING)
        {
            status = tree.Process();
        }

    }

    protected void MoveAgentToLocation(Vector3 location)
    {
        if(currentAgentState == AgentState.Waiting)
        {
            agent.SetDestination(location);
            currentAgentState = AgentState.Working;
        }
    }

    protected bool AgentHasArrivedAtLocation()
    {
        bool hasArrivedAtLocation = false;
        if(agent.remainingDistance < 1.5f && agent.hasPath)
        {
            currentAgentState = AgentState.Waiting;
            hasArrivedAtLocation = true;
        }
        return hasArrivedAtLocation;
    }

    protected virtual void BuildTree(string treeName)
    {
        tree = new BehaviourTree(treeName);
    }

    protected enum AgentState
    {
        Waiting, Working
    }
}
