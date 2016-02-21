using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public class MapManager : MonoBehaviour {
    
    public TextAsset mapFile; // the name of the file containing the map that will be loaded
    [Serializable]
   // the tile struct contains all the information necessary to place a prefab into the scene
    public struct tile {
        public char character;
        public GameObject prefab;
        public bool needsFloor;
    }
    public tile[] tileKey; // the actual mappings
    public GameObject floorPrefab; // the prefab used for flooring

    // these fields are for faster access to cacheable data
    private Dictionary<char, tile> map = new Dictionary<char, tile>();
    private float floorHeight;

	// Use this for initialization
	void Start () {
        // put the tiles into a hashtable for quicker access when building the map
        foreach (tile t in tileKey) {
            this.map[t.character] = t;
        }

        this.floorHeight = floorPrefab.GetComponent<Renderer>().bounds.size.y; // cache the height of the floor prefab

        LoadMapFile(this.mapFile); // load the level into the scene
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // places the character's object into the scene at the specified position
    void PlaceObject(char c, Vector3 pos) {
        if (!map.ContainsKey(c)) { // if the character is unrecognized
            print("Error: unrecognized tile found in map at position: (" + pos.x.ToString() + "," + pos.z.ToString() + ")");
        }
        GameObject prefab = map[c].prefab; // get the prefab corresponding to the character

        if (prefab != null) { // if the prefab is null, place the object
            GameObject obj = Instantiate(prefab); // create the game object in the scene

            // determine the height of the object for correct y placement
            Vector3 bounds = obj.GetComponent<Renderer>().bounds.size; // get object bounds
            pos.y = bounds.y / 2.0f;// determine y placement

			pos.x *= obj.transform.localScale.x;
			pos.z *= obj.transform.localScale.z;

			obj.transform.position = pos; // place the game object in the correct position
        }

        if (map[c].needsFloor) { // if this object needs floor underneath it
            GameObject floor = Instantiate(this.floorPrefab); // make the floor

            // determine the height of the object for correct y placement
            Vector3 floorBounds = floor.GetComponent<Renderer>().bounds.size; // get floor bounds
            pos.y = 0f - (floorBounds.y / 2f) ; // determine y placement

			pos.x *= floor.transform.localScale.x;
			pos.z *= floor.transform.localScale.z;

            floor.transform.position = pos; // place the floor tile underneath the object
        }
    }

    string CleanLine(string line) {
        line = line.Trim('\r'); // trim windows newline
        return line;
    }

    // loads a map into the scene based on its filename
    void LoadMapFile(TextAsset file) {
        try {
            print("Loading map");
            string[] lines = file.text.Trim().Split('\n'); // split the file into lines
            int height = lines.Length; // the number of tiles on the y axis
            string line;
            for(int y = height-1; y > -1; y--) {
                line = lines[y];
                line = CleanLine(line); // remove characters not meant to be parsed
                print("Line " + y.ToString() + ": \"" + line + "\"");
                if (line != null) {
                    for (int x = 0; x < line.Length; x++) { // for each character
                        Vector3 pos = new Vector3(x, 0f, y); // create a position vector based on its position in the text file
                        if (line[x] == '.') { // if this is part of a larger prefab
                            ; // do nothing
                        }
                        else {
                            PlaceObject(line[x], pos); // place the object corresponding to the character into the scene
                        }
                    }
                }
            }
        }
        catch (Exception e) { // catch exceptions
            Console.WriteLine("{0}\n", e.Message);
        }
    }
}
