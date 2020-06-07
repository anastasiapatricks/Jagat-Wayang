using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class TextureHelper
{
    public static Texture2D Copy(Texture2D src)
    {
        Texture2D texture = new Texture2D(src.width, src.height, TextureFormat.RGBA32, false);
        //texture.SetPixels(0, 0, src.width, src.height, src.GetPixels());
        //texture.Apply();
        Graphics.CopyTexture(src, texture);
        return texture;
    }

    public static Texture2D CreateTextureFromMask(bool[,] mask, Texture2D baseTexture, Vector2Int start)
    {
        int height = mask.GetLength(0);
        int width = mask.GetLength(1);

        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (mask[i, j])
                {
                    Color c = baseTexture.GetPixel(
                        (start.x + j) % baseTexture.width,
                        (start.y + i) % baseTexture.height);
                    texture.SetPixel(j, i, c);
                } else
                {
                    texture.SetPixel(j, i, Color.clear);
                }
            }
        }
        texture.Apply();

        return texture;
    }

    public static void Paste(Texture2D dest, bool[,] destMask, Texture2D src, Vector2Int startPos)
    {
        Color Over(Color over, Color below)
        {
            float Blend(float x, float y)
            {
                return x * over.a + y * below.a * (1 - over.a);
            }

            return new Color(
                Blend(over.r, below.r),
                Blend(over.g, below.g),
                Blend(over.b, below.b),
                over.a + below.a * (1 - over.a));
        }

        int endX = Mathf.Min(dest.width, startPos.x + src.width);
        int endY = Mathf.Min(dest.height, startPos.y + src.height);

        for (int i = startPos.y ; i < endY; i++)
        {
            for (int j = startPos.x; j < endX; j++)
            {
                if (destMask[i, j])
                {
                    dest.SetPixel(
                        j, i, Over(
                            dest.GetPixel(j, i),
                            src.GetPixel(j - startPos.x, i - startPos.y)));
                    //dest.SetPixel(j, i, src.GetPixel(startPos.x - j, startPos.y - i));
                }
            }
        }

        dest.Apply();
    }

    public static Sprite GetSprite(Texture2D texture, Sprite like = null)
    {
        return Sprite.Create(texture,
            new Rect(0, 0, texture.width, texture.height),
            like == null ?
                new Vector2(0.5f, 0.5f) :
                new Vector2(like.pivot.x / texture.width, like.pivot.y / texture.height)
            );
    }
}
