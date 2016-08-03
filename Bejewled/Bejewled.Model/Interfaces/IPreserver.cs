namespace Bejewled.Model.Interfaces
{
    public interface IPreserver<T>
    {
        void SaveData(T score, string fileName);
        T LoadData(string fileName);
    }
}
