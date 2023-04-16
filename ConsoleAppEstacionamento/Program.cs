using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Dynamic;
using System.Reflection.Emit;

List<dynamic> clientes = new List<dynamic>();
List<dynamic> veiculos = new List<dynamic>();
List<dynamic> registros = new List<dynamic>();
double valorMinuto = 0;

Console.WriteLine("Sistema de Estacionamento");

while (true)
{
    Console.Clear();
    //Menu
    Console.WriteLine("Digite uma das opções abaixo:");
    Console.WriteLine("1 - Cadastrar Cliente");
    Console.WriteLine("2 - Cadastrar Veículo");
    Console.WriteLine("3 - Cadastro de Valor");
    Console.WriteLine("4 - Registro de Entrada / Saída");
    Console.WriteLine("5 - Pagamento");
    Console.WriteLine("6 - Lista de Clientes");
    Console.WriteLine("7 - Lista de Veículos");
    Console.WriteLine("8 - Relatório de entradas e saídas");
    Console.WriteLine("9 - Registro manual de Entrada / Saída");
    Console.WriteLine("10 - Sair");

    var opcao = campo("\r\nOpção:");
    var sair = false;

    switch (opcao)
    {
        case "":
            Console.WriteLine("Pressione Enter para continuar ...");
            Console.ReadKey();
            break;
        //Cadastro de cliente
        case "1":
            Console.Clear();
            var clienteNome = campo("Informe o nome do cliente");
            var clienteCpf = campo("Informe o CPF do cliente");
            Guid clienteId = Guid.NewGuid();

            dynamic cliente = new
            {
                Id = clienteId,
                Nome = clienteNome,
                Cpf = clienteCpf
            };

            clientes.Add(cliente);

            Console.WriteLine("Cliente cadastrado com sucesso ...");
            Thread.Sleep(1000);
            break;
        //Cadastro de veículo
        case "2":
            Console.Clear();
            var veiculoCpf = campo("Informe o CPF do cliente");
            dynamic cliente2 = dadosCliente(veiculoCpf ?? "");
            if (cliente2 is null) continue;

            var veiculoMarca = campo("Informe a marca do veículo");
            var veiculoModelo = campo("Informe o modelo do veículo");
            var veiculoPlaca = campo("Informe a placa do veículo");

            //Guid idCliente = Guid.NewGuid();

            dynamic veiculo = new
            {
                IdCliente = cliente2.Id,
                Marca = veiculoMarca,
                Modelo = veiculoModelo,
                Placa = veiculoPlaca
            };

            veiculos.Add(veiculo);

            Console.WriteLine("Veículo cadastrado com sucesso ...");
            Thread.Sleep(1000);
            break;
        //Cadastro de preço
        case "3":
            Console.WriteLine("=== Cadastro de preço ===");
            string valorRL = campo("Informe o valor por minuto") ?? "0";
            valorRL = valorRL.Replace(".", ",");

            valorMinuto = double.Parse(valorRL);

            Console.WriteLine("Valor cadastrado..." + valorMinuto.ToString());
            Thread.Sleep(1000);
            break;
        //Registro de entrada e saída de veículos
        case "4":
            registroEntradaSaida(false);
            break;
        //Gera o valor à pagar
        case "5":
            Console.WriteLine("=== Pagamento =====");
            if (valorMinuto <= 0) 
            {
                Console.WriteLine("Valor não cadastrado. Pressione uma tecla para voltar para o menu e cadastre na opção 3!");
                Console.ReadKey();
                continue;
            }

            var pagamentoCPF = campo("Digite o CPF do cliente:");
            dynamic cliente5 = dadosCliente(pagamentoCPF ?? "");
            if (cliente5 is null) continue;

            marcacao(cliente5.Id, "");
            break;
        //Lista de Clientes
        case "6":
            Console.WriteLine("=== Lista de clientes =====");
            foreach (var cli in clientes)
            {
                Console.WriteLine($"Id: {cli.Id}");
                Console.WriteLine($"Nome: {cli.Nome}");
                Console.WriteLine($"CPF: {cli.Cpf}");
                Console.WriteLine("------------------------------");
            }

            Console.WriteLine("Pressione Enter para continuar ...");
            Console.ReadKey();
            break;
        //Lista de Veículos
        case "7":
            Console.WriteLine("=== Lista de veículos =====");
            foreach (var vei in veiculos)
            {
                Console.WriteLine($"IdCliente: {vei.IdCliente}");
                Console.WriteLine($"Marca: {vei.Marca}");
                Console.WriteLine($"Modelo: {vei.Modelo}");
                Console.WriteLine($"Placa: {vei.Placa}");
                Console.WriteLine("------------------------------");
            }

            Console.WriteLine("Pressione Enter para continuar ...");
            Console.ReadKey();
            break;
        //Relatório de entradas e saídas de veículos
        case "8":
            Console.WriteLine("=== Relatório de entradas e saídas =====");
            foreach (var reg in registros)
            {
                Console.WriteLine($"IdCliente: {reg.IdCliente}");
                Console.WriteLine($"Placa: {reg.Placa}");
                Console.WriteLine($"Tipo: {reg.Tipo}");
                Console.WriteLine($"Marcação: {reg.Marcacao}");
                Console.WriteLine("------------------------------");
            }

            Console.WriteLine("Pressione Enter para continuar ...");
            Console.ReadKey();
            break;
        case "9":
            registroEntradaSaida(true);
            break;
        default:
            sair = true;
            break;
    }

    if (sair) break;
}

