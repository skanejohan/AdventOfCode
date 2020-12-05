using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Common
{
    internal static class AreaTraversal<T>
    {
        public static Node Bfs(Area<T> area, (int x, int y) start, Func<T, bool> isTraversable)
        {
            traversableSpaces = new HashSet<(int x, int y)>(area.AllSpaces(isTraversable));
            root = new Node(start, null);
            traversableSpaces.Remove(start);
            AddChildren(area, root, isTraversable);
            return root;

        }

        public static Node FindPosition((int x, int y) pos, Node n)
        {
            if (n.Position == pos)
            {
                return n;
            }
            foreach (var c in n.Children)
            {
                var cn = FindPosition(pos, c);
                if (cn != null)
                {
                    return cn;
                }
            }
            return null;
        }

        public static long DepthOf(Node n, long depth = 0)
            => n.Parent == null ? depth : DepthOf(n.Parent, depth + 1);

        public static long MaxDepth(Node n, long depth = 1)
            => n.Children.Count == 0 ? depth : n.Children.Max(nn => MaxDepth(nn, depth + 1));

        private static void AddChildren(Area<T> area, Node parent, Func<T, bool> isTraversable)
        {
            foreach (var neighbor in area.NeighborsOf(parent.Position, isTraversable).Where(n => traversableSpaces.Contains(n)))
            {
                parent.Children.Add(new Node(neighbor, parent));
                traversableSpaces.Remove(neighbor);
            }
            foreach (var child in parent.Children)
            {
                AddChildren(area, child, isTraversable);
            }
        }

        public class Node
        {
            public (int x, int y) Position { get; private set; }
            public List<Node> Children { get; private set; }
            public Node Parent { get; private set; }

            public Node((int x, int y) position, Node parent)
            {
                Position = position;
                Children = new List<Node>();
                Parent = parent;
            }
        }

        private static Node root;
        private static HashSet<(int x, int y)> traversableSpaces;
    }
}
