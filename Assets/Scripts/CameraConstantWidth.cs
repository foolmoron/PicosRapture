using UnityEngine;
using System.Collections;


public class CameraConstantWidth : MonoBehaviour {

    void Start() {
        GetComponent<Camera>().orthographicSize = (Screen.currentResolution.height / 200f);
    }
}