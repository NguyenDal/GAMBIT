using System;
using System.Collections;
using System.Collections.Generic;
using LSL;
using UnityEngine;

public class KeyboardInlet : MonoBehaviour
{
    
        ContinuousResolver resolver;

        // We need to keep track of the inlet once it is resolved.
        private StreamInlet inlet;

        void Start()
        {
            // the resolver will get the first Marker type of outlet (This code needs to be altered if more than one type is being obtained)
            resolver = new ContinuousResolver("type", "Markers");
            StartCoroutine(ResolveExpectedStream());
        }

        IEnumerator ResolveExpectedStream()
        {

            // once the courotine obtains the results, it will instantiate the inlet (Unity)
            var results = resolver.results();
            while (results.Length == 0)
            {
                yield return new WaitForSeconds(.1f);
                results = resolver.results();
            }

            // instantiate inlet
            inlet = new StreamInlet(results[0]);
            Console.Write(inlet.info().as_xml());
        }

        void Update()
        {
            if (inlet != null)
            {
                string[] sample = new string[1];

                // if key is not available in the console, the inlet will pull the sample from the outlet to see if the key is clicked
                if(!Console.KeyAvailable){

                    // the 0.0 is the timeout, this is to stop unity from blocking (being unresponsive until a value is found)
                    inlet.pull_sample(sample, 0.0);

                    // log it in the console.
                    if(sample[0].Contains("pressed") || sample[0].Contains("released")){
                        Debug.Log(sample[0]);
                    }
                }
            }
        }
}
