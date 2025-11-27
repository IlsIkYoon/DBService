using Microsoft.Data.SqlClient;

namespace DBService.DBManager
{
    public class DBManager
    {

        public DBManager()
        {
            //연결 확인 코드
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=GameUserDB;User Id=gameUser;Password=1234;Encrypt=False;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("DB 연결 성공!");
            }

        }
     


    }
}
