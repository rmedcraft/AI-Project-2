using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapData : MonoBehaviour {
    int height = 5;
    int width = 5;

    public TextAsset text;

    public List<string> GetTextFromFile(TextAsset text) {
        List<string> line = new List<string>();
        if (text != null) {
            string textData = text.text;
            string[] delimeters = { "\r\n", "\n" };
            line = textData.Split(delimeters, System.StringSplitOptions.None).ToList();
        }
        return line;
    }

    public List<string> GetTextFromFile() {
        return GetTextFromFile(text);
    }

    public void GetDimensions(List<string> textLines) {
        height = textLines.Count();
        foreach (string line in textLines) {
            width = line.Length;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // syntax for making a 2D array
    public int[,] MakeMap() {
        List<string> lines = GetTextFromFile();
        GetDimensions(lines);
        int[,] map = new int[width, height];
        for (int r = 0; r < width; r++) {
            for (int c = 0; c < height; c++) {
                map[r, c] = (int)char.GetNumericValue(lines[c][r]);
            }
        }
        // map[0, 3] = 1;
        // map[1, 4] = 1;
        return map;
    }
}
