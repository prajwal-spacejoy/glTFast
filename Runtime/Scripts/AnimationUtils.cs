// Copyright 2020-2021 Andreas Atteneder
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//


#if UNITY_ANIMATION

using System;
using System.Text;
using GLTFast.Schema;
using Unity.Collections;
using UnityEngine;

namespace GLTFast {

    class AnimationUtils {

        public static void AddTranslationCurves(AnimationClip clip, string animationPath, NativeArray<float> times, NativeArray<Vector3> values, InterpolationType interpolationType) {
            AddVec3Curves(clip, animationPath, "localPosition.", times, values, interpolationType);
        }

        public static void AddScaleCurves(AnimationClip clip, string animationPath, NativeArray<float> times, NativeArray<Vector3> values, InterpolationType interpolationType) {
            AddVec3Curves(clip, animationPath, "localScale.", times, values, interpolationType);
        }

        static void AddVec3Curves(AnimationClip clip, string animationPath, string propertyPrefix, NativeArray<float> times, NativeArray<Vector3> values, InterpolationType interpolationType) {
            var curveX = new AnimationCurve();
            var curveY = new AnimationCurve();
            var curveZ = new AnimationCurve();
            for (var i = 0; i < times.Length; i++) {
                curveX.AddKey(CreateKeyframe(i, times, values, x => x.x, interpolationType));
                curveY.AddKey(CreateKeyframe(i, times, values, x => x.y, interpolationType));
                curveZ.AddKey(CreateKeyframe(i, times, values, x => x.z, interpolationType));
            }
            clip.SetCurve(animationPath, typeof(Transform), $"{propertyPrefix}x", curveX);
            clip.SetCurve(animationPath, typeof(Transform), $"{propertyPrefix}y", curveY);
            clip.SetCurve(animationPath, typeof(Transform), $"{propertyPrefix}z", curveZ);
        }

        public static void AddRotationCurves(AnimationClip clip, string animationPath, NativeArray<float> times, NativeArray<Vector4> quaternions, InterpolationType interpolationType) {
            var rotX = new AnimationCurve();
            var rotY = new AnimationCurve();
            var rotZ = new AnimationCurve();
            var rotW = new AnimationCurve();
            for (var i = 0; i < times.Length; i++) {
                rotX.AddKey(CreateKeyframe(i, times, quaternions, x => x.x, interpolationType));
                rotY.AddKey(CreateKeyframe(i, times, quaternions, x => x.y, interpolationType));
                rotZ.AddKey(CreateKeyframe(i, times, quaternions, x => x.z, interpolationType));
                rotW.AddKey(CreateKeyframe(i, times, quaternions, x => x.w, interpolationType));
            }
            clip.SetCurve(animationPath, typeof(Transform), "localRotation.x", rotX);
            clip.SetCurve(animationPath, typeof(Transform), "localRotation.y", rotY);
            clip.SetCurve(animationPath, typeof(Transform), "localRotation.z", rotZ);
            clip.SetCurve(animationPath, typeof(Transform), "localRotation.w", rotW);
        }
        
        public static Keyframe CreateKeyframe<T>(int index, NativeArray<float> timeArray, NativeArray<T> valueArray, Func<T, float> getValue, InterpolationType interpolationType) where T : struct {
            var time = timeArray[index];
            Keyframe keyframe;
            switch (interpolationType) {
                case InterpolationType.STEP:
                    keyframe = new Keyframe(time, getValue(valueArray[index]), float.PositiveInfinity, 0);
                    break;
                case InterpolationType.CUBICSPLINE: {
                    var inTangent = getValue(valueArray[index*3]);
                    var value = getValue(valueArray[index*3 + 1]);
                    var outTangent = getValue(valueArray[index*3 + 2]);
                    keyframe = new Keyframe(time, value, inTangent, outTangent, .5f, .5f);
                    break;
                }
                default: // LINEAR
                    keyframe = new Keyframe(time, getValue(valueArray[index]),0,0,0,0);
                    break;
            }
            return keyframe;
        }
        
        public static string CreateAnimationPath(int nodeIndex, string[] nodeNames, int[] parentIndex) {
            var sb = new StringBuilder();
            do {
                if (sb.Length > 0) {
                    sb.Insert(0,'/');
                }
                sb.Insert(0,nodeNames[nodeIndex]);
                nodeIndex = parentIndex[nodeIndex];
            } while (nodeIndex>=0);
            return sb.ToString();
        }
    }
}

#endif // UNITY_ANIMATION
