using UnityEngine;

namespace CodeBase.Logic.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _rotationAngleX = 55f;
        [SerializeField] private float _distance = 10f;
        [SerializeField] private float _offsetY = 0.5f;

        private Transform _following;

        private void LateUpdate()
        {
            if (_following == null) return;

            Quaternion rotation = Quaternion.Euler(_rotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -_distance) + FollowingPointPosition();

            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject following)
        {
            _following = following.transform;
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y += _offsetY;

            return followingPosition;
        }
    }
}