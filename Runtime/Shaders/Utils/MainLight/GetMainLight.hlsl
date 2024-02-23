#ifndef CUSTOM_GET_MAIN_LIGHT
#define CUSTOM_GET_MAIN_LIGHT

#pragma multi_compile _ _MAIN_LIGHT_SHADOWS
#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
#pragma multi_compile _ _SHADOWS_SOFT
// #pragma multi_compile _ _ADDITIONAL_LIGHTS
// #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
#pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
#pragma multi_compile _ SHADOWS_SHADOWMASK


void MainLight_half(half3 WorldPos, out half3 Direction, out half3 Color, out half DistanceAtten, out half ShadowAtten) {
    #if SHADERGRAPH_PREVIEW
        Direction = half3(.5, .5, 0);
        Color = 1;
        DistanceAtten = 1;
        ShadowAtten = 1;
    #else
        #if SHADOWS_SCREEN
            half4 clipPos = TransformWorldToHClip(WorldPos);
            half4 shadowCoord = ComputeScreenPos(clipPos);
        #else
            half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
        #endif

        Light mainLight = GetMainLight(shadowCoord);

        Direction = mainLight.direction;
        Color = mainLight.color;
        DistanceAtten = mainLight.distanceAttenuation;
        
        #if !defined(_MAIN_LIGHT_SHADOWS) || defined(_RECEIVE_SHADOWS_OFF)
            ShadowAtten = 1.0h;
        #endif

        #if SHADOWS_SCREEN
            ShadowAtten = SampleScreenSpaceShadowMap(shadowCoord);
        #else
            ShadowSamplingData shadowSamplingData = GetMainLightShadowSamplingData();
            half shadowStrength = GetMainLightShadowStrength();
            ShadowAtten = SampleShadowmap(
                shadowCoord, 
                TEXTURE2D_ARGS(_MainLightShadowmapTexture, sampler_MainLightShadowmapTexture), 
                shadowSamplingData, 
                shadowStrength, 
                false
            );
        #endif
    #endif
}

void MainLight_float(float3 WorldPos, out float3 Direction, out float3 Color, out float DistanceAtten, out float ShadowAtten) {
    #if SHADERGRAPH_PREVIEW
        Direction = float3(.5, .5, 0);
        Color = 1;
        DistanceAtten = 1;
        ShadowAtten = 1;
    #else
        #if SHADOWS_SCREEN
            float4 clipPos = TransformWorldToHClip(WorldPos);
            float4 shadowCoord = ComputeScreenPos(clipPos);
        #else
            float4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
        #endif

        Light mainLight = GetMainLight(shadowCoord);
        Direction = mainLight.direction;
        Color = mainLight.color;
        DistanceAtten = mainLight.distanceAttenuation;
        
        #if !defined(_MAIN_LIGHT_SHADOWS) || defined(_RECEIVE_SHADOWS_OFF)
            ShadowAtten = 1.0h;
        #endif

        #if SHADOWS_SCREEN
            ShadowAtten = SampleScreenSpaceShadowMap(shadowCoord);
        #else
            ShadowSamplingData shadowSamplingData = GetMainLightShadowSamplingData();
            float shadowStrength = GetMainLightShadowStrength();
            ShadowAtten = SampleShadowmap(
                shadowCoord, 
                TEXTURE2D_ARGS(_MainLightShadowmapTexture, sampler_MainLightShadowmapTexture), 
                shadowSamplingData, 
                shadowStrength, 
                false
            );
        #endif
    #endif
}

#endif