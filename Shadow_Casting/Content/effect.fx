// effect.fx
//

#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0
#define PS_SHADERMODEL ps_4_0
#endif

float4 visibleBounds;
float2 playerPosition;
float2 playerLookAtTarget;
float rangeInDegrees;

Texture2D TextureA;
sampler2D TextureSamplerA = sampler_state
{
    Texture = <TextureA>;
};

// pixel shader
float4 MyPixelShader(float4 position : SV_Position, float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{
    float4 col = tex2D(TextureSamplerA, texCoord) * color;
    float2 pos = position.xy;
    // i could probably mathematically boolean this down get rid of the ifs in a few ways but im tired.
    float dx = (pos.x - visibleBounds.x) * (visibleBounds.z - pos.x);
    float dy = (pos.y - visibleBounds.y) * (visibleBounds.w - pos.y);
    if (dx < 0.0f)
        clip(-1);
    if (dy < 0.0f)
        clip(-1);
    return col;
}

// pixel shader
float4 ConePixelShader(float4 position : SV_Position, float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{
    float2 playerToScreenPixelNormal = normalize(position.xy - playerPosition.xy);
    float2 playerLookAtDirection = normalize(playerLookAtTarget.xy - playerPosition.xy);
    float theta = saturate(dot(playerLookAtDirection, playerToScreenPixelNormal));
    float acos = theta * theta;
    float pixelDegreesToLineOfSight = acos * 90.0f;

    float4 resultingPixelColor = tex2D(TextureSamplerA, texCoord) * color;

    // clips out anything not within the specified range to the line of site.
    clip(pixelDegreesToLineOfSight - (90 - rangeInDegrees / 2.0));
    return resultingPixelColor;
}

technique BasicColorDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL
            MyPixelShader();
    }
}

technique ConeTechnique
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL
            ConePixelShader();
    }
}