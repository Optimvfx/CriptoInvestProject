namespace Extenstions
{
    public static class CharExtenstions
    {
        public static bool IsMatch(this char chr)
        {
            return char.IsNumber(chr) || chr == '.' || chr == '.' || chr == '-' || chr == '+' || chr == 'E';
        }
    }
}
