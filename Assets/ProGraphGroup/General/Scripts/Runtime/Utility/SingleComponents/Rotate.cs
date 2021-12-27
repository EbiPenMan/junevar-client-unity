using UnityEngine;

namespace ProGraphGroup.General.Utility.SingleComponents
{
    public class Rotate : MonoBehaviour
    {
        private Vector3 _angle;

        void Start()
        {
            _angle = transform.eulerAngles;
        }

        void Update()
        {
            _angle.z += Time.deltaTime * 100;
            transform.eulerAngles = _angle;
        }
    }
}