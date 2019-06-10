﻿using NAudio.Utils;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xmusic;
using Xmusic.Players;
using Xmusic.Extensions;
using Microsoft.Expression.Encoder;
using Microsoft.Expression.Encoder.Profiles;
using Microsoft.Win32;
//using NAudio.WindowsMediaFormat;

namespace XMusicTester
{
    class Program
    {
        static void Main(string[] args)
        {
           
            //string file = @"C:\Users\adelasm\Desktop\M-Overhaul\tests\Aster Aweke - 05. Ayzoh.wma";
            string file = @"C:\Users\adelasm\Desktop\M-Overhaul\tests\r. kelly - down low feat ronald and ernie isley.mp3";
            //string file = @"C:\Users\AdelaPc\Desktop\124\Mase - Feel So Good.wav";
            var p = new XSoundActionParameters
            {
                Action = XActionType.TempoAdjustment,
                SourceFileName = file,
                DeleteTemporaryFiles = true
            };
            try
            {
                XSoundProcessor.Process(p);
                Console.WriteLine(p.TotalTimeSpan);
            }
            catch (Exception ex)
            {
                var trace = ex.StackTrace;
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        private static void test5(string file)
        {
            var targetFilename = file.GenerateOutPutPath(XFileType.Mp3);
            ConvertToMP3(file, targetFilename);
        }

        static void ConvertToMP3(string sourceFilename, string targetFilename)
        {
            using (var reader = new NAudio.Wave.AudioFileReader(sourceFilename))
            using (var writer = new NAudio.Lame.LameMP3FileWriter(targetFilename, reader.WaveFormat, NAudio.Lame.LAMEPreset.STANDARD))
            {
                reader.CopyTo(writer);
            }
        }


        private static void TesT4(string file)
        {
            //XTempoProcessor.DoWorkEx(file);
        }

        //private static void Test3(string file)
        //{
        //    XSoundActionParameters p = new XSoundActionParameters
        //    {
        //        Action = XActionType.Convertion,
        //        DestinationFileType = XFileType.Mp3,
        //        SourceFileName = file,
        //    };
        //    XSoundProcessor.Process(p);
        //    if (p.LastActionResult == XActionResult.Success)
        //    {
        //        p.SourceFileName = p.LastActionResult;
        //        p.DestinationFileType = XFileType.Wav;
        //        XSoundProcessor.Process(p);
        //        if (p.LastActionResult == XActionResult.Success)
        //        {
        //            using (var reader = new XAudioReader(p.Output.ToString()))
        //            using (var writer = new XAudioWriter(reader))
        //            {
        //                //XSoundTempoConverter.ConvertAndSave(reader, writer);
        //                Console.WriteLine("Done");
        //                Console.ReadLine();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Error !!");
        //    }
        //}

        private static void Test2(string file)
        {
            using (var reader = new XAudioReader(file))
            using (var waveFileWriter = new WaveFileWriter(reader.OutFileName,  reader.WaveFormat))
            {
                byte[] buffer = new byte[reader.WaveFormat.AverageBytesPerSecond];
                int read;
                while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    waveFileWriter.Write(buffer, 0, read);
                }
            }
        }

        private static void Test(string file)
        {
            //NAudioWavePlayer palyer = new NAudioWavePlayer();
            using (var reader = new XAudioReader(file))
            using (var writer = new XAudioWriter(reader))
            {

                //var speedControl = new NAudionSoundComponent(reader);
                //palyer.Init(speedControl);

                //palyer.Play();
                //var src = AppDomain.CurrentDomain.RelativeSearchPath;
                //speedControl.PlaybackRate = 1.20f;

                //XSoundTempoConverter.ConvertAndSave(reader, writer);
                Console.WriteLine("Done");
                Console.ReadLine();
            }
        }
    }
}