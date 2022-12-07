using System;
using System.Collections.Generic;

namespace CSharpLib.DataStructures
{
    public class Node<T>
    {
        public T Value { get; set; }

        public List<Node<T>> Children { get; set; }

        public Node<T>? Parent { get; set; }

        public Node(Node<T> parent, T value)
        {
            Children = new List<Node<T>>();
            Parent = parent;
            Value = value;
        }

        public override string ToString()
        {
            var s = "";
            ToS(this, 0);
            return s;

            void ToS(Node<T> node, int indent)
            {
                if (node.Value != null)
                {
                    s += new String(' ', indent) + node.Value + '\n';
                    foreach (var c in node.Children)
                    {
                        ToS(c, indent + 2);
                    }
                }
            }
        }
    }

}
