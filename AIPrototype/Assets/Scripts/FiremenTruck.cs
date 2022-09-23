using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FiremenTruck : Agent
{
    [SerializeField] private Transform emergencyLocation;
    [SerializeField] private GameObject Fireman;

    private Vector3 startPos;
    private bool firemanOnBoard = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        startPos = transform.position;
        BuildTree("Firemen Truck, Go to Emergency");
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (LevelController.Instance.EmergencyCallDone)
            base.Update();
    }

    protected override void BuildTree(string treeName)
    {
        base.BuildTree(treeName);
        Sequence emergency = new Sequence("Emergency");
        Leaf goToEmergencyPosition = new Leaf("Go To Emergency Position", GoToEmergencyPosition);
        Leaf returnToFiremenStation = new Leaf("Return To Firemen Station", ReturnToFiremenStation);

        emergency.AddChild(goToEmergencyPosition);
        emergency.AddChild(returnToFiremenStation);

        tree.AddChild(emergency);
        tree.PrintTree();

    }
    private NodeStatus GoToEmergencyPosition()
    {
        if(!firemanOnBoard)
        {
            Fireman.transform.parent = transform;
            Fireman.gameObject.SetActive(false);
            firemanOnBoard = true;
        }
        
        MoveAgentToLocation(emergencyLocation.position);
        if (AgentHasArrivedAtLocation())
        {
            Fireman.transform.parent = null;
            Fireman.gameObject.SetActive(true);
            firemanOnBoard = false;
            LevelController.Instance.FiremenTruckHasArrivedAtEmergencyLocation = true;
            return NodeStatus.SUCCESS;
        }
        return NodeStatus.RUNNING;
    }

    private NodeStatus ReturnToFiremenStation()
    {
        if(LevelController.Instance.FiremanIsReturnedToTruck)
        {
            if (!firemanOnBoard)
            {
                Fireman.transform.parent = transform;
                Fireman.gameObject.SetActive(false);
                firemanOnBoard = true;
            }

            MoveAgentToLocation(startPos);
            if (AgentHasArrivedAtLocation())
            {
                LevelController.Instance.EmergencyCallDone = false;
                LevelController.Instance.FiremenTruckHasArrivedAtEmergencyLocation = false;
                return NodeStatus.SUCCESS;
            }
           
        }
        return NodeStatus.RUNNING;
    }
}
