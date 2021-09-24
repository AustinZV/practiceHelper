using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows.Input;

public class listner : MonoBehaviour
{
	public part[] parts;
	public GameObject backgroundPrefab;
	public GameObject numberPrefab;
	public bool paused;

	ChuckSubInstance myChuck;
	GameObject TheGameController;

	private bool started;
	private int counter;

	//MainScript TheScript;
	// Start is called before the first frame update
	void Start()
    {
		TheGameController = GameObject.Find("Button");
		myChuck = GetComponent<ChuckSubInstance>();
		counter = 1;
		paused = false;
		started = false;
	}
	

	// Update is called once per frame
	void Update()
	{
        //print(parts);
        if (parts != null)
        {
			//print(parts[0].timesig);
			if (Input.GetKeyDown(KeyCode.A))
			{
				Debug.Log("playing");
				playFile();
			}
			if (Input.GetKeyDown("space") && !paused)
			{
                if (counter <= parts[0].timesig)
                {
                    setUpNumbers();
                }
                GameObject background = Instantiate(backgroundPrefab, backgroundPrefab.transform.position, Quaternion.identity);
				counter++;
				if (!started)
                {
					started = true;
					playFile();
                }
				else
                {
					myChuck.BroadcastEvent("space");
				}
				
			}
        }
    }

	//8*((3*1)-1)/((3*4)+1)-(8/2)
	//8*2/13

	void setUpNumbers()
    {
		int SCREEN_WIDTH = 16;
		float newX = (float)((float)(SCREEN_WIDTH * ((3 * counter) - 1)) / (float)((3 * parts[0].timesig) + 1) - (float)(SCREEN_WIDTH / 2));
		float newY = numberPrefab.transform.position.y;
		float newZ = numberPrefab.transform.position.z;
		GameObject number = Instantiate(numberPrefab, new Vector3(newX, newY, newZ), Quaternion.identity);
		number.GetComponent<numbers>().nameNum(counter, parts[0].timesig);
		//for (int i=0; i < parts[0].timesig; i++)
		//      {
		//          GameObject number = Instantiate(numberPrefab, numberPrefab.transform.position, Quaternion.identity);
		//	number.GetComponent<numbers>().placeNum((i + 1), parts[0].timesig);
		//      }
	}

	public void stopPlaying()
    {
		parts = null;
		paused = false;
		started = false;
		myChuck.SetFloat("stop", 1);
		myChuck.BroadcastEvent("space");
	}

	public void restartPlaying()
    {
		myChuck.SetFloat("stop", 1);
		myChuck.BroadcastEvent("space");
		playFile();
    }

	//public void pauseAudio()
 //   {
	//	paused = true;
 //   }

	void playFile()
	{
		for (int p = 0; p < parts.Length; p++)
		
		//for (int p = 1; p < 2; p++)
		{
			myChuck.RunCode(string.Format(@"

            global Event space;
			global float stop;
			0 => stop;

			[""C0"", ""C#0"", ""D0"", ""D#0"", ""E0"", ""F0"", ""F#0"",  ""G0"",  ""G#0"",  ""A0"",  ""A#0"",  ""B0"",
            ""C1"", ""C#1"", ""D1"", ""D#1"", ""E1"", ""F1"", ""F#1"", ""G1"", ""G#1"", ""A1"", ""A#1"", ""B1"",
            ""C2"", ""C#2"", ""D2"", ""D#2"", ""E2"", ""F2"", ""F#2"", ""G2"", ""G#2"", ""A2"", ""A#2"", ""B2"",
            ""C3"", ""C#3"", ""D3"", ""D#3"", ""E3"", ""F3"", ""F#3"", ""G3"", ""G#3"", ""A3"", ""A#3"", ""B3"",
            ""C4"", ""C#4"", ""D4"", ""D#4"", ""E4"", ""F4"", ""F#4"", ""G4"", ""G#4"", ""A4"", ""A#4"", ""B4"",
            ""C5"", ""C#5"", ""D5"", ""D#5"", ""E5"", ""F5"", ""F#5"", ""G5"", ""G#5"", ""A5"", ""A#5"", ""B5"",
            ""C6"", ""C#6"", ""D6"", ""D#6"", ""E6"", ""F6"", ""F#6"", ""G6"", ""G#6"", ""A6"", ""A#6"", ""B6"",
            ""C7"", ""C#7"", ""D7"", ""D#7"", ""E7"", ""F7"", ""F#7"", ""G7"", ""G#7"", ""A7"", ""A#7"", ""B7"",
            ""C8"", ""C#8"", ""D8"", ""D#8"", ""E8"", ""F8"", ""F#8"", ""G8"", ""G#8"", ""A8"", ""A#8"", ""B8""
            ] @=> string notes[];

			0.1 => float cycTime;

			[{0}] @=> int pitches[];
            [{1}] @=> int durations[];
			{2} @=> int divisions;
			{3} @=> float bpm;
			{4} @=> int timeSig;
            (0.35*bpm/(60.0*divisions)) => float beat;
            
            
            0 => int cont;
			fun void interrupt()
			{{
					1 => cont;
			}}
			spork ~interrupt();


	        SndBuf buf => dac;
            1 => buf.rate;
            0 => buf.loop;

			0 => float remainder;
			0 => float prev;
			0 => float timeRem;

			0::second => dur beatLen;
			now => time lastBeat;

			for (0 => int b; b<timeSig; b++)
			{{
				space => now;
				now-lastBeat => beatLen;
				now => lastBeat;
				
			}}


            for (0 => int i; i<pitches.cap(); i++) 
            {{
				
                if (pitches[i] > 0)
                {{
                    1 => buf.gain;
                    me.sourceDir() + ""../Scripts/Piano/"" + notes[pitches[i]] + "".wav"" => buf.read;
                }}
                else
                {{
                    0 => buf.gain;
                }}
		        0 => buf.pos;

				durations[i] => remainder;
				
				while (remainder != 0)
				{{
					0 => timeRem;


					if ((prev+remainder) < divisions)
					{{
						(beatLen * remainder / divisions )=> now;
						(prev+remainder) => prev;
						0 => remainder;
					}}
					else
					{{
						space => now;
						now-lastBeat => beatLen;
						now => lastBeat;
						remainder - (divisions-prev) => remainder;
						0 => prev;
					}}
						0 => cont;
					if (stop == 1) {{
						break;
						
					}}
					
				}}

				if (stop == 1) {{
					break;
				}}
		    }}
            

			",
			string.Join(",", parts[p].pitches),
			string.Join(",", parts[p].durations),
			parts[p].divisions,
			parts[p].bpm,
			parts[p].timesig));
			//{0} = pitches, {1} = duration
			//for (0 => int reps; reps <
			//<<< currBeat >>>;
			//for (0 => int x; x < reps; x++)
			//{
			//	{
			//(currBeat + durations[i]) / divisions => int reps;
			//(durations[i] + currBeat) % divisions => currBeat;

			//		<<< ""waiting"" >>>;
			//		space => now;
			//	}
			//}
			//(beat * (durations[i] % divisions))::second => now;
			//(beat * durations[i])::second => now;
		}
	}

	void printArray(string[] arr)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			Debug.Log(arr[i]);
		}
	}
}


public struct part
{
	public int[] pitches;
	public int[] durations;
	public int divisions;
	public float bpm;
	public int timesig;
}



//if (avoidFirst == 0)
//{
//	{
//		(now - pastTime) / ((44100)::second) => beat;
//	}
//}
//else
//{
//	{
//		1 => avoidFirst;
//	}
//}
//now => pastTime;
