using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
    public delegate NodeStatus Tick();
    public Tick ProcessMethod;
    public Leaf(string name, Tick execute) : base(name)
    {
        this.Name = name;
        ProcessMethod = execute;
    }

    public override NodeStatus Process()
    {
        if (ProcessMethod != null)
            return ProcessMethod();

        return NodeStatus.FAILED;
    }



}
