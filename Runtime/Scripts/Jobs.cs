﻿// Copyright 2020-2021 Andreas Atteneder
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

using UnityEngine;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace GLTFast.Jobs {

    public unsafe struct CreateIndicesJob : IJobParallelFor  {

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public int* result;

        [ReadOnly]
        public bool lineLoop;

        public void Execute(int i)
        {
            result[i] = i;
        }
    }

    public unsafe struct CreateIndicesFlippedJob : IJobParallelFor  {

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public int* result;

        public void Execute(int i)
        {
            result[i] = i - 2*(i%3-1);
        }
    }


    public unsafe struct GetIndicesUInt8Job : IJobParallelFor  {

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public int* result;

        public void Execute(int i)
        {
            result[i] = input[i];
        }
    }

    public unsafe struct GetIndicesUInt8FlippedJob : IJobParallelFor  {

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public int* result;

        public void Execute(int i)
        {
            result[i*3] = input[i*3];
            result[i*3+2] = input[i*3+1];
            result[i*3+1] = input[i*3+2];
        }
    }

    public unsafe struct GetIndicesUInt16FlippedJob : IJobParallelFor  {

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public ushort* input;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public int* result;

        public void Execute(int i)
        {
            result[i*3] = input[i*3];
            result[i*3+2] = input[i*3+1];
            result[i*3+1] = input[i*3+2];
        }
    }

    public unsafe struct GetIndicesUInt16Job : IJobParallelFor  {

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public ushort* input;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public int* result;

        public void Execute(int i)
        {
            result[i] = input[i];
        }
    }

    public unsafe struct GetIndicesUInt32Job : IJobParallelFor  {

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public uint* input;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public int* result;

        public void Execute(int i)
        {
            result[i] = (int)input[i];
        }
    }

    public unsafe struct GetIndicesUInt32FlippedJob : IJobParallelFor  {

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public uint* input;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public int* result;

        public void Execute(int i)
        {
            result[i*3] = (int)input[i*3];
            result[i*3+2] = (int)input[i*3+1];
            result[i*3+1] = (int)input[i*3+2];
        }
    }

    public unsafe struct GetUVsUInt8Job : IJobParallelFor  {

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector2* result;

        public void Execute(int i)
        {
            result[i].x = input[i*2];
            result[i].y = 1 - input[i*2+1];
        }
    }

    public unsafe struct GetUVsUInt8NormalizedJob : IJobParallelFor  {

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector2* result;

        public void Execute(int i)
        {
            result[i].x = input[i*2] / 255f;
            result[i].y = 1 - input[i*2+1] / 255f;
        }
    }

    public unsafe struct GetUVsUInt16NormalizedJob : IJobParallelFor  {

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public ushort* input;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector2* result;

        public void Execute(int i)
        {
            result[i].x = input[i*2] / (float) ushort.MaxValue;
            result[i].y = 1 - input[i*2+1] / (float) ushort.MaxValue;
        }
    }

    public unsafe struct GetUVsUInt16Job : IJobParallelFor  {

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public ushort* input;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector2* result;

        public void Execute(int i)
        {
            result[i].x = input[i*2];
            result[i].y = 1 - input[i*2+1];
        }
    }

    public unsafe struct GetUVsFloatJob : IJobParallelFor {
        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public float* input;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector2* result;

        public void Execute(int i) {
            result[i].x = ((float*)input)[i*2];
            result[i].y = 1-((float*)input)[i*2+1];
        }
    }

    /// Untested!
    public unsafe struct GetUVsUInt8InterleavedJob : IJobParallelFor  {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        public int outputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector2* result;

        public void Execute(int i)
        {
            float* resultV = (float*) (((byte*)result) + (i*outputByteStride));
            byte* off = input + inputByteStride*i;
            *resultV = *off;
            *(resultV+1) = 1 - *(off+1);
        }
    }

    /// Untested!
    public unsafe struct GetUVsUInt8InterleavedNormalizedJob : IJobParallelFor  {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        public int outputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector2* result;

        public void Execute(int i)
        {
            float* resultV = (float*) (((byte*)result) + (i*outputByteStride));
            byte* off = input + inputByteStride*i;
            *resultV = *off / 255f;
            *(resultV+1) = 1 - *(off+1) / 255f;
        }
    }

    /// Untested!
    public unsafe struct GetUVsUInt16InterleavedJob : IJobParallelFor  {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        public int outputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector2* result;

        public void Execute(int i)
        {
            float* resultV = (float*) (((byte*)result) + (i*outputByteStride));
            ushort* uv = (ushort*) (input + inputByteStride*i);
            *resultV = *uv;
            *(resultV+1) = 1 - *(uv+1);
        }
    }

    /// Untested!
    public unsafe struct GetUVsUInt16InterleavedNormalizedJob : IJobParallelFor  {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        public int outputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector2* result;

        public void Execute(int i)
        {
            float* resultV = (float*) (((byte*)result) + (i*outputByteStride));
            ushort* uv = (ushort*) (input + inputByteStride*i);
            *resultV = *uv / (float) ushort.MaxValue;
            *(resultV+1) = 1 - *(uv+1) / (float) ushort.MaxValue;
        }
    }

    /// Untested!
    public unsafe struct GetUVsInt16InterleavedJob : IJobParallelFor  {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public short* input;

        [ReadOnly]
        public int outputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector2* result;

        public void Execute(int i)
        {
            float* resultV = (float*) (((byte*)result) + (i*outputByteStride));
            short* uv = (short*) (input + inputByteStride*i);
            *resultV = *uv;
            *(resultV+1) = 1 - *(uv+1);
        }
    }

    /// Untested!
    public unsafe struct GetUVsInt16InterleavedNormalizedJob : IJobParallelFor  {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public short* input;

        [ReadOnly]
        public int outputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector2* result;

        public void Execute(int i)
        {
            float* resultV = (float*) (((byte*)result) + (i*outputByteStride));
            short* uv = (short*) (input + inputByteStride*i);
            *resultV = Mathf.Max( *uv / (float) short.MaxValue, -1.0f);
            *(resultV) = 1 - Mathf.Max( *(uv+1) / (float) short.MaxValue, -1.0f);
        }
    }

    /// Untested!
    public unsafe struct GetUVsInt8InterleavedJob : IJobParallelFor  {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public sbyte* input;

        [ReadOnly]
        public int outputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector2* result;

        public void Execute(int i)
        {
            float* resultV = (float*) (((byte*)result) + (i*outputByteStride));
            sbyte* off = input + inputByteStride*i;
            *resultV = *off;
            *(resultV) = 1 - *(off+1);
        }
    }

    /// Untested!
    public unsafe struct GetUVsInt8InterleavedNormalizedJob : IJobParallelFor  {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public sbyte* input;

        [ReadOnly]
        public int outputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector2* result;

        public void Execute(int i)
        {
            float* resultV = (float*) (((byte*)result) + (i*outputByteStride));
            sbyte* off = input + inputByteStride*i;
            *resultV = Mathf.Max( *off / 127f, -1.0f);
            *(resultV) = 1 - Mathf.Max( *(off+1) / 127f, -1.0f);
        }
    }

    public unsafe struct GetColorsVec3FloatJob : IJobParallelFor {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public float* input;

        [WriteOnly]
        public NativeArray<Color> result;

        public void Execute(int i) {
            float* src = (float*) (((byte*) input) + (i * inputByteStride));
            result[i] = new Color {
                r = src[0],
                g = src[1],
                b = src[2],
                a = 1f
            };
        }
    }

    public unsafe struct GetColorsVec3UInt8Job : IJobParallelFor {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [WriteOnly]
        public NativeArray<Color> result;

        public void Execute(int i) {
            byte* src = input + (i * inputByteStride);
            result[i] = new Color {
                r = src[0] / (float) byte.MaxValue,
                g = src[1] / (float) byte.MaxValue,
                b = src[2] / (float) byte.MaxValue,
                a = 1f
            };
        }
    }

    public unsafe struct GetColorsVec3UInt16Job : IJobParallelFor {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public ushort* input;

        [WriteOnly]
        public NativeArray<Color> result;

        public void Execute(int i)
        {
            ushort* src = (ushort*)(((byte*)input) + (i * inputByteStride));
            result[i] = new Color
            {
                r = src[0] / (float)ushort.MaxValue,
                g = src[1] / (float)ushort.MaxValue,
                b = src[2] / (float)ushort.MaxValue,
                a = 1f
            };
        }
    }

    public unsafe struct GetColorsVec4FloatJob : IJobParallelFor {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public float* input;

        [WriteOnly]
        public NativeArray<Color> result;

        public void Execute(int i) {
            float* src = (float*) (((byte*) input) + (i * inputByteStride));
            result[i] = new Color(
                src[0],
                src[1],
                src[2],
                src[3]
            );
        }
    }

    public unsafe struct GetColorsVec4UInt16Job : IJobParallelFor {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public ushort* input;

        [WriteOnly]
        public NativeArray<Color> result;

        public void Execute(int i)
        {
            ushort* src = (ushort*)(((byte*)input) + (i * inputByteStride));
            result[i] = new Color
            {
                r = src[0] / (float)ushort.MaxValue,
                g = src[1] / (float)ushort.MaxValue,
                b = src[2] / (float)ushort.MaxValue,
                a = src[3] / (float)ushort.MaxValue
            };
        }
    }
    
    public unsafe struct GetColorsVec4UInt8Job : IJobParallelFor {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [WriteOnly]
        public NativeArray<Color> result;

        public void Execute(int i)
        {
            byte* src = input + (i * inputByteStride);
            result[i] = new Color
            {
                r = src[0] / (float)byte.MaxValue,
                g = src[1] / (float)byte.MaxValue,
                b = src[2] / (float)byte.MaxValue,
                a = src[3] / (float)byte.MaxValue
            };
        }
    }

    public unsafe struct MemCopyJob : IJob {

        [ReadOnly]
        public long bufferSize;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public void* input;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public void* result;

        public void Execute() {
            System.Buffer.MemoryCopy(
                input,
                result,
                bufferSize,
                bufferSize
            );
        }
    }

    public unsafe struct GetVector3sJob : IJobParallelFor {
        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public float* input;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public float* result;

        public void Execute(int i) {
            int ti = i*3;
            result[ti] = -input[ti];
            result[ti+1] = input[ti+1];
            result[ti+2] = input[ti+2];
        }
    }

    public unsafe struct GetRotationsFloatJob : IJobParallelFor {

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public float* input;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public float* result;

        public void Execute(int i) {
            result[i*4] = input[i*4];
            result[i*4+1] = -input[i*4+1];
            result[i*4+2] = -input[i*4+2];
            result[i*4+3] = input[i*4+3];
        }
    }
    
    public unsafe struct GetRotationsInt16Job : IJobParallelFor {

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public short* input;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public float* result;

        public void Execute(int i) {
            result[i*4] = Mathf.Max( input[i*4] / (float) short.MaxValue, -1f );
            result[i*4+1] = -Mathf.Max( input[i*4+1] / (float) short.MaxValue, -1f );
            result[i*4+2] = -Mathf.Max( input[i*4+2] / (float) short.MaxValue, -1f );
            result[i*4+3] = Mathf.Max( input[i*4+3] / (float) short.MaxValue, -1f );
        }
    }

    /// <summary>
    /// Untested! Converts an array of glTF space quaternions (normalized, signed bytes) to
    /// Quaternions in Unity space (floats). 
    /// </summary>
    public unsafe struct GetRotationsInt8Job : IJobParallelFor {

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public float* result;

        public void Execute(int i) {
            result[i*4] = Mathf.Max( input[i*4] / (float) short.MaxValue, -1f );
            result[i*4+1] = -Mathf.Max( input[i*4+1] / (float) short.MaxValue, -1f );
            result[i*4+2] = -Mathf.Max( input[i*4+2] / (float) short.MaxValue, -1f );
            result[i*4+3] = Mathf.Max( input[i*4+3] / (float) short.MaxValue, -1f );
        }
    }

    /// Untested!
    public unsafe struct GetVector2sInterleavedJob : IJobParallelFor {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        public int outputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector2* result;

        public void Execute(int i) {
            Vector2* resultV = (Vector2*) (((byte*)result) + (i*outputByteStride));
            byte* off = input + (i*inputByteStride);
            *resultV = *(Vector2*)off;
            (*resultV).y = 1-(*resultV).y;
        }
    }

    public unsafe struct GetVector3sInterleavedJob : IJobParallelFor {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;
        
        [ReadOnly]
        public int outputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector3* result;

        public void Execute(int i) {
            var resultV = (float*) (((byte*)result) + (i*outputByteStride));
            var off = (float*) (input + i*inputByteStride);
            *(resultV) = -*((float*)off);
            *((Vector2*)(resultV+1)) = *((Vector2*)(off+1));
        }
    }

    /// Untested!
    public unsafe struct GetTangentsInterleavedJob : IJobParallelFor {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        public int outputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector4* result;

        public void Execute(int i) {
            Vector4* resultV = (Vector4*) (((byte*)result) + (i*outputByteStride));
            byte* off = input + (i*inputByteStride);
            *resultV = *((Vector4*)off);
            (*resultV).z *= -1;
        }
    }

    public unsafe struct GetVector4sInterleavedJob : IJobParallelFor {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        public int outputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector4* result;

        public void Execute(int i) {
            Vector4* resultV = (Vector4*) (((byte*)result) + (i*outputByteStride));
            byte* off = input + (i*inputByteStride);
            *resultV = *((Vector4*)off);
        }
    }

    /// Untested!
    public unsafe struct GetTangentsInt16NormalizedInterleavedJob : IJobParallelFor {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public short* input;

        [ReadOnly]
        public int outputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector4* result;

        public void Execute(int i) {
            Vector4* resultV = (Vector4*) (((byte*)result) + (i*outputByteStride));
            short* off = (short*) (((byte*)input) + (i*inputByteStride));

            Vector4 tmp;
            tmp.x = Mathf.Max( *off / (float) short.MaxValue, -1f );
            tmp.y = Mathf.Max( *(off+1) / (float) short.MaxValue, -1f );
            tmp.z = -Mathf.Max( *(off+2) / (float) short.MaxValue, -1f );
            tmp.w = Mathf.Max( *(off+3) / (float) short.MaxValue, -1f );
            tmp.Normalize();
            *resultV = tmp;
        }
    }

    /// Untested!
    public unsafe struct GetVector4sInt8NormalizedInterleavedJob : IJobParallelFor {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public sbyte* input;

        [ReadOnly]
        public int outputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector4* result;

        public void Execute(int i) {
            Vector4* resultV = (Vector4*) (((byte*)result) + (i*outputByteStride));
            sbyte* off = input + (i*inputByteStride);

            Vector4 tmp;
            tmp.x = Mathf.Max( *off / 127f, -1f );
            tmp.y = -Mathf.Max( *(off+1) / 127f, -1f );
            tmp.z = -Mathf.Max( *(off+2) / 127f, -1f );
            tmp.w = Mathf.Max( *(off+3) / 127f, -1f );
            tmp.Normalize();
            *resultV = tmp;
        }
    }

    public unsafe struct GetUInt16PositionsInterleavedJob : IJobParallelFor
    {
        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        public int outputByteStride;
        
        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector3* result;

        public void Execute(int i) {
            float* resultV = (float*) (((byte*)result) + (i*outputByteStride));
            byte* off = input + (inputByteStride*i);
            *resultV = -*((ushort*)off);
            *(resultV+1) = *(((ushort*)off)+1);
            *(resultV+2) = *(((ushort*)off)+2);
        }
    }

    public unsafe struct GetUInt16PositionsInterleavedNormalizedJob : IJobParallelFor
    {
        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        public int outputByteStride;
        
        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector3* result;

        public void Execute(int i) {
            Vector3* resultV = (Vector3*) (((byte*)result) + (i*outputByteStride));
            byte* off = input + (inputByteStride*i);
            Vector3 tmp;
            tmp.x = -*(((ushort*)off)) / (float) ushort.MaxValue;
            tmp.y = *(((ushort*)off)+1) / (float) ushort.MaxValue;
            tmp.z = *(((ushort*)off)+2) / (float) ushort.MaxValue;
            tmp.Normalize();
            *resultV = tmp;
        }
    }

    public unsafe struct GetVector3FromInt16InterleavedJob : IJobParallelFor {

        [ReadOnly]
        public int inputByteStride;
        

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector3* result;

        [ReadOnly]
        public int outputByteStride;

        public void Execute(int i) {
            var resultV = (float*)  (((byte*)result) + (i*outputByteStride));
            byte* off = input + (i*inputByteStride);
            *resultV = -*(((short*)off));
            *(resultV+1) = *(((short*)off)+1);
            *(resultV+2) = *(((short*)off)+2);
        }
    }

    public unsafe struct GetVector3FromInt16InterleavedNormalizedJob : IJobParallelFor {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        public int outputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector3* result;

        public void Execute(int i) {
            Vector3* resultV = (Vector3*) (((byte*)result) + (i*outputByteStride));
            byte* off = input + (i*inputByteStride);

            Vector3 tmp;
            tmp.x = -Mathf.Max( *(((short*)off)) / (float) short.MaxValue, -1.0f);
            tmp.y = Mathf.Max( *(((short*)off)+1) / (float) short.MaxValue, -1.0f);
            tmp.z = Mathf.Max( *(((short*)off)+2) / (float) short.MaxValue, -1.0f);
            tmp.Normalize();
            *resultV = tmp;
        }
    }

    public unsafe struct GetVector3FromSByteInterleavedJob : IJobParallelFor {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public sbyte* input;

        [ReadOnly]
        public int outputByteStride;
        
        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector3* result;

        public unsafe void Setup(int inputByteStride, sbyte* src, int outputByteStride, Vector3* dst) {
            this.inputByteStride = inputByteStride;
            this.input = src;
            this.outputByteStride = outputByteStride;
            this.result = dst;
        }

        public void Execute(int i) {
            float* resultV = (float*) (((byte*)result) + (i*outputByteStride));
            sbyte* off = input + (inputByteStride*i);

            *resultV = -*off;
            *(resultV+1) = *(off+1);
            *(resultV+2) = *(off+2);
        }
    }

    public unsafe struct GetVector3FromSByteInterleavedNormalizedJob : IJobParallelFor {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public sbyte* input;

        [ReadOnly]
        public int outputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector3* result;

        public unsafe void Setup(int byteStride, sbyte* src, int outputByteStride, Vector3* dst) {
            this.inputByteStride = byteStride;
            this.input = src;
            this.outputByteStride = outputByteStride;
            this.result = dst;
        }

        public void Execute(int i) {
            Vector3* resultV = (Vector3*) (((byte*)result) + (i*outputByteStride));
            sbyte* off = input + (inputByteStride*i);

            Vector3 tmp;
            tmp.x = -Mathf.Max(-1,*off/127f);
            tmp.y = Mathf.Max(-1,*(off+1)/127f);
            tmp.z = Mathf.Max(-1,*(off+2)/127f);
            tmp.Normalize();
            *resultV = tmp;
        }
    }

    public unsafe struct GetVector3FromByteInterleavedJob : IJobParallelFor {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        public int outputByteStride;
        
        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector3* result;

        public unsafe void Setup(int byteStride, byte* src, int outputByteStride, Vector3* dst) {
            this.inputByteStride = byteStride;
            this.input = src;
            this.outputByteStride = outputByteStride;
            this.result = dst;
        }

        public void Execute(int i) {
            byte* off = input + (i*inputByteStride);
            var resultV = (Vector3*) (((byte*) result) + (i*outputByteStride));
            resultV[i].x = -*off;
            resultV[i].y = *(off+1);
            resultV[i].z = *(off+2);
        }
    }

    public unsafe struct GetVector3FromByteInterleavedNormalizedJob : IJobParallelFor {

        [ReadOnly]
        public int inputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        public int outputByteStride;

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Vector3* result;

        public unsafe void Setup(int byteStride, byte* src, int outputByteStride, Vector3* dst) {
            this.inputByteStride = byteStride;
            this.input = src;
            this.outputByteStride = outputByteStride;
            this.result = dst;
        }

        public void Execute(int i) {
            Vector3* resultV = (Vector3*) (((byte*)result) + (i*outputByteStride));
            byte* off = input + (i*inputByteStride);

            Vector3 tmp;
            tmp.x = -Mathf.Max(-1,*off/255f);
            tmp.y = Mathf.Max(-1,*(off+1)/255f);
            tmp.z = Mathf.Max(-1,*(off+2)/255f);
            tmp.Normalize();
            *resultV = tmp;
        }
    }
    
    public unsafe struct GetJointsUInt8Job : IJobParallelFor  {

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        public int inputByteStride;

        [WriteOnly]
        [NativeDisableUnsafePtrRestriction]
        public uint* result;

        [ReadOnly]
        public int outputByteStride;

        public void Execute(int i)
        {
            uint* resultV = (uint*) (((byte*)result) + (i*outputByteStride));
            byte* off = (byte*) (input + (i*inputByteStride));

            *resultV = *off;
            *(resultV+1) = *(off+1);
            *(resultV+2) = *(off+2);
            *(resultV+3) = *(off+3);
        }
    }


    public unsafe struct GetJointsUInt16Job : IJobParallelFor  {

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public byte* input;

        [ReadOnly]
        public int inputByteStride;

        [WriteOnly]
        [NativeDisableUnsafePtrRestriction]
        public uint* result;

        [ReadOnly]
        public int outputByteStride;

        public void Execute(int i)
        {
            uint* resultV = (uint*) (((byte*)result) + (i*outputByteStride));
            ushort* off = (ushort*) (input + (i*inputByteStride));

            *resultV = *off;
            *(resultV+1) = *(off+1);
            *(resultV+2) = *(off+2);
            *(resultV+3) = *(off+3);
        }
    }

    public unsafe struct GetMatricesJob : IJobParallelFor  {

        [ReadOnly]
        [NativeDisableUnsafePtrRestriction]
        public Matrix4x4* input;

        [WriteOnly]
        public NativeArray<Matrix4x4> result;

        public void Execute(int i)
        {
            result[i] = new Matrix4x4(
                new Vector4(
                    input[i].m00,
                    -input[i].m10,
                    -input[i].m20,
                    input[i].m30
                ),
                new Vector4(
                    -input[i].m01,
                    input[i].m11,
                    input[i].m21,
                    input[i].m31
                ),
                new Vector4(
                    -input[i].m02,
                    input[i].m12,
                    input[i].m22,
                    input[i].m32
                ),
                new Vector4(
                    -input[i].m03,
                    input[i].m13,
                    input[i].m23,
                    input[i].m33
                )
            );
        }
    }
}
