using ADONETDisconnectedDEMO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ADONETDisconnectedDEMO.DataAccess
{
    public class ProductDALDC
    {
        private int rowsAffected { get; set; }
        public string ResultText { get; set; }
        
        public void InsertProduct(Product product)
        {
            rowsAffected = 0;
            // Create SQL statement to submit
            string sql = "SELECT * FROM Product";

            // Create a connection
            using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString))
            {
                // Create a SQL Data Adapter
                using (SqlDataAdapter da = new SqlDataAdapter(sql, cnn))
                {
                    // Fill DataTable
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    // Create a command builder
                    using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
                    {
                        dt.Rows.Add(product.ProductId, product.ProductName, product.IntroductionDate, product.Url, product.Price);

                        da.InsertCommand = builder.GetInsertCommand();
                        rowsAffected = da.Update(dt);

                        ResultText = "Rows Affected: " + rowsAffected.ToString();
                    }
                }
            }
        }

        public List<Product> GetProductsAsGenericList()
        {
            rowsAffected = 0;
            List<Product> products = new List<Product>();

            // Initialize DataTable object to null in case of an error
            DataTable dt = null;

            // Create SQL statement to submit
            string sql = "SELECT ProductId, ProductName, IntroductionDate, Url, Price FROM Product";

            // Create a connection
            using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString))
            {
                // Create command object
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    // Create a SQL Data Adapter
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        // Create new DataTable object for filling
                        dt = new DataTable();
                        // Fill DataTable using Data Adapter
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            products = (from row in dt.AsEnumerable()
                                        select new Product
                                        {
                                            ProductId = row.Field<int>("ProductId"),
                                            ProductName = row.Field<string>("ProductName"),
                                            IntroductionDate = row.Field<DateTime>("IntroductionDate"),
                                            Url = row.Field<string>("Url"),
                                            Price = row.Field<decimal>("Price")
                                        }).ToList();

                            rowsAffected = products.Count;

                            ResultText = "Rows Affected: " + rowsAffected.ToString();
                        }
                    }
                }
            }

            return products;
        }

        public List<Product> GetProductsAsGenericListWithWhere(string productName)
        {
            rowsAffected = 0;
            List<Product> products = new List<Product>();

            // Initialize DataTable object to null in case of an error
            DataTable dt = null;

            // Create SQL statement to submit
            string sql = "SELECT ProductId, ProductName, IntroductionDate, Url, Price FROM Product";
            sql += " WHERE ProductName LIKE @ProductName";

            // Create a connection
            using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString))
            {
                // Create command object
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    // Create parameter
                    cmd.Parameters.Add(new SqlParameter("@ProductName", productName));

                    // Create a SQL Data Adapter
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        // Create new DataTable object for filling
                        dt = new DataTable();
                        // Fill DataTable using Data Adapter
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            products = (from row in dt.AsEnumerable()
                                        select new Product
                                        {
                                            ProductId = row.Field<int>("ProductId"),
                                            ProductName = row.Field<string>("ProductName"),
                                            IntroductionDate = row.Field<DateTime>("IntroductionDate"),
                                            Url = row.Field<string>("Url"),
                                            Price = row.Field<decimal>("Price")
                                        }).ToList();

                            rowsAffected = products.Count;

                            ResultText = "Rows Affected: " + rowsAffected.ToString();
                        }
                    }
                }
            }

            return products;
        }
    }
}