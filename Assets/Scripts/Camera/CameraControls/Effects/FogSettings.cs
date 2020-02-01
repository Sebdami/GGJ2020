namespace VFX
{
	using System;
	using UnityEngine;

	[Serializable]
	public struct FogSettings
	{
		public FogSettings(Color color, float fogStart, float fogEnd)
		{
			_fogColor = color;
			_fogStart = fogStart;
			_fogEnd = fogEnd;
		}

		[SerializeField] private Color _fogColor;
		[SerializeField] private float _fogStart;
		[SerializeField] private float _fogEnd;

		public Color fogColor => _fogColor;
		public float fogStart => _fogStart;
		public float fogEnd => _fogEnd;
	}
}
