using UnityEngine;

namespace Damon.Objects
{
    [System.Serializable]
    public class ObjectData
    {
        public int _ID = 1337;
        public string _Name = "Some Name";
        [Space(5)]
        [TextArea(3, 10)]
        public string _Description = "Some Description";

        public ObjectData(int id, string name, string description)
        {
            _ID = id;
            _Name = name;
            _Description = description;
        }
    }
}