using System;
namespace RealWorldIntFinal
{
    public class User 
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string UserStockFile { get; set; }

		public User(string username,string password)
		{
			this.Username = username;
			this.Password = password;
			this.UserStockFile = $"{username}_stocks.xml";
		}
	}
}

