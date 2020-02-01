using DG.Tweening;

namespace CameraControls
{
	using System;
	using UnityEngine;
    using Utility;

    public class CameraController
	{
		public bool hasTarget => _target != null;
		private Transform _target;
		private Transform _targetTransform;

		private Camera _camera;
		private Transform _cameraContainerTransform;
		private Transform _cameraTransform;

		private float _lastGroundedHeight;
		private bool _targetIsGrounded = true;

		private CameraSettings _lastSettings;
		public CameraSettings currentSettings { get; private set; }
		public CameraSettings desiredSettings { get; private set; }
        public CameraSettings previousSettings;

        private float _currentTransitionTime = 0f;
		private float _totalTransitionTime = 0f;
		private float _transitionDelta = 0f;
		private float _lastMoveZ = 0f;
		private float _dynamicZ = 0f;

		private const float FollowSpeedModifierX = 20f;
		private const float FollowSpeedModifierY = 8f;
		private const float FollowSpeedModifierZ = 100f;
		private const float MinHeightDistance = -1.75f;
		private const float MaxHeightDistance = 2;

		private Vector3 _lastCharacterPos = default;

		public CameraController(Camera camera, Transform cameraContainerTransform, CameraTransformData init)
		{
			_camera = camera;
			_cameraTransform = camera.transform;
			_cameraContainerTransform = cameraContainerTransform;

            CameraTransformData initialTransformData = init;
            previousSettings = new CameraSettings(initialTransformData.targetSettings);
            _lastSettings = new CameraSettings(initialTransformData.targetSettings);
			currentSettings = new CameraSettings(initialTransformData.targetSettings);
		}

        public void ResetParam(CameraTransformData init)
        {
            _targetIsGrounded = true;
            _lastGroundedHeight = 0;
             _currentTransitionTime = 0f;
            _totalTransitionTime = 0f;
            _transitionDelta = 0f;
            _lastMoveZ = 0f;
            _dynamicZ = 0f;
            previousSettings = new CameraSettings(init.targetSettings);
            _lastSettings = new CameraSettings(init.targetSettings);
            currentSettings = new CameraSettings(init.targetSettings);
        }

        public void SetFollowTarget(Transform target)
		{
			_target = target;
			_targetTransform = target.transform;
			ResetFollowPosition();
		}



		public void SetDesiredSettings(CameraSettings settings, float transitionTime)
		{
            // Copy the values of the new settings
            _lastSettings.CopyValues(currentSettings);


            desiredSettings = settings;
            currentSettings.followCharacter = desiredSettings.followCharacter;
            _currentTransitionTime = 0;
			_totalTransitionTime = transitionTime;
			_dynamicZ = 0f;



            Evaluate(transitionTime <= 0 ? 1 : 0);
		}

		/// <summary>
		/// A set of values that will be applied on top of the desired settings
		/// </summary>
		public CameraSettings dynamicSettings { get; private set; } = new CameraSettings
		{
			followCharacter = true,
			setFov = true,
			setOffset = true,
			setRotation = true
		};

		/// <summary>
		/// Force the Camera Container to instantly move to the desired position, rather than depending on a smooth lerp.
		/// </summary>
		public void ResetFollowPosition()
		{
			if (hasTarget)
			{
				var position = _targetTransform.position;
				_lastGroundedHeight = position.y;
				_cameraContainerTransform.position = position;
			}
		}

		public void ForceFinishTransition()
		{
            previousSettings.CopyValues(currentSettings);
            _currentTransitionTime = 1;
			_totalTransitionTime = 1;
			_transitionDelta = _currentTransitionTime / _totalTransitionTime;
			_dynamicZ = 0f;
			if (desiredSettings != null)
			{
				Evaluate(_transitionDelta);
			}
		}

		private void UpdateDynamicValues()
		{
			int laneSize = 2;
            dynamicSettings.offset.x = -Mathf.Clamp(_lastCharacterPos.x, -laneSize, laneSize) * .25f;
			dynamicSettings.offset.z = _dynamicZ;
		}

		public bool followInAir { get; set; } = false;

