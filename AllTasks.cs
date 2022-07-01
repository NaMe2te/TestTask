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
        protected List<Task> complitedTasks = new List<Task>();
        protected static string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Desktop\yourTasks.txt";
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
            if (tasks.Count == 0 && complitedTasks.Count == 0)
                return false;
            else
            {
                tasks.ForEach(task => Console.WriteLine(task.ToString()));
                complitedTasks.ForEach(task => Console.WriteLine(task.ToString()));
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
            for(int i = 0; i < tasks.Count; i++)
            {
                if(tasks[i].Id == id)
                {
                    tasks.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        protected bool DeleteByIdInComplitedTasks(int id)
        {
            for (int i = 0; i < complitedTasks.Count; i++)
            {
                if (complitedTasks[i].Id == id)
                {
                    complitedTasks.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public void Save()
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                tasks.ForEach(task => writer.WriteLine(task));
                complitedTasks.ForEach(task => writer.WriteLine(task));
            } 
        }

        public bool Save(string pathToFile)
        {
            try {
                using (StreamWriter writer = new StreamWriter(pathToFile))
                {
                    tasks.ForEach(task => writer.WriteLine(task));
                    complitedTasks.ForEach(task => writer.WriteLine(task));
                    return true;
                }
            }
            catch(DirectoryNotFoundException)
            {
                return false;
            }
        }
        public void Load()
        {
            using(StreamReader reader = new StreamReader(path))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    string[] taskInfo = line.Split(' ');
                    if (taskInfo.Length == 3 && taskInfo[1] != ((char)0x221A).ToString())
                        tasks.Add(new Task(id: int.Parse(taskInfo[0]), nameOfTask: taskInfo[1], deadLine: DateTime.Parse(taskInfo[2])));
                    else if (taskInfo.Length == 3 && taskInfo[1] == ((char)0x221A).ToString())
                        complitedTasks.Add(new Task(id: int.Parse(taskInfo[0]), nameOfTask: (char)0x221A + " " + taskInfo[2]));
                    else if (taskInfo.Length == 2)
                        tasks.Add(new Task(id: int.Parse(taskInfo[0]), nameOfTask: taskInfo[1]));
                    else if (taskInfo.Length == 4)
                        complitedTasks.Add(new Task(id: int.Parse(taskInfo[0]), nameOfTask: (char)0x221A + " " + taskInfo[2], deadLine: DateTime.Parse(taskInfo[3])));
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
                complitedTasks.Add(task);
                complitedTasks.Sort(this);
                return true;
            }
            return false;
        }
        public int Compare(Task o1, Task o2)
        {
            return o1.Id - o2.Id;
        }
        public virtual bool Complited()
        {
            if (complitedTasks.Count == 0)
                return false;
            else
                complitedTasks.ForEach(task => Console.WriteLine(task));
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
            if (tasks.Count != 0 || complitedTasks.Count != 0)
                return false;
            return true;
        }

        public bool isNoComplitedEmpty()
        {
            return tasks.Count == 0;
        }

        public bool isComplitedEmpty()
        {
            return complitedTasks.Count == 0;
        }

        public override string ToString()
        {
            
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
