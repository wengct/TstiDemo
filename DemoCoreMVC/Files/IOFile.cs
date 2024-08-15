using DemoCoreMVC.Interfaces;

namespace DemoCoreMVC.Files
{
    public class IOFile : IFile
    {
        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        public void WriteAllBytes(string path, byte[] bytes)
        {
            File.WriteAllBytes(path, bytes);
        }
    }
}
