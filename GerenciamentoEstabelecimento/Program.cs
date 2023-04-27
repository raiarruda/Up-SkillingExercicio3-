bool executar = true;


List<(Guid id, string nome, string cpf)> clientes = new List<(Guid, string, string)>();
List<(Guid idCliente, string marca, string modelo, string placa)> veiculos = new List<(Guid, string, string, string)>();
List<(Guid idCliente, DateTime entrada, DateTime saida, bool saiu)> movimentacoes = new List<(Guid, DateTime, DateTime, bool)>();
decimal valorPorHora = 1.5M;

while (executar)
{
    Console.Clear();
    Console.WriteLine("""
                    Escolha uma das opções abaixo:
                    1 - Cadastrar Cliente
                    2 - Listar Clientes
                    3 - Cadastrar Veiculo
                    4 - Listar Veiculos
                    5 - Registrar entrada
                    6 - Registrar saida
                    7 - Exibir movimentações fechadas
                    8 - Exibir movimentações abertas
                    9 - Alterar valor por hora;
                    0 - Sair
                    """);

    string? opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            CadastrarCliente();
            break;

        case "2":
            ExibirClientes();
            break;
        case "3":
            CadastrarVeiculo();
            break;
        case "4":
            ExibirVeiculos();
            break;
        case "5":
            RegistrarEntradaDeVeiculo();
            break;
        case "6":
            RegistrarSaidaVeiculo();
            break;
        case "7":
            ExibirHistoricoMovimentacoesFechadas();
            break;
        case "8":
            ExibirHistoricoMovimentacoesAbertas();
            break;
        case "9":
            AlterarValorPorHora();
            break;
        default:
            executar = false;
            break;
    }
}

void RegistrarSaidaVeiculo()
{
    Console.Clear();
    Console.WriteLine("*******************************");
    Console.WriteLine("********Registrar Saída********");
    Console.WriteLine("*******************************");



    Console.WriteLine("Digite o cpf do dono do veiculo");
    string cpf = Console.ReadLine() ?? "";
    Guid idCliente = BuscarUICliente(cpf);

    if (idCliente.Equals(Guid.Empty))
    {
        Console.WriteLine("Cliente não cadastrado! Favor cadsstrar cliente para cadastrar a saida do veiculo");
        CliqueParaContinuar();
        
        return;
    }

    int indexMovimentacao = BuscarMovimentacaoVeiculoNaoSaiu(idCliente);
    if (indexMovimentacao == -1)
    {
        Console.WriteLine("Não existe uma saida pendente para o veiculo");
        CliqueParaContinuar();
        
        return;
    }

    DateTime saida = ObterDataHora("Informe data e hora da saida");

    var movimentacao = movimentacoes[indexMovimentacao];
    movimentacao.saida = saida;
    movimentacao.saiu = true;
    movimentacoes[indexMovimentacao] = movimentacao;

    Console.WriteLine("Saída registrada com sucesso. O veiculo ficou no estacionameto por: ", CalcularDuracaoMovimentacao(
        movimentacao.entrada,
        movimentacao.saida));
    CliqueParaContinuar();
    
}

void RegistrarEntradaDeVeiculo()
{
    Console.Clear();
    Console.WriteLine("*******************************");
    Console.WriteLine("********Registrar Entrada******");
    Console.WriteLine("*******************************");
    Console.WriteLine("Digite o cpf do dono do veiculo");
    
    string cpf = Console.ReadLine() ?? "";
    Guid idCliente = BuscarUICliente(cpf);

    if (idCliente.Equals(Guid.Empty))
    {
        Console.WriteLine("Cliente não cadastrado! Favor cadsstrar cliente para cadastrar a entrada do veiculo");
        CliqueParaContinuar();
        
        return;
    }

    DateTime entrada = ObterDataHora("Informa data e hora da entrada");

    (Guid idCliente, DateTime entrada, DateTime saida, bool saiu) movimentacao = (idCliente, entrada, default, false);
    movimentacoes.Add(movimentacao);
}

