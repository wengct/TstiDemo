namespace DemoCoreMVC.Interfaces
{
    public interface IFile
    {
        byte[] ReadAllBytes(string path);
        void WriteAllBytes(string path, byte[] bytes);
    }
}
