namespace FractalPlatform.Sandbox
{
    internal static class Utils
    {
        public static string GetSolutionPath()
        {
            var type = typeof(Utils);

            var path = type.Assembly.Location;

            return path.Substring(0, path.IndexOf(type.Namespace) - 1);
        }
    }
}
