using UnityEditor;
using UnityEngine;
using LitJson;
using System.Collections.Generic;
using System.Linq;

public class StarViewGenerator : MonoBehaviour
{
    private const float lineSegmentOffset = 0.09f;

    [MenuItem("Star View/Generate Matrix")]
    static void GenerateGrid()
    {
        GameObject lineSegmentXPrefab = Resources.Load<GameObject>("Line Segment X");
        GameObject lineSegmentYPrefab = Resources.Load<GameObject>("Line Segment Y");
        Transform gridMatrixTransform = GameObject.FindGameObjectWithTag("Grid Matrix").transform;

        for (float x = 0; x < 2f; x += lineSegmentOffset)
        {
            Vector3 newPosition = new Vector3(lineSegmentYPrefab.transform.position.x, x, 0);
            Instantiate(lineSegmentYPrefab, newPosition, lineSegmentYPrefab.transform.localRotation, gridMatrixTransform);
        }
        for (float x = 0; x > -2f; x -= lineSegmentOffset)
        {
            Vector3 newPosition = new Vector3(lineSegmentYPrefab.transform.position.x, x, 0);
            Instantiate(lineSegmentYPrefab, newPosition, lineSegmentYPrefab.transform.localRotation, gridMatrixTransform);
        }

        for (float y = 0; y < 3f; y += lineSegmentOffset)
        {
            Vector3 newPosition = new Vector3(y, lineSegmentXPrefab.transform.position.y, 0);
            Instantiate(lineSegmentXPrefab, newPosition, lineSegmentXPrefab.transform.localRotation, gridMatrixTransform);
        }
        for (float y = 0; y > -3f; y -= lineSegmentOffset)
        {
            Vector3 newPosition = new Vector3(y, lineSegmentXPrefab.transform.position.y, 0);
            Instantiate(lineSegmentXPrefab, newPosition, lineSegmentXPrefab.transform.localRotation, gridMatrixTransform);
        }
    }

    [MenuItem("Star View/Generate Stars")]
    static void GenerateStars()
    {
        TextAsset namesDatabaseOne = Resources.Load<TextAsset>("Space Objects Names/names0");
        TextAsset namesDatabaseTwo = Resources.Load<TextAsset>("Space Objects Names/names1");
        JsonData namesDatabaseOneData = JsonMapper.ToObject(namesDatabaseOne.text);
        JsonData namesDatabaseTwoData = JsonMapper.ToObject(namesDatabaseTwo.text);

        HashSet<string> finalNamesHashset = new HashSet<string>();
        List<string> finalNames = finalNamesHashset.ToList();

        for (int i = 0; i < namesDatabaseOneData.Count; i++)
        {
            finalNames.Add(namesDatabaseOneData[i].ToString());
        }
        for (int i = 0; i < namesDatabaseTwoData.Count; i++)
        {
            finalNames.Add(namesDatabaseTwoData[i].ToString());
        }

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

        List<KeyValuePair<string, int>> namesRepeated = new List<KeyValuePair<string, int>>();
        int currentNameIndex = 0;

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
                if (Random.Range(0, 10000) > 9965)
                {
                    int randomStarPrefabIndex = Random.Range(0, 6);
                    GameObject star = Resources.Load<GameObject>("Space Objects Prefabs/Star " + randomStarPrefabIndex);
                    GameObject obj = Instantiate(star, starsContainer);
                    Transform objTransform = obj.GetComponent<Transform>();
                    Vector3 new_position = new Vector3(((i % texture_width) * 0.01f) - ((texture_width / 2) * 0.01f), (-((i / texture_width) * 0.01f)) + ((texture_height / 2) * 0.01f), 0);
                    objTransform.position = new_position;

                    bool didNameExistAlready = false;
                    string newName = finalNames[Random.Range(0, finalNames.Count)];
                    for (int j = 0; j < namesRepeated.Count; j++)
                    {
                        if (newName == namesRepeated[j].Key)
                        {
                            int timesRepeared = namesRepeated[j].Value + 1;
                            namesRepeated[j] = new KeyValuePair<string,int>(namesRepeated[j].Key, timesRepeared);
                            newName = namesRepeated[j].Key + " " + namesRepeated[j].Value;

                            didNameExistAlready = true;
                            break;
                        }
                    }

                    if (didNameExistAlready == false)
                    {
                        namesRepeated.Add(new KeyValuePair<string, int>(newName, 0));
                    }

                    obj.GetComponent<StarController>().Initialize(newName);
                    currentNameIndex++;
                }
            }
        }
    }
}
