using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testTack
{
    class Menu
    {
        AllTasks allTasks = new AllTasks();
        AllGroup classGroup = new AllGroup();
        public Menu()
        {
            StartChoice();
            Switch();
        }
        
        private void Switch()
        {
            Console.WriteLine();
            Console.Write("Введите команду: ");
            string allCommand = Console.ReadLine();
            string[] splitAllCommand = allCommand.Split(' ');
            string command;
            string idOrNameOfGroup;
            if (splitAllCommand.Length > 1)
            {
                command = splitAllCommand[0];
                idOrNameOfGroup = splitAllCommand[1];
            }
            else
            {
                command = splitAllCommand[0];
                idOrNameOfGroup = null;
            }

            switch (command)
            {
                case "/commands":
                    StartChoice();
                    Switch();
                    break;

                case "/add":
                    Console.Write("Опишите свою задачу: ");
                    string taskInfo = Console.ReadLine();
                    int idOfTask = allTasks.AddTask(taskInfo);
                    Console.WriteLine($"Ваша задача c ID \"{idOfTask}\" успешно добавлена!");
                    ContinueChoice();
                    break;

                case "/add-with-deadline":
                    Console.Write("Опишите свою задачу: ");
                    taskInfo = Console.ReadLine();
                    Console.Write("Укажите дедлайн: ");
                    DateTime dateTime = Convert.ToDateTime(Console.ReadLine());
                    idOfTask = allTasks.AddTask(taskInfo, dateTime);
                    Console.WriteLine($"Ваша задача c ID \"{idOfTask}\" и дедлайном {DateTime.Parse(dateTime.ToString()).ToShortDateString()} успешно добавлена!");
                    ContinueChoice();
                    break;

                case "/add-deadline":
                    if (!allTasks.isNoComplitedEmpty())
                    {
                        Console.Write("Укажите дедлайн: ");
                        dateTime = Convert.ToDateTime(Console.ReadLine());
                        AddDeadline(dateTime);
                    }
                    else
                    {
                        Console.WriteLine("Ваш список задач пуст, дедлайн добавить нельзя");
                    }
                    ContinueChoice();
                    break;

                case "/delete":
                    if (idOrNameOfGroup != null && idOrNameOfGroup != " ")
                        {
                        if (!allTasks.isEmpty())
                        {
                            allTasks.All();
                            int id = int.Parse(idOrNameOfGroup);
                            if (allTasks.DeleteById(id))
                            {
                                Console.WriteLine($"Ваша задача с ID \"{id}\" успешно удалена!");
                            }
                            else
                            {
                                if (ContinueChoiceAfterTryForTask(id))
                                    TryToDeleteByIdInTheTask();
                                else { Switch(); }
                            }
                        }
                        else Console.WriteLine("Ваш список задач пуст");
                    }
                    else TryToDeleteByIdInTheTask();
                    ContinueChoice();
                    break;

                case "/all":
                    if (allTasks.All()) { }
                    else Console.WriteLine("Ваш список общих задач пуст");
                    if (classGroup.AllTasksInAllGroups()) { }
                    else Console.WriteLine("Нет ни одной группы");
                    ContinueChoice();
                    break;

                case "/today":
                    if (!allTasks.isEmpty()) {
                        Console.WriteLine("Ваш список задач, который нужно сделать сегодня:");
                        if (allTasks.Today()) { }
                        else
                            Console.WriteLine("Сегодня нет задач, которые нужно сделать");
                    }
                    else
                        Console.WriteLine("Ваш список задач пуст");
                    ContinueChoice();
                    break;

                case "/save":
                    Console.WriteLine("Можете указать путь до файла, в который хотите сохранить задачи," +
                        "или по условию задачи схоранятся на рабочий стол");
                    Console.WriteLine("Нажмите \"1\", чтобы сохранить на рабочий стол, \"2\" - чтобы сохранить в указанный файл");
                    int choice = int.Parse(Console.ReadLine());
                    if (choice == 1)
                    {
                        allTasks.Save();
                        classGroup.Save();
                    }
                    else if (choice == 2)
                    {
                        Console.Write("Введите путь до файла: ");
                        string path = Console.ReadLine();
                        Console.Write("Введите название файла: ");
                        string fileName = Console.ReadLine();
                        path += $@"\{fileName}.txt";
                        if (allTasks.Save(pathToFile: $@"{path}"))
                        {
                            classGroup.Save(pathToFile: $@"{path}");
                            Console.WriteLine($"Файл успешно сохранен по пути \"{path}\"");
                        }
                        else
                            Console.WriteLine($"Путь {path} не был найден");
                    }
                    ContinueChoice();
                    break;

                case "/load":
                    
                    break;

                case "/complete":
                    if(idOrNameOfGroup != null)
                    {
                        if (!allTasks.isNoComplitedEmpty())
                        {
                            int id = int.Parse(idOrNameOfGroup);
                            if (allTasks.CompliteById(id))
                                Console.WriteLine($"Ваша задача с ID \"{id}\" помечена как выполненая!");
                            else
                            {
                                if (ContinueChoiceAfterTryForTask(id))
                                    TryToCompliteByIdInTheTask();
                                else { Switch(); }
                            }
                        }
                        else
                            Console.WriteLine("Список невыполненых задач пуст");
                    }
                    else TryToCompliteByIdInTheTask();
                    ContinueChoice();
                    break;

                case "/completed":
                    Console.WriteLine("Список всех ваших выполненных задач:");
                    if(!allTasks.isComplitedEmpty()) 
                    {
                        allTasks.Completed();    
                    }
                    else Console.WriteLine("Ни одной задачи не выполнено.");
                    ContinueChoice();
                    break;

                case "/create-group":
                    if(idOrNameOfGroup != null)
                    {
                        string nameOfTheGroup = idOrNameOfGroup;
                        classGroup.AddGroup(nameOfTheGroup);
                        Console.WriteLine($"Группа с именем \"{nameOfTheGroup}\" была удачно создана!");
                    }
                    else
                    {
                        Console.Write("Введите название группы: ");
                        string nameOfTheGroup = Console.ReadLine();
                        classGroup.AddGroup(nameOfTheGroup);
                        Console.WriteLine($"Группа с именем \"{nameOfTheGroup}\" была удачно создана!");
                    }
                    ContinueChoice();
                    break;

                case "/delete-group":
                    if(idOrNameOfGroup != null)
                    {
                        if (!classGroup.isEmpty())
                        {
                            classGroup.AllNameGroups();
                            if (classGroup.Search(idOrNameOfGroup))
                            {
                                classGroup.DeleteGroup(idOrNameOfGroup);
                                Console.WriteLine($"Ваша группа с названием \"{idOrNameOfGroup}\" успешно удалена");
                            }
                            else
                            {
                                if (ContinueChoiceAfterTryForGroup(idOrNameOfGroup))
                                    TryToDeleteGroup();
                                else { Switch(); }
                            }
                        }
                        else
                            Console.WriteLine("Ваш список групп пуст");
                    }
                    else TryToDeleteGroup();
                    ContinueChoice();
                    break;

                case "/all-in-group":
                    if(idOrNameOfGroup != null)
                    {
                        if (!classGroup.isEmpty())
                        {
                            if (classGroup.Search(idOrNameOfGroup))
                            {
                                if (!classGroup[idOrNameOfGroup].isEmpty())
                                {
                                    Console.WriteLine($"Все задачи в группе \"{idOrNameOfGroup}\":");
                                    classGroup[idOrNameOfGroup].All();
                                }
                                else
                                    Console.WriteLine($"Список задач в группе \"{idOrNameOfGroup}\" пуст");
                            }
                            else
                            {
                                if (ContinueChoiceAfterTryForGroup(idOrNameOfGroup))
                                    TryAllTasksInTheGroup();
                                else { Switch(); }
                            }
                        }
                        else
                            Console.WriteLine("Ваш список групп пуст");
                    }
                    else TryAllTasksInTheGroup();
                    ContinueChoice();
                    break;

                case "/add-to-group":
                    if(idOrNameOfGroup != null)
                    {
                        if (!classGroup.isEmpty())
                        {
                            if (classGroup.Search(idOrNameOfGroup))
                            {
                                Console.WriteLine("Чтобы добавить задачу без дедлайна, нажмите 1, чтобы добавить c дедлайном, нажмите любую другую цирфу");
                                int isDeadline = int.Parse(Console.ReadLine());
                                if (isDeadline == 1)
                                {
                                    Console.Write("Опишите свою задачу: ");
                                    taskInfo = Console.ReadLine();
                                    idOfTask = classGroup[idOrNameOfGroup].AddTask(taskInfo);
                                    Console.WriteLine($"Ваша задача c ID \"{idOfTask}\" успешно добавлена в группу \"{idOrNameOfGroup}\"!");
                                }
                                else
                                {
                                    Console.Write("Опишите свою задачу: ");
                                    taskInfo = Console.ReadLine();
                                    Console.Write("Введите дедлайн: ");
                                    dateTime = Convert.ToDateTime(Console.ReadLine());
                                    idOfTask = classGroup[idOrNameOfGroup].AddTask(taskInfo, dateTime);
                                    Console.WriteLine($"Ваша задача c ID \"{idOfTask}\" и дедлайном \"{dateTime}\" успешно добавлена в группу \"{idOrNameOfGroup}\"!");
                                }
                            }
                            else
                            {
                                if (ContinueChoiceAfterTryForGroup(idOrNameOfGroup))
                                    TryToAddToGroup();
                                else { Switch(); }
                            }
                        }
                        else
                            Console.WriteLine("Ваш список групп пуст");
                    }
                    else TryToAddToGroup();
                    ContinueChoice();
                    
                    break;

                case "/delete-from-group":
                    if(idOrNameOfGroup != null)
                    {
                        if (!classGroup.isEmpty())
                        {
                            if (classGroup.Search(idOrNameOfGroup))
                            {
                                if (!classGroup[idOrNameOfGroup].isNoComplitedEmpty() || !classGroup[idOrNameOfGroup].isComplitedEmpty())
                                {
                                    classGroup[idOrNameOfGroup].All();
                                    Console.WriteLine("Введите ID задачи, которую нужно удалить");
                                    int id = int.Parse(Console.ReadLine());
                                    if (classGroup[idOrNameOfGroup].DeleteById(id))
                                        Console.WriteLine($"Ваша задача с ID \"{id}\" в группе {idOrNameOfGroup} успешно удалена!");
                                    else
                                    {
                                        Console.WriteLine($"Задачи с ID \"{id}\" в группе {idOrNameOfGroup} не существует, повторите попытку");
                                        TryToDeleteByIdInTheGroup(idOrNameOfGroup);
                                    }
                                }
                                else
                                    Console.WriteLine($"Список задач в группе \"{idOrNameOfGroup}\" пуст");
                            }
                            else
                            {
                                if (ContinueChoiceAfterTryForGroup(idOrNameOfGroup))
                                    TryToDeleteByIdInTheGroup();
                                else { Switch(); }
                            }
                        }
                        else
                            Console.WriteLine("Ваш список групп пуст");
                    }
                    else TryToDeleteByIdInTheGroup();
                    ContinueChoice();
                    break;

                case "/complete-in-group":
                    if(idOrNameOfGroup != null)
                    {
                        if (!classGroup.isEmpty())
                        {
                            if (classGroup.Search(idOrNameOfGroup))
                            {
                                if (!classGroup[idOrNameOfGroup].isNoComplitedEmpty())
                                {
                                    classGroup[idOrNameOfGroup].All();
                                    Console.WriteLine("Введите ID задачи, которую вы выполнили");
                                    int id = int.Parse(Console.ReadLine());
                                    if (classGroup[idOrNameOfGroup].CompliteById(id))
                                        Console.WriteLine($"Ваша задача с ID \"{id}\" в группе {idOrNameOfGroup} успешно отмеченена как выполненная!");
                                    else
                                    {
                                        Console.WriteLine($"Задачи с ID \"{id}\" в группе {idOrNameOfGroup} не существует, повторите попытку");
                                        TryToCompliteByIdInTheGroup(idOrNameOfGroup);
                                    }
                                }
                                else
                                    Console.WriteLine($"Список невыполненых задач в группе \"{idOrNameOfGroup}\" пуст");
                            }
                            else
                            {
                                if (ContinueChoiceAfterTryForGroup(idOrNameOfGroup))
                                    TryToCompliteByIdInTheGroup();
                                else { Switch(); }
                            }
                        }
                        else
                            Console.WriteLine("Ваш список групп пуст");
                    }
                    else TryToCompliteByIdInTheGroup();
                    ContinueChoice();
                    break;

                case "/all-completed-in-group":
                    if(idOrNameOfGroup != null)
                    {
                        if (classGroup.isEmpty())
                        {
                            if (classGroup.Search(idOrNameOfGroup))
                            {
                                if (!classGroup[idOrNameOfGroup].isComplitedEmpty())
                                {
                                    Console.WriteLine($"Список всех выполенных задач в группе {idOrNameOfGroup}");
                                    classGroup[idOrNameOfGroup].Completed();
                                }
                                else
                                    Console.WriteLine($"Список выполненых задач в группе \"{idOrNameOfGroup}\" пуст");
                            }
                            else
                            {
                                if (ContinueChoiceAfterTryForGroup(idOrNameOfGroup))
                                    TryToCompliteByIdInTheGroup();
                                else { Switch(); }
                            }
                        }
                        else
                            Console.WriteLine("Ваш список групп пуст");
                    }
                    else TryToCompliteByIdInTheGroup();
                    ContinueChoice();
                    break;

                case "/add-subtask":
                    Console.Write("Введите описание подзадачи: ");
                    string subTaskInfo = Console.ReadLine();
                    int _id = int.Parse(idOrNameOfGroup);
                    allTasks.AddSubTask(_id, subTaskInfo);
                    ContinueChoice();
                    break;

                case "/complete-subtask":
                    _id = int.Parse(idOrNameOfGroup);
                    allTasks.;
                    ContinueChoice();
                    break;

                case "/exit":
                    allTasks.Save();
                    classGroup.Save();
                    Console.WriteLine("Все ваши записанные задачи были сохранены по умолчанию в файл на рабочем столе");
                    Console.WriteLine("До новых встреч!");
                    break;

                default:
                    Console.WriteLine("Неверно введена команда, введите ее правильно!");
                    Switch();
                    break;
            }
        }
        private void StartChoice()
        {
            Console.WriteLine("Вывести весь список доступных задач \"/commands\"");
            Console.WriteLine("Добавить задачу, не указывая дедлайн \"/add\"");
            Console.WriteLine("Добавить задачу, добавив к ней дедлайн \"/add-with-deadline\"");
            Console.WriteLine("Добавить к существующей задаче дедлайн \"/add-deadline\"");
            Console.WriteLine("Удалить задачу по ее идентификатору \"/delete {id}\"");
            Console.WriteLine("Вывести весь список ваших уже имеющихся задач, включая задачи из группы \"/all\"");
            Console.WriteLine("Вывести список задач, которые нужно сделать сегодня \"/today\"");
            Console.WriteLine("Сохранить все задачи в указанный файл, включая задачи из группы \"/save\"");
            Console.WriteLine("Сохранить все задачи из указанного файла, включая задачи из группы \"/load\"");
            Console.WriteLine("Пометить задачу как выполненую \"/complete {id}\"");
            Console.WriteLine("Посмотреть все выполненные задачи \"/completed\"");
            Console.WriteLine();
            Groups();
            Console.WriteLine();
            SubTasks();
            Console.WriteLine("Закончить работу программы \"/exit\"");
        }

        private void Groups()
        {
            Console.WriteLine("Создать группу \"/create-group {name-group}\"");
            Console.WriteLine("Удалить группу \"/delete-group {name-group}\"");
            Console.WriteLine("Посмотреть все задачи в группе \"/all-in-group {name-group}\"");
            Console.WriteLine("Добавить задачу в группу \"/add-to-group {name-group}\"");
            Console.WriteLine("Удалить задачу из группы \"/delete-from-group {name-group}\"");
            Console.WriteLine("Пометить задачу как выполненую в группе \"/complete-in-group {name-group}\"");
            Console.WriteLine("Вывести все выполненые задачи в группе \"/all-completed-in-group {name-group}\"");
        }

        private void SubTasks()
        {
            Console.WriteLine("Добавить к задаче подзадачу \"/add-subtask {id}\"");
            Console.WriteLine("Добавить к задаче, которая находится в группе, подзадачу \"/add-subtask-to-group {name-group}\"");
            Console.WriteLine("Отметить подзадачу как выполненую \"/compelte-subtask {id}\"");
            Console.WriteLine("Отметить подзадачу как выполненую в группе \"/compelte-subtask-to-group {name-group}\"");
            Console.WriteLine("Удалить подзадачу \"/delete-subtask {id}\"");
            Console.WriteLine("Удалить подзадачу в группе \"/delete-subtask-in-group {name-group}\"");
            Console.WriteLine("Вывести список выполненых подзадач \"/completed-subtask\"");
            Console.WriteLine("Вывести список выполненых подзадач в группе \"/completed-subtask {name-group}\"");
        }

        private void ContinueChoice()
        {
            Console.WriteLine("\nДля того, чтобы продолжить, нажмите \"1\", чтобы выйти - \"2\"");
            int yourChoice = -1;
            try
            {
                yourChoice = int.Parse(Console.ReadLine());
                switch (yourChoice)
                {
                    case 1:
                        Switch();
                        break;
                    case 2:
                        Console.WriteLine("До новых встреч!");
                        return;
                    default:
                        Console.WriteLine("Такой команды нет!");
                        ContinueChoice();
                        break;
                }
            }
            catch 
            {
                Console.WriteLine("Такой команды нет!");
            }
            finally
            {
                if(yourChoice == -1)
                    ContinueChoice();
            }
        }

        private void AddDeadline(DateTime dateTime)
        {
            Console.Write("Введите ID задачи, к которой вы хотите добавить дедлайн: ");
            int id = int.Parse(Console.ReadLine());
            if (allTasks.AddDeadline(id, dateTime))
            {
                Console.WriteLine($"Задаче с индексом \"{id}\" был добавлен дедлайн {DateTime.Parse(dateTime.ToString()).ToShortDateString()}");
            }
            else
            {
                if(ContinueChoiceAfterTryForTask(id))
                    AddDeadline(dateTime);
                else
                    Switch();
            }
        }

        private void TryToDeleteByIdInTheTask()
        {
            if (!allTasks.isEmpty())
            {
                allTasks.All();
                Console.Write("Введите ID задачи, которую хотите удалить: ");
                int id = int.Parse(Console.ReadLine());
                if (allTasks.DeleteById(id))
                {
                    Console.WriteLine($"Ваша задача с ID \"{id}\" успешно удалена!");
                }
                else
                {
                    if (ContinueChoiceAfterTryForTask(id))
                        TryToDeleteByIdInTheTask();
                    else { Switch(); }
                }
            }
            else Console.WriteLine("Ваш список задач пуст");
        }
        private void TryToDeleteByIdInTheGroup(string nameOfTheGroup)
        {
            Console.Write("Введите ID задачи, которую хотите удалить: ");
            int id = int.Parse(Console.ReadLine());
            if (classGroup[nameOfTheGroup].DeleteById(id))
                Console.WriteLine($"Ваша задача с ID \"{id}\" успешно удалена!");
            else
            {
                if (ContinueChoiceAfterTryForTask(id))
                    TryToDeleteByIdInTheGroup(nameOfTheGroup);
                else
                    Switch();
            }
        }

        private void TryToCompliteByIdInTheTask()
        {
            Console.Write("Введите ID задачи, которую хотите выполнить: ");
            int id = int.Parse(Console.ReadLine());
            if (allTasks.DeleteById(id))
            {
                Console.WriteLine($"Ваша задача с ID \"{id}\" успешно удалена!");
            }
            else
            {
                if (ContinueChoiceAfterTryForTask(id))
                    TryToCompliteByIdInTheTask();
                else
                    Switch();
            }
        }
        private void TryToCompliteByIdInTheGroup(string nameOfTheGroup)
        {
            Console.Write("Введите ID задачи, которую хотите удалить: ");
            int id = int.Parse(Console.ReadLine());
            if (classGroup[nameOfTheGroup].CompliteById(id))
            {
                Console.WriteLine($"Ваша задача с ID \"{id}\" в группе {nameOfTheGroup} успешно помеченена как выполненная!");
            }
            else
            {
                if (ContinueChoiceAfterTryForTask(id))
                    TryToCompliteByIdInTheGroup(nameOfTheGroup);
                else
                    Switch();
            }
        }


        private void TryToDeleteGroup()
        {
            classGroup.AllNameGroups();
            Console.Write("Введите название группы, которую котите удалить: ");
            string NameOfTheGroup = Console.ReadLine();
            classGroup.AllNameGroups();
            if (classGroup.Search(NameOfTheGroup))
            {
                classGroup.DeleteGroup(NameOfTheGroup);
                Console.WriteLine($"Ваша группа с названием \"{NameOfTheGroup}\" успешно удалена");
            }
            else
            {
                if (ContinueChoiceAfterTryForGroup(NameOfTheGroup))
                    TryToDeleteGroup();
                else { Switch(); }
            }
        }

        private void TryAllTasksInTheGroup()
        {
            Console.Write("Введите название группы, задачи которых вы хотите вывести: ");
            string nameOfTheGroup = Console.ReadLine();
            if (classGroup.Search(nameOfTheGroup))
            {
                if (!classGroup[nameOfTheGroup].isEmpty())
                {
                    Console.WriteLine($"Все задачи в группе \"{nameOfTheGroup}\":");
                    classGroup[nameOfTheGroup].All();
                }
                else
                    Console.WriteLine($"Список задач в группе \"{nameOfTheGroup}\" пуст");
            }
            else
            {
                if (ContinueChoiceAfterTryForGroup(nameOfTheGroup))
                    TryAllTasksInTheGroup();
                else { Switch(); }
            }
        }
        private void TryToAddToGroup()
        {
            Console.Write("Введите название группы, в которую хотите добавить задачу: ");
            string NameOfTheGroup = Console.ReadLine();
            if (classGroup.Search(NameOfTheGroup))
            {
                Console.WriteLine("Чтобы добавить задачу без дедлайна, нажмите 1, чтобы добавить c дедлайном, нажмите любую другую цирфу");
                int isDeadline = int.Parse(Console.ReadLine());
                if (isDeadline == 1)
                {
                    Console.Write("Опишите свою задачу: ");
                    string taskInfo = Console.ReadLine();
                    int idOfTask = classGroup[NameOfTheGroup].AddTask(taskInfo);
                    Console.WriteLine($"Ваша задача c ID \"{idOfTask}\" успешно добавлена в группу \"{NameOfTheGroup}\"!");
                }
                else
                {
                    Console.Write("Опишите свою задачу: ");
                    string taskInfo = Console.ReadLine();
                    Console.Write("Введите дедлайн: ");
                    DateTime dateTime = Convert.ToDateTime(Console.ReadLine());
                    int idOfTask = classGroup[NameOfTheGroup].AddTask(taskInfo, dateTime);
                    Console.WriteLine($"Ваша задача c ID \"{idOfTask}\" и дедлайном \"{dateTime}\" успешно добавлена в группу \"{NameOfTheGroup}\"!");
                }
            }
            else
            {
                if (ContinueChoiceAfterTryForGroup(NameOfTheGroup))
                    TryToAddToGroup();
                else
                    Switch();
            }
        }

        private void TryToDeleteByIdInTheGroup()
        {
            Console.Write("Введите название группы, из которой хотели бы удалить задачу: ");
            string nameOftheGroup = Console.ReadLine();
            if (classGroup.Search(nameOftheGroup))
            {
                classGroup[nameOftheGroup].All();
                Console.WriteLine("Введите ID задачи, которую нужно удалить");
                int id = int.Parse(Console.ReadLine());
                if (classGroup[nameOftheGroup].DeleteById(id))
                    Console.WriteLine($"Ваша задача с ID \"{id}\" в группе {nameOftheGroup} успешно удалена!");
                else
                {
                    Console.WriteLine($"Задачи с ID \"{id}\" не существует, повторите попытку");
                    TryToDeleteByIdInTheGroup(nameOftheGroup);
                }
            }
            else
            {
                if (ContinueChoiceAfterTryForGroup(nameOftheGroup))
                    TryToDeleteByIdInTheGroup();
                else
                    Switch();
            }
        }

        private void TryToCompliteByIdInTheGroup()
        {
            Console.Write("Введите название группы, в которой хотите пометить задачу как выполненую: ");
            string nameOfTheGroup = Console.ReadLine();
            if (classGroup.Search(nameOfTheGroup))
            {
                if (!classGroup[nameOfTheGroup].isNoComplitedEmpty())
                {
                    classGroup[nameOfTheGroup].All();
                    Console.WriteLine("Введите ID задачи, которую вы выполнили");
                    int id = int.Parse(Console.ReadLine());
                    if (classGroup[nameOfTheGroup].CompliteById(id))
                        Console.WriteLine($"Ваша задача с ID \"{id}\" в группе {nameOfTheGroup} успешно отмеченена как выполненная!");
                    else
                    {
                        Console.WriteLine($"Задачи с ID \"{id}\" в группе {nameOfTheGroup} не существует, повторите попытку");
                        TryToCompliteByIdInTheGroup(nameOfTheGroup);
                    }
                }
                else
                    Console.WriteLine($"Список невыполненых задач в группе \"{nameOfTheGroup}\" пуст");
            }
            else
            {
                if (ContinueChoiceAfterTryForGroup(nameOfTheGroup))
                    TryToCompliteByIdInTheGroup();
                else
                    Switch();
            }
        }

        private void TryAllComplitedInTheGroup()
        {
            Console.Write("Введите название группы, из которой хотите вывести все выполненые задачи: ");
            string nameOfTheGroup = Console.ReadLine();
            if (classGroup.Search(nameOfTheGroup))
            {
                Console.WriteLine($"Список всех выполенных задач в группе {nameOfTheGroup}");
                classGroup[nameOfTheGroup].Completed();
            }
            else
            {
                if (ContinueChoiceAfterTryForGroup(nameOfTheGroup))
                    TryAllComplitedInTheGroup();
                else
                    Switch();
            }
        }

        private bool ContinueChoiceAfterTryForGroup(string nameOfTheGroup)
        {
            Console.WriteLine($"Группа с названием \"{nameOfTheGroup}\" не была найдена");
            Console.WriteLine("Для того, чтобы повторить попытку, нажмите \"1\", чтобы выйти в главное меню - \"2\"");
            //try
            //{
                int yourChoice = int.Parse(Console.ReadLine());
                switch (yourChoice)
                {
                    case 1:
                        return true;
                    default:
                        return false;
                }
            
            
        }
        private bool ContinueChoiceAfterTryForTask(int id)
        {
            Console.WriteLine($"Задачи с таким индексом \"{id}\" не найдена");
            Console.WriteLine("Для того, чтобы повторить попытку, нажмите 1, чтобы выйти в главное меню - \"2\"");
            int yourChoice = int.Parse(Console.ReadLine());
            switch (yourChoice)
            {
                case 1:
                    return true;
                default:
                    return false;
            }
        }
    }
}
