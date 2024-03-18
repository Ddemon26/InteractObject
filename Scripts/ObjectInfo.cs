using Damon.Interfaces;
using TMPro;
using UnityEngine;

namespace Damon.Objects
{
    public class ObjectInfo : MonoBehaviour, IObjectInfo, IOutline
    {
        public ObjectData objectData;

        [SerializeField] private GameObject outline;
        [SerializeField] private GameObject worldDisplayInfo;

        [SerializeField] bool displayInfoInWorldSpace = false;
        [SerializeField] AssignAimConstraint aimConstraint;
        [SerializeField] TextMeshPro worldInfoText;

        public int ID => objectData._ID;
        public string Name => objectData._Name;
        public string Description => objectData._Description;

        private string displayText = "";

        void Awake()
        {
            ToggleOutline(false);
            SetWorldInfoText();
        }

        public string GetInfo()
        {
            return $"ID: {ID}, Name: {Name}, Description: {Description}";
        }

        public void ToggleOutline(bool value)
        {
            if (outline)
            {
                outline.SetActive(value);
                aimConstraint.ActivateAimConstraint(value);
                worldDisplayInfo.SetActive(value);
            }
        }

        public bool IsOutlined() => outline && outline.activeSelf;

        private void SetWorldInfoText()
        {
            if (worldInfoText != null)
            {
                if (displayInfoInWorldSpace)
                {
                    displayText = $"ID: {ID}\nName: {Name}\nDescription: {Description}";
                    worldInfoText.text = displayText;
                }
                else
                {
                    worldInfoText.text = displayText;
                }
            }
        }
    }
}