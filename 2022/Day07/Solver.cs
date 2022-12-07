using CSharpLib;
using CSharpLib.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace Y2022.Day07
{
    public static class Solver
    {
        public static long Part1()
        {
            var tree = CreateTree(LoadData("data.txt"));
            return GetDirSizes(tree).Where(s => s <= 100000).Sum();
        }

        public static long Part2()
        {
            var tree = CreateTree(LoadData("data.txt"));
            var unusedSpace = 70000000 - tree.Value.Item3;
            var sizeToDelete = 30000000 - unusedSpace;
            return GetDirSizes(tree).Where(size => size >= sizeToDelete).Min();
        }

        private static Node<(string, bool, int)> CreateTree(IEnumerable<string> data)
        {
            var tree = new Node<(string, bool, int)>(null, ("", true, 0));
            var currentNode = tree;
            foreach (var line in data)
            {
                if (line == "$ cd ..")
                {
                    currentNode = currentNode?.Parent;
                }
                else if (line.StartsWith("$ cd "))
                {
                    var child = new Node<(string, bool, int)>(currentNode, (line.Substring(5), true, 0));
                    currentNode?.Children.Add(child);
                    currentNode = child;
                }
                else if (line != "$ ls" && !line.StartsWith("dir "))
                {
                    var parts = line.Split(' ');
                    currentNode?.Children.Add(new Node<(string, bool, int)>(currentNode, (parts[1], false, int.Parse(parts[0]))));
                }
            }
            CalcDirSizes(tree);
            return tree;
        }

        private static IEnumerable<string> LoadData(string fileName)
        {
            return new DataLoader(2022, 7).ReadStrings(fileName).Skip(1);
        }

        private static void CalcDirSizes(Node<(string, bool, int)> node)
        {
            foreach (var c in node.Children.Where(n => n.Value.Item2))
            {
                CalcDirSizes(c);
            }

            if (node.Value.Item2)
            {
                var size = node.Children.Select(c => c.Value.Item3).Sum();
                node.Value = (node.Value.Item1, true, size);
            }
        }

        private static IEnumerable<int> GetDirSizes(Node<(string, bool, int)> node)
        {
            var result = new List<int>();
            if (node.Value.Item2)
            {
                result.Add(node.Value.Item3);
                foreach (var c in node.Children)
                {
                    result.AddRange(GetDirSizes(c));
                }
            }
            return result;
        }
    }
}
