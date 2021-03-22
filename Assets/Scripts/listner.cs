using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows.Input;

public class listner : MonoBehaviour
{
	public part[] parts;
	ChuckSubInstance myChuck;
	GameObject TheGameController;
	//MainScript TheScript;
	// Start is called before the first frame update
	void Start()
    {
		TheGameController = GameObject.Find("Button");
		myChuck = GetComponent<ChuckSubInstance>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			Debug.Log("playing");
			playFile();
		}
		if (Input.GetKeyDown("space"))
		{
			myChuck.BroadcastEvent("space");
		}
		//Debug.Log("hi");
	}

	void playFile()
	{
		for (int p = 0; p < parts.Length; p++)
		//for (int p = 1; p < 2; p++)
		{
			myChuck.RunCode(string.Format(@"

            global Event space;

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
			{4} @=> int p;
            (0.35*bpm/(60.0*divisions)) => float beat;
            
            
            0 => int cont;
			fun void wait()
			{{
				0 => int avoidFirst;
				now => time pastTime;
				while(true)
				{{
					space => now;
					1 => cont;
					
					

				}}
			}}
			spork ~ wait();


	        SndBuf buf => dac;
            1 => buf.rate;
            0 => buf.loop;

			0 => float remainder;
			0 => float prev;
			0 => float timeRem;
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
				if (cont == 1) {{
					<<<""yes"">>>;
				}}
					0 => timeRem;

					if (p==2) {{ <<<prev+remainder>>>; }}
					if ((prev+remainder) < divisions)
					{{
						
						while ((timeRem+cycTime) > (beat*remainder))
						{{
							cycTime::second => now;
							timeRem+cycTime => timeRem;
							if (cont == 1)
							{{
								break;
							}}
							
						}}
						if (cont == 0)
						{{
							((beat * remainder)-timeRem)::second => now;
						}}
						(prev+remainder) => prev;

						0 => remainder;
					}}
					else
					{{
						while ((timeRem+cycTime) > (beat * (divisions-prev)))
						{{
							cycTime::second => now;
							timeRem+cycTime => timeRem;
							if (cont == 1)
							{{
								break;
							}}
						}}
						if (cont == 0)
						{{
							((beat * (divisions-prev))-timeRem)::second => now;
						}}
							
						remainder - (divisions-prev) => remainder;
						0 => prev;
						if (cont == 0)
						{{
							space => now;
						}}
					}}
						0 => cont;

				}}
				
			    

                
		    }}
            

			",
			string.Join(",", parts[p].pitches),
			string.Join(",", parts[p].durations),
			parts[p].divisions,
			parts[p].bpm,
			p));
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
