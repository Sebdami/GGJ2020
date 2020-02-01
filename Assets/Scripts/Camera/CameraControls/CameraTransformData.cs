namespace CameraControls
{
	using UnityEngine;

    [CreateAssetMenu(fileName = "CameraTransformData", menuName = "Camera/CameraTransformData")]
    public class CameraTransformData : ScriptableObject
	{
		public CameraController.CameraSettings targetSettings = new CameraController.CameraSettings
		{
			fov      = 68,
			offset   = new Vector3(0,     3.3f, -3.3f),
			rotation = new Vector3(21.5f, 0,    0)
		};
		public float duration = 1f;
		
		public void GrabSettingsFromGame()
		{
			var cam = Camera.main;
			if (cam != null)
			{
				targetSettings.fov = cam.fieldOfView;
				var t = cam.transform;
				targetSettings.offset = t.localPosition;
				targetSettings.rotation = t.localEulerAngles;
			}
		}
		
		public void ApplySettingsToMainCamera()
		{
			var cam = Camera.main;
			if (cam != null)
			{
				cam.fieldOfView = targetSettings.fov;
				var t = cam.transform;
				t.localPosition = targetSettings.offset;
				t.localEulerAngles = targetSettings.rotation;
			}
		}
	}


}
