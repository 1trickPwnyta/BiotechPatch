namespace BiotechPatch
{
    public static class Debug
    {
        public static void Log(object message)
        {
#if DEBUG
            Verse.Log.Message($"[{BiotechPatchMod.PACKAGE_NAME}] {message}");
#endif
        }

        public static void Warning(object message)
        {
            Verse.Log.Warning($"[{BiotechPatchMod.PACKAGE_NAME}] {message}");
        }
    }
}
