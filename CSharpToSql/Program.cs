using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToSql {

	class Program	{

		static List<User> users = new List<User>();

		void Run()	{
			User user = new User();
			user.id = 23;
			user.Username = "YY";
			user.Password = "DEF";
			user.Firstname = "Bob";
			user.Lastname = "JKL";
			user.Email = "ObamaSucks@com";
			user.Phone = "MNO";
			user.IsReviewer = true;
			user.IsAdmin = true;
			user.Active = true;
			Update(user);
		}

		static void Main(string[] args) {    //create a selectable statement which can change the method to use select or insert
			(new Program()).Run();			
		}
		void Update(User user)
		{

			string connStr = @"server=localhost\SQLEXPRESS;database=prsdatabase;Trusted_connection=true";
			SqlConnection conn = new SqlConnection(connStr);
			conn.Open();
			if (conn.State != System.Data.ConnectionState.Open) {
				throw new ApplicationException("Connection did not open");
			}

			string sql = " Update [user] "
					+ " Set Username = @Username, "
					+ " Password = @Password, "
					+ " Firstname = @Firstname, "
					+ " Lastname = @Lastname, "
					+ " Phone = @Phone, "
					+ " Email = @Email, "
					+ " IsReviewer = @IsReviewer, "
					+ " IsAdmin = @IsAdmin, "
					+ " Active = @Active "
					+ " Where Id = @Id ";

			SqlCommand cmd = new SqlCommand(sql, conn);
			cmd.Parameters.Add(new SqlParameter("@Username", user.Username));   //setting a new parameter, the parameter name has a @, and the value
			cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
			cmd.Parameters.Add(new SqlParameter("@Firstname", user.Firstname));
			cmd.Parameters.Add(new SqlParameter("@Lastname", user.Lastname));
			cmd.Parameters.Add(new SqlParameter("@Phone", user.Phone));
			cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
			cmd.Parameters.Add(new SqlParameter("@IsReviewer", user.IsReviewer));
			cmd.Parameters.Add(new SqlParameter("@IsAdmin", user.IsAdmin));
			cmd.Parameters.Add(new SqlParameter("@Active", user.Active));
			cmd.Parameters.Add(new SqlParameter("@Id", user.id));

			int recsAffected = cmd.ExecuteNonQuery();   //by hovering over the .exec it will state the number of rows affected and returning an int.  if 0 then statement incorrect.
			if (recsAffected != 1) {
				System.Diagnostics.Debug.WriteLine("Records insert failed");
			}
			conn.Close();
		}
		void Insert(User user)	{

			string connStr = @"server=localhost\SQLEXPRESS;database=prsdatabase;Trusted_connection=true";
			SqlConnection conn = new SqlConnection(connStr);
			conn.Open();
			if (conn.State != System.Data.ConnectionState.Open) {
				throw new ApplicationException("Connection did not open");
			}

			string sql = "Insert into [user] (Username, Password, Firstname, Lastname, Phone, Email, IsReviewer, IsAdmin, Active) " //calling a string called sql from the statement 
			+ "values (@Username, @Password, @Firstname, @Lastname, @Phone, @Email, @IsReviewer, @IsAdmin, @Active)";

			SqlCommand cmd = new SqlCommand(sql, conn);
			cmd.Parameters.Add(new SqlParameter("@Username", user.Username));   //setting a new parameter, the parameter name has a @, and the value
			cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
			cmd.Parameters.Add(new SqlParameter("@Firstname", user.Firstname));
			cmd.Parameters.Add(new SqlParameter("@Lastname", user.Lastname));
			cmd.Parameters.Add(new SqlParameter("@Phone", user.Phone));
			cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
			cmd.Parameters.Add(new SqlParameter("@IsReviewer", user.IsReviewer));
			cmd.Parameters.Add(new SqlParameter("@IsAdmin", user.IsAdmin));
			cmd.Parameters.Add(new SqlParameter("@Active", user.Active));

			int recsAffected = cmd.ExecuteNonQuery();   //by hovering over the .exec it will state the number of rows affected and returning an int.  if 0 then statement incorrect.
			if (recsAffected != 1) {
				System.Diagnostics.Debug.WriteLine("Records insert failed");
			}
			conn.Close();
		}
			void select() {

			string connStr = @"server=localhost\SQLEXPRESS;database=prsdatabase;Trusted_connection=true";
			SqlConnection conn = new SqlConnection(connStr);
			conn.Open();
			if(conn.State != System.Data.ConnectionState.Open) {
				throw new ApplicationException("Connection did not open");
			}

			string sql = "select * from [user]";  //calling a string called sql from the statement 

			SqlCommand cmd = new SqlCommand(sql, conn);  

			SqlDataReader reader = cmd.ExecuteReader();   //used when using a select statement, store cmd.reader in reader from SqlDataReader

			while (reader.Read()) {   //this will run a while loop and read the entire list until complete, use ? to allow int to get null
				int id = reader.GetInt32(reader.GetOrdinal("Id"));  //int=varibale from SQL, id= new local variable
				string username = reader.GetString(reader.GetOrdinal("Username"));    //GetInt32==>"Id"=column name==>thill end the same
				string password = reader.GetString(reader.GetOrdinal("Password"));
				string firstname = reader.GetString(reader.GetOrdinal("Firstname"));
				string lastname = reader.GetString(reader.GetOrdinal("Lastname"));
				string phone = reader.GetString(reader.GetOrdinal("Phone"));
				string email = reader.GetString(reader.GetOrdinal("Email"));
				bool isreviewer = reader.GetBoolean(reader.GetOrdinal("IsReviewer"));
				bool isadmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin"));
				bool active = reader.GetBoolean(reader.GetOrdinal("Active"));

				User user = new User();
				user.id = id;
				user.Username = username;
				user.Password = password;
				user.Firstname = firstname;
				user.Lastname = lastname;
				user.Phone = phone;
				user.IsReviewer = isreviewer;
				user.IsAdmin = isadmin;
				user.Active = active;

				users.Add(user);

				System.Diagnostics.Debug.WriteLine($"{id}, {username}, {password}, {firstname}, {lastname}, {phone}, {email}, {isadmin}, {isreviewer}, {active}");   //example writeline 
			}
			//GetInt32==>"Id"=column name==>thill end the same
			conn.Close();
		}
	}
}