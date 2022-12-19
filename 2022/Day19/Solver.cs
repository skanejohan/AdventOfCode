using CSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2022.Day19
{
    public static class Solver
    {
        public static long Part1()
        {
            var costss = LoadData("data.txt");
            var state = new State(0, 0, 0, 0, 1, 0, 0, 0, 0);

            var result = 0;
            var blueprintId = 1;
            foreach (var costs in costss)
            {
                result += blueprintId * Solve(state, costs, 24);
                blueprintId++;
            }

            return result;
        }

        public static long Part2()
        {
            var costss = LoadData("data.txt").Take(3);
            var state = new State(0, 0, 0, 0, 1, 0, 0, 0, 0);

            var result = 1;
            foreach (var costs in costss)
            {
                result *= Solve(state, costs, 32);
            }
            return result;
        }

        private static int Solve(State state, Costs costs, int minutes)
        {
            var maxGeodes = 0;
            var visited = new HashSet<State>();
            var stack = new Stack<State>();
            visited.Add(state);
            stack.Push(state);

            while (stack.Any())
            {
                var st = stack.Pop();
                if (st.geode > maxGeodes)
                {
                    maxGeodes = st.geode;
                }
                if (st.steps < minutes)
                {
                    foreach (var n in Neighbors(st, costs, minutes))
                    {
                        if (!visited.Contains(n))
                        {
                            stack.Push(n);
                            visited.Add(n);
                        }
                    }
                }
                visited.Add(st);
            }
            return maxGeodes;
        }

        private static IEnumerable<State> Neighbors(State state, Costs costs, int minutes)
        {
            State s;
            var neighbors = new List<State>();
            var ignoreClayAndOreBots =
                state.ore + state.oreRobots >= costs.geodeRobotOre && state.obsidian + state.obsidianRobots >= costs.geodeRobotObsidian &&
                state.ore + state.oreRobots >= costs.obsidianRobotOre && state.clay + state.clayRobots >= costs.obsidianRobotClay;

            // If we have enough to immediately build a geode-cracking robot, do only that
            if (state.obsidian >= costs.geodeRobotObsidian && state.ore >= costs.geodeRobotOre)
            {
                s = produceOnce(state);
                neighbors.Add(s with
                {
                    geodeRobots = s.geodeRobots + 1,
                    ore = s.ore - costs.geodeRobotOre,
                    obsidian = s.obsidian - costs.geodeRobotObsidian
                });
                return neighbors;
            }

            // We can build an ore-collecting robot...
            if (!ignoreClayAndOreBots)
            {
                s = produceUntil(state, s => s.ore >= costs.oreRobotOre);
                if (s.steps < minutes)
                {
                    s = produceOnce(s);
                    neighbors.Add(s with
                    {
                        oreRobots = s.oreRobots + 1,
                        ore = s.ore - costs.oreRobotOre
                    });
                }
            }

            // ...or we can build a clay-collecting robot...
            if (!ignoreClayAndOreBots)
            {
                s = produceUntil(state, s => s.ore >= costs.clayRobotOre);
                if (s.steps < minutes)
                {
                    s = produceOnce(s);
                    neighbors.Add(s with
                    {
                        clayRobots = s.clayRobots + 1,
                        ore = s.ore - costs.clayRobotOre
                    });
                }
            }

            // ...or we can build an obsidian-collecting robot (if we can collect clay)...
            if (state.clayRobots > 0)
            {
                s = produceUntil(state, s => s.ore >= costs.obsidianRobotOre && s.clay >= costs.obsidianRobotClay);
                if (s.steps < minutes)
                {
                    s = produceOnce(s);
                    neighbors.Add(s with
                    {
                        obsidianRobots = s.obsidianRobots + 1,
                        ore = s.ore - costs.obsidianRobotOre,
                        clay = s.clay - costs.obsidianRobotClay
                    });
                }
            }

            // ...or we can build a geode-cracking robot (if we can collect obsidian)...
            if (state.obsidianRobots > 0)
            {
                s = produceUntil(state, s => s.ore >= costs.geodeRobotOre && s.obsidian >= costs.geodeRobotObsidian);
                if (s.steps < minutes)
                {
                    s = produceOnce(s);
                    neighbors.Add(s with
                    {
                        geodeRobots = s.geodeRobots + 1,
                        ore = s.ore - costs.geodeRobotOre,
                        obsidian = s.obsidian - costs.geodeRobotObsidian
                    });
                }
            }

            // ...or we can keep cracking geodes
            if (state.geodeRobots > 0)
            {
                s = produceUntil(state, s => s.steps == minutes);
                neighbors.Add(s);
            }

            return neighbors.Where(n => n.steps <= minutes);

            State produceUntil(State state, Func<State, bool> satisfied)
            {
                var s = state;
                while (!satisfied(s) && s.steps <= minutes)
                {
                    s = s with
                    {
                        ore = s.ore + s.oreRobots,
                        clay = s.clay + s.clayRobots,
                        obsidian = s.obsidian + s.obsidianRobots,
                        geode = s.geode + s.geodeRobots,
                        steps = s.steps + 1
                    };
                }
                return s;
            }

            State produceOnce(State state)
            {
                return state with
                {
                    ore = state.ore + state.oreRobots,
                    clay = state.clay + state.clayRobots,
                    obsidian = state.obsidian + state.obsidianRobots,
                    geode = state.geode + state.geodeRobots,
                    steps = state.steps + 1
                };
            }
        }

        private static IEnumerable<Costs> LoadData(string fileName)
        {
            foreach (var s in new DataLoader(2022, 19).ReadStrings(fileName))
            {
                var parts = s.Split(": ");
                parts = parts[1].Split(". ");
                var spec = parts[0]["Each ore robot costs ".Length..];
                var oreRobotOre = int.Parse(spec[0..1]);
                spec = parts[1]["Each clay robot costs ".Length..];
                var clayRobotOre = int.Parse(spec[0..1]);
                spec = parts[2]["Each obsidian robot costs ".Length..];
                var obsidianRobotOre = int.Parse(spec.Split(" ore and ")[0]);
                var obsidianRobotClay = int.Parse(spec.Split(" ore and ")[1].Split(" ")[0]);
                spec = parts[3]["Each geode robot costs ".Length..];
                var geodeRobotOre = int.Parse(spec.Split(" ore and ")[0]);
                var geodeRobotObsidian = int.Parse(spec.Split(" ore and ")[1].Split(" ")[0]);
                yield return new Costs(oreRobotOre, clayRobotOre, obsidianRobotOre, obsidianRobotClay, geodeRobotOre, geodeRobotObsidian);
            }
        }

        record Costs(
            int oreRobotOre,
            int clayRobotOre,
            int obsidianRobotOre,
            int obsidianRobotClay,
            int geodeRobotOre,
            int geodeRobotObsidian);

        record State(
            int ore, 
            int clay, 
            int obsidian, 
            int geode, 
            int oreRobots,
            int clayRobots,
            int obsidianRobots,
            int geodeRobots,
            int steps);
    }
}
