using System;
using System.Linq;

using UnityEngine;
using System.Collections;

public static class FloatExtensions {

    public static float rotationNormalizedDeg(this float rotation) {
        rotation = rotation % 360f;
        if (rotation < 0)
            rotation += 360f;
        return rotation;
    }

    public static float rotationNormalizedRad(this float rotation) {
        rotation = rotation % Mathf.PI;
        if (rotation < 0)
            rotation += Mathf.PI;
        return rotation;
    }
}
