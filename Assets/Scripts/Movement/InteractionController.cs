using UnityEngine;
using UnityEngine.Serialization;

namespace Movement
{
    public class InteractionController : MonoBehaviour
    {
        [Tooltip("Maximum distance over which to cast the ray.")]
        public float maxObjectDistance = 1f;

        [FormerlySerializedAs("animationManager")]
        public AnimationController animationController;

        void FixedUpdate()
        {
            InputInfo input = InputManager.GetPressedButtons();
            RaycastUpdate(input, transform.position, animationController.GetCurrentFaceDirectionVector());
        }

        public void RaycastUpdate(InputInfo input, Vector2 currentPosition, Vector2 currentFaceDirection)
        {
            if (currentFaceDirection == Vector2.zero)
            {
                return;
            }

            RaycastHit2D hit = Physics2D.Raycast(currentPosition, currentFaceDirection, maxObjectDistance);

            // Debug.DrawRay(transform.position, dirVector, Color.green);
            OnRaycastHit(hit, input);
        }

        private void OnRaycastHit(RaycastHit2D hit, InputInfo input)
        {
            if (!hit.collider)
            {
                return;
            }

            var interactable = hit.collider.gameObject.GetComponentSafe<Interactable>();
            if (!interactable)
            {
                return;
            }

            // Debug.Log("[raycast hit] @interactable " + hit.collider.gameObject.name);
            interactable.Interact(input.direction, input.action);
        }
    }
}