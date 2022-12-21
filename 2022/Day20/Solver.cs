using CSharpLib;
using System.Collections.Generic;
using System.Linq;

namespace Y2022.Day20
{
    public static class Solver
    {
        public static long Part1()
        {
            var linkedList = new LinkedList<long>(LoadData("data.txt"));
            var nodesInOriginalOrder = new List<LinkedListNode<long>>();
            var n = linkedList.First;
            while (n != null)
            {
                nodesInOriginalOrder.Add(n);
                n = n.Next;
            }

            foreach (var node in nodesInOriginalOrder)
            {
                Move(node);
            }
            var numbers = linkedList.ToList();
            var zero = numbers.IndexOf(0);

            return
                numbers[(1000 + zero) % numbers.Count] +
                numbers[(2000 + zero) % numbers.Count] +
                numbers[(3000 + zero) % numbers.Count];
        }

        public static long Part2()
        {
            var linkedList = new LinkedList<long>(LoadData("data.txt").Select(l => l * 811589153));
            var nodesInOriginalOrder = new List<LinkedListNode<long>>();
            var n = linkedList.First;
            while (n != null)
            {
                nodesInOriginalOrder.Add(n);
                n = n.Next;
            }

            for (int i = 0; i < 10; i++)
            {
                foreach (var node in nodesInOriginalOrder)
                {
                    Move(node);
                }
            }
            var numbers = linkedList.ToList();
            var zero = numbers.IndexOf(0);

            return
                numbers[(1000 + zero) % numbers.Count] +
                numbers[(2000 + zero) % numbers.Count] +
                numbers[(3000 + zero) % numbers.Count];
        }

        private static void Move(LinkedListNode<long> node)
        {
            var moves = node.Value;
            if (moves > 0)
            {
                MoveRight(node, moves);
            }
            else if (moves < 0)
            {
                MoveLeft(node, -moves);
            }
        }

        private static void MoveRight(LinkedListNode<long> node, long moves)
        {
            var list = node.List;
            var dummy = list.AddBefore(node, 0);
            list.Remove(node);

            var timesPassingDummy = moves / list.Count;
            var m = (moves % list.Count + timesPassingDummy) % (list.Count - 1);
            var n = dummy;
            for (var i = 0; i < m; i++)
            {
                n = n.Next ?? n.List.First;
            }
            list.AddAfter(n, node);
            list.Remove(dummy);
        }

        private static void MoveLeft(LinkedListNode<long> node, long moves)
        {
            var list = node.List;
            var dummy = list.AddAfter(node, 0);
            list.Remove(node);

            var timesPassingDummy = moves / list.Count;
            var m = (moves % list.Count + timesPassingDummy) % (list.Count - 1);
            var n = dummy;
            for (var i = 0; i < m; i++)
            {
                n = n.Previous ?? n.List.Last;
            }
            list.AddBefore(n, node);
            list.Remove(dummy);
        }

        private static IEnumerable<long> LoadData(string fileName)
        {
            return new DataLoader(2022, 20).ReadLongs(fileName);
        }

        private static IEnumerable<long> TestData()
        {
            yield return 1;
            yield return 2;
            yield return -3;
            yield return 3;
            yield return -2;
            yield return 0;
            yield return 4;
        }
    }
}
