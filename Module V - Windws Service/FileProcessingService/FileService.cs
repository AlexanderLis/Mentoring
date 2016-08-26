using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf.Filters;

namespace FileProcessingService
{
    class FileService
    {
        FileSystemWatcher watcher;
        string inDir;
        string outDir;
        string resDir;

        Thread workThread;

        ManualResetEvent stopWorkEvent;
        AutoResetEvent newFileEvent;

        public FileService(string inDir, string outDir, string resDir)
        {
            this.inDir = inDir;
            this.outDir = outDir;
            this.resDir = resDir;

            if (!Directory.Exists(inDir))
                Directory.CreateDirectory(inDir);

            if (!Directory.Exists(outDir))
                Directory.CreateDirectory(outDir);

            if (!Directory.Exists(resDir))
                Directory.CreateDirectory(resDir);

            watcher = new FileSystemWatcher(inDir);
            watcher.Created += Watcher_Created;
            watcher.Filter = "img_*.jpeg";

            workThread = new Thread(WorkProcedure);
            stopWorkEvent = new ManualResetEvent(false);
            newFileEvent = new AutoResetEvent(false);
        }

        private void WorkProcedure(object obj)
        {
            do
            {
                var lists = new List<List<file>>();
                char[] delimiters = new char[] { '_', '.' };

                foreach (var file in Directory.EnumerateFiles("in"))
                {
                    if (file.Contains("img"))
                    {

                        var newFile = new file
                        {
                            index = Convert.ToInt32(file.Split(delimiters)[1]),
                            title = file
                        };

                        //add first image set
                        if (lists.Count == 0)
                        {
                            lists.Add(new List<file>() { newFile });
                            continue;
                        }

                        bool filePut = false;

                        //add image to the appropriate set
                        foreach (var f in lists)
                        {
                            if (f.SingleOrDefault(item => item.index == newFile.index + 1 || item.index == newFile.index - 1) == null) continue;
                            f.Add(newFile);
                            filePut = true;
                            break;
                        }
                        if (!filePut)
                        {
                            lists.Add(new List<file>(new[] { newFile }));
                        }
                    }
                    else
                    {
                        var inFile = file;
                        var outFile = Path.Combine("out", Path.GetFileName(file));
                        Move(inFile, outFile);
                    }
                }

                foreach (var docList in lists)
                {
                    
                    Merge(docList, lists.IndexOf(docList));
                }
                                
            }
            while (WaitHandle.WaitAny(new WaitHandle[] { stopWorkEvent, newFileEvent }, 1000) !=0 );
        }

        class file
        {
            public string title;
            public int index;

        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            newFileEvent.Set();
        }

        public void Start()
        {
            workThread.Start();
            watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            stopWorkEvent.Set();
            workThread.Join();   
        }

        private bool TryOpen(string fileName, int tryCount)
        {
            for (int i = 0; i < tryCount; i++)
            {
                try
                {
                    var file = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    file.Close();

                    return true;
                }
                catch (IOException)
                {
                    Thread.Sleep(5000);
                }
            }

            return false;
        }

        private void Move(string pathIn, string pathOut)
        {
            File.Move(pathIn, pathOut);
            
        }

        private void Merge(List<file> docList, int index)
        {


            var document = new Document();
            var section = document.AddSection();

            foreach (var file in docList.Select(x => x.title))
            {
                var img = section.AddImage(file);
                img.Height = document.DefaultPageSetup.PageHeight/2;
                img.Width = document.DefaultPageSetup.PageWidth/2;

                section.AddPageBreak();
            }

            var render = new PdfDocumentRenderer();
            render.Document = document;

            render.RenderDocument();


            render.Save(Path.Combine(resDir, "result" + index + ".pdf"));

            foreach (var file in docList.Select(x => x.title))
            {
                var inFile = file;
                var outFile = Path.Combine(outDir, Path.GetFileName(file));

                if (TryOpen(inFile, 3))
                    File.Move(inFile, outFile);
            }
        }

    }
}
