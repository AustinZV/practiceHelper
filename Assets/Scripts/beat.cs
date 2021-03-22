using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;


public class beat : MonoBehaviour
{
	int counter;
	bool loaded;
	//part[] parts;
	public int trial;

    // Start is called before the first frame update
    void Start()
    {
		enabled = false;
		loaded = false;
	    counter = 0;
		//trial = 0;
    }

	// Update is called once per frame
	void Update()
	{
		if (enabled && !loaded)
		{
			enabled = false;
			
			counter++;
			string path = EditorUtility.OpenFilePanel("Choose Song", "", "musicxml");
			if (path != "")
			{
				GameObject.Find("Listner").GetComponent<listner>().parts = readFile(path);
                //Debug.Log(parts.Length);
                loaded = true;
                //GameObject.Find("Button").GetComponentInChildren<Text>().text = "Play";
            }
		}
		//else if (enabled && loaded)
  //      //if (trial == 5)
		//{
		//	enabled = false;
		//	playFile();
		//}
		
	}



    part[] readFile(string path)
	{
		char[] chars = { ' ',  '<',  '>', '=' };
		using (var sr = new StreamReader(path))
		{
			List<List<note>> parts = new List<List<note>>();
			note currNote = new note();

			float curr_x = -1;
			bool same_time = false;
			bool tie = false;

			int divisions = 0;
			float bpm = 0;

			while (!sr.EndOfStream)
			//for (int j = 0; j < 10; j++)
			{
				string[] line = sr.ReadLine().Trim(chars).Split(chars);
				if (line[0] == "part")
				{
					parts.Add(new List<note>());
					curr_x = -1;
				}

                //pitch
				else if (line[0] == "step")
				{
					currNote.pitch = getStep(line[1]);
				}
                else if (line[0] == "alter")
				{
					currNote.pitch += int.Parse(line[1]);
				}
                else if (line[0] == "octave")
				{
					currNote.pitch += (12 * int.Parse(line[1]));
				}
                else if (line[0] == "rest" || line[0] == "rest/")
				{
					currNote.pitch = -1;
				}

				//duration
				else if (line[0] == "duration")
				{
                    int dur = int.Parse(line[1]);
					if (tie)
					{
                        if (!same_time)
						{
							currNote.duration += dur;
						}
					}
                    else
					{
						currNote.duration = dur;
					}
					
				}
                else if (line[0] == "tie")
				{
					if (line[2].Replace("\"", string.Empty) == "start/")
					{
						tie = true;
					}
					else
					{
						tie = false;
					}
					//Debug.Log(type);
				}

				else if (line[0] == "note" && line.Length > 2)
				{
                    float new_x = float.Parse(line[2].Trim('"'));
                    if (curr_x == new_x)
					{
						same_time = true;
					}
                    else
					{
						curr_x = new_x;
					}
				}

				//log note
				else if (line[0] == "/note")
				{
                    if (!same_time)
					{
						if (!tie)
						{
							parts[parts.Count - 1].Add(currNote);
							currNote = new note();
						}
					}
                    else
					{
						same_time = false;
					}
					//Debug.Log(currNote.pitch);
				}

				else if (line[0] == "divisions")
                {
					Debug.Log("Got here");
					divisions = int.Parse(line[1]);
				}
				else if (line[0] == "sound" && line[1] == "tempo")
                {
					Debug.Log(line[2]);
					Debug.Log(line[2].Split('"')[1]);
					bpm = float.Parse(line[2].Split('"')[1]);
					Debug.Log("after");
                }
			}
			part[] retVal = new part[parts.Count];
            for (int p=0; p<retVal.Length; p++)
			{
				retVal[p].pitches = new int[parts[p].Count];
				retVal[p].durations = new int[parts[p].Count];
                for (int n=0; n<parts[p].Count; n++)
				{
					retVal[p].pitches[n] = parts[p][n].pitch;
					retVal[p].durations[n] = parts[p][n].duration;
				}
				retVal[p].divisions = divisions;
				retVal[p].bpm = bpm;
			}
			return retVal;
		}
	}



	int getStep(string n)
	{
		switch (n)
		{
			case "C":
				return 0;
			case "D":
				return 2;
			case "E":
				return 4;
			case "F":
				return 5;
			case "G":
				return 7;
			case "A":
				return 9;
			case "B":
				return 11;
			default:
				return 0;
		}
	}

    
}



//struct part
//{
//	public int[] pitches;
//	public int[] durations;
//}

//public struct measure
//{
//	int beats;
//	note[] notes;
//}

struct note
{
	public int pitch;
	public int duration;
}


