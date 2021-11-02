using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ONX.CRM.DAL.EF.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PNGName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkExperience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NecessaryPreKnowledge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpecializationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    TeacherId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Groups_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentRequests_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Description", "PNGName", "Title" },
                values: new object[,]
                {
                    { 1, null, "frontend-developer.png", "Frontend Developer" },
                    { 2, null, "net-developer.png", ".NET Developer" },
                    { 3, null, "java-developer.png", "Java Developer" },
                    { 4, null, "game-developer.png", "Unity Developer" },
                    { 5, null, "database-developer.png", "Database Developer" },
                    { 6, null, "ios-developer.png", "iOS Developer" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Bio", "Email", "FirstName", "ImgLink", "LastName", "Phone", "WorkExperience" },
                values: new object[,]
                {
                    { 1, "Area of interest: development of web applications based on Sitecore, ASP.NET MVC / .NET Core and mobile applications using the Xamarin cross-platform framework; Sitecore JavaScript Services (JSS); the introduction of search engines such as Solr, Coveo; using cloud Azure solutions.", "VadzimPapko@gmail.com", "Вадим", "VadzimPapko.jpg", "Папко", "+375291133322", "5 years" },
                    { 2, "Area of interest: development of web applications in ASP.NET MVC using JavaScript libraries (Angular, JQuery), API, microservices, Data Science, Machine Learning. Agile software development methodologies (Agile, Scrum, Kanban, Lean).", "DmitriyAlhimovich@gmail.com", "Дмитрий", "DmitriyAlhimovich.jpg", "Альхимович", "+375293322211", "10 years" },
                    { 3, "Area of interest: optimization, programming of gameplay systems.", "RostislavNikishin@gmail.com", "Ростислав", "RostislavNikishin.jpg", "Никишин", "+375441188800", "4 years" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Cost", "Description", "NecessaryPreKnowledge", "SpecializationId", "Title" },
                values: new object[,]
                {
                    { 7, 360m, "HTML & CSS are two fundamental technologies that every web developer must master. HTML (Hypertext Markup Language) is responsible for the skeleton of a web page: the presence of text, images, tables, buttons, information filling forms and other elements of the user interface. CSS (Cascading Styles Sheet) allows you to transform the appearance of the site into a human-readable form. Knowing only these two technologies will already give you the opportunity to create effective and attractive web pages.", null, 1, "HTML5/CSS3" },
                    { 8, 800m, "JavaScript is one of the most popular programming languages. You can't do without his knowledge in web development. In addition to the implementation of custom logic (FrontEnd), it is used in the development of server (BackEnd), game and mobile applications, to create scripts for test automation, and more. Learning JS is a good investment in the future, as the language will be useful for solving various problems and will be useful in any area of development.", "\"HTML & CSS\" course", 1, "JavaScript" },
                    { 9, 1200m, "Angular Developer is a front-end web application (FrontEnd) developer who uses Google's Angular as a framework to write efficient Single Page Applications (SPA) with a consistent interface. The web services Gmail, Forbes, Upwork, PayPal, Weather.com and many others have been built with Angular.Our training program is aimed at learning all the necessary tools for a successful start in this direction.", "\"JavaScript\" course", 1, "Angular" },
                    { 10, 1400m, "C # (si sharp) is an object-oriented programming language developed by Microsoft. The direct interest of such a large corporation in the language ensures that it continues to evolve and find applications in various industries. C Sharp has absorbed the best qualities, and also inherited the features of the syntax of Java and C ++. The language is used for web development, desktop and mobile applications. If you signed up for a course on C # in Minsk in order to learn how to create web projects, then in the future you need to master the .NET toolkit. Thanks to the huge amount of documentation, C # is quite easy to learn. And its own development environment Visual Studio, ready-made templates, modules, procedures make the language comfortable to use. After completing the basic course \"Programming in C #\", you can choose a direction for further development - to.", null, 2, "Programming on C#" },
                    { 11, 1900m, "ASP.NET is a Microsoft technology for creating websites, web services and applications. Due to its reliability, security and flexibility, it is actively used by large companies. The technology's standard libraries contain many modules, templates and procedures, which makes it convenient for developing and supporting large-scale projects. It is quite difficult to study ASP.NET in courses in Minsk, but those who cope with this task will have one of the highest salaries in the IT sphere.", "\"C # programming\" course", 2, "Industrial programming with ASP.NET" },
                    { 12, 1400m, "Java is a language that has not left all kinds of TOPs of popular programming languages for many years. With its help, various software solutions are created: from computer games and mobile applications for Android to software for banking systems and cloud storage. Reliability, power, efficiency and multi - platform are the main hallmarks of Java. If you want to work with a classic tool that can handle any task - Java is for you.", null, 3, "Industrial software development on Java" },
                    { 13, 1900m, "Java is a general-purpose programming language that is suitable for many tasks and can handle a variety of challenges. Do you want to learn how to use it effectively? Sign up for a Java web development course in Minsk. The program was created by the largest software developer EPAM Systems. Certified graduates of the course will receive an invitation to the EPAM Java lab to further move along the path of employment with the company.", "\"Industrial software development on Java\"  course", 3, "Java Web Development" },
                    { 14, 1400m, "C # (si sharp) is an object-oriented programming language developed by Microsoft. The direct interest of such a large corporation in the language ensures that it continues to evolve and find applications in various industries. C Sharp has absorbed the best qualities, and also inherited the features of the syntax of Java and C ++. The language is used for web development, desktop and mobile applications. If you signed up for a course on C # in Minsk in order to learn how to create web projects, then in the future you need to master the .NET toolkit. Thanks to the huge amount of documentation, C # is quite easy to learn. And its own development environment Visual Studio, ready-made templates, modules, procedures make the language comfortable to use. After completing the basic course \"Programming in C #\", you can choose a direction for further development - to.", null, 4, "Programming on C#" },
                    { 15, 1600m, "Unity is a modern game engine that allows you to create projects of any level. Deus Ex: The Fall, Assassin's Creed: Identity, Wasteland 2, HearthStone and thousands of other games are based on it. Thanks to the low entry threshold and the C # language, anyone can release the first game mechanics in a short time. The course \"Development of mobile games on the Unity engine\" in Minsk will allow you to realize your creative abilities in the field of creating games, master an interesting profession and find a job. During the learning process, students will create several prototypes of games and their own project.", "\"C # programming\" course", 4, "Development of mobile games on the Unity engine" },
                    { 16, 990m, "Working with relational databases and being able to write effective queries in SQL are important hard skills for many IT professionals. They are especially needed for those who want to build a successful career in the field of high technology as a back-end developer or move in the direction of Business Intelligence and data analytics. A course on relational databases and SQL in Minsk will help you get the necessary skills. The training program is developed by an IT company.", null, 5, "Relational databases and SQL" },
                    { 17, 2099m, "iOS is the world's second most popular mobile operating system for Apple devices. To simplify the process of developing applications for iOS, the company released the open source programming language Swift. It is an easier-to-read and error-tolerant alternative to Objective-C. Apple is for minimalism, so Swift has a simple syntax, easy to read and easy to write. After completing the iOS course in Minsk, you will be ready for an interview for the junior iOS developer position, you will be able to work in a mobile development team, apply the iOS SDK and Swift, and create your own applications for the App Store.", null, 6, "Comprehensive Course in iOS App Development" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CourseId", "Number", "StartDate", "Status", "TeacherId" },
                values: new object[,]
                {
                    { 3, 10, "MR00-2671-FG10", new DateTime(2021, 9, 30, 19, 30, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 2, 11, "MR00-5512-DT12", new DateTime(2021, 12, 9, 18, 30, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 4, 12, "MR00-2671-FG10", new DateTime(2021, 9, 30, 19, 30, 0, 0, DateTimeKind.Unspecified), 3, 1 },
                    { 1, 15, "MR00-0012-FT04", new DateTime(2021, 11, 1, 19, 30, 0, 0, DateTimeKind.Unspecified), 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "StudentRequests",
                columns: new[] { "Id", "Comments", "CourseId", "Created", "Email", "FirstName", "ImgLink", "LastName", "Phone", "Type" },
                values: new object[,]
                {
                    { 1, "", 11, new DateTime(2021, 7, 15, 13, 17, 0, 0, DateTimeKind.Unspecified), "Nesterov23@gmail.com", "Андрей", null, "Нестеров", "+375441188132", 1 },
                    { 2, "", 11, new DateTime(2021, 7, 23, 20, 57, 0, 0, DateTimeKind.Unspecified), "Haritonov432@gmail.com", "Роман", null, "Харитонов", "+375441188365", 1 },
                    { 3, "", 11, new DateTime(2021, 8, 23, 8, 13, 0, 0, DateTimeKind.Unspecified), "Agafonov440@gmail.com", "Аркадий", null, "Агафонов", "+375441145830", 1 },
                    { 4, "", 11, new DateTime(2021, 9, 4, 23, 15, 0, 0, DateTimeKind.Unspecified), "Muraviov438@gmail.com", "Алексей", null, "Муравьёв", "+375441185430", 1 },
                    { 5, "", 11, new DateTime(2021, 7, 27, 20, 5, 0, 0, DateTimeKind.Unspecified), "Larionova85@gmail.com", "Ангелина", null, "Ларионова", "+375441348830", 1 },
                    { 6, "", 15, new DateTime(2021, 8, 15, 19, 44, 0, 0, DateTimeKind.Unspecified), "Fedoseyev86@gmail.com", "Денис", null, "Федосеев", "+375441675830", 1 },
                    { 7, "", 15, new DateTime(2021, 9, 3, 22, 36, 0, 0, DateTimeKind.Unspecified), "Zimin984@gmail.com", "Николай", null, "Зимин", "+375441186730", 1 },
                    { 8, "", 15, new DateTime(2021, 8, 20, 21, 17, 0, 0, DateTimeKind.Unspecified), "Pahomov43@gmail.com", "Максим", null, "Пахомов", "+375441188554", 1 },
                    { 9, "", 15, new DateTime(2021, 8, 3, 15, 35, 0, 0, DateTimeKind.Unspecified), "Shubin69@gmail.com", "Геннадий", null, "Шубин", "+375441188896", 1 },
                    { 10, "", 15, new DateTime(2021, 9, 1, 3, 40, 0, 0, DateTimeKind.Unspecified), "Ignatova38@gmail.com", "Екатерина", null, "Игнатова", "+375441183830", 1 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Email", "FirstName", "GroupId", "ImgLink", "LastName", "Phone", "Type" },
                values: new object[,]
                {
                    { 1, "Lazarev0981@gmail.com", "Николай", 3, null, "Лазарев", "+375441188801", 1 },
                    { 23, "Matveyev67@gmail.com", "Владислав", 1, null, "Матвеев", "+375441188823", 2 },
                    { 22, "Egorov212@gmail.com", "Василий", 1, null, "Егоров", "+375441188822", 2 },
                    { 21, "Dorofeyev45@gmail.com", "Виталий", 1, null, "Дорофеев", "+375441188821", 2 },
                    { 30, "Timofeev22@gmail.com", "Виталий", 2, null, "Тимофеев", "+375441188830", 3 },
                    { 29, "Sokolova1212@gmail.com", "Екатерина", 2, null, "Соколова", "+375441188829", 3 },
                    { 28, "Antonov543@gmail.com", "Александр", 2, null, "Антонов", "+375441188828", 3 },
                    { 27, "Anisimov987@gmail.com", "Алексей", 2, null, "Анисимов", "+375441188827", 3 },
                    { 26, "Kalinin45@gmail.com", "Сергей", 2, null, "Калинин", "+375441188826", 3 },
                    { 20, "Fedotov88@gmail.com", "Андрей", 3, null, "Федотов", "+375441188820", 1 },
                    { 19, "Belousov78@gmail.com", "Евгений", 3, null, "Белоусов", "+375441188819", 1 },
                    { 18, "Osipov0990@gmail.com", "Александр", 3, null, "Осипов", "+375441188818", 1 },
                    { 17, "Sidorov610@gmail.com", "Егор", 3, null, "Сидоров", "+375441188817", 1 },
                    { 16, "Maksimov77@gmail.com", "Максим", 3, null, "Максимов", "+375441188816", 1 },
                    { 15, "Krylov96@gmail.com", "Сергей", 3, null, "Крылов", "+375441188815", 1 },
                    { 14, "Nikolayev61@gmail.com", "Роман", 3, null, "Николаев", "+375441188814", 1 },
                    { 13, "Juravliov43@gmail.com", "Светлана", 3, null, "Журавлёва", "+375441188813", 1 },
                    { 12, "Lapin0110@gmail.com", "Виктор", 3, null, "Лапин", "+375441188812", 1 },
                    { 11, "Frolov855@gmail.com", "Геннадий", 3, null, "Фролов", "+375441188811", 1 },
                    { 10, "Jukov07@gmail.com", "Антон", 3, null, "Жуков", "+375441188810", 1 },
                    { 9, "Danilova355@gmail.com", "Виктория", 3, null, "Данилова", "+375441188809", 1 },
                    { 8, "Cvetkova41@gmail.com", "Надежда", 3, null, "Цветкова", "+375441188808", 1 },
                    { 7, "Polyakov99@gmail.com", "Алексей", 3, null, "Поляков", "+375441188807", 1 },
                    { 6, "Ryabov903@gmail.com", "Анна", 3, null, "Рябова", "+375441188806", 1 },
                    { 5, "Sobolev11@gmail.com", "Сергей", 3, null, "Соболев", "+375441188805", 1 },
                    { 4, "Nikitin01@gmail.com", "Роман", 3, null, "Никитин", "+375441188804", 1 },
                    { 3, "Ershov9512@gmail.com", "Александр", 3, null, "Ершов", "+375441188803", 1 },
                    { 2, "Medvedev1990@gmail.com", "Егор", 3, null, "Медведев", "+375441188802", 1 },
                    { 24, "Bobrova72@gmail.com", "Анастасия", 1, null, "Боброва", "+375441188824", 2 },
                    { 25, "Dmitrieva1221@gmail.com", "Наталья", 1, null, "Дмитриева", "+375441188825", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SpecializationId",
                table: "Courses",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CourseId",
                table: "Groups",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TeacherId",
                table: "Groups",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentRequests_CourseId",
                table: "StudentRequests",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupId",
                table: "Students",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "StudentRequests");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Specializations");
        }
    }
}
