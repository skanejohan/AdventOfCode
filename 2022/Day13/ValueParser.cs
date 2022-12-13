namespace Y2022.Day13
{
    internal class ValueParser
    {
        public ValueParser(string s)
        {
            this.s = s;
            index = 0;
        }

        public IValue Parse()
        {
            if (s[index] == '[') // Parse a list
            {
                index++; // Consume '['
                var listValue = new ListValue();
                var listDone = false;
                while (!listDone)
                {
                    if (s[index] == ']')
                    {
                        listDone = true;
                        index++; // Consume ']'
                    }
                    else
                    {
                        listValue.Add(Parse());
                        index++; // Consume ',' or ']'
                        listDone = s[index - 1] == ']';
                    }
                }
                return listValue;
            }
            else // Parse a number
            {
                var n = "";
                while (s[index] >= '0' && s[index] <= '9')
                {
                    n += s[index++];
                }
                return new IntValue(int.Parse(n));
            }
        }

        private string s;
        private int index;
    }
}
