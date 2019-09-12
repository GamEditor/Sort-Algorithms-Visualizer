public static class Convert
{
    public static int ToInt32(this string text)
    {
        try { return System.Convert.ToInt32(text); }
        catch { return 0; }
    }
}