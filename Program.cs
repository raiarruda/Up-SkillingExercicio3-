/* sistema de gestão de entradas e saídas de um veiculo (estacionamento) */
using System;
using System.Numerics;
using System.Reflection;
using System.Text.RegularExpressions;

internal class Program
{
    public class Cliente
    {
        Guid idCliente = Guid.NewGuid();
        String nome = "";
        String cpf = "";
        
        public Guid getId()
        {
            return idCliente;
        }
        public void setNome(string _nome)
        {
            nome = _nome;
        }
        public string getNome()
        {
            return nome;
        }
        public void setCpf(string _cpf)
        {
            cpf = _cpf;
        }
        public string getCpf()
        {
            return cpf;
        }
        public string imprimeDadosCliente()
        {
            string retorno = $"""
                Nome do cliente: {nome}
                CPF: {cpf}
                ID: {idCliente}
                _______________________________
                """;
            return retorno;
        }
    }

    public class Veiculo
    {
        String marca = "";
        String modelo = "";
        String placa = "";
        String idCliente = "";
        public void setMarca(string _marca)
        {
            marca = _marca;
        }
        public string getMarca()
        {
            return marca;
        }
        public void setModelo(string _modelo)
        {
            modelo = _modelo;
        }
        public string getModelo()
        {
            return modelo;
        }
        public void setPlaca(string _placa)
        {
            placa = _placa;
        }
        public string getPlaca()
        {
            return placa;
        }
        public void setIdCliente(string _idCliente)
        {
            idCliente = _idCliente;
        }
        public string imprimeDadosVeiculo()
        {
            string retorno = $"""
                Marca: {marca}
                Modelo: {modelo}
                Placa: {placa}
                ID do Cliente: {idCliente}
                _______________________________
                """;
            return retorno;
        }
    }

    public class Registro
    {
        int idRegistro = 0;
        string idCliente = "";
        string entrada = "";
        string saida = "";

        public void setIdRegistro()
        {
            idRegistro = idRegistro + 1;
        }
        public void setIdCliente(string _idCliente)
        {
            idCliente = _idCliente;
        }
        public string getIdCliente()
        {
            return idCliente;
        }
        public void setEntrada(string _entrada)
        {
            entrada = _entrada;
        }
        public string getEntrada()
        {
            return entrada;
        }
        public void setSaida(string _saida)
        {
            saida = _saida;
        }
        public string getSaida()
        {
            return saida;
        }
        public string imprimeDadosEntradaSaida()
        {
            string retorno = $"""
                ID do Cliente: {idCliente}
                Entrada: {Convert.ToDateTime(entrada)}
                Saida: {Convert.ToDateTime(saida)}
                _______________________________
                """;
            return retorno;
        }
        public string imprimeCupom(DateTime _entrada, DateTime _saida)
        {
            double valorPorMinuto = 0.15;
            double valorCobrado = 0.0;

            TimeSpan diferenca = _saida - _entrada;
            int minutos = Convert.ToInt16(diferenca.TotalMinutes);

            valorCobrado = minutos * valorPorMinuto;

            return $"""
                Valor por minuto: {valorPorMinuto}
                Minutos contabilizados: {minutos}
                Valor a pagar: R$ {valorCobrado}
                ____________________________________
                """;
        }
        
    }

    private static void Main(string[] args)
    {
        List<Cliente> clientes = new List<Cliente>();
        List<Veiculo> veiculos = new List<Veiculo>();
        List<Registro> registros = new List<Registro>();

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"""
                    Escolha uma das opções abaixo:
                    1 - Cadastrar Cliente
                    2 - Listar Clientes
                    3 - Cadastrar Veiculo
                    4 - Listar Veiculos
                    5 - Registrar entrada
                    6 - Registrar saida
                    7 - Exibir movimentacao
                    0 - Sair
                    """);

            var opcao = Console.ReadLine();
            var sair = false;

