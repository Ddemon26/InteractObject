using Damon.Interfaces;
using TMPro;
using UnityEngine;

namespace Damon.Objects
{
    public class ObjectInteraction : MonoBehaviour
    {
        [SerializeField] Camera mainCamera;
        [SerializeField] float maxRaycastDistance = 100f;

        [SerializeField] TextMeshProUGUI objectIdText;
        [SerializeField] TextMeshProUGUI objectNameText;
        [SerializeField] TextMeshProUGUI descriptionText;

        private void Update()
        {
            ProcessInteractions();
        }

        private void ProcessInteractions()
        {
            IObjectInfo objectInfo = GetObjectInfoInFrontOfCamera();
            UpdateInfoDisplays(objectInfo);
            ToggleOutlineOnLookedAtObject();
        }

        private IObjectInfo GetObjectInfoInFrontOfCamera()
        {
            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
            RaycastHit[] hits = Physics.RaycastAll(ray, maxRaycastDistance);

            foreach (RaycastHit hit in hits)
            {
                IObjectInfo objectInfo = hit.collider.GetComponent<IObjectInfo>();
                if (objectInfo != null)
                {
                    return objectInfo;
                }
            }

            return null;
        }

        private void UpdateInfoDisplays(IObjectInfo objectInfo)
        {
            if (objectInfo != null)
            {
                objectIdText.text = objectInfo.ID.ToString();
                objectNameText.text = objectInfo.Name;
                descriptionText.text = objectInfo.Description;
            }
            else
            {
                objectIdText.text = "";
                objectNameText.text = "";
                descriptionText.text = "";
            }
        }

        private IOutline lastOutlined = null;

        private void ToggleOutlineOnLookedAtObject()
        {
            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
            bool hitSomething = Physics.Raycast(ray, out RaycastHit hit, maxRaycastDistance);
            IOutline currentOutline = hitSomething ? hit.collider.GetComponent<IOutline>() : null;
            IObjectInfo currentObjectInfo = hitSomething ? hit.collider.GetComponent<IObjectInfo>() : null;

            if (currentOutline != lastOutlined)
            {
                if (lastOutlined != null)
                {
                    lastOutlined.ToggleOutline(false);
                }

                lastOutlined = currentOutline;

                if (currentOutline != null)
                {
                    currentOutline.ToggleOutline(true);
                    UpdateInfoDisplays(currentObjectInfo);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
            Gizmos.DrawRay(ray.origin, ray.direction * maxRaycastDistance);
        }
    }
}