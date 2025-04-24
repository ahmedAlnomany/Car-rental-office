using ahmed7716.Helpers;
using ahmed7716.Models;
using System.Data;
using System.Data.SqlClient;

namespace ahmed7716.Repositories
{
    public class RentalRepository
    {
        public List<Rental> GetAllRentals()
        {
            List<Rental> rentals = new List<Rental>();
            DataTable dataTable = DBHelper.ExecuteTableFunction("GetAllRentals");
            CarRepository carRepository = new CarRepository();
            CustomerRepository customerRepository = new CustomerRepository();
            foreach (DataRow row in dataTable.Rows)
            {
                rentals.Add(new Rental
                {
                    RentalID = Convert.ToInt32(row["RentalID"]),
                    CustomerID = Convert.ToInt32(row["CustomerID"]),
                    CarID = Convert.ToInt32(row["CarID"]),
                    StartDate = Convert.ToDateTime(row["StartDate"]),
                    EndDate = Convert.ToDateTime(row["EndDate"]),
                    TotalCost = Convert.ToDecimal(row["TotalCost"]),
                    PaymentStatus = row["PaymentStatus"].ToString(),
                    car = carRepository.GetCarById(Convert.ToInt32(row["CarID"])),
                    customer = customerRepository.GetCustomerById(Convert.ToInt32(row["CustomerID"]))


                });
            }

            return rentals;
        }
        public Rental GetRentalById(int id)
        {
            var RentaledCar = GetAllRentals().FirstOrDefault(x => x.RentalID == id);
            return RentaledCar;
        }
        public int AddRental(Rental rental)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@CustomerID", rental.CustomerID),
            new SqlParameter("@CarID", rental.CarID),
            new SqlParameter("@StartDate", rental.StartDate),
            new SqlParameter("@EndDate", rental.EndDate),
            new SqlParameter("@TotalCost", rental.TotalCost),
            new SqlParameter("@PaymentStatus", rental.PaymentStatus)
            };

            DBHelper.ExecuteStoredProcedure("AddRental", parameters);


            return 1;
        }

        public bool UpdateRental(Rental rental)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@RentalID", rental.RentalID),
            new SqlParameter("@CustomerID", rental.CustomerID),
            new SqlParameter("@CarID", rental.CarID),
            new SqlParameter("@StartDate", rental.StartDate),
            new SqlParameter("@EndDate", rental.EndDate),
            new SqlParameter("@TotalCost", rental.TotalCost),
            new SqlParameter("@PaymentStatus", rental.PaymentStatus)
            };

            int rowsAffected = DBHelper.ExecuteStoredProcedure("UpdateRental", parameters).Rows.Count;
            return rowsAffected > 0;
        }

        public bool DeleteRental(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@RentalID", id)
            };

            int rowsAffected = DBHelper.ExecuteStoredProcedure("DeleteRental", parameters).Rows.Count;
            return rowsAffected > 0;
        }
    }

}

