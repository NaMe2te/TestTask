using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testTack
{
    public class SubTask
    {
        public string subTaskInfo;
        public Task BaseTask { get; set; }
        public DateTime? Deadline { get; set; } = null;

        public double subId;
        public double SubId 
        {
            get { return subId; }
            set
            {
                double maxId = BaseTask.Id + 0.1;
                if (value >= maxId)
                    Console.WriteLine("Больше нет возможности добавить подзадачу");
                else
                    subId = value;
            }
        }
        public SubTask(string subTaskInfo, Task task)
        {
            this.subTaskInfo = subTaskInfo;
            BaseTask = task;
            task.subTaskId += 0.01;
            SubId = task.subTaskId;
        }
        
        public override string ToString()
        {
            if(Deadline != null)
                return $" {SubId} {subTaskInfo} {DateTime.Parse(Deadline.ToString()).ToShortDateString()}";
            else
                return $" {SubId} {subTaskInfo}";
        }

    }
}
