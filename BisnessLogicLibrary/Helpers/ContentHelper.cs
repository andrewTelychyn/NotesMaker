using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisnessLogicLibrary
{
    public class ContentHelper
    {
        private List<PartHelper> parts;

        public List<PartHelper> Parts 
        {
            get { return parts; }
            set { 
                parts = value;
                CheckParts();
                size = parts.Count;
            } 
        }

        public List<string> Blocks { get; set; }

        MainBody Body { get; set; }

        private int size;

        public int Attempt { get; set; }

        public ContentHelper(List<PartHelper> list, MainBody mainbody)
        {
            Parts = list;
            Body = mainbody;
            Attempt = 0;
            Blocks = new List<string>();
        }

        public void MainBuild()
        {
            string one = PreBuild(); 

            if (one.ToCharArray().Length > 4096)
            {
                Attempt += 1;
                MainBuild();
            }
            else 
            {
                Blocks.Clear();

                Blocks.Add(one);

                PostBuild(size - Attempt, 0);
            }

        }

        private string PreBuild()
        {
            string outcome = Body.Part + "\n\n";

            for (int i = 0; i < size - Attempt; i++)
            {
                outcome += Parts[i].Build();
            }

            if (Attempt != 0)
            {
                outcome += "[...]\n\n";
            }

            return outcome += "(c) " + Body.Name + " " + Body.Author;
        }

        private void PostBuild(int leftBorderInt, int rightBorderInt)
        {
            string outcome = "[...]\n\n";

            if (size < leftBorderInt || size < rightBorderInt)
                throw new IndexOutOfRangeException();

            for (int i = leftBorderInt; i < size - rightBorderInt; i++)//check left border and check right border of a range
            {
                outcome += Parts[i].Build();
            }

            if (rightBorderInt > 0)
                outcome += "[...]\n\n";         //if rec > 0 then 100% will be another window like this

            outcome += "(c) " + Body.Name + " " + Body.Author;

            if (outcome.ToCharArray().Length > 4096)
                PostBuild(leftBorderInt, ++rightBorderInt);
            else
            { 
                Blocks.Add(outcome);

                if(rightBorderInt > 0)
                    PostBuild(size - rightBorderInt, 0);
            }
        }

        private void CheckParts()
        {
            int value = 3500;

            for (int i = 0; i < Parts.Count; i++)
            {
                var result = Parts[i].Build().ToCharArray().Length;

                if (result > value)
                    SplitPart(Parts[i], i, result, value);
            }
        }

        private void SplitPart(PartHelper part, int index, int result, int value)
        {
            int amount = result / value;
            if ((result % value) > (value/3))
                amount += 1;

            var title = part.part.Title;

            List<Part> localParts = new List<Part>();

            for (int i = 1; i <= amount; i++)
            {
                localParts.Add(new Part($"{title}#{i}"));
            }

            int length = part.part.Notes.Count;

            int partNoteSize = length / amount;

            for (int i = 0; i < amount; i++)
            {
                for (int m = 0; m < partNoteSize; m++)
                {
                    if(length > (partNoteSize * i + m))
                        localParts[i].Notes.Add(part.part.Notes[partNoteSize * i + m]);
                }
            }


            Parts.RemoveAt(index);

            for (int i = 0; i < amount; i++)
            {
                Parts.Insert(index + i, new PartHelper(localParts[i]));
            }

        }
    }

}
