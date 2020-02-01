namespace CameraControls
{
    using System.Collections;
	using UnityEngine;
	using VFX;

	public class CameraThemeController
	{
		//private Camera _mainCamera;

		private CameraThemeData _sourceThemeData;
		private CameraThemeData _targetThemeData;

		private FogEffect _fogEffect;

		private FogSettings _currentFogSettings;
		private ColorCorrectionSettings _currentColorCorrectionSettings;

		private Coroutine _blendRoutine;

		public void OnCurrentThemeChanged(CameraThemeData _targetThemeData, CameraThemeData themeData)
		{
            if (_targetThemeData == null || themeData == null)
                return;

            // mmmmm
            _fogEffect = new FogEffect();

            _sourceThemeData = _targetThemeData;
			_targetThemeData = themeData;
			StartBlendTo(_targetThemeData);
		}

		private void StartBlendTo(CameraThemeData themeData)
		{
			if (_blendRoutine != null)
			{
				CoroutineManager.Instance.StopCoroutine(_blendRoutine);
			}

			_blendRoutine = CoroutineManager.StartStaticCoroutine(DoBlend(themeData));
		}

		private void ApplyCurrentSettings()
		{
			_fogEffect.ApplyEffectSettings(_currentFogSettings);

		}

		public void ForceSetToTargetSettings()
		{
			if (_blendRoutine != null)
			{
                CoroutineManager.Instance.StopCoroutine(_blendRoutine);
            }

			_currentFogSettings = _fogEffect.Blend(_sourceThemeData.fogSettings, _targetThemeData.fogSettings, 1);
			//if (_colorCorrectionEffect != null)
			//{
			//	_currentColorCorrectionSettings = _colorCorrectionEffect.Blend(_sourceThemeData.colorCorrectionSettings, _targetThemeData.colorCorrectionSettings, 1);
			//}

			ApplyCurrentSettings();
		}

		private IEnumerator DoBlend(CameraThemeData targetData)
		{
			float t = 0;
			while (t < 1)
			{
				t = Mathf.Clamp01(t + Time.deltaTime / targetData.blendTime);
				_currentFogSettings = _fogEffect.Blend(_sourceThemeData.fogSettings, targetData.fogSettings, t);

				//if (_colorCorrectionEffect != null)
				//{
				//	_currentColorCorrectionSettings = _colorCorrectionEffect.Blend(_sourceThemeData.colorCorrectionSettings, targetData.colorCorrectionSettings, t);
				//}

				ApplyCurrentSettings();
				yield return null;
			}
		}
	}
}
