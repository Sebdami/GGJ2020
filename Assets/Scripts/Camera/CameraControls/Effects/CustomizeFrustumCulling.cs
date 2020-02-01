namespace VFX
{
	using UnityEngine;

	public class CustomizeFrustumCulling : MonoBehaviour
	{
		private Camera _cam;

		private void Start()
		{
			_cam = GetComponent<Camera>();
		}

		private void OnPreCull()
		{
			if (_cam != null)
			{
				_cam.cullingMatrix = Matrix4x4.Perspective(_cam.fieldOfView + 15f, _cam.aspect, _cam.nearClipPlane, _cam.farClipPlane) * Matrix4x4.Translate(Vector3.forward * -10) * _cam.worldToCameraMatrix;
			}
		}

		private void OnDisable()
		{
			_cam.ResetCullingMatrix();
		}
	}
}