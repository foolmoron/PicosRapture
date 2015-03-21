using UnityEngine;
using System.Collections;

public class Tweening : MonoBehaviour
{
	Vector3 positionDisplacement;
	Vector3 positionInitial;
	Interpolate.Function positionFunc;
	float positionDuration;
	float positionDurationTotal;

	Vector3 rotationDisplacement;
	Vector3 rotationInitial;
	Interpolate.Function rotationFunc;
	float rotationDuration;
	float rotationDurationTotal;
	
	Vector3 scaleDisplacement;
	Vector3 scaleInitial;
	Interpolate.Function scaleFunc;
	float scaleDuration;
	float scaleDurationTotal;
	
	Color colorDisplacement;
	Color colorInitial;
	Interpolate.Function colorFunc;
	float colorDuration;
	float colorDurationTotal;
	
	public void MoveTo(Vector3 destination, float duration, Interpolate.EaseType easingFunction)
	{
		positionInitial = transform.localPosition;
		positionDisplacement = destination - positionInitial;
		positionFunc = Interpolate.Ease(easingFunction);
		positionDurationTotal = duration;
		positionDuration = 0;
	}

	public void RotateTo(Vector3 destination, float duration, Interpolate.EaseType easingFunction)
	{
		rotationInitial = transform.localRotation.eulerAngles;

		rotationDisplacement = destination - rotationInitial;		
		var rotX = rotationDisplacement.x; // check the other way around the circle
		var rotY = rotationDisplacement.y; // for each component
		var rotZ = rotationDisplacement.z;
		var rotX2 = (rotationDisplacement.x + 360) % 360;
		var rotY2 = (rotationDisplacement.y + 360) % 360;
		var rotZ2 = (rotationDisplacement.z + 360) % 360;
		if (Mathf.Abs(rotX2) < Mathf.Abs(rotX)) rotX = rotX2;
		if (Mathf.Abs(rotY2) < Mathf.Abs(rotY)) rotY = rotY2;
		if (Mathf.Abs(rotZ2) < Mathf.Abs(rotZ)) rotZ = rotZ2;
		rotationDisplacement = new Vector3(rotX, rotY, rotZ);

		rotationFunc = Interpolate.Ease(easingFunction);
		rotationDurationTotal = duration;
		rotationDuration = 0;
	}
	
	public void ScaleTo(Vector3 destination, float duration, Interpolate.EaseType easingFunction)
	{
		scaleInitial = transform.localScale;
		scaleDisplacement = destination - scaleInitial;
		scaleFunc = Interpolate.Ease(easingFunction);
		scaleDurationTotal = duration;
		scaleDuration = 0;
	}
	
	public void ColorTo(Color destination, float duration, Interpolate.EaseType easingFunction)
	{
		colorInitial = GetComponent<Renderer>().material.color;
		colorDisplacement = destination - colorInitial;
		colorFunc = Interpolate.Ease(easingFunction);
		colorDurationTotal = duration;
		colorDuration = 0;
	}

	public void FixedUpdate()
	{
		positionDuration += Time.deltaTime;
		if (positionDuration < positionDurationTotal) {
			Vector3 pos = positionInitial;
			pos.x = positionFunc(pos.x, positionDisplacement.x, positionDuration, positionDurationTotal);
			pos.y = positionFunc(pos.y, positionDisplacement.y, positionDuration, positionDurationTotal);
			pos.z = positionFunc(pos.z, positionDisplacement.z, positionDuration, positionDurationTotal);
			transform.localPosition = pos;
		} else positionDuration = positionDurationTotal;
		
		rotationDuration += Time.deltaTime;
		if (rotationDuration < rotationDurationTotal) {
			Vector3 rot = rotationInitial;
			rot.x = rotationFunc(rot.x, rotationDisplacement.x, rotationDuration, rotationDurationTotal);
			rot.y = rotationFunc(rot.y, rotationDisplacement.y, rotationDuration, rotationDurationTotal);
			rot.z = rotationFunc(rot.z, rotationDisplacement.z, rotationDuration, rotationDurationTotal);
			transform.localRotation = Quaternion.Euler(rot);
		} else rotationDuration = rotationDurationTotal;
		
		scaleDuration += Time.deltaTime;
		if (scaleDuration < scaleDurationTotal) {
			Vector3 scale = scaleInitial;
			scale.x = scaleFunc(scale.x, scaleDisplacement.x, scaleDuration, scaleDurationTotal);
			scale.y = scaleFunc(scale.y, scaleDisplacement.y, scaleDuration, scaleDurationTotal);
			scale.z = scaleFunc(scale.z, scaleDisplacement.z, scaleDuration, scaleDurationTotal);
			transform.localScale = scale;
		} else scaleDuration = scaleDurationTotal;
		
		colorDuration += Time.deltaTime;
		if (colorDuration < colorDurationTotal) {
			Color color = colorInitial;
			color.r = colorFunc(color.r, colorDisplacement.r, colorDuration, colorDurationTotal);
			color.g = colorFunc(color.g, colorDisplacement.g, colorDuration, colorDurationTotal);
			color.b = colorFunc(color.b, colorDisplacement.b, colorDuration, colorDurationTotal);
			color.a = colorFunc(color.a, colorDisplacement.a, colorDuration, colorDurationTotal);
			GetComponent<Renderer>().material.color = color;
		} else colorDuration = colorDurationTotal;
	}
}