void ExibirVeiculos()
{
    Console.Clear();
    Console.WriteLine("*******************************");
    Console.WriteLine("********Listar Veiculos********");
    Console.WriteLine("*******************************");
    if (!veiculos.Any())
    {
        Console.WriteLine("Não existe veiculo cadastrado");
        CliqueParaContinuar();
        
        return;
    }

    veiculos.ForEach(veiculo =>
    {
        Console.WriteLine($"""
                Cliente = {BuscarNomeCliente(veiculo.idCliente)}
                Marca = {veiculo.marca}
                Modelo  = {veiculo.modelo}
                Placa  = {veiculo.placa}
        """);

    });
    CliqueParaContinuar();
    
}

void CadastrarVeiculo()
{

    Console.Clear();
    Console.WriteLine("*******************************");
    Console.WriteLine("********Registrar Veiculo*******");
    Console.WriteLine("*******************************");
    
    Console.WriteLine("Digite o cpf do dono do veiculo");
    string cpf = Console.ReadLine() ?? "";
    Guid idCliente = BuscarUICliente(cpf);

    if (idCliente.Equals(Guid.Empty))
    {
        Console.WriteLine("Cliente não cadastrado! Favor cadsstrar cliente para cadastrar o veiculo");
        CliqueParaContinuar();
        
        return;
    }
    Console.WriteLine("Digite o modelo do Veiculo");
    string modelo = Console.ReadLine() ?? "";

    Console.WriteLine("Digite o marca do Veiculo");
    string marca = Console.ReadLine() ?? "";

    Console.WriteLine("Digite o placa do Veiculo");
    string placa = Console.ReadLine() ?? "";

    (Guid Empty, string, string, string) veiculo = (Guid.Empty, "", "", "");

    if (modelo.Length > 0 && marca.Length > 0 && placa.Length > 0)
    {
        veiculo = (idCliente, marca, modelo, placa);
        veiculos.Add(veiculo);
        Console.WriteLine("Veiculo cadastrado com sucesso !!");
    }
    else
        Console.WriteLine("Veiculo invalido");

    CliqueParaContinuar();
    

}

void ExibirClientes()
{
    Console.Clear();
    Console.WriteLine("*******************************");
    Console.WriteLine("********Exibir Clientes********");
    Console.WriteLine("*******************************");

    if (!clientes.Any())
    {

        Console.WriteLine("Não Existe Cliente cadsstrado");
        CliqueParaContinuar();
        
        return;
    }

    clientes.ForEach(cliente =>
    {
        Console.WriteLine($"""
                GUID = {cliente.id}
                Nome = {cliente.nome}
                CPF  = {cliente.cpf}
        """);
    });

    CliqueParaContinuar();
}

void CadastrarCliente()
{
    Console.Clear();
    Console.WriteLine("*******************************");
    Console.WriteLine("*******Registrar Cliente*******");
    Console.WriteLine("*******************************");

    Console.WriteLine("Digite o nome do cliente");
    string nome = Console.ReadLine() ?? "";

    Console.WriteLine("Digite o cpf do cliente");
    string cpf = Console.ReadLine() ?? "";

    (Guid id, string nome, string cpf) cliente = (Guid.Empty, "", "");

    if (nome.Length > 0 && cpf.Length > 0)
    {
        cliente = (Guid.NewGuid(), nome, cpf); 
        clientes.Add(cliente);
        Console.WriteLine("Cliente cadastrado com sucesso !!");
    }
    else
        Console.WriteLine("Cliente invalido");
    CliqueParaContinuar();
}

Guid BuscarUICliente(string cpf)
{
    (Guid id, string nome, string cpf) clienteEncontrado = clientes.Find(cliente => cliente.cpf == cpf);
    if (clienteEncontrado == default)
        return Guid.Empty;
    return clienteEncontrado.id;

}

string BuscarNomeCliente(Guid id)
{
    (Guid id, string nome, string cpf) clienteEncontrado = clientes.Find(cliente => cliente.id == id);
    if (clienteEncontrado == default)
        return "";
    return clienteEncontrado.nome;

}

void AlterarValorPorHora()
{
    Console.Clear();
    Console.WriteLine("*******************************");
    Console.WriteLine("********Alterar Valor********");
    Console.WriteLine("*******************************");

    Console.WriteLine("Qual o valor deverá ser cobrado por hora?");
    valorPorHora = Convert.ToDecimal(Console.ReadLine());

    Console.WriteLine("Valor Atualizado com sucesso. O valor cobrado por hora é R$" + valorPorHora);
    
}

