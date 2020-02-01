namespace VFX
{
	using System;
	using UnityEngine;

	[Serializable]
	public struct ColorCorrectionSettings
	{
		public ColorCorrectionSettings(Texture sourceLut, Texture targetLut, float lutBlend)
		{
			_targetLut = targetLut;
			this.sourceLut = sourceLut;
			this.lutBlend = lutBlend;
		}

		[SerializeField] private Texture _targetLut;

		public Texture targetLut => _targetLut;
		public Texture sourceLut { get; }
		public float lutBlend { get; }
	}
}
