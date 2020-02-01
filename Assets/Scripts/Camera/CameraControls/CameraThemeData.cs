namespace CameraControls
{
	using UnityEngine;
	using VFX;

    [CreateAssetMenu(fileName = "CameraThemeData", menuName = "Camera/CameraThemeData")]
	public class CameraThemeData : ScriptableObject
	{
		[SerializeField] private float _blendTime = 1;
		[SerializeField] private FogSettings _fogSettings = new FogSettings(Color.white, 25, 150);
		//[SerializeField] private ColorCorrectionSettings _colorCorrectionSettings = default;
		//[SerializeField] private bool _overrideBackgroundVisibility = false;
		//[SerializeField] private bool _backgroundVisibility = true;

		public float blendTime => _blendTime;
		public FogSettings fogSettings => _fogSettings;
		//public ColorCorrectionSettings colorCorrectionSettings => _colorCorrectionSettings;
		//public bool overrideBackgroundVisibility => _overrideBackgroundVisibility;
		//public bool backgroundVisibility => _backgroundVisibility;
	}
}
