using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : Node
{
    public BehaviourTree(string name) : base(name)
    {
        this.Name = name;
    }
    public void PrintTree()
    {
        if (this == null)
        {
            Debug.Log("BehaviourTree - PrintTree - Tree is null");
            return;
        }
           
        if(Children.Count == 0)
        {
            Debug.Log("BehaviourTree - PrintTree - Tree is empty");
            return;
        }

        Debug.Log("BehaviourTree - Children: " + Children.Count);
            
        string print = "";
        Stack<Node> stack = new Stack<Node>();
        Node currentNode = this;
        currentNode.SetLevel(0);
        stack.Push(currentNode);
        while (stack.Count != 0)
        {
            currentNode = stack.Pop();
            print += new string('-', currentNode.Level) + currentNode.Name + "\n";
            for (int i = currentNode.Children.Count - 1; i >= 0; i--)
            {
                Node nextNode = currentNode.Children[i];
                nextNode.SetLevel(currentNode.Level + 1);
                stack.Push(nextNode);
            }
        }

        Debug.Log("BehaviourTree - PrintTree - Tree: " + print);

    }

    public override NodeStatus Process()
    {
        return Children[currentChild].Process();
    }
}
