using System;

namespace STARC.Tests.Utils
{
    public class CpfCnpjGenerator
    {
        public static string GenerateCpf()
        {
            long erro, soma1, soma2, parte1, parte2, parte3, parte5, parte6, parte7, dig1, dig2;
            long[] numero = new long[13];

            Random rand = new Random();

            for (var i = 1; i <= 9; i++)
            {
                erro = 1;
                do
                {
                    if (erro > 1)
                    {
                        erro = 1;
                    }
                    numero[i] = (rand.Next()) % 9;
                    erro++;
                } while (numero[i] > 9 || numero[i] < 0);
            }

            //*==========================================*
            //|       Primeiro digito veridicador        |
            //*==========================================*

            soma1 = ((numero[1] * 10) +
                    (numero[2] * 9) +
                    (numero[3] * 8) +
                    (numero[4] * 7) +
                    (numero[5] * 6) +
                    (numero[6] * 5) +
                    (numero[7] * 4) +
                    (numero[8] * 3) +
                    (numero[9] * 2));
            parte1 = (soma1 / 11);
            parte2 = (parte1 * 11);
            parte3 = (soma1 - parte2);
            dig1 = (11 - parte3);
            if (dig1 > 9) dig1 = 0;

            //*==========================================*
            //|        Segundo digito veridicador        |
            //*==========================================*

            soma2 = ((numero[1] * 11) +
                    (numero[2] * 10) +
                    (numero[3] * 9) +
                    (numero[4] * 8) +
                    (numero[5] * 7) +
                    (numero[6] * 6) +
                    (numero[7] * 5) +
                    (numero[8] * 4) +
                    (numero[9] * 3) +
                    (dig1 * 2));
            parte5 = (soma2 / 11);
            parte6 = (parte5 * 11);
            parte7 = (soma2 - parte6);
            dig2 = (11 - parte7);
            if (dig2 > 9) dig2 = 0;

            string cpf = string.Empty;
            for (var i = 1; i <= 9; i++)
            {
                //numeros do CPF
                cpf += Convert.ToString(numero[i]);
            }

            cpf += dig1 + "" + dig2;

            return cpf;
        }

        public static string GenerateCnpj()
        {
            long soma1, soma2, parte1, parte2, parte3, parte5, parte6, parte7, dig1, dig2;
            long[] numero = new long[13];

            Random rand = new Random();

            for (var i = 1; i <= 8; i++)
            {
                numero[i] = (rand.Next()) % 9;
            }

            numero[9] = 0;
            numero[10] = 0;
            numero[11] = 0;
            numero[12] = (rand.Next()) % 9;

            //*==========================================*
            //|       Primeiro digito veridicador        |
            //*==========================================*
            soma1 = ((numero[1] * 5) +
                    (numero[2] * 4) +
                    (numero[3] * 3) +
                    (numero[4] * 2) +
                    (numero[5] * 9) +
                    (numero[6] * 8) +
                    (numero[7] * 7) +
                    (numero[8] * 6) +
                    (numero[9] * 5) +
                    (numero[10] * 4) +
                    (numero[11] * 3) +
                    (numero[12] * 2));
            parte1 = (soma1 / 11);
            parte2 = (parte1 * 11);
            parte3 = (soma1 - parte2);
            dig1 = (11 - parte3);
            if (dig1 > 9) dig1 = 0;

            //*==========================================*
            //|        Segundo digito veridicador        |
            //*==========================================*
            soma2 = ((numero[1] * 6) +
                    (numero[2] * 5) +
                    (numero[3] * 4) +
                    (numero[4] * 3) +
                    (numero[5] * 2) +
                    (numero[6] * 9) +
                    (numero[7] * 8) +
                    (numero[8] * 7) +
                    (numero[9] * 6) +
                    (numero[10] * 5) +
                    (numero[11] * 4) +
                    (numero[12] * 3) +
                    (dig1 * 2));

            parte5 = (soma2 / 11);
            parte6 = (parte5 * 11);
            parte7 = (soma2 - parte6);
            dig2 = (11 - parte7);

            if (dig2 > 9) dig2 = 0;
            
            var cnpj = string.Empty;

            for (var i = 1; i <= 12; i++)
            {
                //numeros do CNPJ
                cnpj += Convert.ToString(numero[i]);
            }
            // dois últimos digitos
            cnpj += dig1 + "" + dig2;

            return cnpj;
        }
    }
}
