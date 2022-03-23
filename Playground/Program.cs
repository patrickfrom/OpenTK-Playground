using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace Playground
{
    public class Program
    {
        static void Main(string[] args)
        {
            var nativeWindowSettings = new NativeWindowSettings() { 
                Size = new Vector2i(1080, 720),
                Title = "Playground",
                APIVersion = Version.Parse("4.1")
            };

            using (var window = new Window(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.Run();
            }
        }
    }
}