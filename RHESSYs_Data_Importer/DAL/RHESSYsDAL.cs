using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RHESSYs_Data_Importer.Models;
using RHESSYs_Data_Importer.Models.RHESSYs_Data_Importer.Models;

namespace RHESSYs_Data_Importer.DAL
{
    public class RHESSYsDAL
    {
        private bool useMySQL = true;

        private static readonly string mySqlConnectionString = ConnectionHelper.GetConnectionString();

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


        /// <summary>
        /// Add data point to FutureMountain dates table
        /// </summary>
        public bool AddWaterDataFrame(WaterDataFrame frame)
        {
            using (var db = new WaterDataDbContext())
            {
                db.WaterData.Add(frame);
                if (db.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
        }

        
        /// <summary>
        /// Add data point to FutureMountain dates table
        /// </summary>
        public bool AddPatchData(int patchId, PatchPointCollection frame)
        {
            using (var db = new PatchDataDbContext())
            {
                PatchDataRecord record = ConvertPatchPointCollectionToRecord(frame, patchId);
                db.PatchData.Add(record);
                if (db.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
        }

        private PatchDataRecord ConvertPatchPointCollectionToRecord(PatchPointCollection frame, int patchId)
        {
            PatchDataRecord record = new PatchDataRecord();
            record.patchID = patchId;
            record.SetData(frame);
            return record;
        }

        /// <summary>
        /// Add data point to FutureMountain dates table
        /// </summary>
        public bool AddTerrainDataFrame(TerrainDataFrameJSONRecord frame)
        {
            try
            {
                using (var db = new TerrainDataDbContext())
                {
                    db.TerrainData.Add(frame);
                    if (db.SaveChanges() > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: ex:" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Add data point to FutureMountain dates table
        /// </summary>
        public bool AddFireDataFrame(FireDataFrameJSONRecord frame)
        {
            try
            {
                using (var db = new FireDataDbContext())
                {
                    db.FireData.Add(frame);
                    if (db.SaveChanges() > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: ex:" + ex.Message);
                return false;
            }
        }
    }
}
