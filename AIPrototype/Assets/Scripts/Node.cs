using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node
{
    protected NodeStatus status;
    public List<Node> Children { get; protected set; }
    protected int currentChild = 0;
    public string Name { get; protected set; }
    public int Level { get; private set; }

    public Node(string name)
    {
        this.Name = name;
        Level = 0;
        Children = new List<Node>();
    }

    public virtual NodeStatus Process()
    {
        return Children[currentChild].Process();
    }

    public void AddChild(Node child)
    {
        Children.Add(child);
    }

    public void SetLevel(int level)
    {
        Level = level;
    }

  
}

public enum NodeStatus
{
    SUCCESS, RUNNING, FAILED
}
