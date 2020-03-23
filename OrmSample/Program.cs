using System;
using ECode.Configuration;
using ECode.Data;
using Sample1.Models;

namespace Sample1
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigurationManager.AddProvider(new EnvironmentVariables());
            ConfigurationManager.AddProvider(new JsonConfigFile("appsettings.json"));


            // 租户：school-123
            ObjectManager.Database.ExecAction("school-123", session =>
            {
                session.Table<StudentModel>()
                       .Add(new StudentModel
                       {
                           SchoolId = "school-123",
                           ID = "zhang3",
                           Name = "张三",
                           Remark = "优等生"
                       });

                session.Table<StudentModel>()
                       .AddRange(new[]{
                           new StudentModel
                           {
                               SchoolId = "school-123",
                               ID = "li4",
                               Name = "李四",
                               Remark = "特困生"
                           },
                           new StudentModel
                           {
                               SchoolId = "school-123",
                               ID = "wang5",
                               Name = "王五",
                               Remark = null
                           }
                       });

                session.Table<StudentModel>()
                       .Update(new
                       {
                           Remark = "我不是特困生"
                       }, t => t.SchoolId == "school-123" && t.ID == "li4");

                session.Table<StudentModel>()
                       .Delete(t => t.SchoolId == "school-123" && t.ID == "wang5");


                // 自增ID
                session.Table<ScoreModel>()
                       .Add(new ScoreModel
                       {
                           SchoolId = "school-123",
                           StudentId = "zhang3",
                           Course = "Chinese",
                           Score = 100
                       });

                session.Table<ScoreModel>()
                       .Add(new ScoreModel
                       {
                           SchoolId = "school-123",
                           StudentId = "li4",
                           Course = "Chinese",
                           Score = 59
                       });


                session.Table<ScoreModel>()
                       .Where(t => t.SchoolId == "school-123")
                       .GroupBy(t => new[] { t.Course })
                       .Select(t => new
                       {
                           Course = t.Course,
                           Avg = SqlAggrFunc.Avg(() => t.Score)
                       })
                       .First();


                session.Table<StudentModel>()
                       .Join<ScoreModel>((t1, t2) => t1.SchoolId == t2.SchoolId && t1.ID == t2.StudentId)
                       .Select((t1, t2) => new
                       {
                           t1.SchoolId,
                           StudentId = t1.ID,
                           StudentName = t1.Name,
                           t2.Course,
                           t2.Score
                       })
                       .ToList();
            });

            // 租户：school-456
            ObjectManager.Database.ExecAction("school-456", session =>
            {
                var tran = session.BeginTransaction();

                session.Table<StudentModel>()
                       .Add(new StudentModel
                       {
                           SchoolId = "school-456",
                           ID = "zhang3",
                           Name = "张三",
                           Remark = "优等生"
                       });

                session.Table<StudentModel>()
                       .AddRange(new[]{
                           new StudentModel
                           {
                               SchoolId = "school-456",
                               ID = "li4",
                               Name = "李四",
                               Remark = "特困生"
                           },
                           new StudentModel
                           {
                               SchoolId = "school-456",
                               ID = "wang5",
                               Name = "王五",
                               Remark = null
                           }
                       });

                session.Table<StudentModel>()
                       .Update(new
                       {
                           Remark = "我不是特困生"
                       }, t => t.SchoolId == "school-456" && t.ID == "li4");

                session.Table<StudentModel>()
                       .Delete(t => t.SchoolId == "school-456" && t.ID == "wang5");


                // 自增ID
                session.Table<ScoreModel>()
                       .Add(new ScoreModel
                       {
                           SchoolId = "school-456",
                           StudentId = "zhang3",
                           Course = "Chinese",
                           Score = 100
                       });

                session.Table<ScoreModel>()
                       .Add(new ScoreModel
                       {
                           SchoolId = "school-456",
                           StudentId = "li4",
                           Course = "Chinese",
                           Score = 59
                       });


                session.Table<ScoreModel>()
                       .Where(t => t.SchoolId == "school-456")
                       .GroupBy(t => new[] { t.Course })
                       .Select(t => new
                       {
                           Course = t.Course,
                           Avg = SqlAggrFunc.Avg(() => t.Score)
                       })
                       .First();


                session.Table<StudentModel>()
                       .Join<ScoreModel>((t1, t2) => t1.SchoolId == t2.SchoolId && t1.ID == t2.StudentId)
                       .Select((t1, t2) => new
                       {
                           t1.SchoolId,
                           StudentId = t1.ID,
                           StudentName = t1.Name,
                           t2.Course,
                           t2.Score
                       })
                       .ToList();

                tran.Rollback();
            });

            Console.WriteLine("OK!");
            Console.ReadLine();
        }
    }
}
