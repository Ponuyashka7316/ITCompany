using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCompany.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace ITCompany.Controllers
{
    public class QueriesController : Controller
    {
        private readonly KvadroContext _context;
        public IConfiguration Configuration { get; }
        string connectionString =
            "Data Source=DESKTOP-EKQ8IAM\\SQLEXPRESS;Initial Catalog=Kvadro;Integrated Security=True";
        public QueriesController(KvadroContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetProjectCost(int? page)
        {

            //ViewBag.OnePage = _context.ProjectInfo.Include(e => e.EmployeeInfo).Take(7).ToList().ToPagedList(page ?? 1, 5);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var kvadroContext = _context.ProjectInfo.Include(e => e.EmployeeInfo);
            var pageNumber = page ?? 1;
            var onePage = kvadroContext.ToPagedList(pageNumber, 5);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            ViewBag.OnePage = onePage;
            ViewBag.time = elapsedTime;
            ViewBag.Type = "EF6";
            return View();
        }


        public IActionResult GetProjectCostADO(int? page)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var pageNumber = page ?? 1;
            ViewBag.Type = "ADO.NET";
            List<ProjectInfo> lstproject = new List<ProjectInfo>();
           
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.ProjectInfo", con);
                //cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ProjectInfo project = new ProjectInfo();
                    project.ProjectName = rdr["ProjectName"].ToString();
                    project.Chief = rdr["Chief"].ToString();
                    project.Cost = decimal.Parse(rdr["Cost"].ToString());
                    project.ProjectStart = DateTime.Parse(rdr["ProjectStart"].ToString());
                    project.ProjectStop = DateTime.Parse(rdr["ProjectStop"].ToString());


                    lstproject.Add(project);
                }
                con.Close();
            }
            var onePage = lstproject.ToPagedList(pageNumber, 5);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            ViewBag.OnePage = onePage;
            ViewBag.time = elapsedTime;

            return View();
        }


        public IActionResult GetProjectCostPLINQ(int? page)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var pageNumber = page ?? 1;
            ViewBag.Type = "ADO.NET";
            List<ProjectInfo> lstproject = new List<ProjectInfo>();
           
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.ProjectInfo", con);
                //cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ProjectInfo project = new ProjectInfo();
                    project.ProjectName = rdr["ProjectName"].ToString();
                    project.Chief = rdr["Chief"].ToString();
                    project.Cost = decimal.Parse(rdr["Cost"].ToString());
                    project.ProjectStart = DateTime.Parse(rdr["ProjectStart"].ToString());
                    project.ProjectStop = DateTime.Parse(rdr["ProjectStop"].ToString());


                    lstproject.Add(project);
                }
                con.Close();
            }
            var onePage = lstproject.ToPagedList(pageNumber, 5);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            ViewBag.OnePage = onePage;
            ViewBag.time = elapsedTime;

            return View();
        }

        public IActionResult GetFinishingProjects(int? page)
        {

            //ViewBag.OnePage = _context.ProjectInfo.Include(e => e.EmployeeInfo).Take(7).ToList().ToPagedList(page ?? 1, 5);
            var periodFrom = new DateTime(2017, 1, 1, 11, 30, 0);
            var periodTo = new DateTime(2018, 12, 31, 12, 30, 0);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var kvadroContext = _context.ProjectInfo.Include(e => e.EmployeeInfo).Where(e => e.ProjectStop.Value > periodFrom && e.ProjectStop.Value < periodTo);
            var pageNumber = page ?? 1;
            var onePage = kvadroContext.ToPagedList(pageNumber, 5);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            ViewBag.OnePage = onePage;
            ViewBag.time = elapsedTime;
            ViewBag.Type = "EF6";
            return View();
        }

        public IActionResult GetFinishedProjectsADO(int? page)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var pageNumber = page ?? 1;
            ViewBag.Type = "ADO.NET";
            List<ProjectInfo> lstproject = new List<ProjectInfo>();
           
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.ProjectInfo WHERE ProjectStop BETWEEN '2017-01-01' AND '2018-12-31'; ", con);
                //cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ProjectInfo project = new ProjectInfo();
                    project.ProjectName = rdr["ProjectName"].ToString();
                    project.Chief = rdr["Chief"].ToString();
                    project.Cost = decimal.Parse(rdr["Cost"].ToString());
                    project.ProjectStart = DateTime.Parse(rdr["ProjectStart"].ToString());
                    project.ProjectStop = DateTime.Parse(rdr["ProjectStop"].ToString());


                    lstproject.Add(project);
                }
                con.Close();
            }
            var onePage = lstproject.ToPagedList(pageNumber, 5);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            ViewBag.OnePage = onePage;
            ViewBag.time = elapsedTime;

            return View();
        }

        public IActionResult GetFinishedProjectsPLINQ(int? page)
        {
            var periodFrom = new DateTime(2017, 1, 1, 11, 30, 0);
            var periodTo = new DateTime(2018, 12, 31, 12, 30, 0);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var pageNumber = page ?? 1;
            ViewBag.Type = "PLINQ";
            //string connectionString =
            //"Data Source=DESKTOP-EKQ8IAM\\SQLEXPRESS;Initial Catalog=Kvadro;Integrated Security=True";
            //DataContext db = new DataContext(connectionString);

            var lstproject = (from r in _context.ProjectInfo.AsParallel()
                              where r.ProjectStop > periodFrom
                              && r.ProjectStop < periodTo
                              select r);
            var onePage = lstproject.ToPagedList(pageNumber, 5);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            ViewBag.OnePage = onePage;
            ViewBag.time = elapsedTime;

            return View();
        }

        public IActionResult GetEmployeesWithProjects(int? page)
        {

           
            var periodFrom = new DateTime(2017, 1, 1, 11, 30, 0);
            var periodTo = new DateTime(2018, 12, 31, 12, 30, 0);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var kvadroContext = _context.EmployeeInfo.Join(_context.ParticipationInProject, d => d.EmployeeId,
        f => f.EmployeeId,
        (d, f) => d);
            var pageNumber = page ?? 1;
            var onePage = kvadroContext.ToPagedList(pageNumber, 5);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            ViewBag.OnePage = onePage;
            ViewBag.time = elapsedTime;
            ViewBag.Type = "EF6";
            return View();
        }

        public IActionResult GetEmployeesWithProjectsADO(int? page)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var pageNumber = page ?? 1;
            ViewBag.Type = "ADO.NET";
            List<EmployeeInfo> lstproject = new List<EmployeeInfo>();
            var query = "SELECT * FROM dbo.EmployeeInfo, dbo.ParticipationInProject where dbo.ParticipationInProject.EmployeeId=dbo.EmployeeInfo.EmployeeId";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                //cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    EmployeeInfo project = new EmployeeInfo();
                    project.EmployeeName = rdr["EmployeeName"].ToString();
                    project.Adress = rdr["Adress"].ToString();
                    project.Year = DateTime.Parse(rdr["Year"].ToString());
                    project.District = rdr["District"].ToString();
                    project.Language = rdr["Language"].ToString();


                    lstproject.Add(project);
                }
                con.Close();
            }
            var onePage = lstproject.ToPagedList(pageNumber, 5);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            ViewBag.OnePage = onePage;
            ViewBag.time = elapsedTime;

            return View();
        }
    }
}