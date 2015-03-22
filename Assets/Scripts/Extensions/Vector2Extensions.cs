﻿using UnityEngine;
using System.Collections;

public static class Vector2Extensions {

    public static Vector2 withX(this Vector2 vector, float x) {
        return new Vector2(x, vector.y);
    }

    public static Vector2 withY(this Vector2 vector, float y) {
        return new Vector2(vector.x, y);
    }

    public static Vector2 plusX(this Vector2 vector, float plusX) {
        return new Vector2(vector.x + plusX, vector.y);
    }

    public static Vector2 plusY(this Vector2 vector, float plusY) {
        return new Vector2(vector.x, vector.y + plusY);
    }

    public static Vector2 timesX(this Vector2 vector, float timesX) {
        return new Vector2(vector.x * timesX, vector.y);
    }

    public static Vector2 timesY(this Vector2 vector, float timesY) {
        return new Vector2(vector.x, vector.y * timesY);
    }

	public static Vector2 Rotate(this Vector2 vector, float degrees) {
		float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
		float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
		
		float tx = vector.x;
		float ty = vector.y;
		vector.x = (cos * tx) - (sin * ty);
		vector.y = (sin * tx) + (cos * ty);
		return vector;
	}
}
