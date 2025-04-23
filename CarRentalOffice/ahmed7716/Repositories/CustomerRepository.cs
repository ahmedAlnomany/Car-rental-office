using ahmed7716.Helpers;
using ahmed7716.Models;
using System.Data;
using System.Data.SqlClient;


namespace ahmed7716.Repositories
{
    public class CustomerRepository
    {
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            DataTable dataTable = DBHelper.ExecuteTableFunction("GetAllCustomers");

            foreach (DataRow row in dataTable.Rows)
            {
                customers.Add(new Customer
                {
                    CustomerID = Convert.ToInt32(row["CustomerID"]),
                    Name = row["Name"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Email = row["Email"].ToString(),
                    Adrss = row["Address"].ToString(),
                    LicenseNumber = row["LicenseNumber"].ToString()
                });
            }

            return customers;
        }

        public int AddCustomer(Customer customer)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@Name", customer.Name),
            new SqlParameter("@Phone", customer.Phone),
            new SqlParameter("@Email", customer.Email),
            new SqlParameter("@Address", customer.Adrss),
            new SqlParameter("@LicenseNumber", customer.LicenseNumber)
            };

            DBHelper.ExecuteStoredProcedure("AddCustomer", parameters);

            // للحصول على آخر ID تم إدخاله
            DataTable dt = DBHelper.ExecuteQuery("SELECT SCOPE_IDENTITY() AS NewID");
            return Convert.ToInt32(dt.Rows[0]["NewID"]);
        }

        public bool UpdateCustomer(Customer customer)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@CustomerID", customer.CustomerID),
            new SqlParameter("@Name", customer.Name),
            new SqlParameter("@Phone", customer.Phone),
            new SqlParameter("@Email", customer.Email),
            new SqlParameter("@Address", customer.Adrss),
            new SqlParameter("@LicenseNumber", customer.LicenseNumber)
            };

            int rowsAffected = DBHelper.ExecuteStoredProcedure("UpdateCustomer", parameters).Rows.Count;
            return rowsAffected > 0;
        }

        public bool DeleteCustomer(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@CustomerID", id)
            };

            int rowsAffected = DBHelper.ExecuteStoredProcedure("DeleteCustomer", parameters).Rows.Count;
            return rowsAffected > 0;
        }
    }
}
