using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    public Selector(string name) : base(name)
    {
        this.Name = name;
    }

    public override NodeStatus Process()
    {
        NodeStatus status = Children[currentChild].Process();

        if (status == NodeStatus.SUCCESS)
        {
            currentChild = 0;
            return NodeStatus.SUCCESS;
        }
        if(status == NodeStatus.RUNNING)
            return NodeStatus.RUNNING;

        currentChild++;
        if(currentChild >= Children.Count)
        {
            currentChild = 0;
            return NodeStatus.FAILED;
        }

        return NodeStatus.RUNNING;
    }
}