DateTime ObterDataHora(string mensagem)
{
    Console.WriteLine(mensagem);

    Console.WriteLine("Digite o dia (1 a 31):");
    int dia = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Digite o mês (1 a 12):");
    int mes = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Digite o ano:");
    int ano = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Digite a hora (0 a 23):");
    int hora = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Digite o minuto (0 a 59):");
    int minuto = Convert.ToInt32(Console.ReadLine());

    return new DateTime(ano, mes, dia, hora, minuto, 0);
}


int BuscarMovimentacaoVeiculoNaoSaiu(Guid id)
{
    int indexMovimentacao = movimentacoes.FindIndex(movimentacao => movimentacao.idCliente == id && movimentacao.saiu == false);

    return indexMovimentacao != -1 ? indexMovimentacao : -1;
}
TimeSpan CalcularDuracaoMovimentacao(DateTime entrada, DateTime saida)
{
    return saida - entrada;
}
decimal CalcularValorAPagar(TimeSpan duracao)
{

    int horas = (int)duracao.TotalHours;

    if (duracao.Minutes > 0)
    {
        horas++;
    }

    return horas * valorPorHora;
}


void ExibirHistoricoMovimentacoesFechadas()
{
    Console.Clear();

    Console.WriteLine("********************************************");
    Console.WriteLine("********Exibir Movimetação com Saída********");
    Console.WriteLine("********************************************");

    var movimentacoesFechadas = movimentacoes.FindAll(m => m.saiu = true);
    if (!movimentacoesFechadas.Any())
    {
        Console.WriteLine("Não há movmentações fechadas");
        CliqueParaContinuar();
        
        return;
    }

    movimentacoesFechadas.ForEach(movimentacao =>
    {
        var veiculo = BuscarInformacoesVeiculo(movimentacao.idCliente);
        var duracao = CalcularDuracaoMovimentacao(movimentacao.entrada, movimentacao.saida);
        Console.WriteLine($"""
                Nome do cliente = {BuscarNomeCliente(movimentacao.idCliente)}
                Placa: {veiculo.placa}
                Modelo: {veiculo.modelo}
                Marca: {veiculo.marca}

                Entrada: {movimentacao.entrada.ToShortDateString()} {movimentacao.entrada.ToShortTimeString()}
                Saida: {movimentacao.saida.ToShortDateString()} {movimentacao.saida.ToShortTimeString()}

                Tempo total: {duracao}
                Valor total: {CalcularValorAPagar(duracao)}

                """);
    });
    CliqueParaContinuar();
    
}


void ExibirHistoricoMovimentacoesAbertas()
{
    Console.Clear();
    Console.WriteLine("********************************************");
    Console.WriteLine("********Exibir Movimetação sem Saída********");
    Console.WriteLine("********************************************");

    var movimentacoesAbertas = movimentacoes.FindAll(m => m.saiu = true);
    if (!movimentacoesAbertas.Any())
    {
        Console.WriteLine("Não há movimentações abertas");
        CliqueParaContinuar();
        
        return;
    }

    movimentacoesAbertas.ForEach(movimentacao =>
    {
        var veiculo = BuscarInformacoesVeiculo(movimentacao.idCliente);
        Console.WriteLine($"""
                Nome do cliente = {BuscarNomeCliente(movimentacao.idCliente)}
                Placa: {veiculo.placa}
                Modelo: {veiculo.modelo}
                Marca: {veiculo.marca}

                Entrada: {movimentacao.entrada.ToShortDateString()} {movimentacao.entrada.ToShortTimeString()}
              
                """);
    });

    CliqueParaContinuar();
}


(Guid idCliente, string marca, string modelo, string placa) BuscarInformacoesVeiculo(Guid idCliente)
{
    var veiculoEncontrado = veiculos.Find(veiculo => veiculo.idCliente == idCliente);
    return veiculoEncontrado;
}

void CliqueParaContinuar()
{
    Console.WriteLine("Pressione qualquer tecla para continuar.");
    Console.ReadKey();

}