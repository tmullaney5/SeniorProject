using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Microsoft.CognitiveServices.Speech;

namespace SightWordsProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //SynthesisToSpeakerAsync().Wait();
            //Console.WriteLine("Press any key to exit...");
            //Console.ReadKey();
            /* 
            string name = "JEFF";
            Console.WriteLine(name);
            Console.WriteLine("What is your name?");
            Console.WriteLine("Website should load after you put your name in.");
            string name2 = Console.ReadLine();
            Console.WriteLine($"Input is [{name2}]");
            // DEBUG CONSOLE doesn't take input, so can't do ReadLine().
            // need to find a way to use an external terminal or something to test TTS
            */
            

            CreateWebHostBuilder(args).Build().Run();    
        }

        public static async Task SynthesisToSpeakerAsync()
        {
            // Creates an instance of a speech config with specified subscription key and service region.
            // Replace with your own subscription key and service region (e.g., "westus").
            var config = SpeechConfig.FromSubscription("cf557ebf404d4b47bbbbc317141491e8", "westus");

            // Creates a speech synthesizer using the default speaker as audio output.
            using (var synthesizer = new SpeechSynthesizer(config))
            {
                // Receive a text from console input and synthesize it to speaker.
                Console.WriteLine("Type some text that you want to speak...");
                Console.WriteLine("3");
                Console.WriteLine("2");
                Console.WriteLine("1");
                Console.Write("> ");
                string text = "This is the text to speech voice. Website will load right after this.";

                using (var result = await synthesizer.SpeakTextAsync(text))
                {
                    Console.WriteLine($"Input is [{text}]");
                    if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                    {
                        Console.WriteLine($"Speech synthesized to speaker for text [{text}]");
                    }
                    else if (result.Reason == ResultReason.Canceled)
                    {
                        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                        Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                        if (cancellation.Reason == CancellationReason.Error)
                        {
                            Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                            Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                            Console.WriteLine($"CANCELED: Did you update the subscription info?");
                        }
                    }
                }
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();       
    }
}
