using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testTack
{
    class AllGroup 
    {
        Dictionary<string, Group> groups = new Dictionary<string, Group>();
        protected static string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Desktop\yourTasks.txt";
        public AllGroup() { }
        public AllGroup(string NameofGroup)
        {
            AddGroup(NameofGroup);
        }
        public Group this[string NameOfGroup]
        {
            get => groups[NameOfGroup];
        }
        public void AddGroup(string NameofGroup)
        {
            groups.Add(NameofGroup, new Group(NameofGroup));
        }   
        public bool AllTasksInAllGroups()
        {
            if (groups.Count == 0)
                return false;
            foreach (var group in groups.Values)
            {
                Console.WriteLine($"Группа \"{group.NameOfGroup}\"");
                if (group.Tasks.Count == 0 && group.CompletedTasks.Count == 0) 
                    Console.WriteLine($"    Группа \"{group.NameOfGroup}\" пока что не содержит задач."); 
                else
                {
                    group.Tasks.ForEach(task => Console.WriteLine($"    {task}"));
                    group.CompletedTasks.ForEach(task => Console.WriteLine($"    {task}"));
                }
            }
            return true;
        }

        public void AllNameGroups()
        {
            foreach(var group in groups.Keys)
                Console.WriteLine($"Группа {group}");
        }

        public bool DeleteGroup(string NameOfGroup)
        {
            return groups.Remove(NameOfGroup);
        }
        public bool Search(string NameOfGroup)
        {
            foreach(var group in groups.Keys)
            {
                if (group == NameOfGroup)
                    return true;
            }
            return false;
        }
        public bool isEmpty()
        {
            return groups.Count == 0;
        }
        public void Save()
        {
            using(StreamWriter writer = new StreamWriter(path, true))
            {
                if (groups.Count == 0)
                    writer.WriteLine("Список задач пуст");
                else
                {
                    foreach (var group in groups.Values)
                    {
                        writer.WriteLine($"Группа \"{group.NameOfGroup}\"");
                        if (group.Tasks.Count == 0 && group.CompletedTasks.Count == 0)
                            writer.WriteLine($"    Группа \"{group.NameOfGroup}\" пока что не содержит задач.");
                        else
                        {
                            group.Tasks.ForEach(task => writer.WriteLine($"    {task}"));
                            group.CompletedTasks.ForEach(task => writer.WriteLine($"    {task}"));
                        }
                    }
                }
            }
        }
        public void Save(string pathToFile)
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                if (groups.Count == 0)
                    writer.WriteLine("Список задач пуст");
                else
                {
                    foreach (var group in groups.Values)
                    {
                        writer.WriteLine($"Группа \"{group.NameOfGroup}\"");
                        if (group.Tasks.Count == 0 && group.CompletedTasks.Count == 0)
                            writer.WriteLine($"    Группа \"{group.NameOfGroup}\" пока что не содержит задач.");
                        else
                        {
                            group.Tasks.ForEach(task => writer.WriteLine($"    {task}"));
                            group.CompletedTasks.ForEach(task => writer.WriteLine($"    {task}"));
                        }
                    }
                }
            }
        }
    }
}
