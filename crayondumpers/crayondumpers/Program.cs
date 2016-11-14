using System;

namespace crayondumpers
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (CrayonDumpers game = new CrayonDumpers())
            {
                game.Run();
            }
        }
    }
#endif
}

