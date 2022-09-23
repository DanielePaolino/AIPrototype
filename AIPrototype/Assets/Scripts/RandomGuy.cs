using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGuy : Agent
{
    [SerializeField] private Transform telephone;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        BuildTree("Random Guy, Make Emergency Call");
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (LevelController.Instance.HouseIsBurning)
            base.Update();
    }

    protected override void BuildTree(string treeName)
    {
        base.BuildTree(treeName);
        Leaf makeEmergencyCall = new Leaf("Make Emergency Call", MakeEmergencyCall);

        tree.AddChild(makeEmergencyCall);
        tree.PrintTree();

    }
    private NodeStatus MakeEmergencyCall()
    {
        MoveAgentToLocation(telephone.position);
        if (AgentHasArrivedAtLocation())
        {
            LevelController.Instance.EmergencyCallDone = true;
            return NodeStatus.SUCCESS;
        }
        return NodeStatus.RUNNING;
    }

}
