namespace Bejewled.Model.Models.Preservers
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    using Bejewled.Model.Interfaces;

    public class BinaryPreserver<T> : IPreserver<T>
    {
        public void SaveData(T score, string fileName)
        {
            BinarySerialize(score, fileName);
        }

        public T LoadData(string fileName)
        {
            return BinaryDeserialize<T>(fileName);
        }

        private void BinarySerialize<T>(T givenObject, string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, givenObject);
            }
        }

        private T BinaryDeserialize<T>(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }
}
