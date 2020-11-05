using System;
using System.Collections.Generic;

namespace AdventOfCode.Common
{
    class IntCodeComputer
    {
        private enum AddressingMode
        {
            Position = 0,
            Immediate = 1,
            Relative = 2,
        };

        private class Operation
        {
            public int OpCode { get; }
            public AddressingMode AddressingModeParam1 { get; }
            public AddressingMode AddressingModeParam2 { get; }
            public AddressingMode AddressingModeParam3 { get; }
            public Operation(long value)
            {
                var s = $"{value:D5}";
                OpCode = int.Parse(s.Substring(3, 2));
                AddressingModeParam1 = (AddressingMode)int.Parse(s.Substring(2, 1));
                AddressingModeParam2 = (AddressingMode)int.Parse(s.Substring(1, 1));
                AddressingModeParam3 = (AddressingMode)int.Parse(s.Substring(0, 1));
            }
        }

        public bool IsDone { get; private set; }

        private readonly Data data;
        private readonly Func<long> inFunc;
        private readonly Func<long, bool> outFunc;
        private long instructionPointer;

        public IntCodeComputer(IEnumerable<long> inData, Func<long> inFunc, Func<long,bool> outFunc)
        {
            data = new Data(inData);
            this.inFunc = inFunc;
            this.outFunc = outFunc;
            instructionPointer = 0;
            IsDone = false;
        }

        public string GetState()
        {
            return $"{data}, ip = {instructionPointer}";
        }

        public bool Run()
        {
            if (IsDone)
            {
                return false;
            }

            while (ProcessInstruction())
            {
                
            }

            return IsDone;
        }

        public long GetValueAt(long index)
        {
            return data.Read(index, AddressingMode.Immediate);
        }

        private bool ProcessInstruction()
        {
            var operation = new Operation(data.Read(instructionPointer++, AddressingMode.Immediate));
            switch (operation.OpCode)
            {
                // Opcode 1 adds together numbers read from two positions and stores the result in a third position.
                // The three integers immediately after the opcode tell you these three positions - the first two 
                // indicate the positions from which you should read the input values, and the third indicates the 
                // position at which the output should be stored.
                case 1:
                {
                    var value1 = data.Read(instructionPointer++, operation.AddressingModeParam1);
                    var value2 = data.Read(instructionPointer++, operation.AddressingModeParam2);
                    data.Write(instructionPointer++, value1 + value2, operation.AddressingModeParam3);
                    return true;
                }

                // Opcode 2 works exactly like opcode 1, except it multiplies the two inputs instead of adding them.
                case 2:
                {
                    var value1 = data.Read(instructionPointer++, operation.AddressingModeParam1);
                    var value2 = data.Read(instructionPointer++, operation.AddressingModeParam2);
                    data.Write(instructionPointer++, value1 * value2, operation.AddressingModeParam3);
                    return true;
                }

                // Opcode 3 takes a single integer as input and saves it to the position given by its only parameter.
                // For example, the instruction 3,50 would take an input value and store it at address 50.
                case 3:
                {
                    var value = inFunc();
                    data.Write(instructionPointer++, value, operation.AddressingModeParam1);
                    return true;
                }

                // Opcode 4 outputs the value of its only parameter. For example, the instruction 4,50 would output the value at address 50.
                case 4:
                {
                    var value = data.Read(instructionPointer++, operation.AddressingModeParam1);
                    return outFunc(value);
                }

                // Opcode 5 is jump-if-true: if the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                case 5:
                {
                    var isTrue = data.Read(instructionPointer++, operation.AddressingModeParam1) != 0;
                    var value2 = data.Read(instructionPointer++, operation.AddressingModeParam2);
                    if (isTrue)
                    {
                        instructionPointer = value2;
                    }
                    return true;
                }

                // Opcode 6 is jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                case 6:
                {
                    var isTrue = data.Read(instructionPointer++, operation.AddressingModeParam1) != 0;
                    var value2 = data.Read(instructionPointer++, operation.AddressingModeParam2);
                    if (!isTrue)
                    {
                        instructionPointer = value2;
                    }
                    return true;
                }

                // Opcode 7 is less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                case 7:
                {
                    var value1 = data.Read(instructionPointer++, operation.AddressingModeParam1);
                    var value2 = data.Read(instructionPointer++, operation.AddressingModeParam2);
                    var valueToStore = value1 < value2 ? 1 : 0;
                    data.Write(instructionPointer++, valueToStore, operation.AddressingModeParam3);
                    return true;
                }

                // Opcode 8 is equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                case 8:
                {
                    var value1 = data.Read(instructionPointer++, operation.AddressingModeParam1);
                    var value2 = data.Read(instructionPointer++, operation.AddressingModeParam2);
                    var valueToStore = value1 == value2 ? 1 : 0;
                    data.Write(instructionPointer++, valueToStore, operation.AddressingModeParam3);
                    return true;
                }

                // Opcode 9 adjusts the relative base by the value of its only parameter. The relative base increases (or decreases, if the value is negative) by the value of the parameter.
                case 9:
                {
                    var value1 = data.Read(instructionPointer++, operation.AddressingModeParam1);
                    data.IncreaseRelativeBase(value1);
                    return true;
                }

                // Opcode 99 halts the program.
                case 99:
                {
                    IsDone = true;
                    return false;
                }
            }

            return false;
        }


        private class Data
        {
            private long relativeBase;
            private readonly Dictionary<long, long> data;

            public Data(IEnumerable<long> inData)
            {
                var index = 0;
                data = new Dictionary<long, long>();    
                foreach (var d in inData)
                {
                    data[index++] = d;
                }
                relativeBase = 0;
            }

            public void IncreaseRelativeBase(long value)
            {
                relativeBase += value;
            }

            public long Read(long index, AddressingMode am)
            {
                var valueAtIndex = Get(index);
                switch (am)
                {
                    case AddressingMode.Position:
                        return Get(valueAtIndex);
                    case AddressingMode.Relative:
                        return Get(relativeBase + valueAtIndex);
                    case AddressingMode.Immediate:
                        return valueAtIndex;
                    default:
                        throw new NotImplementedException();
                }
            }

            public void Write(long index, long value, AddressingMode am)
            {
                var valueAtIndex = Get(index);
                switch (am)
                {
                    case AddressingMode.Position:
                        data[valueAtIndex] = value;
                        return;
                    case AddressingMode.Relative:
                        data[relativeBase + valueAtIndex] = value;
                        return;
                    case AddressingMode.Immediate:
                        throw new NotImplementedException();
                    default:
                        throw new NotImplementedException();
                }
            }

            public override string ToString()
            {
                var opCodes = new List<long>();
                var index = 0;
                var ok = true;
                while (ok)
                {
                    if (data.TryGetValue(index++, out var v))
                    {
                        opCodes.Add(v);
                    }
                    else
                    {
                        ok = false;
                    }
                }
                return $"[{string.Join(',', opCodes)}] - rb = {relativeBase}";
            }

            private long Get(long address)
            {
                return data.ContainsKey(address) ? data[address] : 0;
            }
        }
    }

}
