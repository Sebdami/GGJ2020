namespace VFX
{
	using UnityEngine;

	public class FogEffect : CameraEffect<FogSettings>
	{
		private bool _hasLimitedDistance;
		private float _limitedFarDistance;

		public override FogSettings Blend(FogSettings fromSettings, FogSettings toSettings, float t)
		{
			return new FogSettings(Color.Lerp(fromSettings.fogColor, toSettings.fogColor, t),
				Mathf.Lerp(fromSettings.fogStart, toSettings.fogStart, t),
				Mathf.Lerp(fromSettings.fogEnd, toSettings.fogEnd, t));
		}

		public override void ApplyEffectSettings(FogSettings effectSettings)
		{
			//Shader.SetGlobalColor(Shader.PropertyToID("_DepthFog"), effectSettings.fogColor);

			RenderSettings.fogColor = effectSettings.fogColor;
            Camera.main.backgroundColor = effectSettings.fogColor;

   //         float reducedFar = _hasLimitedDistance && _limitedFarDistance < effectSettings.fogEnd ? effectSettings.fogEnd - _limitedFarDistance : 0;

			
			//float fogStartDistance = effectSettings.fogStart - reducedFar / 2;
			//float fogEndDistance = effectSettings.fogEnd - reducedFar;
			RenderSettings.fogStartDistance = effectSettings.fogStart;
            RenderSettings.fogEndDistance = effectSettings.fogEnd;

        }

		//public void LimitFogDistance(bool limit, float farDistance)
		//{
		//	_hasLimitedDistance = limit;
		//	_limitedFarDistance = farDistance;
		//}
	}
}
