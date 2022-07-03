using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testTack
{
    public class Task
    {
        List<SubTask> subTasks = new List<SubTask>();
        List<SubTask> completedSubTasks = new List<SubTask>();
        public List<SubTask> SubTasks { get { return subTasks; } }
        public List<SubTask> CompletedSubTasks { get { return completedSubTasks; } }
        public string TaskInfo { get; set; }
        public DateTime? Deadline { get; set; } = null;
        public double subTaskId;
        public static int id;
        public int Id { get; set; }
        public bool comlited = false;
        public Task() { }
        public Task(string nameOfTask)
        {
            TaskInfo = nameOfTask;
            id++;
            Id = id;
            subTaskId = id;
        }
        public Task(string nameOfTask, DateTime deadLine)
        {
            TaskInfo = nameOfTask;
            Deadline = deadLine;
            id++;
            Id = id;
            subTaskId = id;
        }
        public Task(int id, string nameOfTask, DateTime deadLine)
        {
            Id = id;
            Task.id = id;
            TaskInfo = nameOfTask;
            Deadline = deadLine;
            subTaskId = id;
        }
        public Task(int id, string nameOfTask)
        {
            Id = id;
            Task.id = id;
            TaskInfo = nameOfTask;
            subTaskId = id;
        }
        public override string ToString()
        {
            if (Deadline != null)
                return $"ID: {Id} {TaskInfo} {DateTime.Parse(Deadline.ToString()).ToShortDateString()}";
            else
                return $"ID: {Id} {TaskInfo}";
        }

        public void Add(string subTaskInfo)
        {
            subTasks.Add(new SubTask(subTaskInfo, this));
        }

        public bool Delete(double subTaskId)
        {
            for (int i = 0; i < subTasks.Count; i++)
            {
                if (subTasks[i].subId == subTaskId)
                {
                    subTasks.RemoveAt(i);
                    return true;
                }
            }
            for (int i = 0; i < completedSubTasks.Count; i++)
            {
                if (completedSubTasks[i].subId == subTaskId)
                {
                    completedSubTasks.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        public bool CompleteById(double id)
        {
            if (id > 0 && id <= Task.id)
            {
                SubTask subTask = null;
                for (int i = 0; i < subTasks.Count; i++)
                {
                    if (subTasks[i].SubId == id)
                    {
                        subTask = subTasks[i];
                        subTasks.Remove(subTasks[i]);
                        break;
                    }
                }
                subTask.subTaskInfo = ((char)0x221A) + " " + subTask.subTaskInfo;
                completedSubTasks.Add(subTask);
                return true;
            }
            return false;
        }
        public bool Completed()
        {
            if (completedSubTasks.Count == 0)
                return false;
            else
                completedSubTasks.ForEach(subTask => Console.WriteLine(subTask));
            return true;
        }
        public void All()
        {
            subTasks.ForEach(subTask => Console.WriteLine($"{subTask}"));
            completedSubTasks.ForEach(subTask => Console.WriteLine($"{subTask}"));
        }
        public bool isEmpty()
        {
            if (completedSubTasks.Count == 0 && subTasks.Count == 0)
                return true;
            return false;
        }
    }
}
