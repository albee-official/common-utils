#ifndef SOBELOUTLINES_INCLUDED
#define SOBELOUTLINES_INCLUDED

#pragma multi_compile REQUIRE_DEPTH_TEXTURE
#pragma multi_compile REQUIRE_NORMAL_TEXTURE
// #define REQUIRE_DEPTH_TEXTURE

static float2 sobelSamplePoints[9] = {
    float2(-1, 1), float2(0, 1), float2(1, 1),
    float2(-1, 0), float2(0, 0), float2(1, 0),
    float2(-1, -1), float2(0, -1), float2(1, -1)
};

static float sobelXMatrix[9] = {
    1, 0, -1,
    2, 0, -2,
    1, 0, -1
};

static float sobelYMatrix[9] = {
    1, 2, 1,
    0, 0, 0,
    -1, -2, -1
};

void Sobel_float(float2 UV, float Thickness, out float Atten, out float3 DebugColour) {
    float2 sobel = 0;
    [unroll] for (int i = 0; i < 9; i++) {
        float depth = SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV + sobelSamplePoints[i] * Thickness);
        // float depth = lerp(_ProjectionParams.z, _ProjectionParams.y, raw_depth);
    
        sobel += depth * float2(sobelXMatrix[i], sobelYMatrix[i]);
    }

    float3 normal = SHADERGRAPH_SAMPLE_SCENE_NORMAL(UV);
    
    Atten = length(sobel);

    DebugColour = float3(0, 1, 0);
    if (Atten == 0) {
        DebugColour = float3(1, 0, 0);
    }
}

#endif