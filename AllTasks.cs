using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testTack
{
    class AllTasks : IComparer<Task>
    {
        protected List<Task> tasks = new List<Task>();
        protected List<Task> completedTasks = new List<Task>();
        protected static string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Desktop\yourTasks.txt";
        public Task this[int id]
        {
            get => tasks[id];
        }
        public Task this[int id]
        {
            get => completedTasks[id];
        }
        public int AddTask(string taskInfo)
        {
            Task task = new Task(taskInfo);
            tasks.Add(task);
            return task.Id;
        }
        public int AddTask(string taskInfo, DateTime dateTime)
        {
            Task task = new Task(taskInfo, dateTime);
            tasks.Add(task);
            return task.Id;
        }
        private void Add(Task task)
        {
            tasks.Add(task);
        }

        public bool All()
        {
            if (tasks.Count == 0 && completedTasks.Count == 0)
            {
                return false;
            }
            else
            {
                tasks.ForEach(task =>
                {
                    Console.WriteLine(task.ToString());
                    if(!task.isEmpty())
                        Console.WriteLine($"{task.CompletedSubTasks.Count}/{task.CompletedSubTasks.Count + task.SubTasks.Count}");
                    task.SubTasks.ForEach(subTask => Console.WriteLine(subTask));
                });
                completedTasks.ForEach(task =>
                {
                    Console.WriteLine(task.ToString());
                    if (!task.isEmpty())
                        Console.WriteLine($"{task.CompletedSubTasks.Count}/{task.CompletedSubTasks.Count + task.SubTasks.Count}");
                    task.CompletedSubTasks.ForEach(subTask => Console.WriteLine(subTask));
                });
                return true;
            }
        }

        public bool DeleteById(int id)
        {
            if (id > 0 && id <= Task.id)
            {
                if (!DeleteByIdInTasks(id))
                    if (!DeleteByIdInComplitedTasks(id)) return false;
                    else return true;
                else return true;
            }
            return false;
        }
        protected bool DeleteByIdInTasks(int id)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Id == id)
                {
                    tasks.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        protected bool DeleteByIdInComplitedTasks(int id)
        {
            for (int i = 0; i < completedTasks.Count; i++)
            {
                if (completedTasks[i].Id == id)
                {
                    completedTasks.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public void Save()
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                tasks.ForEach(task =>
                {
                    writer.WriteLine(task);
                    if(!task.isEmpty())
                    {
                        task.SubTasks.ForEach(subTask => Console.WriteLine(subTask));
                        task.CompletedSubTasks.ForEach(subTask => Console.WriteLine(subTask));
                    }
                });
                completedTasks.ForEach(task =>
                {
                    writer.WriteLine(task);
                    if (!task.isEmpty())
                    {
                        task.SubTasks.ForEach(subTask => Console.WriteLine(subTask));
                        task.CompletedSubTasks.ForEach(subTask => Console.WriteLine(subTask));
                    }
                });
            }
        }

        public bool Save(string pathToFile)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(pathToFile))
                {
                    tasks.ForEach(task => writer.WriteLine(task));
                    completedTasks.ForEach(task => writer.WriteLine(task));
                    return true;
                }
            }
            catch (DirectoryNotFoundException)
            {
                return false;
            }
        }
        public void Load()
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] taskInfo = line.Split(' ');
                    if (taskInfo.Length == 3 && taskInfo[1] != ((char)0x221A).ToString())
                        tasks.Add(new Task(id: int.Parse(taskInfo[0]), nameOfTask: taskInfo[1], deadLine: DateTime.Parse(taskInfo[2])));
                    else if (taskInfo.Length == 3 && taskInfo[1] == ((char)0x221A).ToString())
                        completedTasks.Add(new Task(id: int.Parse(taskInfo[0]), nameOfTask: (char)0x221A + " " + taskInfo[2]));
                    else if (taskInfo.Length == 2)
                        tasks.Add(new Task(id: int.Parse(taskInfo[0]), nameOfTask: taskInfo[1]));
                    else if (taskInfo.Length == 4)
                        completedTasks.Add(new Task(id: int.Parse(taskInfo[0]), nameOfTask: (char)0x221A + " " + taskInfo[2], deadLine: DateTime.Parse(taskInfo[3])));
                    else if (taskInfo[0] == "Ваш" && taskInfo[1] == "список" && taskInfo[2] == "общих") { }


                }
            }
        }

        public bool CompliteById(int id)
        {
            if (id > 0 && id <= Task.id)
            {
                tasks[id - 1].comlited = true;
                Task task = null;
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (tasks[i].Id == id)
                    {
                        task = tasks[i];
                        tasks.RemoveAt(i);
                        break;
                    }
                }
                task.TaskInfo = ((char)0x221A) + " " + task.TaskInfo;
                completedTasks.Add(task);
                completedTasks.Sort(this);
                return true;
            }
            return false;
        }
        public int Compare(Task o1, Task o2)
        {
            return o1.Id - o2.Id;
        }
        public virtual bool Completed()
        {
            if (completedTasks.Count == 0)
                return false;
            else
                completedTasks.ForEach(task => Console.WriteLine(task));
            return true;

        }

        public bool AddDeadline(int id, DateTime dateTime)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Id == id)
                {
                    tasks[i].Deadline = dateTime;
                    return true;
                }
            }
            return false;
        }
        public virtual bool Today()
        {
            bool flag = false;
            foreach (Task task in tasks)
            {
                if ((DateTime.Parse(task.Deadline.ToString()).ToShortDateString()) == (DateTime.Parse(DateTime.Today.ToString()).ToShortDateString()))
                {
                    Console.WriteLine(task);
                    flag = true;
                }
            }
            return flag;
        }

        public bool isEmpty()
        {
            if (tasks.Count != 0 || completedTasks.Count != 0)
                return false;
            return true;
        }

        public bool isNoComplitedEmpty()
        {
            return tasks.Count == 0;
        }

        public bool isComplitedEmpty()
        {
            return completedTasks.Count == 0;
        }

        public void AddSubTask(int id, string subTaskInfo)
        {
            for(int i = 0; i < tasks.Count; i++)
            {
                if(tasks[i].Id == id)
                {
                    tasks[i].Add(subTaskInfo);
                }
            }
        }
        public void CompleteSubTask(int id)
        {
            SubTask subTask = null;
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Id == id)
                {
                    tasks.RemoveAt(i);
                    break;
                }

            }
            subTask.subTaskInfo = ((char)0x221A) + " " + subTask.subTaskInfo;
            CompletedTasks.Add(subTask);
            completedTasks.Sort(this);
            return true;
        }
    }

    /*class AllTasksCompare : IComparer<Task>
    {
        public int Compare(Task fTask, Task sTask)
        {
            if (fTask == null) return -1;
            if (sTask== null) return 1;
            return Compare(fTask.Id, sTask.Id);
        }
    }*/
}