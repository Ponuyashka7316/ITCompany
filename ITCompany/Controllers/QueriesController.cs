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

namespace ITCompany.Controllers
{
    public class QueriesController : Controller
    {
        private readonly KvadroContext _context;
        public IConfiguration Configuration { get; }

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
            string connectionString =
            "Data Source=DESKTOP-EKQ8IAM\\SQLEXPRESS;Initial Catalog=Kvadro;Integrated Security=True";
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
            string connectionString =
            "Data Source=DESKTOP-EKQ8IAM\\SQLEXPRESS;Initial Catalog=Kvadro;Integrated Security=True";
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
    }
}