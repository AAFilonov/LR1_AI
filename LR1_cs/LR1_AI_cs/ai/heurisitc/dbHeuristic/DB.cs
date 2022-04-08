using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai.heurisitc.dbHeuristic
{
    public class DB
    {
        //private string DB_FILE_NAME = "AI_DB.db";
        public string DB_FILE_NAME ;
        private string CURRENT_PATH = Directory.GetCurrentDirectory();
        private string TABLE_NAME = "templates_table";

        public DB(string dbFileName)
        {
            if (!File.Exists(CURRENT_PATH + "\\" + DB_FILE_NAME))
            {
                createTable(dbFileName);
            }
        }

        private void createTable(string dbFileName)
        {
            this.DB_FILE_NAME = dbFileName;
            SQLiteConnection
                .CreateFile(CURRENT_PATH + "\\" +
                            DB_FILE_NAME);

            using (SQLiteConnection Connect = getConnetion())
            {
                string commandText =
                    "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                    "[id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                    "[template] NVARCHAR(128)," +
                    "[target] NVARCHAR(128)," +
                    "[score] INTEGER)";

                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open();
                Command.ExecuteNonQuery();
                Connect.Close();
            }
        }

        public SQLiteConnection getConnetion()
        {
            return new SQLiteConnection(@"Data Source=" + CURRENT_PATH + "\\" +
                                        DB_FILE_NAME +
                                        "; Version=3;");
        }

        public void save(State stateToSave, State targetState, int eval)
        {
            var templateStr = Parser.toString(stateToSave);
            var targetStr = Parser.toString(targetState);

            var result = findByTargetAndTemplate(targetStr, templateStr);
            if (result.Count != 0)
            {
                Console.WriteLine("Record for  state " + stateToSave + " " + targetStr + " already exist,  eval is " +
                                  result[0].Item3 + "");
            }
            else
            {
                Console.WriteLine("Saving state " + stateToSave + " " + targetStr + " with eval(" + eval + ")");
                insert(eval, templateStr, targetStr);
            }
        }

        private void insert(int eval, string stateString, string stateTargetString)
        {
            var connection = getConnetion();
            String querry = "INSERT INTO " + TABLE_NAME + "( [template],[target],[score]) " +
                            "VALUES ('" + stateString + "','" + stateTargetString + "'," + eval + ")";
            SQLiteCommand Command = new SQLiteCommand(querry, connection);
            connection.Open(); // открыть соединение
            Command.ExecuteNonQuery(); // выполнить запрос
            connection.Close();
        }

    
        public List<Tuple<String, String, int>> findByTarget(string targetStateStr)
        {
            var whereStatement =
                " WHERE [target] = '" + targetStateStr;
            return find(whereStatement);
        }

        public List<Tuple<String, String, int>> findByTargetAndTemplate(string targetStateStr,
            string templateStateStr)
        {
            var whereStatement =
                " WHERE [target] = '" + targetStateStr + "' AND [template] ='" + templateStateStr + "'";
            return find(whereStatement);
        }

        private List<Tuple<String, String, int>> find(string whereStatement)
        {
            var connection = getConnetion();
            String querry = "SELECT * FROM " + TABLE_NAME + " " + whereStatement;

            SQLiteCommand Command = new SQLiteCommand(querry, connection);
            connection.Open();
            var results = new List<Tuple<string, string, int>>();
            using (var reader = Command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        var id = reader.GetValue(0);
                        string templateStr = reader.GetString(1);
                        var targetStr = reader.GetString(2);
                        int score = (int) reader.GetInt32(3);
                        Tuple<String, String, int> result =
                            new Tuple<string, string, int>(templateStr, targetStr, score);
                        results.Add(result);
                    }
                }
            }

            return results;
        }

        public static void print(string[] arr, int eval)
        {
            Console.Write("Saving state [");
            for (var index = 0; index < arr.ToList().Count; index++)
            {
                var i = arr.ToList()[index];
                Console.Write(i.ToString() + ",");
            }

            Console.Write("] (" + eval + ")\n");
        }

        public List<State> findDistingTargetStates()
        {
            var connection = getConnetion();
            String querry = "SELECT DISTINCT [target] FROM " + TABLE_NAME ;

            SQLiteCommand Command = new SQLiteCommand(querry, connection);
            connection.Open();
            var results = new List<State>();
            using (var reader = Command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        var targetStr = reader.GetString(0);
                        var state = Parser.fromString(targetStr);
                        results.Add(state);
                    }
                }
            }

            return results;
        }
    }
}