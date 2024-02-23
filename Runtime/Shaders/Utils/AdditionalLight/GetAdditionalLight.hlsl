#ifndef CUSTOM_GET_ADDITIONAL_LIGHT
#define CUSTOM_GET_ADDITIONAL_LIGHT

// #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
// #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
#pragma multi_compile _ _SHADOWS_SOFT
#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
// #pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
#pragma multi_compile _ ADDITIONAL_LIGHT_CALCULATE_SHADOWS
#pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
#pragma multi_compile _ SHADOWS_SHADOWMASK


void AdditionalLight_half(half Index, half3 WorldPos, out half3 Direction, out half3 Color, out half DistanceAtten, out half ShadowAtten) {
    Index = floor(Index);
    
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

        uint numAdditionalLights = GetAdditionalLightsCount();

        if (Index >= numAdditionalLights) {
            DistanceAtten = 0;
            ShadowAtten = 0;
            Color = 0;
            Direction = 0;
            
            return;
        }

        Light light = GetAdditionalLight(Index, WorldPos);
                
        DistanceAtten = light.distanceAttenuation;
        Color = light.color;
        Direction = light.direction;

        #if SHADOWS_SCREEN
            ShadowAtten = SampleScreenSpaceShadowMap(shadowCoord);
        #else
            // god I hate this f*cking piece of software for not having a proper documentation. 
            ShadowAtten = AdditionalLightRealtimeShadow(Index, WorldPos, light.direction);

            // Account for baked goodness
            ShadowSamplingData shadowSamplingData = GetAdditionalLightShadowSamplingData(Index);
            ShadowAtten *= SampleShadowmap(
                shadowCoord, 
                TEXTURE2D_ARGS(_AdditionalLightsShadowmapTexture, sampler_AdditionalLightsShadowmapTexture), 
                shadowSamplingData, 
                GetAdditionalLightShadowParams(Index),
                false
            );
        #endif


    #endif
}

void AdditionalLight_float(float Index, float3 WorldPos, out float3 Direction, out float3 Color, out float DistanceAtten, out float ShadowAtten) {
    Index = floor(Index);
    
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

        uint numAdditionalLights = GetAdditionalLightsCount();

        if (Index >= numAdditionalLights) {
            DistanceAtten = 0;
            ShadowAtten = 0;
            Color = 0;
            Direction = 1;
            
            return;
        }

        Light light = GetAdditionalLight(Index, WorldPos);
                
        DistanceAtten = light.distanceAttenuation;
        Color = light.color;
        Direction = light.direction;
        
        #if SHADOWS_SCREEN
            ShadowAtten = SampleScreenSpaceShadowMap(shadowCoord);
        #else
            ShadowAtten = AdditionalLightRealtimeShadow(Index, WorldPos, light.direction);

            // Account for baked goodness
            ShadowSamplingData shadowSamplingData = GetAdditionalLightShadowSamplingData(Index);
            ShadowAtten *= SampleShadowmap(
                shadowCoord, 
                TEXTURE2D_ARGS(_AdditionalLightsShadowmapTexture, sampler_AdditionalLightsShadowmapTexture), 
                shadowSamplingData, 
                GetAdditionalLightShadowParams(Index),
                false
            );
        #endif
    #endif
}

void lum_float(float3 color, out float Luminance) {
	Luminance = color.r * 0.3 + color.g * 0.59 + color.b * 0.11;
}


void lum_half(half3 color, out half Luminance) {
	Luminance = color.r * 0.3 + color.g * 0.59 + color.b * 0.11;
}

#endif