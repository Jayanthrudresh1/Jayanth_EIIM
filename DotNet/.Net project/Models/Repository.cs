using System;
using System.Data.SqlClient;
namespace _Net_project.Models;

    
public class Repository
    {
        public static List<LoginModel> adminList = new List<LoginModel>();
 
        public static List<LoginModel> GetAdminDetails()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(getConnection()))
                {
            
                connection.Open();
                SqlCommand sqlCommand=new SqlCommand("select * from Admin_details",connection);
                SqlDataReader sqlReader=sqlCommand.ExecuteReader();
                while (sqlReader.Read())
                {
                    LoginModel admin = new LoginModel();
                    admin.Email=sqlReader["email"].ToString();
                    admin.Password=sqlReader["password"].ToString();
                
                    adminList.Add(admin);                  
                }
                }                
            }
 
             catch (SqlException exception)
            {                  
                Console.WriteLine("Datebase error"+exception);              
            }
            return adminList;
        }
 
        public static int AdminLogin(LoginModel admin)
        {
            string Email = admin.Email;
            string password = admin.Password;
            List<LoginModel> adminList = GetAdminDetails();
            foreach(LoginModel item in adminList)
            {
                if(String.Equals(Email,item.Email) && String.Equals(password,item.Password))
                {
                    return 1;
                }
            }
            return 2;
         }



    public static List<SignupModel> userList = new List<SignupModel>();
 
        public static List<SignupModel> GetUserDetails()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(getConnection()))
                {
                connection.Open();
                SqlCommand sqlCommand=new SqlCommand("select * from User_details",connection);
                SqlDataReader sqlReader=sqlCommand.ExecuteReader();
                while (sqlReader.Read())
                 {
                    SignupModel user = new SignupModel();
                    user.Username=sqlReader["username"].ToString();
                    user.Email=sqlReader["email"].ToString();
                    user.Password=sqlReader["password"].ToString();

                    userList.Add(user);                  
                }
                }                
            }
 
             catch (SqlException exception)
            {                  
                Console.WriteLine("Datebase error"+exception);              
            }
            
            return userList;

        }
 
        public static Tuple<int, string> UserLogin(SignupModel user)
        {
            string Email = user.Email;
            string Password = user.Password;
            List<SignupModel> userList = GetUserDetails();
            foreach(SignupModel item in userList)
            {
                if(String.Equals(Email,item.Email) && String.Equals(Password,item.Password))
                {
                    int variable1 = 1;
                    string variable2 = item.Username;
                    return  Tuple.Create(variable1, variable2);
                }
            }
                    return  Tuple.Create(2,"");
         }

    public static int UserSignup(SignupModel user)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(getConnection()))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO User_details (username, email, password) VALUES (@Username, @Email, @Password)", connection);
                sqlCommand.Parameters.AddWithValue("@Username", user.Username);
                sqlCommand.Parameters.AddWithValue("@Email", user.Email);
                sqlCommand.Parameters.AddWithValue("@Password", user.Password);
                

                int rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return 1; // Successful signup
                }
            }
        }
        catch (SqlException exception)
        {
            Console.WriteLine("Database error" + exception);
        }

        return 2; // Signup failed
    }



        public static string getConnection()
        {
        var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json",optional:true,reloadOnChange:true);
        IConfiguration configuration = build.Build();
        string connectionString = Convert.ToString(configuration.GetConnectionString("DefaultConnection"));
        return connectionString;
        } 
         
         }
