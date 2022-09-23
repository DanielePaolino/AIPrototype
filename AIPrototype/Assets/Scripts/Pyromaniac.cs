using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyromaniac : Agent
{
    [SerializeField] private Transform houseToBurn;
    [SerializeField] private Transform[] locationsToBurn;
    [SerializeField] private Transform gasoline;
    [SerializeField] private Transform escapeLocation;

    private int roomToBurn = 0;

    protected override void Start()
    {
        base.Start();
        roomToBurn = Random.Range(0, locationsToBurn.Length);
        BuildTree("Pyromaniac, Burn Everything!");

    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void BuildTree(string treeName)
    {
        base.BuildTree(treeName);

        Sequence burnHouse = new Sequence("SetUp Fire");

        Leaf takeGasoline = new Leaf("Take Gasoline", TakeGasoline);
        Leaf goToHouse = new Leaf("Go to house", GoToHouse);
        Leaf burnRandomRoom = new Leaf("Burn random room", BurnRandomRoom);
        Leaf goToStartPos = new Leaf("Go To Start Pos", GoToStartPos);

        burnHouse.AddChild(takeGasoline);
        burnHouse.AddChild(goToHouse);
        burnHouse.AddChild(burnRandomRoom);
        burnHouse.AddChild(goToStartPos);

        tree.AddChild(burnHouse);

        tree.PrintTree();
    }

    private NodeStatus TakeGasoline()
    {
        MoveAgentToLocation(gasoline.position);
        if (AgentHasArrivedAtLocation())
        {
            return NodeStatus.SUCCESS;
        }
        return NodeStatus.RUNNING;
    }

    private NodeStatus GoToHouse()
    {
        MoveAgentToLocation(houseToBurn.position);
        if (AgentHasArrivedAtLocation())
        {
            return NodeStatus.SUCCESS;
        }
        return NodeStatus.RUNNING;
    }

    private NodeStatus BurnRandomRoom()
    {
        MoveAgentToLocation(locationsToBurn[roomToBurn].position);
        if (AgentHasArrivedAtLocation())
        {
            LevelController.Instance.HouseIsBurning = true;
            LevelController.Instance.BurningRoom = locationsToBurn[roomToBurn].position;
            LevelController.Instance.StartFire();
            return NodeStatus.SUCCESS;
        }
   
        return NodeStatus.RUNNING;
    }

    private NodeStatus GoToStartPos()
    {
        MoveAgentToLocation(escapeLocation.position);
        if (AgentHasArrivedAtLocation())
        {
            return NodeStatus.SUCCESS;
        }
        return NodeStatus.RUNNING;
    }


}
