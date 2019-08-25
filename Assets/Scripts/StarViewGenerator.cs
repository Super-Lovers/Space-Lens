using UnityEditor;
using UnityEngine;

public class StarViewGenerator : MonoBehaviour
{
    [MenuItem("Star View/Generate Basic")]
    static void Generate()
    {
        GameObject star = Resources.Load<GameObject>("Space Objects Prefabs/Star");
        Sprite starsSprite =
            GameObject.FindGameObjectWithTag("Space Objects Texture").GetComponent<SpriteRenderer>().sprite;
        Transform starsContainer =
            GameObject.FindGameObjectWithTag("Space Objects Container").GetComponent<Transform>();

        Color[] pixels = starsSprite.texture.GetPixels();
        //IEnumerable<Color> blackPixels = from pixel in pixels
        //                                 where
        //                                     pixel.r == 0 &&
        //                                     pixel.g == 0 &&
        //                                     pixel.b == 0 && pixel.a == 1
        //                                 select pixel;

        //Debug.Log($"<color=#ff0000>{pixels.Length}</color>");
        //Debug.Log($"<color=#0000ff>{blackPixels.Count()}</color>");

        System.Random rng = new System.Random();
        float texture_width = starsSprite.rect.size.x;
        float texture_height = starsSprite.rect.size.y;
        
        for (int i = 0; i < pixels.Length; i++)
        {
            Color currentPixel = pixels[i];
            // Black pixel match
            if (currentPixel.r == 0 &&
                currentPixel.g == 0 &&
                currentPixel.b == 0 &&
                currentPixel.a == 1)
            {
                if (rng.Next(1000) > 998)
                {
                    GameObject obj = Instantiate(star, starsContainer);
                    Transform objTransform = obj.GetComponent<Transform>();
                    Vector3 new_position = new Vector3(((i % texture_width) * 0.01f), (-((i / texture_width) * 0.01f)), 0);
                    objTransform.position = new_position;
                }
            }
        }
    }
}
