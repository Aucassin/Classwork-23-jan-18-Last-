using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classwork_23_jan_18__Last_
{
    class File
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string Extention
        {
            get
            {
                string[] parts = Name.Split('.');
                return parts[parts.Length - 1];
            }
        }

        public void Parse(string str)
        {
            //Text: file.txt(6B);Some string content
            var data = str.Split(':')[1];
            ParseInternal(data.Split(';'));
        }

        protected virtual void ParseInternal(string[] data)
        {
            var parts = data[0].Split('(', ')');
            Name = parts[0];
            Size = parts[1];
        }

        public override string ToString()
        {
            return $@"  {Name}
        Extention: {Extention}
        Size: {Size}";
        }
    }

    class TextFile : File
    {
        public string Content { get; set; }
        protected override void ParseInternal(string[] data)
        {
            base.ParseInternal(data);
            Content = data[1];
        }

        public override string ToString()
        {
            return base.ToString() + $@"
        Content: {Content}";
        }
    }

    class Image : File
    {
        public string Resolution { get; set; }
        protected override void ParseInternal(string[] data)
        {
            base.ParseInternal(data);
            Resolution = data[1];
        }
        public override string ToString()
        {
            return base.ToString() + $@"
        Resolution: {Resolution}";
        }
    }

    class Movie : Image
    {
        public string Length { get; set; }
        protected override void ParseInternal(string[] data)
        {
            base.ParseInternal(data);
            Length = data[2];
        }
        public override string ToString()
        {
            return base.ToString() + $@"
        Lenght: {Length}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string text = @"Text:file.txt(6B);Some string content
            Image:img.bmp(19MB);1920?1080
            Text:data.txt(12B);Another string
            Text:data1.txt(7B);Yet another string
            Movie:logan.2017.mkv(19GB);1920?1080;2h12m";

            var strs = text.Split('\n');

            File[] result = new File[strs.Length];
            int index = 0;
            foreach (var item in strs)
            {
                string type = item.Split(':')[0];
                File file =null;
                switch(type)
                {
                    case "Text":
                        file = new TextFile();
                        break;
                    case "Movie":
                        file = new Movie();
                        break;
                    case "Image":
                        file = new Image();
                        break;
                }
                file.Parse(item);
                result[index] = file;
                index++;
            }

            var t = result.GroupBy(f => f.GetType());

            foreach (var item in t)
            {
                var types = item.ToList();
                types.ForEach(f => Console.WriteLine(f));
            }

            //Console.WriteLine("Text Files:");
            //foreach (var item in result)
            //{
            //    if(item is TextFile)
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine("Movies:");
            //foreach (var item in result)
            //{
            //    if (item is Movie)
            //        Console.WriteLine(item);
            //}
            //Console.WriteLine("Images:");
            //foreach (var item in result)
            //{
            //    if (item is Image)
            //        Console.WriteLine(item);
            //}

        }
    }
}
