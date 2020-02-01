namespace VFX
{
	public abstract class CameraEffect<T>
	{
		public abstract T Blend(T fromSettings, T toSettings, float t);
		public abstract void ApplyEffectSettings(T effectSettings);
	}
}
