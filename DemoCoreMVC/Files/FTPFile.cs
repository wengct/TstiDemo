using DemoCoreMVC.Interfaces;

namespace DemoCoreMVC.Files
{
    public class FTPFile : IFile
    {
        public byte[] ReadAllBytes(string path)
        {
            throw new NotImplementedException();
        }

        public void WriteAllBytes(string path, byte[] bytes)
        {
            throw new NotImplementedException();
        }
    }
}
