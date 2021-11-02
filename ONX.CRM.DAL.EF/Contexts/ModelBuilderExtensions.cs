using System;
using Microsoft.EntityFrameworkCore;
using ONX.CRM.DAL.Enums;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.DAL.EF.Contexts
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //Add specialization
            var specialization01 = new Specialization()
            {
                Id = 1,
                Title = "Frontend Developer",
                Description = null,
                PNGName = "frontend-developer.png"
            };
            var specialization02 = new Specialization()
            {
                Id = 2,
                Title = ".NET Developer",
                Description = null,
                PNGName = "net-developer.png"
            };
            var specialization03 = new Specialization()
            {
                Id = 3,
                Title = "Java Developer",
                Description = null,
                PNGName = "java-developer.png"
            };
            var specialization04 = new Specialization()
            {
                Id = 4,
                Title = "Unity Developer",
                Description = null,
                PNGName = "game-developer.png"
            };
            var specialization05 = new Specialization()
            {
                Id = 5,
                Title = "Database Developer",
                Description = null,
                PNGName = "database-developer.png"
            };
            var specialization06 = new Specialization()
            {
                Id = 6,
                Title = "iOS Developer",
                Description = null,
                PNGName = "ios-developer.png"
            };

            modelBuilder.Entity<Specialization>().HasData(specialization01, specialization02,
                specialization03, specialization04,
                specialization05, specialization06);

            //Add courses
            var course01 = new Course()
            {
                Id = 7,
                Title = "HTML5/CSS3",
                Description = "HTML & CSS are two fundamental technologies that every web" +
                              " developer must master. HTML (Hypertext Markup Language) " +
                              "is responsible for the skeleton of a web page: the presence of text, " +
                              "images, tables, buttons, information filling forms and other elements of the" +
                              " user interface. CSS (Cascading Styles Sheet) allows you to transform " +
                              "the appearance of the site into a human-readable form. Knowing only " +
                              "these two technologies will already give you the opportunity to create" +
                              " effective and attractive web pages.",
                NecessaryPreKnowledge = null,
                SpecializationId = 1,
                Cost = 360
            };
            var course02 = new Course()
            {
                Id = 8,
                Title = "JavaScript",
                Description = "JavaScript is one of the most popular programming languages. " +
                              "You can't do without his knowledge in web development. " +
                              "In addition to the implementation of custom logic (FrontEnd), " +
                              "it is used in the development of server (BackEnd), game and " +
                              "mobile applications, to create scripts for test automation, and more. Learning " +
                              "JS is a good investment in the future, as the language will be useful " +
                              "for solving various problems and will be useful in any area of development.",
                NecessaryPreKnowledge = "\"HTML & CSS\" course",
                SpecializationId = 1,
                Cost = 800
            };
            var course03 = new Course()
            {
                Id = 9,
                Title = "Angular",
                Description = "Angular Developer is a front-end web application (FrontEnd) " +
                              "developer who uses Google's Angular as a framework to write efficient" +
                              " Single Page Applications (SPA) with a consistent interface. The web" +
                              " services Gmail, Forbes, Upwork, PayPal, Weather.com and" +
                              " many others have been built with Angular.Our training program " +
                              "is aimed at learning all the necessary tools for a " +
                              "successful start in this direction.",
                NecessaryPreKnowledge = "\"JavaScript\" course",
                SpecializationId = 1,
                Cost = 1200
            };
            var course04 = new Course()
            {
                Id = 10,
                Title = "Programming on C#",
                Description = "C # (si sharp) is an object-oriented programming language" +
                              " developed by Microsoft. The direct interest of such a " +
                              "large corporation in the language ensures that it " +
                              "continues to evolve and find applications in various industries. " +
                              "C Sharp has absorbed the best qualities, and also inherited" +
                              " the features of the syntax of Java and C ++. The language " +
                              "is used for web development, desktop and mobile applications. " +
                              "If you signed up for a course on C # in Minsk in order to learn " +
                              "how to create web projects, then in the future you need to " +
                              "master the .NET toolkit. Thanks to the huge amount of " +
                              "documentation, C # is quite easy to learn. And its own development " +
                              "environment Visual Studio, ready-made templates, modules," +
                              " procedures make the language comfortable to use. After completing " +
                              "the basic course \"Programming in C #\", you can choose a direction" +
                              " for further development - to.",
                NecessaryPreKnowledge = null,
                SpecializationId = 2,
                Cost = 1400

            };
            var course05 = new Course()
            {
                Id = 11,
                Title = "Industrial programming with ASP.NET",
                Description = "ASP.NET is a Microsoft technology for creating websites," +
                              " web services and applications. Due to its reliability, " +
                              "security and flexibility, it is actively used by large companies." +
                              " The technology's standard libraries contain many modules, " +
                              "templates and procedures, which makes it convenient for " +
                              "developing and supporting large-scale projects. It is quite" +
                              " difficult to study ASP.NET in courses in Minsk, but those who " +
                              "cope with this task will have one of the highest salaries" +
                              " in the IT sphere.",
                NecessaryPreKnowledge = "\"C # programming\" course",
                SpecializationId = 2,
                Cost = 1900

            };
            var course06 = new Course()
            {
                Id = 12,
                Title = "Industrial software development on Java",
                Description = "Java is a language that has not left all kinds of TOPs" +
                              " of popular programming languages for many years. With its " +
                              "help, various software solutions are created: from computer " +
                              "games and mobile applications for Android to software for banking" +
                              " systems and cloud storage. Reliability, power, efficiency and" +
                              " multi - platform are the main hallmarks of Java. If you want" +
                              " to work with a classic tool that can handle any task - Java is for you.",
                NecessaryPreKnowledge = null,
                SpecializationId = 3,
                Cost = 1400

            };
            var course07 = new Course()
            {
                Id = 13,
                Title = "Java Web Development",
                Description = "Java is a general-purpose programming language " +
                              "that is suitable for many tasks and can handle a " +
                              "variety of challenges. Do you want to learn how " +
                              "to use it effectively? Sign up for a Java web development " +
                              "course in Minsk. The program was created by the largest " +
                              "software developer EPAM Systems. Certified graduates of the " +
                              "course will receive an invitation to the EPAM Java " +
                              "lab to further move along the path of employment with the company.",
                NecessaryPreKnowledge = "\"Industrial software development on Java\"  course",
                SpecializationId = 3,
                Cost = 1900

            };
            var course08 = new Course()
            {
                Id = 14,
                Title = "Programming on C#",
                Description = "C # (si sharp) is an object-oriented programming language" +
                              " developed by Microsoft. The direct interest of such a " +
                              "large corporation in the language ensures that it " +
                              "continues to evolve and find applications in various industries. " +
                              "C Sharp has absorbed the best qualities, and also inherited" +
                              " the features of the syntax of Java and C ++. The language " +
                              "is used for web development, desktop and mobile applications. " +
                              "If you signed up for a course on C # in Minsk in order to learn " +
                              "how to create web projects, then in the future you need to " +
                              "master the .NET toolkit. Thanks to the huge amount of " +
                              "documentation, C # is quite easy to learn. And its own development " +
                              "environment Visual Studio, ready-made templates, modules," +
                              " procedures make the language comfortable to use. After completing " +
                              "the basic course \"Programming in C #\", you can choose a direction" +
                              " for further development - to.",
                NecessaryPreKnowledge = null,
                SpecializationId = 4,
                Cost = 1400

            };
            var course09 = new Course()
            {
                Id = 15,
                Title = "Development of mobile games on the Unity engine",
                Description = "Unity is a modern game engine that allows you to create " +
                              "projects of any level. Deus Ex: The Fall, Assassin's " +
                              "Creed: Identity, Wasteland 2, HearthStone and thousands " +
                              "of other games are based on it. Thanks to the low entry " +
                              "threshold and the C # language, anyone can release the " +
                              "first game mechanics in a short time. The course \"Development " +
                              "of mobile games on the Unity engine\" in Minsk will allow " +
                              "you to realize your creative abilities in the field of " +
                              "creating games, master an interesting profession and find a " +
                              "job. During the learning process, students will create several " +
                              "prototypes of games and their own project.",
                NecessaryPreKnowledge = "\"C # programming\" course",
                SpecializationId = 4,
                Cost = 1600

            };
            var course10 = new Course()
            {
                Id = 16,
                Title = "Relational databases and SQL",
                Description = "Working with relational databases and being able to write " +
                              "effective queries in SQL are important hard skills for many " +
                              "IT professionals. They are especially needed for those who " +
                              "want to build a successful career in the field of high technology " +
                              "as a back-end developer or move in the direction of Business " +
                              "Intelligence and data analytics. A course on relational databases and " +
                              "SQL in Minsk will help you get the necessary skills. The training " +
                              "program is developed by an IT company.",
                NecessaryPreKnowledge = null,
                SpecializationId = 5,
                Cost = 990
            };
            var course11 = new Course()
            {
                Id = 17,
                Title = "Comprehensive Course in iOS App Development",
                Description = "iOS is the world's second most popular mobile " +
                              "operating system for Apple devices. To simplify the " +
                              "process of developing applications for iOS, the company " +
                              "released the open source programming language Swift. It " +
                              "is an easier-to-read and error-tolerant alternative to " +
                              "Objective-C. Apple is for minimalism, so Swift has a simple " +
                              "syntax, easy to read and easy to write. After completing " +
                              "the iOS course in Minsk, you will be ready for an interview " +
                              "for the junior iOS developer position, you will be able " +
                              "to work in a mobile development team, apply the iOS SDK and " +
                              "Swift, and create your own applications for the App Store.",
                NecessaryPreKnowledge = null,
                SpecializationId = 6,
                Cost = 2099
            };
            modelBuilder.Entity<Course>().HasData(course01, course02, course03, course04, course05,
                course06, course07, course08, course09, course10, course11);

            //Add groups
            DateTime dateTimeForGroup01 = new DateTime(2021, 11, 01, 19, 30, 00);
            var group01 = new Group()
            {
                Id = 1,
                Number = "MR00-0012-FT04",
                TeacherId = 3,
                CourseId = 15,
                StartDate = dateTimeForGroup01,
                Status = GroupStatus.Pending

            };
            DateTime dateTimeForGroup02 = new DateTime(2021, 12, 09, 18, 30, 00);
            var group02 = new Group()
            {
                Id = 2,
                Number = "MR00-5512-DT12",
                TeacherId = 2,
                CourseId = 11,
                StartDate = dateTimeForGroup02,
                Status = GroupStatus.Pending
            };
            DateTime dateTimeForGroup03 = new DateTime(2021, 09, 30, 19, 30, 00);
            var group03 = new Group()
            {
                Id = 3,
                Number = "MR00-2671-FG10",
                TeacherId = 1,
                CourseId = 10,
                StartDate = dateTimeForGroup03,
                Status = GroupStatus.Started
            };
            DateTime dateTimeForGroup04 = new DateTime(2021, 09, 30, 19, 30, 00);
            var group04 = new Group()
            {
                Id = 4,
                Number = "MR00-2671-FG10",
                TeacherId = 1,
                CourseId = 12,
                StartDate = dateTimeForGroup04,
                Status = GroupStatus.Finished
            };

            modelBuilder.Entity<Group>().HasData(group01, group02, group03, group04);

            var teacher01 = new Teacher()
            {
                Id = 1,
                FirstName = "Вадим",
                LastName = "Папко",
                Email = "VadzimPapko@gmail.com",
                Phone = "+375291133322",
                WorkExperience = "5 years",
                Bio = "Area of interest: development of web applications based on Sitecore, " +
                      "ASP.NET MVC / .NET Core and mobile applications using the Xamarin " +
                      "cross-platform framework; Sitecore JavaScript Services (JSS); the " +
                      "introduction of search engines such as Solr, Coveo; using cloud Azure solutions."
            };
            var teacher02 = new Teacher()
            {
                Id = 2,
                FirstName = "Дмитрий",
                LastName = "Альхимович",
                Email = "DmitriyAlhimovich@gmail.com",
                Phone = "+375293322211",
                WorkExperience = "10 years",
                Bio = "Area of interest: development of web applications " +
                      "in ASP.NET MVC using JavaScript libraries (Angular, JQuery), " +
                      "API, microservices, Data Science, Machine Learning. " +
                      "Agile software development methodologies (Agile, Scrum, Kanban, Lean)."
            };
            var teacher03 = new Teacher()
            {
                Id = 3,
                FirstName = "Ростислав",
                LastName = "Никишин",
                Email = "RostislavNikishin@gmail.com",
                Phone = "+375441188800",
                WorkExperience = "4 years",
                Bio = "Area of interest: optimization, programming of gameplay systems."
            };
            modelBuilder.Entity<Teacher>().HasData(teacher01, teacher02, teacher03);

            var student01 = new Student()
            {
                Id = 1,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Николай",
                LastName = "Лазарев",
                Email = "Lazarev0981@gmail.com",
                Phone = "+375441188801",
            };
            var student02 = new Student()
            {
                Id = 2,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Егор",
                LastName = "Медведев",
                Email = "Medvedev1990@gmail.com",
                Phone = "+375441188802",
            };
            var student03 = new Student()
            {
                Id = 3,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Александр",
                LastName = "Ершов",
                Email = "Ershov9512@gmail.com",
                Phone = "+375441188803",
            };
            var student04 = new Student()
            {
                Id = 4,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Роман",
                LastName = "Никитин",
                Email = "Nikitin01@gmail.com",
                Phone = "+375441188804",
            };
            var student05 = new Student()
            {
                Id = 5,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Сергей",
                LastName = "Соболев",
                Email = "Sobolev11@gmail.com",
                Phone = "+375441188805",
            };
            var student06 = new Student()
            {
                Id = 6,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Анна",
                LastName = "Рябова",
                Email = "Ryabov903@gmail.com",
                Phone = "+375441188806",
            };
            var student07 = new Student()
            {
                Id = 7,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Алексей",
                LastName = "Поляков",
                Email = "Polyakov99@gmail.com",
                Phone = "+375441188807",
            };
            var student08 = new Student()
            {
                Id = 8,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Надежда",
                LastName = "Цветкова",
                Email = "Cvetkova41@gmail.com",
                Phone = "+375441188808",
            };
            var student09 = new Student()
            {
                Id = 9,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Виктория",
                LastName = "Данилова",
                Email = "Danilova355@gmail.com",
                Phone = "+375441188809",
            };
            var student10 = new Student()
            {
                Id = 10,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Антон",
                LastName = "Жуков",
                Email = "Jukov07@gmail.com",
                Phone = "+375441188810",
            };
            var student11 = new Student()
            {
                Id = 11,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Геннадий",
                LastName = "Фролов",
                Email = "Frolov855@gmail.com",
                Phone = "+375441188811",
            };
            var student12 = new Student()
            {
                Id = 12,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Виктор",
                LastName = "Лапин",
                Email = "Lapin0110@gmail.com",
                Phone = "+375441188812",
            };
            var student13 = new Student()
            {
                Id = 13,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Светлана",
                LastName = "Журавлёва",
                Email = "Juravliov43@gmail.com",
                Phone = "+375441188813",
            };
            var student14 = new Student()
            {
                Id = 14,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Роман",
                LastName = "Николаев",
                Email = "Nikolayev61@gmail.com",
                Phone = "+375441188814",
            };
            var student15 = new Student()
            {
                Id = 15,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Сергей",
                LastName = "Крылов",
                Email = "Krylov96@gmail.com",
                Phone = "+375441188815",
            };
            var student16 = new Student()
            {
                Id = 16,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Максим",
                LastName = "Максимов",
                Email = "Maksimov77@gmail.com",
                Phone = "+375441188816",
            };
            var student17 = new Student()
            {
                Id = 17,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Егор",
                LastName = "Сидоров",
                Email = "Sidorov610@gmail.com",
                Phone = "+375441188817",
            };
            var student18 = new Student()
            {
                Id = 18,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Александр",
                LastName = "Осипов",
                Email = "Osipov0990@gmail.com",
                Phone = "+375441188818",
            };
            var student19 = new Student()
            {
                Id = 19,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Евгений",
                LastName = "Белоусов",
                Email = "Belousov78@gmail.com",
                Phone = "+375441188819",
            };
            var student20 = new Student()
            {
                Id = 20,
                GroupId = 3,
                Type = StudentType.Online,
                FirstName = "Андрей",
                LastName = "Федотов",
                Email = "Fedotov88@gmail.com",
                Phone = "+375441188820",
            };
            var student21 = new Student()
            {
                Id = 21,
                GroupId = 1,
                Type = StudentType.Offline,
                FirstName = "Виталий",
                LastName = "Дорофеев",
                Email = "Dorofeyev45@gmail.com",
                Phone = "+375441188821",
            };
            var student22 = new Student()
            {
                Id = 22,
                GroupId = 1,
                Type = StudentType.Offline,
                FirstName = "Василий",
                LastName = "Егоров",
                Email = "Egorov212@gmail.com",
                Phone = "+375441188822",
            };
            var student23 = new Student()
            {
                Id = 23,
                GroupId = 1,
                Type = StudentType.Offline,
                FirstName = "Владислав",
                LastName = "Матвеев",
                Email = "Matveyev67@gmail.com",
                Phone = "+375441188823",
            };
            var student24 = new Student()
            {
                Id = 24,
                GroupId = 1,
                Type = StudentType.Offline,
                FirstName = "Анастасия",
                LastName = "Боброва",
                Email = "Bobrova72@gmail.com",
                Phone = "+375441188824",
            };
            var student25 = new Student()
            {
                Id = 25,
                GroupId = 1,
                Type = StudentType.Offline,
                FirstName = "Наталья",
                LastName = "Дмитриева",
                Email = "Dmitrieva1221@gmail.com",
                Phone = "+375441188825",
            };
            var student26 = new Student()
            {
                Id = 26,
                GroupId = 2,
                Type = StudentType.Mix,
                FirstName = "Сергей",
                LastName = "Калинин",
                Email = "Kalinin45@gmail.com",
                Phone = "+375441188826",
            };
            var student27 = new Student()
            {
                Id = 27,
                GroupId = 2,
                Type = StudentType.Mix,
                FirstName = "Алексей",
                LastName = "Анисимов",
                Email = "Anisimov987@gmail.com",
                Phone = "+375441188827",
            };
            var student28 = new Student()
            {
                Id = 28,
                GroupId = 2,
                Type = StudentType.Mix,
                FirstName = "Александр",
                LastName = "Антонов",
                Email = "Antonov543@gmail.com",
                Phone = "+375441188828",
            };
            var student29 = new Student()
            {
                Id = 29,
                GroupId = 2,
                Type = StudentType.Mix,
                FirstName = "Екатерина",
                LastName = "Соколова",
                Email = "Sokolova1212@gmail.com",
                Phone = "+375441188829",
            };
            var student30 = new Student()
            {
                Id = 30,
                GroupId = 2,
                Type = StudentType.Mix,
                FirstName = "Виталий",
                LastName = "Тимофеев",
                Email = "Timofeev22@gmail.com",
                Phone = "+375441188830",
            };

            modelBuilder.Entity<Student>().HasData(student01, student02, student03, student04, student05,
                 student06, student07, student08, student09, student10,
                 student11, student12, student13, student14, student15,
                 student16, student17, student18, student19, student20,
                 student21, student22, student23, student24, student25,
                 student26, student27, student28, student29, student30);

            DateTime dateTimeRequestCreated01 = new DateTime(2021, 07, 15, 13, 17, 00);
            var studentRequest01 = new StudentRequest()
            {
                Id = 1,
                Created = dateTimeRequestCreated01,
                CourseId = 11,
                FirstName = "Андрей",
                LastName = "Нестеров",
                Email = "Nesterov23@gmail.com",
                Phone = "+375441188132",
                Type = StudentType.Online,
                Comments = ""
            };
            DateTime dateTimeRequestCreated02 = new DateTime(2021, 07, 23, 20, 57, 00);
            var studentRequest02 = new StudentRequest()
            {
                Id = 2,
                Created = dateTimeRequestCreated02,
                CourseId = 11,
                FirstName = "Роман",
                LastName = "Харитонов",
                Email = "Haritonov432@gmail.com",
                Phone = "+375441188365",
                Type = StudentType.Online,
                Comments = ""
            };
            DateTime dateTimeRequestCreated03 = new DateTime(2021, 08, 23, 08, 13, 00);
            var studentRequest03 = new StudentRequest()
            {
                Id = 3,
                Created = dateTimeRequestCreated03,
                CourseId = 11,
                FirstName = "Аркадий",
                LastName = "Агафонов",
                Email = "Agafonov440@gmail.com",
                Phone = "+375441145830",
                Type = StudentType.Online,
                Comments = ""
            };
            DateTime dateTimeRequestCreated04 = new DateTime(2021, 09, 04, 23, 15, 00);
            var studentRequest04 = new StudentRequest()
            {
                Id = 4,
                Created = dateTimeRequestCreated04,
                CourseId = 11,
                FirstName = "Алексей",
                LastName = "Муравьёв",
                Email = "Muraviov438@gmail.com",
                Phone = "+375441185430",
                Type = StudentType.Online,
                Comments = ""
            };
            DateTime dateTimeRequestCreated05 = new DateTime(2021, 07, 27, 20, 05, 00);
            var studentRequest05 = new StudentRequest()
            {
                Id = 5,
                Created = dateTimeRequestCreated05,
                CourseId = 11,
                FirstName = "Ангелина",
                LastName = "Ларионова",
                Email = "Larionova85@gmail.com",
                Phone = "+375441348830",
                Type = StudentType.Online,
                Comments = ""
            };
            DateTime dateTimeRequestCreated06 = new DateTime(2021, 08, 15, 19, 44, 00);
            var studentRequest06 = new StudentRequest()
            {
                Id = 6,
                Created = dateTimeRequestCreated06,
                CourseId = 15,
                FirstName = "Денис",
                LastName = "Федосеев",
                Email = "Fedoseyev86@gmail.com",
                Phone = "+375441675830",
                Type = StudentType.Online,
                Comments = ""
            };
            DateTime dateTimeRequestCreated07 = new DateTime(2021, 09, 3, 22, 36, 00);
            var studentRequest07 = new StudentRequest()
            {
                Id = 7,
                Created = dateTimeRequestCreated07,
                CourseId = 15,
                FirstName = "Николай",
                LastName = "Зимин",
                Email = "Zimin984@gmail.com",
                Phone = "+375441186730",
                Type = StudentType.Online,
                Comments = ""
            };
            DateTime dateTimeRequestCreated08 = new DateTime(2021, 08, 20, 21, 17, 00);
            var studentRequest08 = new StudentRequest()
            {
                Id = 8,
                Created = dateTimeRequestCreated08,
                CourseId = 15,
                FirstName = "Максим",
                LastName = "Пахомов",
                Email = "Pahomov43@gmail.com",
                Phone = "+375441188554",
                Type = StudentType.Online,
                Comments = ""
            };
            DateTime dateTimeRequestCreated09 = new DateTime(2021, 08, 03, 15, 35, 00);
            var studentRequest09 = new StudentRequest()
            {
                Id = 9,
                Created = dateTimeRequestCreated09,
                CourseId = 15,
                FirstName = "Геннадий",
                LastName = "Шубин",
                Email = "Shubin69@gmail.com",
                Phone = "+375441188896",
                Type = StudentType.Online,
                Comments = ""
            };
            DateTime dateTimeRequestCreated10 = new DateTime(2021, 09, 01, 03, 40, 00);
            var studentRequest10 = new StudentRequest()
            {
                Id = 10,
                Created = dateTimeRequestCreated10,
                CourseId = 15,
                FirstName = "Екатерина",
                LastName = "Игнатова",
                Email = "Ignatova38@gmail.com",
                Phone = "+375441183830",
                Type = StudentType.Online,
                Comments = ""
            };

            modelBuilder.Entity<StudentRequest>().HasData(studentRequest01, studentRequest02,
                studentRequest03, studentRequest04, studentRequest05,
                studentRequest06, studentRequest07, studentRequest08,
                studentRequest09, studentRequest10);
        }
    }
}
