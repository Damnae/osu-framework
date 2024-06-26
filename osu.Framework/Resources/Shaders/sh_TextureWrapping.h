#ifndef TEXTURE_WRAPPING_H
#define TEXTURE_WRAPPING_H

float wrap(float coord, int mode, float rangeMin, float rangeMax)
{
    if (mode == 1)
    {
        return clamp(coord, rangeMin, rangeMax);
    }
    else if (mode == 3)
    {
        return mod(coord - rangeMin, rangeMax - rangeMin) + rangeMin;
    }
    
    return coord;
}

vec2 wrap(vec2 texCoord, vec4 texRect)
{
    return vec2(wrap(texCoord.x, g_WrapModeS, texRect[0], texRect[2]), wrap(texCoord.y, g_WrapModeT, texRect[1], texRect[3]));
}

vec4 wrappedSampler(vec2 wrappedCoord, vec4 texRect, texture2D wrapTexture, sampler wrapSampler, float lodBias)
{
    if (g_WrapModeS == 2 && (wrappedCoord.x < texRect[0] || wrappedCoord.x > texRect[2]) ||
        g_WrapModeT == 2 && (wrappedCoord.y < texRect[1] || wrappedCoord.y > texRect[3]))
        return vec4(0.0);

    return texture(sampler2D(wrapTexture, wrapSampler), wrappedCoord, lodBias);
}

#endif