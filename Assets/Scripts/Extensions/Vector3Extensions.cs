using UnityEngine;
using System.Collections;

public static class Vector3Extensions {

    public static Vector2 to2(this Vector3 vector) {
        return vector;
    }

    public static Vector3 withX(this Vector3 vector, float x) {
        return new Vector3(x, vector.y, vector.z);
    }

    public static Vector3 withY(this Vector3 vector, float y) {
        return new Vector3(vector.x, y, vector.z);
    }

    public static Vector3 withZ(this Vector3 vector, float z) {
        return new Vector3(vector.x, vector.y, z);
    }

    public static Vector3 plusX(this Vector3 vector, float plusX) {
        return new Vector3(vector.x + plusX, vector.y, vector.z);
    }

    public static Vector3 plusY(this Vector3 vector, float plusY) {
        return new Vector3(vector.x, vector.y + plusY, vector.z);
    }

    public static Vector3 plusZ(this Vector3 vector, float plusZ) {
        return new Vector3(vector.x, vector.y, vector.z + plusZ);
    }
}