        public void Update(float dt, bool withProgress)
		{
			if (_target == null || _targetTransform == null)
			{
				return;
			}

			if (desiredSettings != null)
			{
				_currentTransitionTime += dt;
				_transitionDelta = _currentTransitionTime / _totalTransitionTime;

				if (desiredSettings.followCharacter == false)
				{
					_dynamicZ += _lastMoveZ * Mathf.Pow((1f - _transitionDelta), 2);
				}
				Evaluate(_transitionDelta);
			}

			if (currentSettings.followCharacter)
			{
				_lastMoveZ = _targetTransform.position.z - _lastCharacterPos.z;
				_lastCharacterPos = _targetTransform.position;
			}

			UpdateDynamicValues();

			var charPos = _lastCharacterPos + dynamicSettings.offset;
			var pos = charPos;
			var camPos = _cameraContainerTransform.position;
			// (Svend) We could consider changing this to use Vector3.SmoothDamp as it's a more "proper" way of achieving this effect.
			camPos.x = Mathf.Lerp(camPos.x, pos.x, dt * FollowSpeedModifierX);
			camPos.z = Mathf.Lerp(camPos.z, pos.z, dt * FollowSpeedModifierZ);
            camPos.y = _targetTransform.position.y;
      
            //camPos.y = _targetTransform.parent.position.y;

            var delta = charPos.y - camPos.y;
			if (currentSettings.followCharacter)
			{
				if (delta > MaxHeightDistance)
				{
					camPos.y += delta - MaxHeightDistance;
				}
				else if (delta < MinHeightDistance)
				{
					camPos.y += delta - MinHeightDistance;
				}
			}

			if (withProgress)
			{
				_cameraContainerTransform.position = camPos;
			}

			//Debug.DrawRay(_camera.transform.position, _cameraTransform.forward * 3, Color.yellow, 5f);

			Vector3 newRot = _cameraTransform.localEulerAngles;

			float xRotDifference = currentSettings.rotation.x - newRot.x;

			if (xRotDifference > 180)
			{
				xRotDifference -= 360;
			}
			else if (xRotDifference < -180)
			{
				xRotDifference += 360;
			}

			newRot.x += Mathf.Lerp(0, xRotDifference, dt);

			_cameraTransform.localRotation =Quaternion.Euler(newRot);
		}

		private delegate Vector3 Vector3Lerp(Vector3 a, Vector3 b, float t);

		// Allow changing lerp method in CameraSettings?
		private AnimationCurve _fovCurve = AnimationCurve.EaseInOut(0,0,1,1);
		private Vector3Lerp _offsetLerp = SineEaseInOut;
		private Vector3Lerp _rotationLerp = SineEaseInOut;

		private static float SineEaseInOut(float a, float b, float t)
		{
			return a + (b - a) * Easings.SineEaseInOut(Mathf.Clamp01(t));
		}
		private static Vector3 SineEaseInOut(Vector3 a, Vector3 b, float t)
		{
			t = Easings.SineEaseInOut(Mathf.Clamp01(t));
			return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		private void Evaluate(float percent)
		{
			percent = Mathf.Clamp01(percent);

			if (desiredSettings.setFov)
			{
				currentSettings.fov = Mathf.Lerp(_lastSettings.fov, desiredSettings.fov, _fovCurve.Evaluate(percent));
				_camera.fieldOfView = currentSettings.fov;
			}

			if (desiredSettings.setOffset)
			{
				currentSettings.offset = _offsetLerp(_lastSettings.offset, desiredSettings.offset, percent);
				_cameraTransform.localPosition = currentSettings.offset;
			}

			if (desiredSettings.setRotation)
			{
				currentSettings.rotation = _rotationLerp(_lastSettings.rotation, desiredSettings.rotation, percent);
				//Debug.Log($"2 Current: ({_cameraTransform.localEulerAngles}) New: ({_current.rotation})");
				_cameraTransform.localRotation = Quaternion.Euler(currentSettings.rotation);
			}



            if (percent >= 1f)
			{
				desiredSettings = null;
			}
		}


        public enum EFollowInTheAirType
        {
            NONE,
            SMOOTH,
            INSTANT
        }

        [Serializable]
		public class CameraSettings
		{
			public bool followCharacter = true;

            public bool setFov;
			public float fov;

			public bool setRotation;
			public Vector3 rotation;

			public bool setOffset;
			public Vector3 offset;

			public CameraSettings()
			{
			}

			public CameraSettings(CameraSettings other)
			{
				CopyValues(other);
			}

			public void CopyValues(CameraSettings other)
			{
				followCharacter = other.followCharacter;
				fov = other.fov;
                setFov = other.setFov;
				rotation = other.rotation;
				setRotation = other.setRotation;
				offset = other.offset;
				setOffset = other.setOffset;
            }
		}
	}
}