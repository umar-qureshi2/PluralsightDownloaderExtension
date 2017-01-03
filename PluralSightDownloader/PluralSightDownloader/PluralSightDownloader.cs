//Copyright 2017 Umar-Qureshi ( umar INSERT DOT/NO DOT mcs AT g m a i l)

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

///
/// Code formatted with custom format and not MS C#, initial version is inspired from a blog which i can not find anymore.
///
using System;
using System.Linq;
[assembly: Fiddler.RequiredVersion ("2.2.8.6")]

namespace PluralsightDownloader
    {
    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Threading;
    using Fiddler;


    class PluralSight
        {
        private string url, localPath;
        public PluralSight(string url, string localPath)
            {
            this.url = url;
            this.localPath = localPath;
            }

        public void DownloadFile()
            {
            var webClient = new WebClient ();
            webClient.DownloadFileAsync (new Uri (url), localPath);
            }

        }
    public class VideoDownloader : IAutoTamper, IFiddlerExtension
        {

        private string rootPath;
        public int ExpectedChapterIndex
            {
            get; set;
            }

        public VideoDownloader()
            {
            this.rootPath = @"D:\PluralSight";
            ExpectedChapterIndex = 6;

            }

        public void OnLoad()
            {
            }

        public void OnBeforeUnload()
            {
            }

        public void AutoTamperRequestBefore(Session oSession)
            {
            if ( oSession.uriContains ("pluralsight.com") )
                {
                if ( oSession.uriContains (".mp4") )
                    {
                    var temp = oSession.url.Split ('/');
                    var chapterIndex = -1;
                    if ( temp.Length > ExpectedChapterIndex )
                        {
                        chapterIndex = ExpectedChapterIndex;
                        }
                    else
                        {
                        //sometimes there comes a url with different order but did not find that while testing. will update for that here or apply a pattern
                        }
                    var author = rootPath + "\\" + temp[chapterIndex - 2];
                    //this.createFolder(author);
                    string chapter;
                    var title = rootPath + "\\" + this.GetTitle (temp[chapterIndex - 1], out chapter);
                    chapter = title + "\\" + chapter;
                    this.createFolder (title);
                    this.createFolder (chapter);
                    var file = chapter + "\\" + temp[chapterIndex] + ".mp4";
                    if ( !File.Exists (file) )
                        {
                        var pluralSight = new PluralSight ("http://" + oSession.url, file);
                        var thread = new Thread (new ThreadStart (pluralSight.DownloadFile));
                        thread.Start ();
                        }

                    else
                        {
                        }
                    }
                }
            }



        private void createFolder(string path)
            {
            if ( !Directory.Exists (path) )
                {
                Directory.CreateDirectory (path);
                }
            }


        private string GetTitle(string candidatTitle, out string chapter)
            {
            var temp = candidatTitle.Split ('-');
            var title = string.Empty;
            bool stop = false;
            int i = 0;
            for ( i = 0; i < temp.Length && !stop; i++ )
                {
                if ( Regex.IsMatch (temp[i], "m[0-9][0-9]*") )
                    {
                    stop = true;
                    }
                }

            title = string.Join (" ", temp.Take (i - 1).ToArray ()).TrimEnd ();
            var numberTemp = temp[i - 1].Substring (1);
            int number = -1;
            if ( Int32.TryParse (numberTemp, out number) )
                chapter = String.Format ("{0:00}", number) + " " + string.Join (" ", temp.Skip (i).ToArray ()).TrimEnd ();
            else
                chapter = string.Join (" ", temp.Skip (i - 1).ToArray ()).TrimEnd ().Substring (1);

            return title;
            }

        public void AutoTamperRequestAfter(Session oSession)
            {
            }

        public void AutoTamperResponseBefore(Session oSession)
            {
            }

        public void AutoTamperResponseAfter(Session oSession)
            {
            }

        public void OnBeforeReturningError(Session oSession)
            {
            }
        }
    }