namespace CSharpLib
{
    public static class IntExtensions
    {
        public static int Wrap(this int n, int around)
        {
            return (n - 1) % around + 1;
        }
    }
}
