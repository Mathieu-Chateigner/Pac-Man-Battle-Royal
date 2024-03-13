using UnityEngine;

namespace Game
{
    public class ColliderWithSprite : MonoBehaviour
    {
        public bool IsCollidingWithSprite(Vector2 worldPoint, SpriteRenderer spriteRenderer,
            float alphaThreshold = 0.1f)
        {
            Vector2 localPos = spriteRenderer.transform.InverseTransformPoint(worldPoint);
            var sprite = spriteRenderer.sprite;
            var rect = sprite.rect;
            var textureSize = new Vector2(sprite.texture.width, sprite.texture.height);
            var texturePos = new Vector2((localPos.x * sprite.pixelsPerUnit) + rect.width * 0.5f,
                (localPos.y * sprite.pixelsPerUnit) + rect.height * 0.5f);

            if (texturePos.x < 0 || texturePos.y < 0 || texturePos.x >= textureSize.x || texturePos.y >= textureSize.y)
                return false; // Outside of the sprite

            var pixelColor = spriteRenderer.sprite.texture.GetPixel((int)texturePos.x, (int)texturePos.y);
            return pixelColor.a > alphaThreshold;
        }
    }
}