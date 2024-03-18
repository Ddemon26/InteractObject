using UnityEngine;
using UnityEngine.Animations;

namespace Damon.Objects
{
    [RequireComponent(typeof(AimConstraint))]
    public class AssignAimConstraint : MonoBehaviour
    {
        [Tooltip("The AimConstraint component to be assigned.")]
        [SerializeField] AimConstraint aimConstraint;
        [Tooltip("The tag of the camera to be assigned as the source of the AimConstraint.")]
        [SerializeField] string cameraTag = "MainCamera";

        [SerializeField] bool activateAimConstraintOnStart = true;

        private void OnValidate()
        {
            if (aimConstraint == null)
            {
                aimConstraint = GetComponent<AimConstraint>();
            }
        }

        private void Awake()
        {
            aimConstraint = GetComponent<AimConstraint>();
            SetSource();
        }

        private void Start()
        {
            ActivateAimConstraint(activateAimConstraintOnStart);
        }

        private void SetSource()
        {
            if (aimConstraint != null)
            {
                var source = new ConstraintSource
                {
                    sourceTransform = GameObject.FindGameObjectWithTag(cameraTag).transform,
                    weight = 1
                };

                aimConstraint.AddSource(source);
                aimConstraint.SetSource(0, source);
            }
        }

        public void ActivateAimConstraint(bool value)
        {
            if (aimConstraint != null)
            {
                aimConstraint.constraintActive = value;
            }
        }
    }
}
