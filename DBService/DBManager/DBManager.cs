using Microsoft.Data.SqlClient;
using System.Numerics;

namespace DBService.DBManager
{
    public class DBManager
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=GameUserDB;User Id=gameUser;Password=1234;Encrypt=False;";
        SqlConnection connection;

        public DBManager()
        {
            //연결 확인 코드
            

            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("DB 연결 성공!");
            }

        }

        public bool CreateCharacter(ulong ID)
        {
            int defaultLevel = 1;
            int defaultHp = 1;

            //여기서부터 DB 쿼리 작업 
            string query = "INSERT INTO UserData (UserLevel, UserHP) VALUES (@Level, @HP)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Level", defaultLevel);
                cmd.Parameters.AddWithValue("@HP", defaultHp);

                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0; 
            }
            return true;
        }

        public bool ReadCharacter(ulong ID, out uint level, out uint hp)
        {
            string query = @"
            SELECT UserLevel, UserHP
            FROM UserData
            WHERE UserID = @ID";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ID", ID);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) // 데이터가 존재하면 true
                    {
                        level = (uint)(int)reader["UserLevel"];
                        hp = (uint)(int)reader["UserHP"];
                        return true;
                    }
                    else
                    {
                        // 해당 ID 존재 X
                        level = 0;
                        hp = 0;

                        return false;
                    }
                }
            }
        }

        public bool UpdateCharacter(ulong ID, uint level, uint hp)
        {
            string query = @"
                UPDATE UserData
                SET UserLevel = @Level, UserHP = @HP
                WHERE ID = @ID";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Level", level);
                cmd.Parameters.AddWithValue("@HP", hp);

                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public bool DeleteCharacter(ulong ID)
        {
            string query = @"
                DELETE UserData
                WHERE UserID = @ID";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ID", ID);

                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
    }
}
