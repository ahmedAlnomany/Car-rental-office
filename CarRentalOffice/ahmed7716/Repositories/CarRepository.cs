using ahmed7716.Helpers;
using ahmed7716.Models;
using System.Data;
using System.Data.SqlClient;

namespace ahmed7716.Repositories
{
    public class CarRepository
    {
        public List<Car> GetAllCars()
        {
            List<Car> cars = new List<Car>();
            DataTable dataTable = DBHelper.ExecuteTableFunction("GetAllCars");

            foreach (DataRow row in dataTable.Rows)
            {
                cars.Add(new Car
                {
                    Id = Convert.ToInt32(row["CarID"]),
                    Name = row["Make"].ToString(),
                    Model = row["Model"].ToString(),
                    Year = Convert.ToInt32(row["Year"]),
                    Color = row["Color"].ToString(),
                    PlateNumberce = row["PlateNumber"].ToString(),
                    Status = row["Status"].ToString()
                });
            }

            return cars;
        }
        public Car GetCarById(int id)
        {
            var car = GetAllCars().FirstOrDefault(x => x.Id == id);
            return car;
        }

        public int AddCar(Car car)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@Make", car.Name),
            new SqlParameter("@Model", car.Model),
            new SqlParameter("@Year", car.Year),
            new SqlParameter("@Color", car.Color),
            new SqlParameter("@PlateNumber", car.PlateNumberce),
            new SqlParameter("@Status", car.Status)
            };

            DBHelper.ExecuteStoredProcedure("AddCar", parameters);

            return 1;
        }

        public bool UpdateCar(Car car)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@CarID", car.Id),
            new SqlParameter("@Make", car.Name),
            new SqlParameter("@Model", car.Model),
            new SqlParameter("@Year", car.Year),
            new SqlParameter("@Color", car.Color),
            new SqlParameter("@PlateNumber", car.PlateNumberce),
            new SqlParameter("@Status", car.Status)
            };

            int rowsAffected = DBHelper.ExecuteStoredProcedure("UpdateCar", parameters).Rows.Count;
            return rowsAffected > 0;
        }

        public bool DeleteCar(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@CarID", id)
            };

            int rowsAffected = DBHelper.ExecuteStoredProcedure("DeleteCar", parameters).Rows.Count;
            return rowsAffected > 0;
        }
    }
}
