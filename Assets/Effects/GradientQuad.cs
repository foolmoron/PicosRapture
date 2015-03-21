using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GradientQuad : MonoBehaviour {

    public float[] YStops;
    public Color[] ColorStops;

    Mesh mesh;

    void Start() {
        mesh = GetComponent<MeshFilter>().sharedMesh;
    }

    public Color GetColorAtY(float y, float[] yStops, Color[] colorStops) {
        var index = 0;
        for (int i = 0; i < (yStops.Length - 1); i++) {
            if (y >= yStops[i]) {
                index = i;
            }
        }
        var lowerColor = colorStops[index];
        var upperColor = colorStops[index + 1];
        var lowerY = yStops[index];
        var upperY = yStops[index + 1];
        return Color.Lerp(lowerColor, upperColor, (y - lowerY) / (upperY - lowerY));
    }

    void Update() {
        var bottomY = transform.position.y - transform.localScale.y / 2;
        var bottomColor = GetColorAtY(bottomY, YStops, ColorStops);
        var topY = transform.position.y + transform.localScale.y / 2;
        var topColor = GetColorAtY(topY, YStops, ColorStops);

        var colors = new Color[4];
        colors[0] = bottomColor;
        colors[1] = topColor;
        colors[2] = bottomColor;
        colors[3] = topColor;
        (mesh ?? GetComponent<MeshFilter>().sharedMesh).colors = colors;
    }
}