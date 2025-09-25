﻿using TallinnaRakenduslikCollegeTARpe24_StenUesson.Models;

namespace TallinnaRakenduslikCollegeTARpe24_StenUesson.Data
{
    public class DbInitializer
    {
        public static void Initializer(SchoolContext context)
        {

            context.Database.EnsureCreated();
            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[]
            {
                new Student{FirstName="Artjom", LastName="Skatškov", Gender="Mees", EnrollmentDate=DateTime.Parse("2069-04-20")},
                new Student{FirstName="Meredith", LastName="Alonso", Gender="Naine", EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstName="Arturo",LastName="Anand", Gender = "Mees", EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstName="Gytis",LastName="Barzdukas", Gender="Mees", EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstName="Yan",LastName="Li", Gender="Mees", EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstName="Peggy",LastName="Justice", Gender = "Naine", EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student{FirstName="Laura",LastName="Norman", Gender = "Naine", EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstName="Nino",LastName="Olivetto", Gender = "Mees", EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student{FirstName="Carson",LastName="Alexander", Gender = "Mees", EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student{FirstName="Mikk",LastName="Kivisild", Gender = "Mees", EnrollmentDate=DateTime.Parse("2727-07-27")}
            };
            //iga õpilane lisatakse ükshaaval läbi foreach tsükli
            foreach (Student student in students)
            {
                context.Students.Add(student);
            }
            //ja andmebaasi muudatused salvestatakse
            context.SaveChanges();

            //eelnev struktuur, kui kursustega
            var couses = new Course[]
                {
                    new Course{CourseID=1050, Title="Keemia", Credits=3},
                    new Course{CourseID=4022, Title="Mikroökonoomika", Credits=3},
                    new Course{CourseID=4021, Title="Mikroökonoomika", Credits=3},
                    new Course{CourseID=1045, Title="Calculus", Credits=4},
                    new Course{CourseID=3141, Title="Trigonomeetria", Credits=4},
                    new Course{CourseID=2021, Title="Kompositsioon", Credits=3},
                    new Course{CourseID=2042, Title="Kirjandus", Credits=4},
                    new Course{CourseID=6789, Title="Plaadilugeja", Credits=1}
                };
            foreach (Course course in couses)
            {
                context.SaveChanges();
            }

            //var enrollments = new Enrollment[]
            //{
            //     new Enrollment{StudentID=1, CourseID=1050, Grade=Grade.A},
            //     new Enrollment{StudentID=1, CourseID=1050, Grade=Grade.B},
            //     new Enrollment{StudentID=1, CourseID=4022, Grade=Grade.C},

            //     new Enrollment{StudentID=2, CourseID=4022, Grade=Grade. F},
            //     new Enrollment{StudentID=2, CourseID=4021, Grade=Grade.C},
            //     new Enrollment{StudentID=2, CourseID=4021, Grade=Grade.B},

            //     new Enrollment{StudentID=8, CourseID=1045, Grade=Grade.F},

            //     new Enrollment{StudentID=3, CourseID=1045},

            //     new Enrollment{StudentID=4, CourseID=3141, Grade=Grade.B},

            //     new Enrollment{StudentID=5, CourseID=3141, Grade=Grade.A},

            //     new Enrollment{StudentID=7, CourseID=2021, Grade=Grade.B},

            //     new Enrollment{StudentID=6, CourseID=2021},

            //     new Enrollment{StudentID=8, CourseID=2042, Grade=Grade.C},

            //     new Enrollment{StudentID=9, CourseID=2042, Grade=Grade.A},

            //     new Enrollment{StudentID=10, CourseID=6789, Grade=Grade.A},
            //};
            //foreach (Enrollment enrollment in enrollments)
            //{
            //    context.Enrollments.Add(enrollment);
            //}
            //context.SaveChanges();

            if (context.Instructors.Any()) { return; }
            var instructors = new Instructor[]
            {
                new Instructor
                {
                    LastName = "AAA",
                    FirstName = "EE",
                    City = "Tallinn",
                    HireDate = DateTime.Now
                    //VocationalCredentials=""
                },
                new Instructor
                {
                    LastName = "SES",
                    FirstName = "NED",
                    City = "Tallinn",
                    HireDate = DateTime.Now,
                    //VocationalCredentials=""
                },
                new Instructor
                {
                    LastName = "Parker",
                    FirstName = "Peter",
                    City = "Tallinn",
                    HireDate = DateTime.Now,
                    //VocationalCredentials=""
                },

            };
            context.Instructors.AddRange(instructors);
            context.SaveChanges();

            if (context.Departments.Any()) { return; }
            var departments = new Department[]
            {
                new Department
                {
                    Name = "Department of Finance",
                    Budget = 0,
                    StartDate = DateTime.Parse("2024-09-01"),
                    DepartmentDescription = "Money Grubbers",
                    MonthlyRevenue = 0,
                    InstructorID = 1
                },
                new Department
                {
                    Name = "Department of Contracts",
                    Budget = 0,
                    StartDate = DateTime.Parse("2024-04-09"),
                    DepartmentDescription = "Those who read between lines",
                    MonthlyRevenue = 0,
                    InstructorID = 2
                },
                new Department
                {
                    Name = "Department of PR",
                    Budget = 0,
                    StartDate = DateTime.Parse("2024-02-03"),
                    DepartmentDescription = "Look at How Fabulous we are!",
                    MonthlyRevenue = 0,
                    InstructorID = 3
                }
            };
            context.Departments.AddRange(departments);
            context.SaveChanges();
        }
    }
}
