using UnityEngine;
using UnityEngine.Events;

namespace data
{
    public class DataSingleton : MonoBehaviour
    {
        // Define an event that will be triggered when the data is loaded
        public static event UnityAction DataLoaded;

        // Our singleton instance
        private static Data _data;

        public static Data GetData()
        {
            return _data;
        }

        // This function loads the file from a default location as shown below
        public static void Load(string fileName)
        {
            var file = System.IO.File.ReadAllText(fileName);
            var temp = JsonUtility.FromJson<Data>(file);
            _data = temp;

            // Trigger the DataLoaded event when the data is loaded
            DataLoaded?.Invoke();
        }
    }
}