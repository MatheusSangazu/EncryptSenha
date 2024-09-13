using System;
using System.Data.SqlClient;

namespace EncryptSenha
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string a = "";
            string escolha = "";
            string S = "";
             Console.WriteLine("Deseja Alimentar o banco de dados? S/N");
             escolha = Console.ReadLine();
             if(escolha == "S")
             {
               
                a = SenhaAleatoria();
                S = Cadastrar(a);
                Console.WriteLine("Senha: " + a + " / Criptografia " + S);
                

                
                 while (S != "0408AC5A8B33487AED180271493F5A7A")
                 {
                    a = SenhaAleatoria();
                    S = Cadastrar(a);
                    Console.WriteLine("Senha: "+ a + " / Criptografia " + S);
                   
                 }
                
                Console.WriteLine("--------------------!!!!!!!!!!!!!!!!!BINGO!!!!!!!!!!!!!!!-----------------------");
                
             }

            else
             {
                 Console.WriteLine("Volte sempre");
             }

             
            Console.ReadKey();
        }


        

        public static string Cadastrar(string SA)
        {


         string str_Incluir = @"Insert into SenhasCry (Senha,Crypt) values 
            (@Senha,@Crypt)";
            string StringConexao = @"data source=DATA; User Id=ADD_ID; Password=SENHA; Initial Catalog=BD_ESTUDOS";

            SqlConnection conection = new SqlConnection(StringConexao);


            SqlCommand comm = new SqlCommand(str_Incluir, conection);



            comm.Parameters.AddWithValue("@Senha", SA);
            comm.Parameters.AddWithValue("@Crypt", MD5Hash.CalculaHash(SA));
           
            



            conection.Open();

            comm.ExecuteNonQuery();

            conection.Close();
            return MD5Hash.CalculaHash(SA);

        }
        public static string SenhaAleatoria()
        {

            int Tamanho = 8; // Numero de digitos da senha

            string senha = string.Empty;

            for (int i = 0; i < Tamanho; i++)

            {

                Random random = new Random();

                int codigo = Convert.ToInt32(random.Next(48, 122).ToString());



                if ((codigo >= 48 && codigo <= 57) || (codigo >= 97 && codigo <= 122))

                {

                    string _char = ((char)codigo).ToString();

                    if (!senha.Contains(_char))

                    {

                        senha += _char;

                    }

                    else

                    {

                        i--;

                    }

                }

                else

                {

                    i--;

                }

            }





            return senha;

            
        }
        public static class MD5Hash
        {

            public static string CalculaHash(string Senha)

            {

                try

                {

                    System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(Senha);

                    byte[] hash = md5.ComputeHash(inputBytes);

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    for (int i = 0; i < hash.Length; i++)

                    {

                        sb.Append(hash[i].ToString("X2"));

                    }

                    return sb.ToString(); // Retorna senha criptografada

                }

                catch (Exception)

                {

                    return null; // Caso encontre erro retorna nulo

                }

            }

        }



    }
}

