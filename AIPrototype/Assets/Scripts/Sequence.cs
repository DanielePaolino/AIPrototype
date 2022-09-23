using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    public Sequence(string name) : base(name)
    {
        this.Name = name;
    }


    public override NodeStatus Process()
    {
        NodeStatus status = Children[currentChild].Process();

        switch (status)
        {
            case NodeStatus.SUCCESS:
                currentChild++;
                if (currentChild >= Children.Count)
                {
                    currentChild = 0;
                    return NodeStatus.SUCCESS;
                }
                return NodeStatus.RUNNING;
            
            case NodeStatus.RUNNING:
                return NodeStatus.RUNNING;
            case NodeStatus.FAILED:
                return NodeStatus.FAILED;
            default:
                return NodeStatus.RUNNING;
        }
    }

}
