using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testTack
{
    public class Task 
    {
        public string TaskInfo { get; set; }
        public DateTime? Deadline { get; set; } = null;
        public static int id;
        public int Id { get; set; }
        public bool comlited = false;
        public Task() {}
        public Task(string nameOfTask)
        {
            TaskInfo = nameOfTask;
            id++;
            Id = id;
        }
        public Task(string nameOfTask, DateTime deadLine)
        {
            TaskInfo = nameOfTask;
            Deadline = deadLine;
            id++;
            Id = id;
        }
        public Task(int id, string nameOfTask, DateTime deadLine)
        {
            Id = id;
            Task.id = id;
            TaskInfo = nameOfTask;
            Deadline = deadLine;
        }
        public Task(int id, string nameOfTask)
        {
            Id = id;
            Task.id = id;
            TaskInfo = nameOfTask;
        }
        public override string ToString()
        {
            if (Deadline != null)
                return Id + " " + TaskInfo + " " + DateTime.Parse(Deadline.ToString()).ToShortDateString();
            else
                return Id + " " + TaskInfo;
        }
    }
}
