namespace testovanie_codu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Initializer.Execute();
        }
    }
    internal static class Initializer
    {
        static public void Execute()
        {

        }

        static private Dictionary<string, string> InitParamsParser()
        {
            Dictionary<string, string> parameters = new()
            {
                {"lang",""},
                {"mode",""},
                {"tgit_dir",""},
                {"last_proj_dir",""}
            };
            using (StreamReader paramFile = new StreamReader("init_params.txt"))
            {
                while (paramFile.ReadLine() is String line)
                {

                }
            }
            return parameters;
        }
    }
}