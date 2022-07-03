using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testTack
{
    class Group : AllTasks
    {
        public List<Task> Tasks { get { return tasks; } }
        public List<Task> CompletedTasks { get { return completedTasks; } }
        
        public string NameOfGroup { get; set; }

        public Group(string NameOfGroup)
        {
            this.NameOfGroup = NameOfGroup;
        }

        public bool AllInTheGroup()
        {
            if (tasks.Count == 0)
                return false;
            else
            {
                tasks.ForEach(task => Console.WriteLine("    " + task.ToString()));
                return true;
            }
        }

        
        public override bool Today()
        {
            bool flag = false;
            foreach (Task task in tasks)
            {
                if ((DateTime.Parse(task.Deadline.ToString()).ToShortDateString()) == (DateTime.Parse(DateTime.Today.ToString()).ToShortDateString()))
                {
                    Console.WriteLine("    " + task.Id + " " + task.TaskInfo + " " + DateTime.Parse(task.Deadline.ToString()).ToShortDateString());
                    flag = true;
                }
            }
            return flag;
        }
    }
}
