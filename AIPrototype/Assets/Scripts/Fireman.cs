using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fireman : Agent
{
    [SerializeField] private Transform waterPump1;
    [SerializeField] private Transform waterPump2;
    [SerializeField] private Transform firemenTruck;
    [SerializeField] private Transform burningHouse;

    private Vector3 waterPumpActivatedPosition;

    protected override void Start()
    {
        base.Start();
        BuildTree("FiremanTree, Solve Emergency");
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(Conditions())
            base.Update();
    }

    private bool Conditions()
    {
        return LevelController.Instance.HouseIsBurning && LevelController.Instance.EmergencyCallDone && LevelController.Instance.FiremenTruckHasArrivedAtEmergencyLocation;
    }


    protected override void BuildTree(string treeName)
    {
        base.BuildTree(treeName);
        Sequence extinguishFire = new Sequence("Extinguish Fire!");
        Selector activateWaterPump = new Selector("Activate water pump");

        Leaf activateWaterPump1 = new Leaf("Activate water pump 1", ActivateWaterPump1);
        Leaf activateWaterPump2 = new Leaf("Activate water pump 2", ActivateWaterPump2);
        Leaf goToBurningHouse = new Leaf("Go to burning house", GoToBurningHouse);
        Leaf goToBurningRoom = new Leaf("Go to burning room", GoToBurningRoom);
        Leaf disableWaterPump = new Leaf("Disable Water Pump", DisableWaterPump);
        Leaf returnToFiremenTruck = new Leaf("Return to firemen truck", ReturnToFiremenTruck);

        activateWaterPump.AddChild(activateWaterPump1);
        activateWaterPump.AddChild(activateWaterPump2);

        extinguishFire.AddChild(activateWaterPump);
        extinguishFire.AddChild(goToBurningHouse);
        extinguishFire.AddChild(goToBurningRoom);
        extinguishFire.AddChild(goToBurningHouse);
        extinguishFire.AddChild(disableWaterPump);
        extinguishFire.AddChild(returnToFiremenTruck);

        tree.AddChild(extinguishFire);

        tree.PrintTree();

    }
    private NodeStatus ActivateWaterPump1()
    {
        MoveAgentToLocation(waterPump1.position);
        if (AgentHasArrivedAtLocation())
        {
            waterPumpActivatedPosition = waterPump1.position;
            return NodeStatus.SUCCESS;
        }

        return NodeStatus.RUNNING;
    }

    private NodeStatus ActivateWaterPump2()
    {
        MoveAgentToLocation(waterPump2.position);
        if (AgentHasArrivedAtLocation())
        {
            waterPumpActivatedPosition = waterPump2.position;
            return NodeStatus.SUCCESS;
        }
           

        return NodeStatus.RUNNING;
    }

    private NodeStatus ReturnToFiremenTruck()
    {
        MoveAgentToLocation(firemenTruck.position);
        if (AgentHasArrivedAtLocation())
        {
            LevelController.Instance.FiremanIsReturnedToTruck = true;
            return NodeStatus.SUCCESS;
        }
            

        return NodeStatus.RUNNING;
    }

    private NodeStatus GoToBurningHouse()
    {
        MoveAgentToLocation(burningHouse.position);
        if (AgentHasArrivedAtLocation())
            return NodeStatus.SUCCESS;

        return NodeStatus.RUNNING;
    }

    private NodeStatus GoToBurningRoom()
    {
        MoveAgentToLocation(LevelController.Instance.BurningRoom);
        if (AgentHasArrivedAtLocation())
        {
            LevelController.Instance.ExtinguishFire();
            return NodeStatus.SUCCESS;
        }
            

        return NodeStatus.RUNNING;
    }

    private NodeStatus DisableWaterPump()
    {
        MoveAgentToLocation(waterPumpActivatedPosition);
        if (AgentHasArrivedAtLocation())
        {
            return NodeStatus.SUCCESS;
        }


        return NodeStatus.RUNNING;
    }
}
