using System;
using System.Linq;

using UnityEngine;
using System.Collections;

public static class IntExtensions {

    public static readonly int[] POWERS_OF_2 =
        Enumerable.Range(0, 32).Select(bitshift => 1 << bitshift).ToArray();

    public static int indexOfFirstTrueBit(this int value) {
        // Kind of a weird function... it returns the index (which goes from the least significant bit #0 to the most #31) of the first true (1) bit.
        // Useful for getting the sequential index of a layer from a LayerMask bitfield, which means you can use a LayerMask field to expose a layer
        // selection field to the user using LayerMask instead of making a custom inspector and using EditorGUI.LayerField.
        // Basically if you need a user-specified layer and are too lazy for EditorGUI.LayerField, use LayerMask in conjunction with this function.
        for (int i = 0; i < POWERS_OF_2.Length; i++) {
            if ((value & POWERS_OF_2[i]) != 0) {
                return i;
            }

        }
        return -1;
    }

    public static int indexOfFirstFalseBit(this int value) {
        // Same as above, but looks for the first false (0) bit instead.
        for (int i = 0; i < POWERS_OF_2.Length; i++) {
            if ((value & POWERS_OF_2[i]) == 0) {
                return i;
            }
        }
        return -1;
    }
}
