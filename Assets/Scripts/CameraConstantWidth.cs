using UnityEngine;
using System.Collections;


public class CameraConstantWidth : MonoBehaviour {

    public Vector2 DefaultSize = new Vector2(720, 960);

    new Camera camera;

    void Start() {
        camera = GetComponent<Camera>();
    }

    void Update() {
        var defaultRatio = DefaultSize.x / DefaultSize.y;
        var screenRatio = (float)Screen.width / Screen.height;
        (camera ?? GetComponent<Camera>()).orthographicSize = (DefaultSize.y / 200) * (defaultRatio / screenRatio);
    }
}