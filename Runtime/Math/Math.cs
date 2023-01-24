using System.Collections.Generic;
using UnityEngine;

namespace CommonUtils
{
    /// <summary> Useful math utilities. </summary>
    public static class Math {

        /// <summary> Precalculated √2 </summary>
        public static float sqrtTwo { get; private set; } = Mathf.Sqrt(2);

        /// <summary> Precalculated √3 </summary>
        public static float sqrtThree { get; private set; } = Mathf.Sqrt(3);

        /// <summary> Precalculated √5 </summary>
        public static float sqrtFive { get; private set; } = Mathf.Sqrt(5);

        /// <summary> Precalculated √2 / 2 </summary>
        public static float sqrtTwoDevidedByTwo { get; private set; } = Mathf.Sqrt(2) / 2;
        
        /// <summary> Precalculated isometric i vector. Multiply X component of the vector, to translate it to isometric space. </summary>
        public static Vector2 isometric_i { get; private set; } = new Vector2(1, .5f);

        /// <summary> Precalculated isometric j vector. Multiply Y component of the vector, to translate it to isometric space. </summary>
        public static Vector2 isometric_j { get; private set; } = new Vector2(1, -.5f);


        /// <summary> Sorts key-value pairs based on the keys. Lower to Higher. </summary>
        public static KeyValuePair<float, T>[] SortArray<T>(KeyValuePair<float, T>[] array, int leftIndex = 0, int rightIndex = -1) {
            if (rightIndex == -1) rightIndex = array.Length - 1;
            if (array.Length == 0) return new KeyValuePair<float, T>[0];

            var i = leftIndex;
            var j = rightIndex;
            var pivot = array[leftIndex].Key;
            
            while (i <= j) {
                while (array[i].Key < pivot) {
                    i++;
                }
                
                while (array[j].Key > pivot) {
                    j--;
                }

                if (i <= j) {
                    var temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }
            
            if (leftIndex < j)
                SortArray(array, leftIndex, j);
            if (i < rightIndex)
                SortArray(array, i, rightIndex);
                
            return array;
        }


        /// <summary> Sorts both arrays based on the first one. Lower to Higher. </summary>
        public static T[] SortArray<T>(float[] valueArray, T[] keysArray, int leftIndex = 0, int rightIndex = -1) {
            if (valueArray.Length != keysArray.Length) throw new System.Exception("Provided arrays are different lengths!");
            if (keysArray.Length == 0) return new T[0];

            if (rightIndex == -1) rightIndex = valueArray.Length - 1;

            var i = leftIndex;
            var j = rightIndex;
            var pivot = valueArray[leftIndex];
            
            while (i <= j) {
                while (valueArray[i] < pivot) {
                    i++;
                }
                
                while (valueArray[j] > pivot) {
                    j--;
                }

                if (i <= j) {
                    var temp_val = valueArray[i];
                    valueArray[i] = valueArray[j];
                    valueArray[j] = temp_val;

                    var temp_key = keysArray[i];
                    keysArray[i] = keysArray[j];
                    keysArray[j] = temp_key;

                    i++;
                    j--;
                }
            }
            
            if (leftIndex < j)
                SortArray(valueArray, keysArray, leftIndex, rightIndex: j);
            if (i < rightIndex)
                SortArray(valueArray, keysArray, leftIndex: i, rightIndex);
                
            return keysArray;
        }


        /// <summary> Remaps the value from one range to another. </summary>
        public static float RemapValue(float input, float inputStart, float inputEnd, float outputStart, float outputEnd) {
            input = Mathf.Clamp(input, inputStart, inputEnd);
            float slope = 1f * (outputEnd - outputStart) / (inputEnd - inputStart);
            return outputStart + slope * (input - inputStart);
        }

        /// <summary> Convert Vector3 to Vector2 in XY space. </summary>
        public static Vector2 v3tov2(Vector3 v3) {
            return new Vector2(v3.x, v3.y);
        }

        /// <summary> Convert Vector2 to Vector3 in XY space. </summary>
        public static Vector3 v2tov3(Vector2 v2) {
            return new Vector3(v2.x, v2.y, 0);
        }
    }
}