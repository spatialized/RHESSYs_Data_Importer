using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RHESSYs_Data_Importer.Models;

namespace RHESSYs_Data_Importer.DAL
{
    public class RHESSYsDAL
    {
        /// <summary>
        /// Add data point to FutureMountain cubedata table
        /// </summary>
        public bool AddDataPoint(CubeDataPoint data)
        {
            using (var db = new CubeDataDbContext())
            {
                db.CubeData.Add(data);
                if (db.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Add data point to FutureMountain dates table
        /// </summary>
        public bool AddDate(Date date)
        {
            using (var db = new DatesDbContext())
            {
                db.Dates.Add(date);
                if (db.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
