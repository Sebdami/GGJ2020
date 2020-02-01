namespace VFX
{

	using UnityEngine;

	public class BendEffect : ScriptableObject
	{
		[SerializeField] private Vector2 _bend = default;
		private static readonly int bendMultiplier = Shader.PropertyToID("_BendMultiplier");

		private void OnValidate()
		{
			ApplyBend();
		}

		public void ApplyBend()
		{
			ApplyBend(bendActive);
		}

		public void ApplyBend(bool active)
		{
			var bend = active ? _bend * .001f : Vector2.zero;
			Shader.SetGlobalVector(bendMultiplier, bend);

#if UNITY_EDITOR
			UnityEditor.SceneView.RepaintAll();
#endif
		}

#if UNITY_EDITOR
		public void ToggleBendPersistent()
		{
			bendActive = !bendActive;
			ApplyBend();
		}

		private bool bendActive
		{
			get => UnityEditor.EditorPrefs.GetBool("BendEffectActive", true);
			set => UnityEditor.EditorPrefs.SetBool("BendEffectActive", value);
		}
#else
		private const bool bendActive = true;
#endif
	}
}