            switch (opcao)
            {
                case "1":
                    Console.Clear();

                    Cliente c = new Cliente();
                    
                    Console.WriteLine("Digite o nome do cliente");
                    c.setNome(Console.ReadLine());

                    Console.WriteLine("Digite o cpf do cliente");
                    c.setCpf(Console.ReadLine());

                    clientes.Add(c);

                    Console.WriteLine("Cliente cadastrado com sucesso ...");
                    Thread.Sleep(1000);
                    break;

                case "2":
                    Console.Clear();

                    if (clientes.Count() > 0)
                    {
                        for (int i = 0; i < clientes.Count(); i++)
                        {
                            Console.WriteLine(clientes[i].imprimeDadosCliente());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Não existem clientes cadastrados.");
                    }

                    Console.WriteLine("Pressione Enter para continuar ...");
                    Console.ReadKey();

                    break;

                case "3":
                    Console.Clear();

                    Veiculo v = new Veiculo();

                    Console.WriteLine("Digite a marca do veiculo");
                    v.setMarca(Console.ReadLine());

                    Console.WriteLine("Digite o modelo do veiculo");
                    v.setModelo(Console.ReadLine());

                    Console.WriteLine("Digite a placa do veiculo");
                    v.setPlaca(Console.ReadLine());

                    veiculos.Add(v);

                    Console.WriteLine("Digite o cpf do cliente: ");
                    v.setIdCliente(buscaIdClientePorCpf(Console.ReadLine()));

                    Console.WriteLine("Veiculo cadastrado com sucesso ...");
                    Thread.Sleep(1000);

                    break;

                case "4":
                    Console.Clear();

                    if (veiculos.Count() > 0)
                    {
                        for (int i = 0; i < veiculos.Count(); i++)
                        {
                            Console.WriteLine(veiculos[i].imprimeDadosVeiculo());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Não existem veiculos cadastrados.");
                    }

                    Console.WriteLine("Pressione Enter para continuar ...");
                    Console.ReadKey();

                    break;

                case "5":
                    Console.Clear();

                    Registro r = new Registro();

                    r.setIdRegistro();

                    Console.WriteLine("Digite o cpf do cliente: ");
                    r.setIdCliente(buscaIdClientePorCpf(Console.ReadLine()));

                    Console.WriteLine("Digite a hora da entrada (HH:MM): ");
                    r.setEntrada(Console.ReadLine());

                    registros.Add(r);

                    Console.WriteLine("Entrada registrada.");
                    Thread.Sleep(1000);

                    break;

                case "6":
                    Console.Clear();

                    Console.WriteLine("Digite o cpf do cliente: ");
                    string idCliente = buscaIdClientePorCpf(Console.ReadLine());

                    for (int i = 0; i < registros.Count; i++)
                    {
                        if (registros[i].getIdCliente() == idCliente && registros[i].getSaida() == "")
                        {
                            Console.WriteLine("Digite a hora da saida (HH:MM): ");
                            registros[i].setSaida(Console.ReadLine());
                            
                            Console.WriteLine("Saida registrada.");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            Console.WriteLine("Cliente não possui registro de entrada.");
                        }
                    }

                    break;

                case "7":
                    for (int i = 0; i < registros.Count; i++)
                    {
                        Console.WriteLine(registros[i].imprimeDadosEntradaSaida());

                        DateTime entrada = Convert.ToDateTime(registros[i].getEntrada());
                        DateTime saida = Convert.ToDateTime(registros[i].getSaida());
                        
                        Console.WriteLine(registros[i].imprimeCupom(entrada, saida));
                    }

                    Console.WriteLine("Pressione Enter para continuar ...");
                    Console.ReadKey();

                    break;

                default:
                    sair = true;
                    break;
            }

            if (sair) break;
        }

        string buscaIdClientePorCpf(string _cpf)
        {
            for (int i = 0; i < clientes.Count; i++)
            {
                if (clientes[i].getCpf() == _cpf)
                {
                    return Convert.ToString(clientes[i].getId());
                }
            }
            return "";
        }

        string buscaNomeclientePorId(string _id)
        {
            for (int i = 0; i < clientes.Count; i++)
            {
                if (Convert.ToString(clientes[i].getId) == _id)
                {
                    return clientes[i].getNome();
                }
            }
            return "Cliente não existe!";
        }
        
    }
}