dynamic dadosCliente(string _CPF) 
{
    dynamic clienteObj = clientes.FirstOrDefault(o => o.Cpf == _CPF);
    var idCliente = "";
    if (clienteObj is not null)
    {
        idCliente = (clienteObj.Id).ToString();
    }
    if (clienteObj is null || string.IsNullOrEmpty(idCliente))
    {
        Console.WriteLine("Cliente não encontrado. Pressione uma tecla para voltar para o menu...");
        Console.ReadKey();
        return null;
    }
    return clienteObj;
}

dynamic dadosVeiculo(Guid _idCliente)
{
    dynamic veiculoObj = veiculos.FirstOrDefault(o => o.IdCliente == _idCliente);
    var idCliente = "";
    if (veiculoObj is not null)
    {
        idCliente = (veiculoObj.IdCliente).ToString();
    }
    if (veiculoObj is null || string.IsNullOrEmpty(idCliente))
    {
        Console.WriteLine("Veículo não encontrado. Pressione uma tecla para voltar para o menu...");
        Console.ReadKey();
        return null;
    }
    return veiculoObj;
}

void marcacao(Guid _idCliente, string _Placa)
{
    dynamic filter = new ExpandoObject();
    filter.IdCliente = _idCliente;
    //filter.Placa = _Placa;

    IEnumerable<dynamic> result = registros.Where(item =>
    {
        foreach (var prop in filter)
        {
            if (item.GetType().GetProperty(prop.Key).GetValue(item, null) != prop.Value)
            {
                return false;
            }
        }
        return true;
    });

    DateTime entrada = DateTime.Now;
    DateTime saida = DateTime.Now;

    foreach (var item in result)
    {
        //Console.WriteLine("entradas"+ item.Marcacao);
        if(item.Tipo == "ENTRADA")
        {
            entrada = item.Marcacao;
        }
        else if (item.Tipo == "SAÍDA")
        {
            saida = item.Marcacao;
        }
    }

    if (saida < entrada)
    {
        Console.WriteLine("Saída não registrada!");
    }
    else
    {
        TimeSpan diff = saida - entrada;

        int minutos = (int)diff.TotalMinutes;

        //Se ficou menos de um minuto para o minuto????
        if (minutos == 0) minutos = 1;

        Console.WriteLine("Entrada: " + entrada);
        Console.WriteLine("Saída: " + saida);
        Console.WriteLine("Valor: R$ " + (valorMinuto * minutos));
    }
    Console.WriteLine("Pressiona uma tecla para voltar para o menu...");
    Console.ReadKey();
}

string campo(string _label)
{
    string userInput = "";
    Console.WriteLine(_label);
    while (string.IsNullOrEmpty(userInput))
    {
        userInput = Console.ReadLine() ?? "";
        if (string.IsNullOrEmpty(userInput)) Console.CursorTop--;
    }
    return userInput;
}

void registroEntradaSaida(bool manual)
{
    Console.WriteLine("=== Registro " + (manual ? "manual " : "") + "de entrada / saída ===");
    Console.Clear();
    Console.WriteLine("Digite uma das opções abaixo:");
    Console.WriteLine("1 - Registrar entrada");
    Console.WriteLine("2 - Registrar saída");
    Console.WriteLine("3 - Sair");

    var opcao4 = campo("\r\nOpção:");
    var sair4 = false;

    var tipo = "";
    switch (opcao4)
    {
        case "1":
            tipo = "E";
            break;
        case "2":
            tipo = "S";
            break;
        default:
            sair4 = true;
            break;
    }
    if (sair4) return;

    var registroCPF = campo("Digite o CPF do cliente:");
    dynamic cliente4 = dadosCliente(registroCPF ?? "");
    if (cliente4 is null) return;

    dynamic veiculo4 = dadosVeiculo(cliente4.Id);
    if (veiculo4 is null) return;

    string? _tipo = (tipo == "E" ? "ENTRADA" : (tipo == "S" ? "SAÍDA" : "ERRO"));

    DateTime registroMarcacao = DateTime.Now;
    if (manual)
    {
        //var dataMarcacao = campo("Data do registro");
        var dataMarcacao = DateTime.Now.Date.ToString("dd/MM/yyyy");
        var horaMarcacao = campo("Hora do registro");
        var dataHoraMarcacao = dataMarcacao + " " + horaMarcacao;
        if (dataHoraMarcacao.Trim() != "") {
            registroMarcacao = DateTime.Parse(dataHoraMarcacao);
        }
    }

    dynamic registro = new
    {
        IdCliente = cliente4.Id,
        Placa = veiculo4.Placa,
        Tipo = _tipo,
        Marcacao = registroMarcacao
    };

    registros.Add(registro);

    if (_tipo == "ERRO")
    {
        Console.WriteLine("Erro no registro!");
    }
    else
    {
        Console.WriteLine(_tipo + " registrada com sucesso!");
    }
    Thread.Sleep(1000);
}