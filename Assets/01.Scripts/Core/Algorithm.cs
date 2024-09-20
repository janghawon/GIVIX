using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

/*
* Class: Algorithm
* Author: 장하원
* Created: 2024년 6월 20일 화요일
* Description: 알고리즘을 편하게 사용할 수 있게 도와주는 클래스
*/

namespace Extension
{
    public static class Algorithm
    {
        private static Random _random = new Random();

        public static T[] Shuffle<T>(this T[] toShuffleArr)
        {
            for (int i = toShuffleArr.Length - 1; i > 0; i--)
            {
                int r = _random.Next(i + 1);

                T value = toShuffleArr[r];
                toShuffleArr[r] = toShuffleArr[i];
                toShuffleArr[i] = value;
            }

            return toShuffleArr;
        }

        public static List<T> Shuffle<T>(this List<T> toShuffleArr)
        {
            for (int i = toShuffleArr.Count - 1; i > 0; i--)
            {
                int r = _random.Next(i + 1);

                T value = toShuffleArr[r];
                toShuffleArr[r] = toShuffleArr[i];
                toShuffleArr[i] = value;
            }

            return toShuffleArr;
        }
    }

}

