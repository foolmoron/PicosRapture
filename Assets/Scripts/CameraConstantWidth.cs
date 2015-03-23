using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraConstantWidth : MonoBehaviour {

    public Vector2 DefaultSize = new Vector2(720, 960);

    new Camera camera;

    void Start() {
        camera = GetComponent<Camera>();
    }

    void Update() {
        var defaultRatio = DefaultSize.x / DefaultSize.y;
        var screenRatio = (float)Screen.width / Screen.height;
        var newSize = (DefaultSize.y / 200) * (defaultRatio / screenRatio);
        var roundedSize = (float)System.Math.Round(newSize - 0.05, 2); // precision errors in multiplying can make the size a little off, so floor to 2nd decimal point
        (camera ?? GetComponent<Camera>()).orthographicSize = roundedSize;
    }
}