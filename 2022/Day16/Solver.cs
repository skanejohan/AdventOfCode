using CSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2022.Day16
{
    public static class Solver
    {
        // This ugly beast gave me the correct answers, even though it took a while... It should really be re-written,
        // see e.g. https://github.com/encse/adventofcode/blob/master/2022/Day16/Solution.cs for ideas.
        public static long Part1()
        {
            LoadData("testdata.txt");
            var currentState = new State("AA", "AA", new HashSet<string>(), 30, 0);

            var maxReleased = 0L;
            var stack = new Stack<(State, int)>();
            var visited = new HashSet<string>();
            stack.Push((currentState, 0));
            //visited.Add(currentState.ToStringWithoutTime());
            while (stack.Any())
            {
                var (state, depth) = stack.Pop();
                if (state.PressureReleased > maxReleased)
                {
                    maxReleased = state.PressureReleased;
                    Console.WriteLine($"{depth} - {state} - {maxReleased}");
                }
                if (depth < 30 && state.PressureReleased + Heuristic(state) > maxReleased)
                {
                    foreach (var n in GetNeighbors(state))
                    {
                        if (!visited.Contains(n.ToStringWithoutTime()))
                        {
                            stack.Push((n, depth + 1));
                            //visited.Add(n.ToStringWithoutTime());
                        }
                    }
                }
                visited.Add(currentState.ToStringWithoutTime());
            }
            return maxReleased;
        }

        public static long Part2()
        {
            LoadData("data.txt");
            var currentState = new State("AA", "AA", new HashSet<string>(), 26, 0);

            var maxReleased = 0L;
            var stack = new Stack<(State, int)>();
            var visited = new HashSet<string>();
            stack.Push((currentState, 0));
            //visited.Add(currentState.ToStringWithoutTime());
            while (stack.Any())
            {
                var (state, depth) = stack.Pop();
                if (state.PressureReleased > maxReleased)
                {
                    maxReleased = state.PressureReleased;
                    Console.WriteLine($"{depth} - {state} - {maxReleased}");
                }
                if (depth < 26 && state.PressureReleased + Heuristic(state) > maxReleased)
                {
                    foreach (var n in GetNeighbors2(state))
                    {
                        if (!visited.Contains(n.ToStringWithoutTime()))
                        {
                            stack.Push((n, depth + 1));
                            //visited.Add(n.ToStringWithoutTime());
                        }
                    }
                }
                visited.Add(currentState.ToStringWithoutTime());
            }
            return maxReleased;
        }

        private static IEnumerable<State> GetNeighbors(State currentState)
        {
            var pq = new PriorityQueue<State, int>();
            var remainingMinutes = currentState.RemainingMinutes - 1;

            // Can we open the valve?
            if (!currentState.OpenValves.Contains(currentState.Valve))
            {
                var flow = data[currentState.Valve].Flow;
                if (flow > 0)
                {
                    var openValves = new HashSet<string>(currentState.OpenValves);
                    openValves.Add(currentState.Valve);
                    var pressureReleased = flow * remainingMinutes;
                    //yield return new State(currentState.Valve, openValves, remainingMinutes, currentState.PressureReleased + pressureReleased);
                    pq.Enqueue(new State(currentState.Valve, currentState.ElephantValve, openValves, remainingMinutes, currentState.PressureReleased + pressureReleased), 0);
                }
            }

            // Move to the neighboring valves
            foreach (var valve in data[currentState.Valve].Neighbors)
            {
                var flow = currentState.OpenValves.Contains(valve) ? data[valve].Flow : 0;
                //yield return new State(valve, currentState.OpenValves, remainingMinutes, currentState.PressureReleased);
                pq.Enqueue(new State(valve, currentState.ElephantValve, currentState.OpenValves, remainingMinutes, currentState.PressureReleased), int.MaxValue - flow);
            }

            while (pq.Count > 0)
            {
                yield return pq.Dequeue();
            }
        }

        private static IEnumerable<State> GetNeighbors2(State currentState)
        {
            var pq = new PriorityQueue<State, int>();
            var remainingMinutes = currentState.RemainingMinutes - 1;

            var elephantMoveStates = new List<State>();

            if (!currentState.OpenValves.Contains(currentState.ElephantValve) && currentState.ElephantValve != currentState.Valve)
            {
                var flow = data[currentState.ElephantValve].Flow;
                if (flow > 0)
                {
                    var openValves = new HashSet<string>(currentState.OpenValves);
                    openValves.Add(currentState.ElephantValve);
                    var pressureReleased = flow * remainingMinutes;
                    elephantMoveStates.Add(new State(currentState.Valve, currentState.ElephantValve, openValves, remainingMinutes, currentState.PressureReleased + pressureReleased));
                }
            }

            foreach (var valve in data[currentState.ElephantValve].Neighbors)
            {
                elephantMoveStates.Add(new State(currentState.Valve, valve, currentState.OpenValves, remainingMinutes, currentState.PressureReleased));
            }

            // Can I open the valve?
            if (!currentState.OpenValves.Contains(currentState.Valve))
            {
                var flow = data[currentState.Valve].Flow;
                if (flow > 0)
                {
                    var openValves = new HashSet<string>(currentState.OpenValves);
                    openValves.Add(currentState.Valve);
                    var pressureReleased = flow * remainingMinutes;
                    foreach (var es in elephantMoveStates)
                    {
                        pq.Enqueue(new State(currentState.Valve, es.ElephantValve, openValves.Union(es.OpenValves).ToHashSet(), remainingMinutes, es.PressureReleased + pressureReleased), 0);
                    }
                }
            }

            // Move to the neighboring valves
            foreach (var valve in data[currentState.Valve].Neighbors)
            {
                var flow = currentState.OpenValves.Contains(valve) ? data[valve].Flow : 0;
                //yield return new State(valve, currentState.OpenValves, remainingMinutes, currentState.PressureReleased);
                foreach (var es in elephantMoveStates)
                {
                    pq.Enqueue(new State(valve, es.ElephantValve, es.OpenValves, remainingMinutes, es.PressureReleased), int.MaxValue - flow);
                }
            }

            while (pq.Count > 0)
            {
                yield return pq.Dequeue();
            }
        }

        private static long Heuristic(State currentState) // What is the upper limit of what we can do from now?
        {
            var flows = orderedFlowRates
                .Where(fr => !currentState.OpenValves.Contains(fr.Valve))
                .Take((currentState.RemainingMinutes + 1) / 2)
                .Select(fr => fr.Flow);
            var sum = 0;
            var mins = currentState.RemainingMinutes;
            while(mins > 0 && flows.Any())
            {
                sum += mins * flows.First();
                flows = flows.Skip(1);
                mins -= 2;
            }
            return sum;
        }

        private static void LoadData(string fileName)
        {
            orderedFlowRates.Clear();
            data = new Dictionary<string, (int, string[])>();
            foreach (var line in new DataLoader(2022, 16).ReadStrings(fileName))
            {
                var l = line["Valve ".Length..];
                var parts = l.Split(" has flow rate=");
                var node = parts[0];
                var tl = parts[1].Contains("tunnels") ? "tunnels lead" : "tunnel leads";
                var v = parts[1].Contains("valves") ? "valves" : "valve";
                parts = parts[1].Split($"; {tl} to {v} ");
                var flowRate = int.Parse(parts[0]);
                var targetNodes = parts[1].Split(", ");
                data[node] = (flowRate, targetNodes);
                if (flowRate > 0)
                {
                    orderedFlowRates.Add((flowRate, node));
                }
            }
            orderedFlowRates = orderedFlowRates.OrderBy(fr => fr.Flow).ToList();
            orderedFlowRates.Reverse();
        }

        private static Dictionary<string, (int Flow, string[] Neighbors)> data = new();
        private static List<(int Flow, string Valve)> orderedFlowRates = new();

        class State
        {
            public string Valve { get; }
            public string ElephantValve { get; }
            public int RemainingMinutes { get; }
            public long PressureReleased { get; }
            public HashSet<string> OpenValves { get; }
            public State(string valve, string elephantValve, HashSet<string> openValves, int remainingMinutes, long pressureReleased)
            {
                Valve = valve;
                ElephantValve = elephantValve;
                RemainingMinutes = remainingMinutes;
                PressureReleased = pressureReleased;
                OpenValves = openValves;
            }
            public override string ToString()
            {
                return $"{Valve} - {ElephantValve} - {RemainingMinutes} - {PressureReleased} - {string.Join(',', OpenValves)}";
            }
            public string ToStringWithoutTime()
            {
                var openValves = OpenValves.ToList().OrderBy(s => s);
                return $"{Valve} - {ElephantValve} - {string.Join(',', openValves)}";
            }
        }
    }
}
