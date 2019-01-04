using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_18._12._18
{
    class Program
    {
        static public  ModelEntity db;
        static public SqlDataAdapter da;
        static public DataSet ds; 

        static void Main(string[] args)
        {
          
            SqlToDataMethod5();
        }

        static void SqlToDataMethod()
        {
            db = new ModelEntity();

            string connectionString = @"data source=LAPTOP-NSMJD0QF\SQLEXPRESS;initial catalog=CRCMS_new;integrated security=True";
            SqlConnection con = new SqlConnection(connectionString);
            string queryString = "Select * From Area";
            SqlDataAdapter adapter = new SqlDataAdapter(queryString, con);

            DataSet ds = new DataSet();
            adapter.Fill(ds, "Area");            

            DataTable list = ds.Tables["Area"];                     

            var query1 = from a in ds.Tables["Area"].AsEnumerable()
                         where (int)a["TypeArea"] == 1
                         orderby a["AreaId"]
                         select new { Name = a["Name"], FullName = a["FullName"], IP = a["IP"] };
                

            foreach (var a in query1)
            {
                Console.WriteLine(a.Name + "\t" + a.FullName + "\t" + a.IP);
            }
        }

        static void SqlToDataMethod1()
        {
            db = new ModelEntity();
            var query = (from m in db.Area
                         where m.ParentId == 0
                         select new { m.Name, m.FullName, m.IP });

            foreach (var item in query)
            {
                Console.WriteLine(item.Name + "\t" + item.FullName + "\t" + item.IP);
            }

        }

        static void SqlToDataMethod2()
        {
            db = new ModelEntity();

            int[] pav = new int[6];

            var query = db.Area
                       .Where(w => pav.Contains(w.PavilionId));

            foreach (var item in query)
            {
                Console.WriteLine(item.Name + "\t" + item.FullName + "\t" + item.IP);
            }
        }

        static void SqlToDataMethod3()
        {
            db = new ModelEntity();

            int[] pav = new int[6];

            var query = from q in db.Area
                        where pav.Contains(q.PavilionId)
                        select new { q.Name, q.FullName, q.IP };

            foreach (var item in query)
            {
                Console.WriteLine(item.Name + "\t" + item.FullName + "\t" + item.IP);
            }
        }

        static void SqlToDataMethod4()
        {
            db = new ModelEntity();

            var query = from q in db.Area
                        let wp = q.WorkingPeople
                        where wp > 1
                        select q;

            foreach (var item in query)
            {
                Console.WriteLine(item.Name + "\t" + item.IP + "\t" + item.WorkingPeople);
            }
        }

        static void SqlToDataMethod5()
        {         
            db = new ModelEntity();

            var query = from q in db.Area
                        select new { q.ParentId, q.FullName, q.Dependence, q.PavilionId }
                        into nq
                        where nq.Dependence > 0
                        select nq;

            foreach (var item in query)
            {
                Console.WriteLine(item.ParentId + "\t" + item.FullName + "\t" + item.Dependence + "\t" + item.PavilionId);
            }
        }
    }
}
